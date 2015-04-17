using SMSGatewayWpf.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSGatewayWpf.ViewModels.Message
{
    public class OutboxCollectionViewSource : BaseMessageViewModel<Outbox>
    {
        private MutableObservableCollection<Outbox> outbox;
        public override MutableObservableCollection<Outbox> Source
        {
            get
            {
                return outbox;
            }
            set
            {
                NotifyIfChanged(ref outbox, value);
            }
        }

        public OutboxCollectionViewSource()
        {
            Source = new MutableObservableCollection<Outbox>(); 
        }
         
    }
}
