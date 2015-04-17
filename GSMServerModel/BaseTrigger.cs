using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace GSMServerModel
{
    [DataContract]
    public abstract class BaseTrigger
    {
        public BaseTrigger() { }
        [DataMember]
        public string ID { get; set; }

    }
}
