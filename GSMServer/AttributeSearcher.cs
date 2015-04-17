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

namespace GSMServer
{
    internal class AttributeSearcher
    {
        public virtual List<Type> Search(Type attributeSearch)
        {
            List<Type> ret = new List<Type>();
            foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type t in a.GetTypes())
                {
                    if (t.IsInterface == true)
                    { 
                        object[] cust = t.GetCustomAttributes(attributeSearch, true);
                        if ((cust.Length > 0) && cust[0].GetType() == attributeSearch)                            
                        {
                            ret.Add(t);
                        }
                    }
                } 
            }
            if (ret.Count == 0)
                return null;
            else
                return ret;
        }
    }
}
