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
using GSMCommunication.Feature;

namespace GSMServer
{
    internal class RandomConnectionProvider : IQueue<BasicInformation>
    {
        private Dictionary<int, BasicInformation> slot;
        private Dictionary<int, string> pickup;
        private Random rnd;
        private static object s_InternalSyncObject = new object();

        public RandomConnectionProvider()
        {
            slot = new Dictionary<int, BasicInformation>();
            pickup = new Dictionary<int, string>();
            rnd = new Random();
        }

        //[System.Diagnostics.DebuggerStepperBoundary] 
        public BasicInformation Get()
        {
            BasicInformation ret = null;
            lock (s_InternalSyncObject)
            {
                if (slot.Count == 0)
                    return null;

                int randomGet = rnd.Next(0, slot.Count);
                if (!pickup.ContainsKey(randomGet))
                {
                    while (ret == null)
                    {
                        if (slot.ContainsKey(randomGet))
                        {
                            ret = slot[randomGet];
                            slot.Remove(randomGet);
                        }
                    }
                    pickup.Add(randomGet, ret.Connector.PortName);
                }
            }
            return ret;
        }
         
        //[System.Diagnostics.DebuggerStepperBoundary]
        public virtual void Add(BasicInformation item)
        {
            BasicInformation connection = slot.Values.Where(s => s.Connector.PortName.ToLower() == item.Connector.PortName.ToLower()).SingleOrDefault();
            if (connection == null)
            {
                item.BeginExecuting -= OnBeginExecuting;
                item.EndExecuting -= OnEndExecuting;
                item.BeginExecuting += OnBeginExecuting;
                item.EndExecuting += OnEndExecuting;
                int id = GetID(); 
                slot.Add(id, item);
                if (pickup.ContainsValue(item.Connector.PortName))
                {
                    int position = pickup.Where(i => i.Value == item.Connector.PortName).Select(x => x.Key).SingleOrDefault();
                    pickup.Remove(position);
                }
            } 
            else
            {
                int position = slot.Where(i => i.Value == connection).Select(x => x.Key).SingleOrDefault();
                if (pickup.ContainsKey(position))
                {
                    pickup.Remove(position);
                }
            }

            if (slot.Count == 0)
            {
                throw new NullReferenceException("RandomConnectionProvider");
            }
        }

        protected int GetID()
        {
            int id = 0;
            if (slot.Count == 0)
            {
                id = 0;
            }
            else
            {
                while (true)
                {
                    if (!slot.ContainsKey(id))
                    {
                        break;
                    }
                    id += 1;
                } 
            }
            return id;
        }

        public List<BasicInformation> ToList()
        {
            return slot.Values.ToList();
        }

        public int Count
        {
            get { return slot.Count; }
        }

        public void Clear()
        {
            slot.Clear();
            pickup.Clear();
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
