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
    public class TrashCollectionViewSource : BaseMessageViewModel<Trash>
    {
        private MutableObservableCollection<Trash> trash;
        public override MutableObservableCollection<Trash> Source
        {
            get
            {
                return trash;
            }
            set
            {
                NotifyIfChanged(ref trash, value);
            }
        }

        public TrashCollectionViewSource()
        {
            Source = new MutableObservableCollection<Trash>(); 
        }
         

    }
}
