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
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace GSMCommunication.Feature
{
    public class Call  
    {
        public BasicInformation Connection { get; private set; }

        public Call(BasicInformation connector) 
        {
            this.Connection = connector;    
        }

        /// <summary>
        /// Dial a telephone number.
        /// After issuing this command, the modem will attempt to establish a connection and dial the number n. 
        /// </summary>
        /// <param name="number">The telephone number to be dialed. </param> 
        public BaseResult<GenericTypeResult<string>> Dial(string number)
        {
            BaseResult<GenericTypeResult<string>> response = new BaseResult<GenericTypeResult<string>>();
            string responseD;
            Stopwatch s = new Stopwatch();
            s.Start();
            responseD = Connection.Connector.Execute(Commands.AT + Commands.D + number + ";", delayWhenRead:5000);
            s.Stop();
            response.Response.Result = responseD;
            response.ExecutionTime = s.Elapsed;
            return response;
        }

        /// <summary>
        /// Instructs the DCE to disconnect from the line, terminating any call in progress.
        /// </summary>
        /// <param name="number">Disconnect and terminate call (0)</param>
        public BaseResult<GenericTypeResult<bool>> Hang(string value)
        {
            BaseResult<GenericTypeResult<bool>> response = new BaseResult<GenericTypeResult<bool>>();
            string responseD;
            Stopwatch s = new Stopwatch();
            s.Start();
            responseD = Connection.Connector.Execute(Command.ToString(Commands.H));
            s.Stop();
            if (responseD.Contains(Commands.RESULT_OK))
            {
                response.Response.Result = true;
            }
            response.ExecutionTime = s.Elapsed;
            return response;  
        }

        /// <summary>
        /// Answer incoming call
        /// </summary>
        public BaseResult<GenericTypeResult<bool>> Answer()
        {
            BaseResult<GenericTypeResult<bool>> response = new BaseResult<GenericTypeResult<bool>>();
            string responseD;
            Stopwatch s = new Stopwatch();
            s.Start();
            responseD = Connection.Connector.Execute(Commands.A); 
            s.Stop();
            if (responseD.Contains(Commands.RESULT_OK))
            {
                response.Response.Result = true;
            }
            response.ExecutionTime = s.Elapsed;
            return response;   
        }

        /// <summary>
        /// Allows control of the Unstructured Supplementary Service Data(USSD) according to 3GPP TS 22.090 [23]. Both network and mobile initiated operations are supported.
        /// </summary>
        /// <param name="code">
        /// 0 = Disable the result code presentation
        /// 1 = Enable the result code presentation
        /// 2 =  Cancel session (not applicable to read command response) 
        /// </param>
        /// <param name="number">Sequences of digits which may be entered by a mobile user with a handset.</param>
        /// <returns>A sequence entered is sent to the network which replies with an alphanumerical string.</returns>
        public BaseResult<USSDResult> SendUSSD(string code, string number)
        {
            BaseResult<USSDResult> response = new BaseResult<USSDResult>();
            string responseCUSD;
            Stopwatch s = new Stopwatch();
            s.Start();
            Connection.Connector.Execute(Command.Set(Commands.CSCS, "\"GSM\""));
            responseCUSD = Connection.Connector.Execute(Command.Set(Commands.CUSD, code + "," + (char)34 + number + (char)34), delayWhenRead: 6500);
            s.Stop();
            Match match = new Regex(@"\+CUSD: (\d+)," + (char)34 + @"(.*[A-Za-z0-9-_ #$%+=@!{},/|`~&*()'<>?.:;_].*)" + (char)34 + @",(\d+)").Match(responseCUSD);
            if (match.Success)
            {
                response.ExecutionTime = s.Elapsed;
                response.Response.Result = match.Groups[2].Value; 
            } 
            return response;
        }

    }
}
