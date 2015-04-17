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
    internal class Plugin : IPlugin
    {
        private Type type;
        private object instance;
        private PluginMap map; 
         
        public Plugin(object instance, PluginMap map)
        {
            this.type = instance.GetType();
            this.instance = instance;
            this.map = map;
        }
         
        public void NotifySent(string data)
        {
            MethodInfo m = map.TryGet(PluginRoutine.OnDataSent);
            if (m == null) return;
            Func<object, object[], object> dlgate = m.Invoke;
            dlgate.BeginInvoke(instance, new object[] { data }, null, null); 
        }
         
        public void NotifyReceived(string data)
        {
            MethodInfo m = map.TryGet(PluginRoutine.OnDataReceived);
            if (m == null) return;
            Func<object, object[], object> dlgate = m.Invoke; 
            dlgate.BeginInvoke(instance, new object[] { data }, null, null); 
        }
         
        public void CreateInstance()
        {
            MethodInfo m = map.TryGet(PluginRoutine.Main);
            if (m == null) return;
            Func<object, object[], object> dlgate = m.Invoke; 
            dlgate.BeginInvoke(instance, null, null, null);
        }
         
        public void Close()
        {
            MethodInfo m = map.TryGet(PluginRoutine.OnDisposed);
            if (m == null) return;
            m.Invoke(PluginRoutine.OnDisposed, null);
            instance = null;
            type = null;
        }

        public string GetName()
        {
            return type.Module.Name;
        }
    }
}
