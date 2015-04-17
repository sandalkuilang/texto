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
using System.Diagnostics;
 
namespace GSMCommunication
{
    public class Command
    {
        private const string AT = "AT";
         
        /// <summary>
        /// Appending command with =?
        /// </summary>
        [System.Diagnostics.DebuggerStepThrough]
        public static string Check(string command)
        {
            return Command.AT + command + "=?"; 
        }

        /// <summary>
        /// Appending command with =
        /// </summary>
        [System.Diagnostics.DebuggerStepThrough]
        public static string Set(string command, object value)
        {
            return Command.AT + command + "=" + value;
        }

        /// <summary>
        /// Appending AT + command
        /// </summary>
        [System.Diagnostics.DebuggerStepThrough]
        public static string Set(string command)
        {
            return Command.AT + command;
        }
          
        /// <summary>
        /// Append command with ?
        /// </summary>
        [DebuggerStepThrough]
        public static string Get(string command)
        {
            return Command.AT + command + "?"; 
        }

        [DebuggerStepThrough]
        public static string ToString(string command)
        {
            return Command.AT + command; 
        }

        /// <summary>
        /// Append ":"
        /// </summary>
        [DebuggerStepThrough]
        public static string ToResponse(string command)
        { 
            return command + ":"; 
        }
    }
}
