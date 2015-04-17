using SMSGatewayWpf.Core;
using SMSGatewayWpf.ViewModels.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSGatewayWpf.ViewModels.Contact
{
    public class ContactMessageBar : IMessageCommandBar
    {
        public System.Windows.Input.ICommand ComposeCommand { get; set; }

        public ContactMessageBar()
        {
            ComposeCommand = new DelegateCommand<object>(new Action<object>(OnCompose));
        }

        public void OnCompose(object args)
        {
            ComposeNewContactModel newMessage = new ComposeNewContactModel();
            DialogService.Instance.ShowDialog<Views.Dialogs.ComposeContact>(newMessage);
            ObjectPool.Instance.Resolve<DatabaseCollectionViewSource>().StartSyncronizing();
        }
    }
}
