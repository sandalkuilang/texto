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
    internal class PluginMap
    {
        private IDictionary<string, MethodInfo> map = new Dictionary<string, MethodInfo>();

        [System.Diagnostics.DebuggerStepThrough]
        public PluginMap()
        {
            map.Add("Main", null);
            map.Add("OnDataSent", null);
            map.Add("OnDataReceived", null); 
        }

        [System.Diagnostics.DebuggerStepThrough]
        public bool IsValid()
        {
            foreach (string key in map.Keys)
            {
                if (map[key] != null)
                {
                    return true;
                }
            }
            return false;
        }

        [System.Diagnostics.DebuggerStepThrough]
        public void Set(string key, MethodInfo method)
        { map[key] = method; }

        public ICollection<string> Keys
        { [System.Diagnostics.DebuggerStepThrough]
            get { return map.Keys.ToList(); } 
        }

        [System.Diagnostics.DebuggerStepThrough]
        public bool ContainsKey(string key)
        { return map.ContainsKey(key); }

        [System.Diagnostics.DebuggerStepThrough]
        public MethodInfo TryGet(string key)
        {
            if (map.ContainsKey(key))
                return map[key];
            else
                return null;
        }

    }
}
