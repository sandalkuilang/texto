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

namespace GSMServer.Worker
{
    internal class MemoryWorkerPool : IQueue<PacketEventArgs>
    {
        private Queue<PacketEventArgs> slot;

        private readonly object syncLock = new object();

        [System.Diagnostics.DebuggerStepThrough]
        public MemoryWorkerPool()
        { 
            slot = new Queue<PacketEventArgs>(); 
        }

        public int Count
        {
            [System.Diagnostics.DebuggerStepThrough]
            get
            {
                return slot.Count; 
            }
        }

        [System.Diagnostics.DebuggerStepThrough]
        public List<PacketEventArgs> ToList()
        { 
            return slot.ToList(); 
        }

        [System.Diagnostics.DebuggerStepThrough]
        public void Add(PacketEventArgs item)
        {
            lock (syncLock)
            { 
                slot.Enqueue(item);
            }
        }
         
        [System.Diagnostics.DebuggerStepThrough]
        public PacketEventArgs Get()
        {
            lock(syncLock)
            {
                PacketEventArgs ret = null;
                if (slot.Count == 0)
                    return null;
                ret = slot.Dequeue();
                return ret;
            }
        }

        [System.Diagnostics.DebuggerStepThrough]
        public void Dispose()
        { 
            slot.Clear(); 
        }

        [System.Diagnostics.DebuggerStepThrough]
        public void Clear()
        { 
            slot.Clear(); 
        }
         
    }
}
