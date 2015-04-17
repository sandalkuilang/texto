/*
    Sample Generic SMS Command
 
    Copyright (C) 2013 Yudha - yudha_hyp@yahoo.com

    This library is free software; you can redistribute it and/or
    modify it, in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  
*/
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
using GSMClient;
using GSMServerModel; 

namespace Client
{
    class Program
    {
        private static Thread threadRead;

        //private static GSMClient.IClient client;
        private static GSMClient.TcpCommand tcpCommand;

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
            {
                tcpCommand = new GSMClient.TcpCommand(ip, port, new DefaultConfigurationKey());
                //client = new GSMClient.Client(ip, port, new DefaultConfigurationKey());
            }
            else
            {
                //client = new GSMClient.Client(hostName, port, new DefaultConfigurationKey());
                tcpCommand = new GSMClient.TcpCommand(hostName, port, new DefaultConfigurationKey());
            }

            //client.Signature = ConfigurationManager.AppSettings["SMSGWSignature"];
            //client.Open();
            tcpCommand.Connection.Open();
            Console.WriteLine("Connection established {0}\n", ((TcpConnection)tcpCommand.Connection).TcpClient.Client.RemoteEndPoint);
            Console.WriteLine("Format Command:");
            Console.WriteLine("[InterfaceName].[MethodName](+param: [YourParameterName]=[YourValue])\n");
            Console.WriteLine("if your parameter more than one\n");
            Console.WriteLine("[InterfaceName].[MethodName](+param: [YourParameterName1]=[YourValue1] +param: [YourParameterName2]=[YourValue2])\n");
            Console.WriteLine("Example:");
            Console.WriteLine("ISMS.Send(+param: Number=08111 +param: Message=Hello)");
            Console.WriteLine("ICall.SendUSSD(+param: Code=1 +param: Number=*111#)");
            Console.WriteLine();
            //Send(client.GetTcpClient());
            Send(((TcpConnection)tcpCommand.Connection).TcpClient);
            Console.ReadKey();
        }
        private static void Send(TcpClient tcpClient)
        {
            ConsoleKeyInfo key = new ConsoleKeyInfo();
            try
            {
                string data;
                Header header;
                Request request; 
                while (key.Key != ConsoleKey.Escape)
                {
                    if (tcpClient.Connected && threadRead == null)
                    {
                        threadRead = new Thread(new ParameterizedThreadStart(Read));
                        threadRead.Start(tcpClient);
                    }
                    data = Console.ReadLine(); 
                    if (tcpClient.Connected)
                    {
                        header = new Header("TMMIN", "SMS-GW", "generic-command");
                        request = new Request(null, header, null);
                        
                        request.QueueWorkItem = new QueueWorkItem();
                        request.QueueWorkItem.Command = data;
                        request.QueueWorkItem.Enabled = true;
                        try
                        {
                            tcpCommand.Request = request;
                            tcpCommand.Write();
                        }
                        finally { }
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
            Crypter c = new Cryptography.Crypter(new DefaultConfigurationKey());
            StringBuilder decrypt = new StringBuilder();
            byte[] read;
            int readInt;
              
            while (true)
            {
                if (!client.Connected)
                    break;
                
                read = new byte[client.ReceiveBufferSize];  
                try
                {
                    readInt = stream.Read(read, 0, read.Length);
                    decrypt.Append(c.Decrypt(Encoding.GetEncoding("ibm850").GetString(read).Replace("\0", "")));
                     
                    Console.WriteLine("From server : {0}", decrypt.Replace("\0", ""));
                    Console.ForegroundColor = ConsoleColor.Gray;
                    decrypt.Clear();
                }
                catch 
                { 
                }
            }
        }
    }
}
