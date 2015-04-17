using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace GSMServerModel
{
    [DataContract]
    public class DailyTrigger : BaseTrigger
    {
        [DataMember]
        public int RecursEvery { get; set; } 
    }
}
