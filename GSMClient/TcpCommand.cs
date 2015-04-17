using Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSMClient
{
    public class TcpCommand : BaseGSMCommand
    {
        private Encoding encoding;
        public TcpCommand(IGSMConnection connection, IKeySym publicKey)
            : base(connection)
        {
            encoding = Encoding.GetEncoding("ibm850");
            base.PublicKey = publicKey;
        }

        public TcpCommand(string hostName, int port, IKeySym publicKey)
            : base(new TcpConnection(hostName, port))
        {
            encoding = Encoding.GetEncoding("ibm850");
            base.PublicKey = publicKey;
        }

        public override string Write()
        {
            byte[] dataToSend = encoding.GetBytes(base.Encrypt());
            int result = ((TcpConnection)base.Connection).TcpClient.Client.Send(dataToSend);
            if (result > 0)
                return "1";
            else
                return "0";
        }

        public override int Write(string data)
        {
            byte[] dataToSend = encoding.GetBytes(base.Encrypt(data)); 
            return ((TcpConnection)base.Connection).TcpClient.Client.Send(dataToSend);
        }

        public override int Write(byte[] data, int startIndex, int length)
        {
            string dataToEncrypt = encoding.GetString(data, 0 ,length);
            byte[] dataToSend = encoding.GetBytes(base.Encrypt(dataToEncrypt));
            return ((TcpConnection)base.Connection).TcpClient.Client.Send(dataToSend, 0, dataToSend.Length, System.Net.Sockets.SocketFlags.Broadcast);
        }
    }
}
