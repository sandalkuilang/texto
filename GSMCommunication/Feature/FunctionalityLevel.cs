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
    /// <summary>
    /// Set the hardware functionality to a certain level. 
    /// </summary>
    public enum FunctionalityLevel
    {
        /// <summary>
        /// Minimal functionality [default]. 
        /// </summary>
        MinimumFunctionality,
        /// <summary>
        /// Full functionality. 
        /// </summary>
        FullFunctionality,
        /// <summary>
        /// Disable transmit RF circuits (not supported usually) 
        /// </summary>
        DisableTransmitRF,
        /// <summary>
        /// Disable receive RF circuits (not supported usually) 
        /// </summary>
        DisableReceiveRF,
        /// <summary>
        /// Disable transmit and receive RF circuits (flight mode) 
        /// </summary>
        DisableTransmitReceiveRF,
        /// <summary>
        /// GSM only
        /// </summary>
        GSMOnly,
        /// <summary>
        /// WCDMA only 
        /// </summary>
        WCMAOnly
    }
}
