/*
    Socket
 
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
using System.Diagnostics;
using System.IO;

namespace Sockets
{
    public class ActionInvoker  
    { 
        private object[] currentAttr;
        private Type type;
        private string iface;
        private Dictionary<string, MethodInfo> actionList;
        public Type InterfaceType {get;private set;}

        public List<string> Commands
        {
            get
            {
                return actionList.Keys.ToList();
            }
        }

        public object Instance
        { 
            get 
            { 
                return currentAttr[0]; 
            } 
        }
        public bool UserLowercase { get; set; }

        public ActionInvoker(object target, string interfaceName)
        {
            this.UserLowercase = true;
            type = target.GetType();
            iface = interfaceName;
            actionList = new Dictionary<string, MethodInfo>();
            if (!interfaceName.Contains("."))
                InterfaceType = Type.GetType(ResourcesEmbedded.Namespace + "." + interfaceName);
            else
                InterfaceType = Type.GetType(interfaceName);
            currentAttr = type.GetCustomAttributes(InterfaceType, false);
            this.GetFilter();
        }

        public ActionInvoker(object target, object interfaceObject)
        {
            this.UserLowercase = true;
            type = target.GetType();
            iface = ((Type)interfaceObject).FullName;
            actionList = new Dictionary<string, MethodInfo>();
            InterfaceType = (Type)interfaceObject;
            currentAttr = type.GetCustomAttributes(InterfaceType, false);
            this.GetFilter();
        }

        [DebuggerStepThrough]
        public void GetFilter()
        {
            MethodInfo[] list = InterfaceType.GetMethods();
            foreach (MethodInfo l in list)
            {
                if (this.UserLowercase)
                {
                    actionList.Add(l.Name.ToLower(), l);
                }
                else
                {
                    actionList.Add(l.Name, l);                
                }
            }
        }
         
        public object TryInvoke(string MethodName, object[] args)
        {
            if (currentAttr.Length == 0 ) return null;
            object ret = string.Format("'{0}' is not recognized as a command.", MethodName);
            if ((actionList.Count > 0) && (actionList.ContainsKey(MethodName.ToLower())))
            { 
                if (actionList[MethodName.ToLower()].GetParameters().Length == 0)
                {
                    ret = actionList[MethodName.ToLower()].Invoke(currentAttr[0], BindingFlags.IgnoreCase | BindingFlags.InvokeMethod | BindingFlags.Instance, null, null, null);
                }
                else
                {
                    ret = actionList[MethodName.ToLower()].Invoke(currentAttr[0], BindingFlags.IgnoreCase | BindingFlags.InvokeMethod | BindingFlags.Instance, null, args, null);
                } 
            }            
            return ret;
        }
         
    }
}
