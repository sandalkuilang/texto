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

namespace GSMServer
{
    internal abstract class MultithreadedSingleton<TClass, TInterface> where TClass : class, TInterface, new()
    {
        private static volatile TClass instance = new TClass();
        private static object syncRoot = new Object();

        public static TInterface Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new TClass();
                    }
                }
                return instance;
            }
        }
    }
}
