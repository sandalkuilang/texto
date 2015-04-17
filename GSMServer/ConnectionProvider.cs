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
using GSMCommunication;
using System.Reflection;
using GSMCommunication.Feature;

namespace GSMServer
{
    internal class ConnectionProvider : IQueue<BasicInformation>
    {
        private Queue<BasicInformation> slot;
        private static object s_InternalSyncObject = new object();

        public ConnectionProvider()
        { 
            slot = new Queue<BasicInformation>(); 
        }
         
        public BasicInformation Get()
        {
            BasicInformation ret = null;
            lock (s_InternalSyncObject)
            {
                if (slot.Count == 0)
                    return null;
                ret = slot.Dequeue(); 
            }
            return ret;
        }

        public void Add(BasicInformation item)
        {
            if (slot.Where(s => s.Connector.PortName.ToLower() == item.Connector.PortName.ToLower()).SingleOrDefault() == null)
            {
                item.BeginExecuting -= OnBeginExecuting;
                item.EndExecuting -= OnEndExecuting;
                item.BeginExecuting += OnBeginExecuting;
                item.EndExecuting += OnEndExecuting;
                slot.Enqueue(item);
            } 
        }

        public List<BasicInformation> ToList()
        { 
            return slot.ToList(); 
        }


        public int Count
        { 
            get 
            { 
                return slot.Count; 
            } 
        }

        public void Clear()
        { 
            slot.Clear(); 
        }

        private void OnBeginExecuting(object sender, EventArgs e)
        { 
        }

        private void OnEndExecuting(object sender, EventArgs e)
        { 
            Add((BasicInformation)sender); 
        } 
    }
}
