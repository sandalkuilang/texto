/*
    Sample Sending SMS
 
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

namespace SampleSendingSMS
{

    internal abstract class Collection : ICollection<string>
    {

        private IDictionary<string, string> map;

        public Collection()
        {
            map = new Dictionary<string, string>();
        }

        public void Add(string key, string value)
        {
            if (!map.ContainsKey(key))
            {
                map.Add(key, value);
            }
        }

        public void Remove(string key)
        {
            if (map.ContainsKey(key))
            {
                map.Remove(key);
            }
        }

        public string Get(string key)
        {
            if (map.ContainsKey(key))
            {
                return map[key];
            }
            return null;
        }

        public void Clear()
        {
            map.Clear();
        }
    }
}
