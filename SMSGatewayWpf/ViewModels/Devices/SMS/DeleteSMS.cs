using SMSGatewayWpf.Core;
using SMSGatewayWpf.Core.Gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SMSGatewayWpf.ViewModels.Devices.SMS
{
    public class DeleteSMS : BaseBindableModel
    {
        public ICommand DeleteCommand { get; set; }
         
        public DeleteSMS()
        {
            DeleteCommand = new DelegateCommand(new System.Action(OnDelete)); 
        }

        private void OnDelete()
        {
            Task.Factory.StartNew(() => 
            {
                IGatewayService service = GatewayManager.Instance.GetService();
                IsBusy = false;
                Response = service.Execute(GSMClient.Command.CommandCollection.SMSDelete);
                IsEnabled = true;
            });
        }

    }
}
