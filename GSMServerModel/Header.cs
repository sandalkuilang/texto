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
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace GSMServerModel
{
    [DataContract]
    [Serializable]
    public class Header
    {
        [DataMember]
        public string Signature { get;  set; }
        [DataMember]
        public string Application { get;  set; }
        [DataMember]
        public string RequestType { get;  set; }

        public Header()
        { }

        public Header(string sign, string app, string requestType)
        {
            Signature = sign;
            Application = app;
            RequestType = requestType;
        }
    }
}
