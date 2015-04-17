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
using System.Net.Sockets;
using System.Text;

namespace Sockets
{
    public class PacketEventArgs
    {
        private byte[] data;
        private Socket client;
        private Encoding encoding;

        public byte[] Data
        {
            get
            {
                return data;
            }
            set
            {
                data = value;
            }
        }

        public Socket Client
        {
            get
            {
                return client;
            }
        }

        public Encoding Encoding
        {
            get
            {
                return encoding;
            }
        }

        [System.Diagnostics.DebuggerStepThrough]
        public PacketEventArgs(Socket client, byte[] data, Encoding encoding)
        {
            this.client = client;
            this.data = data;
            this.encoding = encoding;
        }
    }
}
