using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSMServer.Worker.Model
{
    public  class QueueWorkItem
    {   
        public string SeqNbr { get; set; } 
        public string Command { get; set; } 
        public string ScheduleID { get; set; } 
        public DateTime Created { get; set; } 
        public bool Enabled { get; set; }
        public DateTime RecursPoint { get; set; }
        public DateTime LastExecuted { get; set; }
        public DateTime NextExecuted { get; set; }
        public string Status{ get; set; }
    }
}
