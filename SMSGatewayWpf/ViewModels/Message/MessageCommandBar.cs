using SMSGatewayWpf.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SMSGatewayWpf.ViewModels.Message
{
    public class MessageCommandBar : IMessageCommandBar
    {

        public ICommand ComposeCommand { get; set; }

        public MessageCommandBar()
        {
            ComposeCommand = new DelegateCommand<object>(new Action<object>(OnCompose));
        }
         
        public void OnCompose(object args)
        {
            ComposeMessageModel newMessage = new ComposeMessageModel(); 
            DialogService.Instance.ShowDialog<Views.Dialogs.ComposeMessage>(newMessage);
        }
    }
}
