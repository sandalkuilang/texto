using SMSGatewayWpf.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SMSGatewayWpf.ViewModels.Contact
{
    public class LookupContactSource : DatabaseCollectionViewSource
    {
        public ICommand OkCommand { get; set; }

        public LookupContactSource()
        {
            OkCommand = new DelegateCommand<object>(new Action<object>(OnOk));
            StartSyncronizing();
        }

        private void OnOk(object args)
        {

        }
    }
}
