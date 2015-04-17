using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSGatewayWpf.ViewModels.Message
{
    public class Queue : BaseBindableModel
    {

        private string id;
        public string ID
        {
            get
            {
                return id;
            }
            set
            {
                NotifyIfChanged(ref id, value);
            }
        }

        private string command;
        public string Command
        {
            get
            {
                return command;
            }
            set
            {
                NotifyIfChanged(ref command, value);
            }
        }

        private DateTime created;
        public DateTime Created
        {
            get
            {
                return created;
            }
            set
            {
                NotifyIfChanged(ref created, value);
            }
        }

        private DateTime lastExecuted;
        public DateTime LastExecuted
        {
            get
            {
                return lastExecuted;
            }
            set
            {
                NotifyIfChanged(ref lastExecuted, value);
            }
        }

        private DateTime nextExecuted;
        public DateTime NextExecuted
        {
            get
            {
                return nextExecuted;
            }
            set
            {
                NotifyIfChanged(ref nextExecuted, value);
            }
        }
         
    }
}
