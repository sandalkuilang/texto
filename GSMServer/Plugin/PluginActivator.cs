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
using System.Runtime.Remoting;

namespace GSMServer.Plugin
{
    internal class PluginActivator
    {
       
        public static Plugin Create(string assemblyFile, string typeName)
        {
            if (!assemblyFile.Contains(".dll"))
                assemblyFile += ".dll";

            if (!System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + assemblyFile))
                return null;

            Assembly asm = Assembly.LoadFile(AppDomain.CurrentDomain.BaseDirectory + assemblyFile);
            Type type = asm.GetType(typeName);
            PluginMap map = new PluginMap();
            MethodInfo method = null; 
            foreach (string key in map.Keys)
            {
                if (map.ContainsKey(key))
                {
                    method = type.GetMethod(key);
                    map.Set(key, method);
                }    
            } 
            if (map.IsValid())
            {
                Plugin plug = new Plugin(asm.CreateInstance(typeName), map);
                plug.CreateInstance();
                return plug;
            }
            else
                return null;
        }
    }
}
