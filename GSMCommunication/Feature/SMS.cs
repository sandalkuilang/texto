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
using System.IO.Ports;
using System.Text.RegularExpressions;
using GSMCommunication.PDUDecoder;
using System.Threading;
using GSMCommunication.Feature;
using System.IO;
using System.Diagnostics;
 
namespace GSMCommunication.Feature
{
    public class SMS
    {
        private const string FAILED_SEND = "ERROR: Failed to sending message.";
        public BasicInformation Connection { get; private set; }


        [System.Diagnostics.DebuggerStepThrough]
        public SMS(BasicInformation connector)  
        { 
            this.Connection = connector; 
        }
       
        /// <summary>
        /// Delete one or several messages from preferred message storage (“BM” SMS-CB ‘RAM storage’, “SM” SMSPP storage ‘SIM storage’ or “SR” SMS Status-Report storage). 
        /// </summary>
        /// <param name="index">Delete message at location <index> (default value).</param>
        /// <returns>return true if OK, otherwise false</returns>
        public BaseResult<GenericTypeResult<bool>> Delete(int index)
        { 
            string response = ""; 
            BaseResult<GenericTypeResult<bool>> ret = new BaseResult<GenericTypeResult<bool>>();
            Stopwatch s = new Stopwatch();

            s.Start();
            response = Connection.Connector.Execute(Command.Set(Commands.CMGD, index.ToString()));
            s.Stop();

            if (response.Contains("OK"))
            {
                ret.Response.Result = true;
            }
            ret.ExecutionTime = s.Elapsed;
            return ret;
        }

        /// <summary>
        /// Delete All messages.
        /// </summary>
        /// <returns>return true if OK, otherwise false</returns>
        public BaseResult<GenericTypeResult<bool>> DeleteAll()
        { 
            string response = "";
            BaseResult<GenericTypeResult<bool>> ret = new BaseResult<GenericTypeResult<bool>>();
            Stopwatch s = new Stopwatch();

            s.Start();
            response = Connection.Connector.Execute(Command.Set(Commands.CMGD, "1, 4"));
            s.Stop();

            if (response.Contains("OK"))
            {
                ret.Response.Result = true;
            }
            ret.ExecutionTime = s.Elapsed;
            return ret;
        }

        /// <summary>
        /// Allows the message storage area to be selected (for reading, writing, etc). 
        /// </summary>
        /// <param name="mem1">Memory used to list, read and delete messages.</param>
        /// <param name="mem2">Memory used to write and send messages.</param>
        /// <param name="mem3">Memory to which received SMS are preferred to be stored.</param> 
        [System.Diagnostics.DebuggerStepThrough]
        public BaseResult<GenericTypeResult<string>> SetMessageStorage(string mem1, string mem2, string mem3)
        { 
            string response; 
            string format = (char)34 + "{0}" + (char)34 + "," + (char)34 + "{2}" + (char)34 + "," + (char)34 + "{2}" + (char)34;
            BaseResult<GenericTypeResult<string>> ret = new BaseResult<GenericTypeResult<string>>();
            Stopwatch s = new Stopwatch(); 

            s.Start();
            response = Connection.Connector.Execute(Command.Set(Commands.CPMS, string.Format(format, mem1, mem2, mem3)));
            s.Stop();

            ret.Response.Result = response;
            ret.ExecutionTime = s.Elapsed;
            return ret;
        }
         
        /// <summary>
        /// Select preferred message format.
        /// </summary>
        /// <param name="pduMode">True if PDU mode selected, false if in Text Mode.</param>
        /// <returns>return true if OK, otherwise false</returns>
        public BaseResult<GenericTypeResult<bool>> SetMessageFormat(bool pduMode)
        {  
            string response;
            BaseResult<GenericTypeResult<bool>> ret = new BaseResult<GenericTypeResult<bool>>();
            Stopwatch s = new Stopwatch();

            s.Start();
            if (pduMode)
            { // PDU Mode 
                response = Connection.Connector.Execute(Command.Set(Commands.CMGF, "0"), true); 
                if (response.Contains("OK"))
                {
                    ret.Response.Result = true;
                }
            }
            else
            { // Text Mode
                response = Connection.Connector.Execute(Command.Set(Commands.CMGF, "1"), true);
                if (response.Contains("OK"))
                {
                    ret.Response.Result = true;
                }
            }
            s.Stop();
            ret.ExecutionTime = s.Elapsed;
            return ret;
        }

