using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SMSGatewayWpf.ViewModels.Message
{
    public abstract class BaseMessageModel : BaseDataRow
    {

        private string sender;
        public string Sender
        {
            get
            {
                return sender;
            }
            set
            {
                NotifyIfChanged(ref sender, value);
            }
        }

        private string from;
        public string From
        {
            get
            {
                return from;
            }
            set
            {
                NotifyIfChanged(ref from, value);
            }
        }

        private string seqNbr;
        public string SeqNbr
        {
            get
            {
                return seqNbr;
            }
            set
            {
                NotifyIfChanged(ref seqNbr, value);
            }
        }

        private string message;
        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                NotifyIfChanged(ref message, value);
            }
        }


        private DateTime time;
        public DateTime Time
        {
            get
            {
                return time;
            }
            set
            {
                NotifyIfChanged(ref time, value);
            }
        }

    }
}
