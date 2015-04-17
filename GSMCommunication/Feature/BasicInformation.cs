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
using GSMCommunication.Feature;
using GSMCommunication;
using System.Diagnostics; 

namespace GSMCommunication.Feature
{
    public class BasicInformation
    {  
          
        public BaseConnector Connector { get; private set; }
        public string PortName { get; set; }
        public int SignalQuality { get; set; }
        public string SoftwareVersion { get; set; }
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public string ServiceCenter { get; set; }
        public string Operator { get; set; }
        public string CardID { get; set; }
        public string IntlMobileSubcriberID { get; set; }
        public string OwnNumber {get; set;}
        public bool PDUMode { get; set; }

        public bool IsOpen
        {
            get
            {
                return Connector.IsOpen;
            }
        }


        public event EventHandler BeginExecuting;
        public event EventHandler EndExecuting;
         
        [System.Diagnostics.DebuggerStepThrough]
        public BasicInformation(string portName)
        { 
            this.Connector = new ATController(portName);
            this.PortName = Connector.PortName;
        }

        [System.Diagnostics.DebuggerStepThrough]
        public BasicInformation(BaseConnector connector)
        { 
            this.Connector = connector;
            this.PortName = Connector.PortName;
        }

        [System.Diagnostics.DebuggerStepThrough]
        public BasicInformation(string portName, int baudRate, Parity parity, StopBits stopBits, int dataBits, Handshake handShake)
        { 
            this.Connector = new ATController(portName, baudRate, parity, stopBits, dataBits, handShake);
            this.PortName = Connector.PortName;
        }

        [System.Diagnostics.DebuggerStepThrough]
        public BasicInformation(string portName, int baudRate, Parity parity, StopBits stopBits, int dataBits, Handshake handShake, string serviceCenter, bool pduMode)
        { 
            this.Connector = new ATController(portName, baudRate, parity, stopBits, dataBits, handShake);
            ServiceCenter = serviceCenter;
            PDUMode = pduMode;
            this.PortName = portName;
        }

        public void OnBeginExecuting()
        {
            //Connector.Open();
            if (BeginExecuting != null) BeginExecuting(this, null);
        }

        public void OnEndExecuting()
        {
            //Connector.Close();
            if (EndExecuting != null) EndExecuting(this, null);
        }
        
        /// <summary>
        /// Closes the port connection
        /// </summary>
        [System.Diagnostics.DebuggerStepThrough]
        public void Close()
        { 
            this.Connector.Close(); 
        }

        /// <summary>
        /// Releases all resources used by the Component. 
        /// </summary>
        [System.Diagnostics.DebuggerStepThrough]
        public void Dispose()
        {
            this.Connector.DiscardBuffer();
            this.Connector.Close();
            this.Connector.Dispose(); 
        }

        /// <summary>
        /// Send basic AT command to the device.
        /// </summary>
        /// <returns>return true if OK, otherwise false.</returns>
        [System.Diagnostics.DebuggerStepThrough]
        public bool VerifyDevice()
        {
            string response = ""; 
            response = Connector.Execute(Command.Set(Commands.Empty));
            if (response == null) 
            { 
                return false; 
            }
            if (response.Contains(Commands.RESULT_OK))
            { 
                return true; 
            }
            return false;
        }
         
        /// <summary>
        /// Selects the level of functionality in the ME.
        /// </summary>
        /// <param name="level">The level of modem functionality.</param> 
        public BaseResult<GenericTypeResult<bool>> SetFunctionality(FunctionalityLevel level)
        {
            string response = Commands.RESULT_OK;
            BaseResult<GenericTypeResult<bool>> ret = new BaseResult<GenericTypeResult<bool>>();
            Stopwatch s = new Stopwatch();

            s.Start();
            response = Connector.Execute(Command.Set(Commands.CFUN, (int)level), delayWhenRead:2000);
            s.Stop();

            if (response.Contains(Commands.RESULT_OK))
            {
                ret.Response.Result = true;
            }
            else
            {
                ret.Response.Result = false;
            }
            ret.ExecutionTime = s.Elapsed; 
            return ret;
        }

