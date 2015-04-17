using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks; 
using System.Xml.Serialization;
using System.Xml;
using Cryptography;
using System.Configuration; 

namespace Client
{
    class Program
    {
        private static Thread threadRead; 

        private static GSMClient.IClient client;

        static void Main(string[] args)
        { 
            Console.Write("Input server name : " );
            string hostName = Console.ReadLine();
            string ip = "";

            string portStr = ConfigurationManager.AppSettings["ServerPort"];
            int port = 0;
            if (!string.IsNullOrEmpty(portStr))
                port = Convert.ToInt32(portStr);

            foreach (IPAddress ipaddress in Dns.GetHostEntry(hostName).AddressList)
            {
                if (ipaddress.AddressFamily == AddressFamily.InterNetwork)
                    ip = ipaddress.ToString();
                break;
            }
            if (ip != "")
                client = new GSMClient.Client(ip, port); 
            else
                client = new GSMClient.Client(hostName, port);
            client.Open();
            Console.WriteLine("Connection established {0}", client.GetClient().Client.RemoteEndPoint);
            Send(client.GetClient());
            Console.ReadKey();
        }
        private static void Send(TcpClient tcpClient)
        {
            ConsoleKeyInfo key = new ConsoleKeyInfo();
            try
            {
                while (key.Key != ConsoleKey.Escape)
                {
                    if (tcpClient.Connected && threadRead == null)
                    {
                        threadRead = new Thread(new ParameterizedThreadStart(Read));
                        threadRead.Start(tcpClient);
                    }
                    string data = Console.ReadLine(); 
                    if (tcpClient.Connected)
                    {
                        client.Send(data);
                    }
                    else
                        break; 
                }
                tcpClient.Close();
            }
            catch  
            {
            }
        }
        private static void Read(object target)
        {
            TcpClient client = (TcpClient)target;
            NetworkStream stream = client.GetStream();
            while (true)
            {
                if (!client.Connected)
                    break;
                byte[] read = new byte[client.ReceiveBufferSize]; 
                int readInt;
                try
                {
                    readInt = stream.Read(read, 0, read.Length);
                    Crypter c = new Cryptography.Crypter(new ConfigKeySym());
                    string decrypt = c.Decrypt(ASCIIEncoding.ASCII.GetString(read).Replace("\0", ""));
                    Console.WriteLine("From server : {0}", decrypt.Replace("\0", ""));
                }
                catch 
                {
                    return;
                }
            }
        }
    }
}
