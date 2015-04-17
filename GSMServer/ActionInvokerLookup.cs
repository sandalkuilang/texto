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
using Sockets;

namespace GSMServer
{
    internal class ActionInvokerLookup : AttributeSearcher 
    {
        private List<ActionInvoker> action;
        private object target;
        private Type typeofTarget;

        public ActionInvokerLookup(object target)
        {
            this.target = target;
            this.typeofTarget = target.GetType();
            action = new List<ActionInvoker>();
            List<Type> contract = Search(typeof(Contracts.ContractAttribute));
            foreach (Type c in contract)
            { 
                action.Add(new ActionInvoker(target, c)); 
            }
        }

        public List<ActionInvoker> ToList()
        {
            return action.ToList();
        }

        [System.Diagnostics.DebuggerStepThrough]
        public ActionInvoker GetInvoker<T>()
        {
            Type type = typeof(T);
            for (int i = 0; i < action.Count; i++)
            {
                if (type == action[i].InterfaceType)
                    return action[i];
            }
            return null;
        }

        [System.Diagnostics.DebuggerStepThrough]
        public T Get<T>()
        {
            Type type = typeof(T);
            for (int i = 0; i < action.Count; i++)
            {
                if (type == action[i].InterfaceType) 
                    return (T)action[i].Instance;  
            }
            return default(T);
        }
    }
}
