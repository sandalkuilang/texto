using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace GSMServerModel
{
    [DataContract]
    public class WeeklyTrigger : BaseTrigger
    {
        [DataMember]
        public int RecursEvery { get; set; }
        [DataMember]
        public int Sunday { get; set; }
        [DataMember]
        public int Monday { get; set; }
        [DataMember]
        public int Tuesday { get; set; }
        [DataMember]
        public int Wednesday { get; set; }
        [DataMember]
        public int Thursday { get; set; }
        [DataMember]
        public int Friday { get; set; }
        [DataMember]
        public int Saturday { get; set; }
    }
}