        /// <summary>
        /// Used to indicate to which service center the message must be sent.
        /// </summary>
        /// <param name="serviceCenter">The number of Service Center.</param>
        /// <returns>return true if OK, otherwise false</returns>
        /// <remarks>The application tries to send a message without having indicated the service center address, an error will be generated.</remarks>
        [System.Diagnostics.DebuggerStepThrough]
        public BaseResult<GenericTypeResult<bool>> SetServiceCenter(string serviceCenter)
        { 
            string response;
            BaseResult<GenericTypeResult<bool>> ret = new BaseResult<GenericTypeResult<bool>>();
            Stopwatch s = new Stopwatch();

            s.Start();
            if (!string.IsNullOrEmpty(serviceCenter))
            {
                response = Connection.Connector.Execute(Command.Set(Commands.CSCA, (char)34 + serviceCenter) + (char)34, false);
                if (response.Contains("OK"))
                {
                    ret.Response.Result = true;
                }
            }
            s.Stop();
            ret.ExecutionTime = s.Elapsed;
            return ret;
        }

        /// <summary>
        /// Sends message from a TE to the network (SMS-SUBMIT).
        /// </summary>
        /// <param name="messageText">The message to be sent. multiple recipient can be split by ";" (semicolon)</param>
        /// <param name="phoneNumber">Recipient address.</param>  
        public BaseResult<SMSSendResult> Send(string messageText, string phoneNumber)
        { 
            string response = "";
            string error = "";
            BaseResult<SMSSendResult> result;
            System.Diagnostics.Stopwatch sp = new System.Diagnostics.Stopwatch();
            BaseResult<GenericTypeResult<RegistrationStatus>> status = new BaseResult<GenericTypeResult<RegistrationStatus>>();
            status.Response.Result = RegistrationStatus.Unknown;

            BaseResult<GenericTypeResult<PhoneActivityStatus>> activityStatus = Connection.GetActivityStatus();
            if (activityStatus.Response.Result != PhoneActivityStatus.Ready) 
                return null; 

            SetMessageFormat(Connection.PDUMode);
            status = Connection.GetRegistrationStatus(); 
            if (Connection.PDUMode)
            {
                if (string.IsNullOrEmpty(Connection.ServiceCenter)) 
                    throw new ArgumentNullException("ServiceCenter cannot be null or empty, please check at configuration."); 

                sp.Start();
                // multiple recipient
                if (phoneNumber.Contains(";"))
                {
                    foreach (string number in phoneNumber.Split(';'))
                    {
                        if (!string.IsNullOrEmpty(number))
                        {
                            SmsSubmitPdu[] pduCodes = GSMCommunication.PDUDecoder.SmartMessaging.SmartMessageFactory.CreateConcatTextMessage(messageText, number);
                            for (int i = 0; i <= pduCodes.Length - 1; i++)
                            {
                                response = Connection.Connector.Execute(Command.Set(Commands.CMGS, pduCodes[i].ActualLength) + '\u000D');
                                response = Connection.Connector.Execute(string.Concat(pduCodes[i].ToString(), (char)26), delayWhenRead: 3500);
                            }
                        }
                    } 
                }
                else
                {
                    SmsSubmitPdu[] pduCodes = GSMCommunication.PDUDecoder.SmartMessaging.SmartMessageFactory.CreateConcatTextMessage(messageText, phoneNumber);
                    for (int i = 0; i <= pduCodes.Length - 1; i++)
                    {
                        response = Connection.Connector.Execute(Command.Set(Commands.CMGS, pduCodes[i].ActualLength) + '\u000D');
                        response = Connection.Connector.Execute(string.Concat(pduCodes[i].ToString(), (char)26), delayWhenRead: 3500);
                    }
                }
                
                sp.Stop();
            }
            else
            { 
                sp.Start();
                // multiple recipient
                if (phoneNumber.Contains(";"))
                {
                    foreach (string number in phoneNumber.Split(';'))
                    {
                        if (!string.IsNullOrEmpty(number))
                        {
                            response = Connection.Connector.Execute(Command.Set(Commands.CMGS, (char)34 + number + (char)34) + '\u000D');
                            response = Connection.Connector.Execute(messageText + '\u001A', delayWhenRead: 3500);
                        }
                    }
                }
                else
                {
                    response = Connection.Connector.Execute(Command.Set(Commands.CMGS, (char)34 + phoneNumber + (char)34) + '\u000D');
                    response = Connection.Connector.Execute(messageText + '\u001A', delayWhenRead: 3500);
                } 
                sp.Stop();
            }

            Match match = new Regex(@"\+CMGS: (\d+)(,(\w+))*").Match(response);             
            if (match.Success) 
                response = match.Groups[0].Value; 
            else 
                error = FAILED_SEND; 
            
            result = new BaseResult<SMSSendResult>();
            result.ExecutionTime = sp.Elapsed;
            result.Response = new SMSSendResult(Connection.OwnNumber, phoneNumber, 
                messageText, 
                Connection.PDUMode, 
                Connection.Operator, 
                sp.Elapsed, 
                status.Response.Result.ToString(), 
                error);
            
            return result;
        }
     
