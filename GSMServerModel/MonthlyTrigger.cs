using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace GSMServerModel
{
    [DataContract]
    public class MonthlyTrigger : BaseTrigger
    { 
        [DataMember]
        public string Days { get; set; }
        [DataMember]
        public int January { get; set; } 
        [DataMember]
        public int February { get; set; }
        [DataMember]
        public int March { get; set; }
        [DataMember]
        public int April { get; set; }
        [DataMember]
        public int May { get; set; }
        [DataMember]
        public int June { get; set; }
        [DataMember]
        public int July { get; set; }
        [DataMember]
        public int August { get; set; }
        [DataMember]
        public int September { get; set; }
        [DataMember]
        public int October { get; set; }
        [DataMember]
        public int November { get; set; }
        [DataMember]
        public int December { get; set; }
    }
}
