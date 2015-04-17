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
using GSMCommunication.Feature;
using System.Diagnostics;

namespace GSMCommunication.Feature
{
    public class PhoneBook 
    {
        public const int TON = 129;
        public const int NPI = 145;
        
        public BasicInformation Connection { get; private set; }
          
        public PhoneBook(BasicInformation connector) 
        {
            this.Connection = connector;
        }

        /// <summary>
        /// This command returns phonebook entries with alphanumeric fields starting with a given pattern. 
        /// </summary>
        /// <param name="memory">Phonebook memory storage. see PhoneBookMemoryStorage.</param>
        /// <param name="name">Searched pattern string (depends on the format of data stored in the phonebooks)</param> 
        public BaseResult<GenericTypeResult<List<PhoneNumberInfo>>> FindPhoneBook(string memory, string name)
        { 
            string response;
            BaseResult<GenericTypeResult<List<PhoneNumberInfo>>> ret = new BaseResult<GenericTypeResult<List<PhoneNumberInfo>>>();
            Stopwatch s = new Stopwatch();

            s.Start();
            response = Connection.Connector.Execute(Command.Set(Commands.CPBS, (char)34 + memory + (char)34));
            response = Connection.Connector.Execute(Command.Set(Commands.CPBF, (char)34 + name + (char)34));
            s.Stop();

            Regex regex = new Regex(@"\+CPBF: " + "(\\d+),\"(.+)\",(\\d+),\"(.+)\".*\\r\\n");
            List<PhoneNumberInfo> info = new List<PhoneNumberInfo>();
            for (Match match = regex.Match(response); match.Success; match = match.NextMatch())
            {
                info.Add(new PhoneNumberInfo()
                {
                    Index = Convert.ToInt32(match.Groups[1].Value),
                    Memory = memory,
                    PhoneNumber = match.Groups[2].Value,
                    Name = match.Groups[4].Value
                });
            }
            ret.ExecutionTime = s.Elapsed;
            ret.Response.Result = info;
            return ret;
        }

        /// <summary>
        /// Read a phonebook entry in the current phonebook memory storage.  
        /// </summary>
        /// <param name="memory">Phonebook memory storage. see PhoneBookMemoryStorage.</param> 
        /// <param name="fromIndex">First entry location (or range of locations) where to read phonebook entry.</param>
        /// <param name="toIndex">Last entry location (or range of locations) where to read phonebook entry.</param>
        public BaseResult<GenericTypeResult<List<PhoneNumberInfo>>> ReadPhoneBook(string memory, int fromIndex, int toIndex)
        { 
            string response; 
            string command = fromIndex.ToString();
            BaseResult<GenericTypeResult<List<PhoneNumberInfo>>> ret = new BaseResult<GenericTypeResult<List<PhoneNumberInfo>>>();
            Stopwatch s = new Stopwatch();
             
            if (toIndex > 0)
            {
                command += "," + toIndex;
            }
            s.Start();
            if (!string.IsNullOrEmpty(memory))
            {
                response = Connection.Connector.Execute(Command.Set(Commands.CPBS, (char)34 + memory + (char)34));
            }
            response = Connection.Connector.Execute(Command.Set(Commands.CPBR, command));
            s.Stop();

            Regex regex = new Regex(@"\+CPBR: " + "(\\d+),\"(.+)\",(\\d+),\"(.+)\".*");
            List<PhoneNumberInfo> info = new List<PhoneNumberInfo>();
            for (Match match = regex.Match(response); match.Success; match = match.NextMatch())
            {
                info.Add(new PhoneNumberInfo() 
                {
                    Index = Convert.ToInt32(match.Groups[1].Value),
                    Memory = memory,
                    PhoneNumber = match.Groups[2].Value,
                    Name = match.Groups[4].Value
                });
            }
            ret.Response.Result = info;
            ret.ExecutionTime = s.Elapsed;
            return ret;
        }

        /// <summary>
        /// Get phonebook memory storage
        /// </summary> 
        public BaseResult<GenericTypeResult<PhoneBookInfo>> GetInfo()
        { 
            string response = ""; 
            PhoneBookInfo info = null;
            BaseResult<GenericTypeResult<PhoneBookInfo>> ret = new BaseResult<GenericTypeResult<PhoneBookInfo>>();
            Stopwatch s = new Stopwatch();

            s.Start();
            response = Connection.Connector.Execute(Command.Get(Commands.CPBS));
            s.Stop();

            Match match = new Regex(@"\+CPBS: " + (char)34 + "(.*[A-Z].*)" + (char)34 + @",(\d+),(\d+)").Match(response);
            if (match.Success)
            {
                info = new PhoneBookInfo();
                info.Memory = match.Groups[1].Value;
                info.Used = Convert.ToInt32(match.Groups[2].Value);
                info.Available = Convert.ToInt32(match.Groups[3].Value);
                response = Connection.Connector.Execute(Command.Check(Commands.CPBR));
                match = new Regex(@"\+CPBR: \((.*[0-9-].*)\),(\d+),(\d+)").Match(response);
                if (match.Success)
                {
                    info.MaxLengthPhoneNumber = Convert.ToInt32(match.Groups[2].Value);
                    info.MaxLengthName = Convert.ToInt32(match.Groups[3].Value);
                }
                ret.Response.Result = info;
            }
            ret.ExecutionTime = s.Elapsed;
            return ret;
        }

        /// <summary>
        /// Select phonebook memory storage
        /// </summary>
        /// <param name="memory">Phonebook memory storage. see PhoneBookMemoryStorage.</param>
        /// <returns>return true if OK, otherwise false.</returns>
        public BaseResult<GenericTypeResult<bool>> SetPhoneBookMemory(string memory)
        { 
            string response;
            BaseResult<GenericTypeResult<bool>> ret = new BaseResult<GenericTypeResult<bool>>(); 
            Stopwatch s = new Stopwatch();

            s.Start();
            response = Connection.Connector.Execute(Command.Set(Commands.CPBS, (char)34 + memory + (char)34));
            s.Stop();

            if (response.Contains("OK"))
            {
                ret.Response.Result = true;
            }
            ret.ExecutionTime = s.Elapsed;
            return ret;
        }

        /// <summary>
        /// Writes a phonebook entry in location number index in the current phonebook memory storage.  
        /// </summary>
        /// <param name="memory">Phonebook memory storage. see PhoneBookMemoryStorage.</param>
        /// <returns>return true if OK, otherwise false.</returns>
        public BaseResult<GenericTypeResult<bool>> WritePhoneBook(string memory, string index, string name, string phoneNumber)
        { 
            string response;  
            int tonNPI = TON;
            BaseResult<GenericTypeResult<bool>> ret = new BaseResult<GenericTypeResult<bool>>();
            Stopwatch s = new Stopwatch();

            if (index == "0")
                throw new IndexOutOfRangeException("index must be greater than zero");
            if (phoneNumber.Substring(0,1) == "+")
                tonNPI = NPI;
            if (!string.IsNullOrEmpty(memory)) response = Connection.Connector.Execute(Command.Set(Commands.CPBS, (char)34 + memory + (char)34));
            

            s.Start();
            response = Connection.Connector.Execute(Command.Set(Commands.CPBW, index + "," + (char)34) + phoneNumber + (char)34 + "," + tonNPI + "," + (char)34 + name + (char)34);
            s.Stop();

            if (response.Contains("OK"))
            {
                ret.Response.Result = true;
            }
            ret.ExecutionTime = s.Elapsed;
            return ret;
        }

    }
}
