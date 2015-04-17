using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSMServer.Worker
{
    internal class WorkItem
    {
        public int SeqNbr { get; set; }
        public string Command { get; set; }
    }
}
