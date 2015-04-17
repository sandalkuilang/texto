using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SMSGatewayWpf.ViewModels.Message
{
    public interface IMessageCommandBar
    {
        ICommand ComposeCommand { get; set; }
        void OnCompose(object args);
    }
}
