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

namespace Sockets
{
    public abstract class BaseTcpMonitor
    {

        #region Fields

        private IPEndPoint localEndPoint = null; 

        #endregion

        #region Properties

        /// <summary>
        /// Gets all client connections.
        /// </summary>
        public Dictionary<int, Socket> Clients { get; private set; }

        /// <summary>
        /// Gets the underlying EndPoint of the current TcpListener.
        /// </summary>  
        public IPEndPoint LocalEndPoint
        {
            [System.Diagnostics.DebuggerStepThrough]
            get
            {
                return localEndPoint;
            }
        }

        /// <summary>
        /// Gets the underlying Encoding.
        /// </summary>
        public Encoding Encoding { get; set; }

        /// <summary>
        /// Gets a value that indicates whether TcpListener is actively listening for client connections.
        /// </summary>
        public bool Active { get; private set; }
         
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the TcpListener class that listens on the specified port.
        /// </summary> 
        public BaseTcpMonitor(int port = 13000)
        {
            Clients = new Dictionary<int, Socket>();
            IPAddress[] ipAddressL = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
            foreach (IPAddress address in ipAddressL)
            {
                if (address.AddressFamily == AddressFamily.InterNetwork)
                {
                    localEndPoint = new IPEndPoint(address, port);
                    break;
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the TcpListener class that listens for incoming connection attempts on the specified local IP address and port number.
        /// </summary> 
        public BaseTcpMonitor(string ipAddress, int port = 13000)
        {
            Clients = new Dictionary<int, Socket>();
            if (string.IsNullOrEmpty(ipAddress))
            {
                IPAddress[] ipAddressL = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
                foreach (IPAddress address in ipAddressL)
                {
                    if (address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        localEndPoint = new IPEndPoint(address, port);
                        break;
                    }
                }
            }
            else
            {
                localEndPoint = new IPEndPoint(IPAddress.Parse(ipAddress), port);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Begins an asynchronous operation to accept an incoming connection attempt.
        /// </summary>
        public abstract void BeginAcceptClient();

        /// <summary>
        /// Asynchronously accepts an incoming connection attempt and creates a new Socket to handle remote host communication.
        /// </summary>
        public abstract void EndAcceptClient();

        /// <summary>
        /// Starts listening for incoming connection requests.
        /// </summary>
        public abstract void Start();

        /// <summary>
        /// Closes the listener.
        /// </summary>
        public abstract void Stop();

        #endregion
          
    }
}
