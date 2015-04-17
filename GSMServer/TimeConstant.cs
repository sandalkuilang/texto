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

namespace GSMServer
{
    internal class TimeConstant
    {
        public const int INFINITE = -1;
        /// <summary>
        /// Maximum waiting ticks between next request
        /// </summary>
        public const int MAX_WAIT_TIMEOUT = 10000;
        /// <summary>
        /// Time needed by queue checker to signal Wait Block Sending SMS
        /// </summary>
        public const int DEFAULT_INTERVAL_QUEUE = 1500;
        /// <summary>
        /// Time needed by the queue to read SMS inbox
        /// </summary>
        public const int READ_TIMEOUT = 5000; 
    }
}
