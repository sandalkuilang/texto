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
using System.Resources;
using System.Threading;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Diagnostics;
using System.Configuration;

namespace GSMCommunication
{
    public class MEErrorManager
    {
        private ResourceManager resources;
        private static object s_InternalSyncObject = new object();
        private static MEErrorManager loader;

        [DebuggerStepThrough]
        internal MEErrorManager()
        {
            this.resources = new ResourceManager("GSMCommunication.lang." + ConfigurationManager.AppSettings["LangDefault"], Assembly.GetExecutingAssembly());
        }
        [DebuggerStepThrough]
        public static string GetString(string response)
        {
            MEErrorManager loader = GetLoader();
            if (loader == null)
            {
                return null;
            }
            Match match = new Regex(@"\+CMS ERROR: (\d+)(,(\w+))*").Match(response);
            if (match.Success)
            {
                return loader.resources.GetString("CMS_ERROR_" + match.Groups[1].Value, null);
            }
            else
                return response;
        }

        private static object InternalSyncObject
        {
            [DebuggerStepThrough]
            get
            {
                if (s_InternalSyncObject == null)
                {
                    object obj2 = new object();
                    Interlocked.CompareExchange(ref s_InternalSyncObject, obj2, null);
                }
                return s_InternalSyncObject;
            }
        }

        [DebuggerStepThrough]
        private static MEErrorManager GetLoader()
        {
            if (loader == null)
            {
                lock (InternalSyncObject)
                {
                    if (loader == null)
                    {
                        loader = new MEErrorManager();
                    }
                }
            }
            return loader;
        }

        public static ResourceManager Resources
        {
            [DebuggerStepThrough]
            get
            {
                return GetLoader().resources;
            }
        }
    }
}
