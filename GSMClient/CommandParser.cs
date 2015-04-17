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

namespace GSMClient
{
    internal class CommandParser : IParser<ParamArgs>
    { 
        public ParamArgs Parse(string input)
        {
            string command = "";
            string args = "";
            ParamArgs param = new ParamArgs();
            Match match = new Regex(@"([a-zA-Z0-9]+).([a-zA-Z0-9]+)").Match(input);
            if (match.Success)
            {
                command = string.Format("{0}.{1}", match.Groups[1].Value, match.Groups[2].Value);
                param.Add("Command", command);
                args = input.Substring(command.Length);
                if (args.StartsWith("("))
                    args = args.Remove(0,1);

                if (args.EndsWith(")"))
                    args = args.Remove(args.Length-1, 1);

                Regex regex = new Regex("([a-zA-Z0-9]+)=([0-9A-Za-z#$%=@!{},`~&*()'<>?.:;_|^/+\t\r\n\\[\\]\"-]*)"); 
                string[] splitArgs = args.Split(new string[]{"+param:"}, StringSplitOptions.None);
                foreach (string arg in splitArgs)
                {
                    match = regex.Match(arg);                    
                    if (match.Success)
                        param.Add(match.Groups[1].Value.Trim(), arg.Trim().Substring(match.Groups[1].Length + 1));
                }
                return param;
            }
            return null;
        }
    }
}
