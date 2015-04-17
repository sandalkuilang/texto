using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSGatewayWpf.ViewModels.Message
{
    public class QueueWork
    {
        public string Source { get; set; }
        public string SeqNbr { get; set; }
        public string Command { get; set; }
        public string ScheduleID { get; set; }
        public DateTime Created { get; set; }
        public bool Enabled { get; set; }
        public DateTime RecursPoint { get; set; }
        public DateTime LastExecuted { get; set; }
        public DateTime NextExecuted { get; set; }
        public string Status { get; set; }
    }
}
