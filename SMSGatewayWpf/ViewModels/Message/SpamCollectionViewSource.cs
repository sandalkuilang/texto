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
    public class SpamCollectionViewSource : BaseMessageViewModel<Spam>
    {
        private MutableObservableCollection<Spam> spam;
        public override MutableObservableCollection<Spam> Source
        {
            get
            {
                return spam;
            }
            set
            {
                NotifyIfChanged(ref spam, value);
            }
        }

        public SpamCollectionViewSource()
        {
            Source = new MutableObservableCollection<Spam>(); 
        }
         
    }
}
