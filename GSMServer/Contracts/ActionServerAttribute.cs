using GSMServer.Security;
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
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace GSMServer.Contracts
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ActionServerAttribute : Attribute, IServer
    {
        public List<GSMCommunication.Feature.BasicInformation> GetAvailableConnections()
        { 
            IServer server = ObjectPool.Instance.Resolve<IServer>();
            return server.GetAvailableConnections();
        }

        public List<string> GetRegisteredPlugins()
        {
            IServer server = ObjectPool.Instance.Resolve<IServer>();
            return server.GetRegisteredPlugins();
        }

        public void Refresh()
        {
           if (UnauthorizedCall.Instance.IsAuthorized())
           {
               IServer server = ObjectPool.Instance.Resolve<IServer>(); 
               server.Refresh();
           } 
        }

        public bool Detect()
        {
            if (UnauthorizedCall.Instance.IsAuthorized())
            {
                IServer server = ObjectPool.Instance.Resolve<IServer>(); 
                return server.Detect();
            }
            return false;
        }

        public void OnDeviceRemoved()
        {
            if (UnauthorizedCall.Instance.IsAuthorized())
            {
                IServer server = ObjectPool.Instance.Resolve<IServer>(); 
                server.OnDeviceRemoved();
            }
        }
    }
}
