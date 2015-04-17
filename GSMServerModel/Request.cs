/*
    SMS Gateway
 
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
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Dynamic;

namespace GSMServerModel
{
    [DataContract]
    public class Request
    {
        private bool cancel;

        [DataMember]
        public IPEndPoint RemoteEndPoint { get;  set; }
        [DataMember]
        public Header Header { get; private set; }
        [DataMember]
        public ExpandoObject Data { get; set; }

        [DataMember]
        public QueueWorkItem QueueWorkItem { get; set; }
        public bool Cancel
        {
            get
            {
                return cancel;
            }
            set
            {
                cancel = value;
                if (value)
                {
                    Header = null;
                    Data = null;
                }
            }
        }

        public Request(IPEndPoint remoteEndPoint, Header header, ExpandoObject data)
        {
            RemoteEndPoint = remoteEndPoint;
            Header = header;
            Data = data;
        }
    }
}
