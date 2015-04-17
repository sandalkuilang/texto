/*
    Socket
 
    Copyright (C) 2013 Yudha - yudha_hyp@yahoo.com

    This library is free software; you can redistribute it and/or
    modify it, in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Sockets
{
    public class SocketEventArgs : EventArgs
    {
        private Socket socket;
        private DateTime time; 

        public IPAddress Address
        {
            get
            {
                return ( (IPEndPoint)this.socket.RemoteEndPoint ).Address;
            }
        }

        public int Port
        {
            get
            {
                return ((IPEndPoint)this.socket.RemoteEndPoint).Port;
            }
        }

        public DateTime Time
        {
            get
            {
                return time;
            }
        }

        public Socket Client
        {
            get
            {
                return socket;
            }
        }
         
        [System.Diagnostics.DebuggerStepThrough]
        public SocketEventArgs(Socket Client )
        {
            this.socket = Client;
            this.time = DateTime.Now; 
        }
    }
}
