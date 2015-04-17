using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSGatewayWpf.ViewModels.Message
{
    public class Outbox : BaseMessageModel
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

        private string senderOperator;
        public string SenderOperator
        {
            get
            {
                return senderOperator;
            }
            set
            {
                NotifyIfChanged(ref senderOperator, value);
            }
        }
    }
}