        /// <summary>
        /// Returns List Messages.
        /// </summary>
        /// <param name="status">Select status of the message</param> 
        public List<BaseResult<SMSReadResult>> Read(SMSStatus status)
        { 
            List<BaseResult<SMSReadResult>> listReads = new List<BaseResult<SMSReadResult>>();
            System.Diagnostics.Stopwatch sp = new System.Diagnostics.Stopwatch();

            BaseResult<GenericTypeResult<PhoneActivityStatus>> activityStatus = Connection.GetActivityStatus();
            if (activityStatus.Response.Result != PhoneActivityStatus.Ready)
            {
                return listReads;
            }

            SetMessageFormat(Connection.PDUMode);
            string readValue = "ALL";
            string response;
            Regex regexReadPattern;
            if (Connection.PDUMode)
            { 
                regexReadPattern = new Regex("\\+CMGL: (\\d+),(\\d+),(?:\"(.*[A-Z0-9-_ ].*)\")?,(\\d+)\\r(\\w+)");
                switch (status)
                {
                    case SMSStatus.ReceivedUnreadMessage:
                        readValue = "0";
                        break;
                    case SMSStatus.ReceivedReadMessage:
                        readValue = "1";
                        break;
                    case SMSStatus.StoredUnsentMessage:
                        readValue = "2";
                        break;
                    case SMSStatus.StoredSentMessage:
                        readValue = "3";
                        break;
                    case SMSStatus.AllMessages:
                        readValue = "4";
                        break;
                }
            }
            else
            {
                regexReadPattern = new Regex(@"\+CMGL: (\d+)," + (char)34 + "(.*[A-Z0-9-_ ].*)" + (char)34 + "," + (char)34 + "(.*[0-9-_ +].*)" + (char)34 + ",(?:\"(.*[A-Z0-9-_ ].*)\")?," + @"(.*[0-9-_ +/:].*)" + "\r([ 0-9A-Za-z#$%=@!{},`~&*()'<>?.:;_|^/+\t\r\n\\[\\]\"-]([^(CMGL)]+)*)");
                switch (status)
                {
                    case SMSStatus.ReceivedUnreadMessage:
                        readValue = "\"REC UNREAD\"";
                        break;
                    case SMSStatus.ReceivedReadMessage:
                        readValue = "\"REC READ\"";
                        break;
                    case SMSStatus.StoredUnsentMessage:
                        readValue = "\"STO UNSENT\"";
                        break;
                    case SMSStatus.StoredSentMessage:
                        readValue = "\"STO SENT\"";
                        break;
                    case SMSStatus.AllMessages:
                        readValue = "\"ALL\"";
                        break;
                }
            } 
            Connection.Connector.DiscardBuffer();

            sp.Start();
            response = Connection.Connector.Execute(Command.Set(Commands.CMGL, readValue));
            sp.Stop();

            int index = 1;
            BaseResult<SMSReadResult> smp; 
            for (Match match = regexReadPattern.Match(response); match.Success; match = match.NextMatch())
            {
                IncomingSmsPdu sms = IncomingSmsPdu.Decode(match.Groups[5].Value, true); 

                if (!string.IsNullOrEmpty(match.Groups[1].Value))
                {
                    index = int.Parse(match.Groups[1].Value);
                }
                SMSReadResult read = new SMSReadResult(index, 
                                                        ((GSMCommunication.PDUDecoder.SmsDeliverPdu)(sms)).OriginatingAddress,
                                                        ((GSMCommunication.PDUDecoder.SmsDeliverPdu)(sms)).UserDataText, 
                                                        ((GSMCommunication.PDUDecoder.SmsDeliverPdu)(sms)).SCTimestamp.ToDateTime(),
                                                        match.Groups[2].Value, 
                                                        Connection.Operator); 
                smp = new BaseResult<SMSReadResult>();
                smp.Response = read;
                smp.ExecutionTime = sp.Elapsed;
                listReads.Add(smp);
            }
            return listReads;
        }
         
        [System.Diagnostics.DebuggerStepThrough]
        private static int GetATLength(string pduString)
        {
            string hex = String.Format("{0:X}", pduString.Substring(0, 2));
            return (pduString.Length - Convert.ToInt32(hex, 16) * 2 - 2) / 2;
        }
         
    }
}
