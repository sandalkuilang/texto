using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSGatewayWpf.ViewModels.Message
{
    public class Trash : BaseMessageModel
    {

        private string receiver;
        public string Receiver
        {
            get
            {
                return receiver;
            }
            set
            {
                NotifyIfChanged(ref receiver, value);
            }
        }

        private string to;
        public string To
        {
            get
            {
                return to;
            }
            set
            {
                NotifyIfChanged(ref to, value);
            }
        }

        private string senderReceiver;
        public string SenderOperator
        {
            get
            {
                return senderReceiver;
            }
            set
            {
                NotifyIfChanged(ref senderReceiver, value);
            }
        }

        private string source;
        public string Source
        {
            get
            {
                return source;
            }
            set
            {
                NotifyIfChanged(ref source, value);
            }
        }

        private int isRead;
        public int IsRead
        {
            get { return isRead; }
            set { NotifyIfChanged(ref this.isRead, value); }
        }
        
    }
}