        /// <summary>
        /// Get network registration
        /// </summary> 
        public BaseResult<GenericTypeResult<RegistrationStatus>> GetRegistrationStatus()
        {
            BaseResult<GenericTypeResult<RegistrationStatus>> ret = new BaseResult<GenericTypeResult<RegistrationStatus>>();
            Stopwatch s = new Stopwatch();
            s.Start();
            string response = Connector.Execute(Command.Get(Commands.CREG), delayWhenRead:3150);
            s.Stop();
            Match match = new Regex(@"\+CREG: (\d+),(\d+)").Match(response);
            if (match.Success)
            {
                ret.ExecutionTime = s.Elapsed;
                ret.Response.Result = (RegistrationStatus)(Convert.ToInt32(match.Groups[2].Value)); 
            } 
            return ret;
        }

        /// <summary>
        /// Get signal quality, approximate dBm.
        /// </summary>
        /// <returns>in percent</returns> 
        public BaseResult<GenericTypeResult<int>> GetSignalQuality()
        {
            Stopwatch s = new Stopwatch();
            BaseResult<GenericTypeResult<int>> ret = new BaseResult<GenericTypeResult<int>>();

            s.Start();
            string response = Connector.Execute(Command.ToString(Commands.CSQ));
            s.Stop();

            Match match = new Regex(@"\+CSQ: (\d+),(\d+)").Match(response);
            if (match.Success)
                response = match.Groups[1].Value;
            else
                response = "";

            int signal = 0;
            if (response != "")
            {
                signal = (-113) + (2 * Convert.ToInt32(response));
                this.SignalQuality = signal;
            }

            ret.Response.Result = signal;
            ret.ExecutionTime = s.Elapsed;
            return ret;
        }

        /// <summary>
        /// Set whether error messages should be displayed in numeric format or verbose format.
        /// </summary>
        /// <param name="format">Value between 1 (Numeric) and 2 (Verbose)</param>
        public BaseResult<GenericTypeResult<bool>> SetErrorMessageFormat(int format)
        {
            BaseResult<GenericTypeResult<bool>> ret = new BaseResult<GenericTypeResult<bool>>();
            Stopwatch s = new Stopwatch();
            s.Start();
            string response = Connector.Execute(Command.Set(Commands.CMEE, format.ToString()));
            s.Stop();

            if (response.Contains("OK"))
            {
                ret.Response.Result = true;
            }

            ret.ExecutionTime = s.Elapsed;
            return ret;
        }

        /// <summary>
        /// Get the TA about the possible character set used by the TE.
        /// </summary>  
        public BaseResult<GenericTypeResult<List<string>>> GetPossibleCharacterSet()
        { 
            List<string> list = null;
            BaseResult<GenericTypeResult<List<string>>> ret = new BaseResult<GenericTypeResult<List<string>>>(); 
            Stopwatch s = new Stopwatch();

            s.Start(); 
            string response = Connector.Execute(Command.Check(Commands.CSCS));
            s.Stop(); 

            Match match = new Regex(@"\+CSCS: \(" + (char)34 + @"(.*[A-Za-z0-9].*)" + (char)34 + "," + (char)34 + @"(.*[A-Za-z0-9].*)" + (char)34 + "," + (char)34 + @"(.*[A-Za-z0-9].*)" + (char)34 + "," + (char)34 + @"(.*[A-Za-z0-9])" + (char)34 + @"\)").Match(response);
            list = new List<string>();
            if (match.Success)
            {
                for (int i = 1; i < match.Groups.Count; i++)
                { 
                    list.Add(match.Groups[i].Value); 
                }
            }
            ret.Response.Result = list;
            ret.ExecutionTime = s.Elapsed;
            return ret;
        }

