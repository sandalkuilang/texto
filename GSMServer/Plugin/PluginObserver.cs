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
using System.Reflection;

namespace GSMServer.Plugin
{
    internal class PluginObserver
    {
        IDictionary<string, IPlugin> map;

        [System.Diagnostics.DebuggerStepThrough]
        public PluginObserver()
        { map = new Dictionary<string, IPlugin>(); }

        [System.Diagnostics.DebuggerStepThrough]
        public void Add(string key, IPlugin plg)
        { map.Add(key, plg); }

        [System.Diagnostics.DebuggerStepThrough]
        public IPlugin Get(string key)
        { return map[key]; }

        [System.Diagnostics.DebuggerStepThrough]
        public IDictionary<string, IPlugin> ToDictionary()
        { return map; }

        [System.Diagnostics.DebuggerStepThrough]
        public void NotifySent(string data)
        {
            foreach (string key in map.Keys)
            { 
                map[key].NotifySent(data); 
            }
        }

        public void NotifyReceived(string data)
        {
            foreach (string key in map.Keys)
            { 
                map[key].NotifyReceived(data); 
            }
        }

        public void Close()
        {
            foreach (string key in map.Keys)
            { 
                map[key].Close(); 
            }
        }
    }
}
