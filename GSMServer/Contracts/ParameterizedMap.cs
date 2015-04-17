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

namespace GSMServer.Contracts
{
    public class ParameterizedMap
    {
        private IDictionary<string, object> param;

        public ParameterizedMap()
        { param = new Dictionary<string, object>(); }

        [System.Diagnostics.DebuggerStepThrough]
        public void Add(string key, object value)
        { param.Add(key, value); }

        [System.Diagnostics.DebuggerStepThrough]
        public T TryGet<T>(string key)
        {
            if (param.ContainsKey(key))
                return (T)param[key];
            else
                return default(T);
        }

        [System.Diagnostics.DebuggerStepThrough]
        public void TryAdd(string key, object value)
        {
            if (!param.ContainsKey(key))
                Add(key, value);
        }

        [System.Diagnostics.DebuggerStepThrough]
        public bool Remove(string key)
        { return param.Remove(key); }

        [System.Diagnostics.DebuggerStepThrough]
        public IDictionary<string, object> ToDictionary()
        { return param; }

        [System.Diagnostics.DebuggerStepThrough]
        public List<KeyValuePair<string, object>> ToList()
        { return param.ToList(); }
    }
}
