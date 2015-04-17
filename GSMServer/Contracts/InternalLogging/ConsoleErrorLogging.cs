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

namespace GSMServer.Contracts.InternalLogging
{
    internal class ConsoleErrorLogging : IErrorLogging
    {

        private const string ErrorFormat = "Error : {0}";
        public void Write(Exception ex)
        {
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(string.Format(ErrorFormat, ex.Message));
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void Write(string message)
        {
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(string.Format(ErrorFormat, message));
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
