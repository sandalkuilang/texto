using SMSGatewayWpf.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SMSGatewayWpf.ViewModels.Message
{
    public class Inbox : BaseMessageModel
    {

        private string status;
        public string Status
        {
            get { return status; }
            set { NotifyIfChanged(ref this.status, value); }
        }

        private int isRead;
        public int IsRead
        {
            get { return isRead; }
            set { NotifyIfChanged(ref this.isRead, value); }
        }
        
    }
}
