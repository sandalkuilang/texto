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

namespace GSMClient
{
    internal class ParamArgs 
    {
        private IDictionary<string, object> map;

        [System.Diagnostics.DebuggerStepThrough]
        public ParamArgs()
        { map = new Dictionary<string, object>(); }

        [System.Diagnostics.DebuggerStepThrough]
        public void Add(string key, object value)
        { map.Add(key, value); }

        [System.Diagnostics.DebuggerStepThrough]
        public T Get<T>(string key)
        {
            if (map.ContainsKey(key))
                return (T)map[key];
            else
                return default(T);
        }

        public int Count { get { return map.Count; } }

        [System.Diagnostics.DebuggerStepThrough]
        public void Remove(string key)
        { map.Remove(key); }

        [System.Diagnostics.DebuggerStepThrough]
        public void Clear()
        { map.Clear(); }

        [System.Diagnostics.DebuggerStepThrough]
        public IDictionary<string, object> ToDictionary()
        { return map; }
    }
}