        /// <summary>
        /// Get the TA about the character set used by the TE.
        /// </summary>  
        public BaseResult<GenericTypeResult<string>> GetCharacterSet()
        {
            BaseResult<GenericTypeResult<string>> ret = new BaseResult<GenericTypeResult<string>>();
            Stopwatch s = new Stopwatch();

            s.Start(); 
            string response = Connector.Execute(Command.Get(Commands.CSCS));
            s.Stop();

            Match match = new Regex(@"\+CSCS: " + (char)34 + @"(.*[A-Za-z0-9].*)" + (char)34).Match(response);
            if (match.Success)
            {
                ret.Response.Result = match.Groups[1].Value; 
            }
            ret.ExecutionTime = s.Elapsed;
            return ret;
        }

        /// <summary>
        /// Informs the TA about the character set used by the TE.
        /// </summary>
        /// <param name="characterSet">string ("IRA","GSM","PCCP437","8859-1") </param> 
        /// <returns>return true if OK, otherwise false.</returns> 
        public BaseResult<GenericTypeResult<bool>> SetCharacterSet(string characterSet)
        {
            BaseResult<GenericTypeResult<bool>> ret = new BaseResult<GenericTypeResult<bool>>();
            Stopwatch s = new Stopwatch();

            s.Start(); 
            string response = Connector.Execute(Command.Set(Commands.CSCS, (char)34 + characterSet + (char)34));
            s.Stop();

            if (response.Contains("OK"))
            {
                ret.Response.Result = true;
            }
            else
            {
                ret.Response.Result = false;
            }
            ret.ExecutionTime = s.Elapsed;
            return ret;
        }

        /// <summary>
        /// Get the reporting of ME errors.
        /// </summary>  
        public BaseResult<GenericTypeResult<int>> GetErrorMessageFormat()
        {
            BaseResult<GenericTypeResult<int>> ret = new BaseResult<GenericTypeResult<int>>();
            Stopwatch s = new Stopwatch();

            s.Start(); 
            string response = Connector.Execute(Command.Get(Commands.CMEE));
            s.Stop();

            Match match = new Regex(@"\+CMEE: (\d+)").Match(response);
            if (match.Success)
            {
                response = match.Groups[1].Value;
                ret.Response.Result = Convert.ToInt32(response);
            }
            ret.ExecutionTime = s.Elapsed;
            return ret;
        }

        /// <summary>
        /// Returns information to identify the ME manufacturer. 
        /// </summary>  
        public BaseResult<GenericTypeResult<string>> GetManufacturer()
        {
            BaseResult<GenericTypeResult<string>> ret = new BaseResult<GenericTypeResult<string>>();
            Stopwatch s = new Stopwatch();

            s.Start(); 
            string response = Connector.Execute(Command.ToString(Commands.GMI));
            s.Stop();

            Match match = new Regex(@"\+GMI\r\r(.*[A-Za-z0-9-_ #$%+=@!{},/|`~&*()'<>?.:;_].*)\r\rOK\r").Match(response);
            if (match.Success)
            { 
                response = match.Groups[1].Value;
                ret.Response.Result = response.Trim();
                this.Manufacturer = ret.Response.Result;
            }
            ret.ExecutionTime = s.Elapsed;
            return ret;
        }

        /// <summary>
        /// Get SMSC address, through which mobile originated SMs are transmitted.
        /// </summary>  
        public BaseResult<GenericTypeResult<string>> GetServiceCenter()
        {
            BaseResult<GenericTypeResult<string>> ret = new BaseResult<GenericTypeResult<string>>();
            Stopwatch s = new Stopwatch();

            s.Start(); 
            string response = Connector.Execute(Command.Get(Commands.CSCA));
            s.Stop();

            Match match = new Regex(@"\+CSCA: " + (char)34 + @"(.*[0-9-_+].*)" + (char)34 + @",(\d+)").Match(response);
            if (match.Success)
            {
                response = match.Groups[1].Value;
                this.ServiceCenter = response;
            }
            else
            {
                response = ""; 
            }
            ret.Response.Result = response;
            ret.ExecutionTime = s.Elapsed;
            return ret;
        }

