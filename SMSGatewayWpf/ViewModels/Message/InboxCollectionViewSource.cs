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
    public class InboxCollectionViewSource : BaseMessageViewModel<Inbox>
    {
        private MutableObservableCollection<Inbox> inbox;
        public override MutableObservableCollection<Inbox> Source
        {
            get
            {
                return inbox;
            }
            set
            {
                NotifyIfChanged(ref inbox, value);
            }
        }
         
        public InboxCollectionViewSource()
        {
            Source = new MutableObservableCollection<Inbox>();
        }

    }
}
