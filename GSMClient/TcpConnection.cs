using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace GSMClient
{
    public class TcpConnection : IGSMConnection
    {
        public string HostName { get; set; }
        public int Port { get; set; }

        private bool disposed = false;

        public TcpClient TcpClient { get; set; }

        public TcpConnection()
        {
            TcpClient = new TcpClient();
        }

        public TcpConnection(string hostName, int port)
        {
            this.HostName = hostName;
            this.Port = port;
            TcpClient = new TcpClient();
        }

        public TcpConnection(IPEndPoint localEP)
        {
            this.HostName = localEP.Address.ToString();
            this.Port = localEP.Port;
            TcpClient = new TcpClient();
        }

        public void Open()
        {
            if (disposed)
                TcpClient = new TcpClient();
            
            TcpClient.Connect(HostName, Port);              
        }

        public void Close()
        {
            TcpClient.Close();
            Dispose(true);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }
            if (disposing)
            {
                TcpClient.Close();
                TcpClient = null;
            }
            disposed = true;
        }

        public bool IsOpen
        {
            get { return TcpClient.Connected; }
        }
    }
}
