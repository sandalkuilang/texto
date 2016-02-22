using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSMClient;
using System.Net.Sockets;
using Crypto;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using GSMServerModel;
using System.Configuration;
using SMSGatewayWpf.Core.Gateway; 

namespace SMSGatewayWpf.Core.Gateway
{

    public class TcpService : BaseGatewayService
    {
        public override BaseGatewayConnection Connection { get; set; }
         
        public  TcpService(string ipAddress, int port)
        { 
            if (string.IsNullOrEmpty(ipAddress))
            {
                throw new ArgumentNullException("IPAddress");
            }
            if (port == 0)
            {
                throw new ArgumentNullException("Port");
            }

            gsmCommand = new TcpCommand(ipAddress, port, new ApplicationSettingKeySym());
            Connection = new TcpConnection(gsmCommand); 
             
        } 
      
        /// <summary>
        /// Send a command to gateway and wait the result.
        /// </summary>
        /// <param name="command">see GSMClient.Command.CommandCollection</param>
        /// <returns>the result is in JSON format</returns>   
        public override string Execute(string command)
        { 
            if ((Connection.Connected) && (!string.IsNullOrEmpty(command)))
            {
                Request.QueueWorkItem.Command = command;
                Request.QueueWorkItem.Enabled = true;

                gsmCommand.Request = Request;
                gsmCommand.Write();

                StringBuilder decrypt = new StringBuilder();
                byte[] read; 
                string removeUnusedCharacter;
                string decryptedResult;
                int readInt;

                TcpClient tcpClient = ((GSMClient.TcpConnection)gsmCommand.Connection).TcpClient;
                NetworkStream stream = tcpClient.GetStream();
                while (true)
                {
                    if (!tcpClient.Client.Connected)
                        break;
                     
                    read = new byte[tcpClient.ReceiveBufferSize];
                    try
                    {
                        readInt = tcpClient.Client.Receive(read); 

                        removeUnusedCharacter = Encoding.GetEncoding("ibm850").GetString(read).Replace("\0", "");
                        decryptedResult = crypter.Decrypt(removeUnusedCharacter);
                        decrypt.Append(decryptedResult);
                        return decrypt.ToString();
                    }
                    catch (SocketException)
                    {
                         
                    }
                    catch (Exception) { }
                }
            }
            return string.Empty;
        } 

        public override string Execute(Request request)
        {
            throw new NotImplementedException();
        }
    }
}
