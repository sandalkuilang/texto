using Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSMServer.Worker
{
    internal class WorkerPoolManager
    {
        private List<IQueue<PacketEventArgs>> container;
        private Random rnd;
        private IQueue<PacketEventArgs> currentUsedWorker;

        public IQueue<PacketEventArgs> CurrentWorker
        {
            get
            {
                return currentUsedWorker;
            }
        }
         
        public WorkerPoolManager() : this(new List<IQueue<PacketEventArgs>> ())
        {

        }

        public WorkerPoolManager(List<IQueue<PacketEventArgs>> pool)
        {
            rnd = new Random();
            this.container = pool;
        }

        public void AddPool(IQueue<PacketEventArgs> item)
        {
            if (!this.container.Contains<IQueue<PacketEventArgs>>(item))
            {
                this.container.Add(item);
            }
        }

        public IQueue<PacketEventArgs> GetPool(int index)
        {
            return this.container[index];
        }

        public List<IQueue<PacketEventArgs>> ToList()
        {
            return container;
        }

        public void RemovePool(int index)
        {
            this.container.RemoveAt(index);
        }

        public void AddJob(PacketEventArgs item)
        {
            int randomGet = rnd.Next(0, container.Count);
            container[randomGet].Add(item);
        }

        public void AddJob(int workerPoolIndex, PacketEventArgs item)
        {
            container[workerPoolIndex].Add(item);
        }

        public PacketEventArgs GetJob()
        { 
            int randomGet = rnd.Next(0, container.Count);

            if (container[randomGet].Count > 0)
            {
                currentUsedWorker = container[randomGet];
                return container[randomGet].Get();
            }
            else
            {
                HashSet<int> bags = new HashSet<int>();
                bags.Add(randomGet);
                currentUsedWorker = container[randomGet];
                PacketEventArgs job = container[randomGet].Get();
                if (job == null)
                {
                    while(true)
                    {
                        if (bags.Count >= container.Count)
                        {
                            return null;
                        }
                        else
                        {
                            randomGet = rnd.Next(0, container.Count);
                            if (!bags.Contains(randomGet))
                            {
                                bags.Add(randomGet);
                                job = container[randomGet].Get();
                                if (job != null)
                                {
                                    currentUsedWorker = container[randomGet];
                                    return job;
                                }
                                else
                                {
                                    return null;
                                }
                            }
                        }
                    }
                }
                else
                {
                    return job;
                } 
            }
        }

        public void Clear()
        {
            foreach(IQueue<PacketEventArgs> worker in container)
            {
                worker.Clear();
            }
        }

        public int Count
        {
            get
            {
                return container.Count;
            }
        }

    }
}
