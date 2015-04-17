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

namespace GSMCommunication.Feature
{
    public class SMSSendResult : Result
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Message { get; set; }
        public bool PDUMode { get; set; }
        public string Operator { get; set; } 
        public DateTime Sent { get; set; }
        public string NetworkStatus { get; set; }

        public string Error { get; set; }

        public SMSSendResult()
        {
        }

        public SMSSendResult(string from, string to, string message, bool pduMode, string operatorNetwork, TimeSpan executionTime, string networkStatus, string error)
        {
            Init(from, to, message, PDUMode, operatorNetwork, executionTime, networkStatus, error);
        }

        private void Init(string from, string to, string message, bool pduMode, string op, TimeSpan executionTime, string networkStatus, string error)
        {
            this.From = from;
            this.To = to;
            this.Message = message; 
            this.PDUMode = pduMode;
            this.Operator = op;
            this.Sent = DateTime.Now;
            this.NetworkStatus = networkStatus;
            this.Error = error;
        }

        public override string TypeName
        { get { return this.GetType().Name; } }
    }
}
