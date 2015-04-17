using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace GSMServerModel
{
    [DataContract]
    public class QueueWorkItem
    {
        [DataMember]
        public string SeqNbr { get; set; }
        [DataMember]
        public string Command { get; set; } 
        [DataMember]
        public BaseTrigger Schedule { get; set; }
        [DataMember]
        public DateTime Created { get; set; } 
        [DataMember]
        public bool Enabled { get; set; }

        [DataMember]
        public DateTime RecursPoint { get; set; }
        [DataMember]
        public DateTime LastExecuted { get; set; }
        [DataMember]
        public DateTime NextExecuted{ get; set; }
        [DataMember]
        public string Status { get; set; }
    }
}