        /// <summary>
        /// Returns information to identify the ME version, revision level or date. 
        /// </summary>  
        public BaseResult<GenericTypeResult<string>> GetSoftwareVersion()
        {
            BaseResult<GenericTypeResult<string>> ret = new BaseResult<GenericTypeResult<string>>();
            Stopwatch s = new Stopwatch();

            s.Start(); 
            string response = Connector.Execute(Command.ToString(Commands.CGMR));
            s.Stop();

            Match match = new Regex(@"\+CGMR\r\r(.*[A-Za-z0-9-_ #$%+=@!{},/|`~&*()'<>?.:;_].*)\r\rOK\r").Match(response);
            if (match.Success)
            { 
                response = match.Groups[1].Value;
                ret.Response.Result = response.Trim();
                this.SoftwareVersion = ret.Response.Result;
            }
            ret.ExecutionTime = s.Elapsed;
            return ret;
        }

        /// <summary>
        /// Returns information to identify the ME model. 
        /// </summary>  
        public BaseResult<GenericTypeResult<string>> GetModelInformation()
        {
            BaseResult<GenericTypeResult<string>> ret = new BaseResult<GenericTypeResult<string>>();
            Stopwatch s = new Stopwatch();

            s.Start(); 
            string response = Connector.Execute(Command.ToString(Commands.GMM));
            s.Stop();

            Match match = new Regex(@"\+GMM(\r|\r\r\n|r\n)(.*[A-Za-z0-9-_ #$%+=@!{},/|`~&*()'<>?.:;_].*)\r\rOK\r").Match(response);
            if (match.Success)
            { 
                response = match.Groups[2].Value;
                ret.Response.Result = response.Trim();
                this.Model = ret.Response.Result;
            } 
            ret.ExecutionTime = s.Elapsed;
            return ret;
        }
         
        /// <summary>
        /// Return IMSI to identify the individual SIM card.
        /// </summary>  
        public BaseResult<GenericTypeResult<string>> GetIMSI()
        { 
            string response = "";
            BaseResult<GenericTypeResult<string>> ret = new BaseResult<GenericTypeResult<string>>();
            Stopwatch s = new Stopwatch();
             
            s.Start(); 
            response = Connector.Execute(Command.ToString(Commands.CIMI));
            s.Stop();

            Match match = new Regex(@"\+CIMI\r\r(\d+)").Match(response);
            if (match.Success)
            {
                response = match.Groups[1].Value;
            }
            else
            {
                response = "";
            }
            ret.Response.Result = response;
            ret.ExecutionTime = s.Elapsed;
            this.IntlMobileSubcriberID = response; 
            return ret;
        }

        /// <summary>
        /// Get the network operator.
        /// </summary>  
        public BaseResult<GenericTypeResult<string>> GetOperator()
        { 
            string response = "";
            const short maxLoop = 2;
            short loop = 0;
            BaseResult<GenericTypeResult<string>> ret = new BaseResult<GenericTypeResult<string>>();
            Stopwatch s = new Stopwatch();

            s.Start();  
            Connector.Execute(Command.Set(Commands.COPS, "3,0"));
            do
            {
                loop++;
                if (loop >= maxLoop) break;
                System.Threading.Thread.Sleep(500);            
                response = Connector.Execute(Command.Get(Commands.COPS)); 
                Match match = new Regex(@"\+COPS: (\d+),(\d+)," + "\"" + @"(.*[a-zA-Z0-9-_ ].*)" + "\"").Match(response);
                if (match.Success)
                {
                    response = match.Groups[3].Value;
                    this.Operator = response;
                    break;
                }
                else
                    response = "";
            }
            while (string.IsNullOrEmpty(response));
            s.Stop();
            ret.ExecutionTime = s.Elapsed;
            ret.Response.Result = response;

            this.Operator = response; 
            return ret;
        }

