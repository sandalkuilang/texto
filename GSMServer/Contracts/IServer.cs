/*
    SMS Gateway
 
    Copyright (C) 2013 Yudha - yudha_hyp@yahoo.com

    This library is free software; you can redistribute it and/or
    modify it, in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  
*/
using GSMCommunication.Feature;
using GSMServer.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSMServer.Contracts
{
    [Contract]
    public interface IServer
    {
        void OnDeviceRemoved();
        bool Detect();
        void Refresh();
        List<BasicInformation> GetAvailableConnections();
        List<string> GetRegisteredPlugins();
    }
}
