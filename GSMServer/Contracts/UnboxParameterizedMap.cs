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
using GSMCommunication;
using GSMServerModel;
using GSMCommunication.Feature; 

namespace GSMServer.Contracts
{
    public class UnboxParameterizedMap
    {
        private Dictionary<string, object> datamap = new Dictionary<string, object>();

        [System.Diagnostics.DebuggerStepperBoundary]
        public UnboxParameterizedMap(ParameterizedMap map)
        { 
            Unmap(map); 
        }

        [System.Diagnostics.DebuggerStepperBoundary]
        private void Unmap(ParameterizedMap map)
        {
            this.datamap.Add("base", map.TryGet<BasicInformation>("base"));
            PacketEventArgs packet = map.TryGet<PacketEventArgs>("packet");
            if (packet != null)
            { 
                DynamicData data = map.TryGet<DynamicData>("parameters");
                foreach (string key in data.GetDictionary().Keys)
                {
                    this.datamap.Add(key, data.GetDictionary()[key]);
                } 
            } 
        }

        [System.Diagnostics.DebuggerStepperBoundary]
        public T TryGet<T>(string key)
        {
            if (datamap.ContainsKey(key))
            { 
                return (T)datamap[key]; 
            }
            else
                return default(T);
        }

    }
}