        /// <summary>
        /// Returns the activity status of the mobile equipment
        /// </summary>
        /// <returns></returns>
        public BaseResult<GenericTypeResult<PhoneActivityStatus>> GetActivityStatus()
        {
            string response;
            BaseResult<GenericTypeResult<PhoneActivityStatus>> ret = new BaseResult<GenericTypeResult<PhoneActivityStatus>>();
            Stopwatch s = new Stopwatch();
            PhoneActivityStatus status = PhoneActivityStatus.Unavailable;

            s.Start();
            response = Connector.Execute(Command.ToString(Commands.CPAS));
            s.Stop();

            Match match = new Regex(@"\+CPAS: (\d+)").Match(response);
            if (match.Success)
            {
                switch (match.Groups[1].Value)
                {
                    case "0":
                        status = PhoneActivityStatus.Ready;
                        break;
                    case "1":
                        status = PhoneActivityStatus.Unavailable;
                        break;
                    case "2":
                        status = PhoneActivityStatus.Unknown;
                        break;
                    case "3":
                        status = PhoneActivityStatus.Ringing;
                        break;
                    case "4":
                        status = PhoneActivityStatus.CallInProgress;
                        break;
                    case "5":
                        status = PhoneActivityStatus.Sleep;
                        break; 
                }
            }
            ret.ExecutionTime = s.Elapsed;
            ret.Response.Result = status;
            return ret;
        }

        /// <summary>
        /// Returns information to identify the individual ME. Typically IMEI (International Mobile station Equipment Identity).
        /// </summary>  
        public BaseResult<GenericTypeResult<string>> GetSerialNumber()
        { 
            string response;
            BaseResult<GenericTypeResult<string>> ret = new BaseResult<GenericTypeResult<string>>();
            Stopwatch s = new Stopwatch();

            s.Start();
            response = Connector.Execute(Command.ToString(Commands.CGSN));
            s.Stop();

            Match match = new Regex(@"\+CGSN\r\r(\d+)").Match(response);
            if (match.Success)
            { 
                response = match.Groups[1].Value; 
            }
            ret.ExecutionTime = s.Elapsed;
            ret.Response.Result = response;

            return ret; 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns> 
        public BaseResult<GenericTypeResult<string>> GetHardwareVersion()
        { 
            string response;
            BaseResult<GenericTypeResult<string>> ret = new BaseResult<GenericTypeResult<string>>();
            Stopwatch s = new Stopwatch();

            s.Start();
            response = Connector.Execute(Command.ToString(Commands.WHWV));
            s.Stop();

            Match match = new Regex(@"Hardware Version (\d+).(\d+)(\d+)").Match(response);
            if (match.Success)
            { 
                response = string.Format("{0}.{1}{2}", match.Groups[1].Value, match.Groups[2].Value, match.Groups[3].Value); 
            }
            ret.ExecutionTime = s.Elapsed;
            ret.Response.Result = response;
            return ret;
        }
         
        public string GetDateProduction()
        { 
            string response;
            response = Connector.Execute(Command.ToString(Commands.WDOP));
            response = response.Replace("AT+WDOP\r\r\nProduction Date (W/Y):", "");
            response = response.Replace("\r\n\r\nOK\r\n", ""); 
            return response;
        }
         
        public string GetCardID()
        { 
            string response;
            response = Connector.Execute(Command.ToString(Commands.CCID));
            Match match = new Regex(@"\+CCID: " + (char)34 + @"(\d+)" + (char)34).Match(response);
            if (match.Success)
            { 
                response = match.Groups[1].Value; 
            }
            CardID = response; 
            return response;
        }

    }
}
