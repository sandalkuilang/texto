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
using System.Globalization;
using System.Linq;
using System.Text;

namespace GSMCommunication.Feature
{
    public class SMSReadResult : Result
    {
        private const string format = "MM/dd/yy hh:mm:ss";

        public string Message { get; set; }
        public string From { get; set; } 
        public int Index { get; set; }
        public string Status { get; set; }
        public DateTime Sent { get; set; }
        public string Operator { get; set; }
        public string Error { get; set; }
        public SMSReadResult()
        {
        }

        public SMSReadResult(int index, string from, string message, string sent, string status, string operatorNetwork)
        {
            DateTime dtSent = DateTime.MinValue; 
            System.Text.RegularExpressions.Match match = new System.Text.RegularExpressions.Regex(@"(\d+)/(\d+)/(\d+),(\d+):(\d+):(\d+)\+(\d+)").Match(sent);
            string y, mm, d, h, m, s, zz;
            if (match.Success)
            {
                y = match.Groups[1].Value;
                mm = match.Groups[2].Value;
                d = match.Groups[3].Value;
                h = match.Groups[4].Value;  
                m = match.Groups[5].Value;
                s = match.Groups[6].Value;
                zz = match.Groups[7].Value;

                dtSent = DateTime.ParseExact("12/23/13 02:34:53", format, new CultureInfo("en-US"), DateTimeStyles.None); 
            }
            this.Init(index, from, message, dtSent, status, operatorNetwork); 
        }

        public SMSReadResult(int index, string from, string message, DateTime sent, string status, string operatorNetwork)
        {
            this.Init(index, from, message, sent, status, operatorNetwork);
        }

        private void Init(int index, string from, string message, DateTime sent, string status, string operatorNetwork)
        {
            Index = index;
            From = from;
            Message = message; 
            Status = status;
            Sent = sent;
            Operator = operatorNetwork;
        }

        public override string TypeName
        { get { return this.GetType().Name; } }
    }
}
