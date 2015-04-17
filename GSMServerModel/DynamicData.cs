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
using System.Dynamic;
using System.Collections;
using System.ComponentModel;

namespace GSMServerModel
{
    public class DynamicData : DynamicObject
    {
        private Dictionary<string, object> dictionary
        = new Dictionary<string, object>();

        [System.Diagnostics.DebuggerStepThrough]
        public Dictionary<string, object> GetDictionary()
        { return dictionary; }

        
        public int Count
        { [System.Diagnostics.DebuggerStepThrough]get { return dictionary.Count; } }

        [System.Diagnostics.DebuggerStepThrough]
        public override bool TryGetMember(
        GetMemberBinder binder, out object result)
        {
            string name = binder.Name.ToLower();
            return dictionary.TryGetValue(name, out result);
        }

        public void Add(string key, object value)
        {
            dictionary.Add(key, value);
        }

        [System.Diagnostics.DebuggerStepThrough]
        public override bool TrySetMember(
        SetMemberBinder binder, object value)
        {
            dictionary[binder.Name.ToLower()] = value;
            return true;
        }
    }
 
}