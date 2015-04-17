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
    public class DeleteAllSMS : BaseBindableModel
    {
        public ICommand DeleteAllCommand { get; set; }

        public DeleteAllSMS()
        {
            DeleteAllCommand = new DelegateCommand(new System.Action(OnDeleteAll)); 
        }

        private void OnDeleteAll()
        {
            Task.Factory.StartNew(() =>
            {
                IGatewayService service = GatewayManager.Instance.GetService();
                IsBusy = true;
                Response = service.Execute(GSMClient.Command.CommandCollection.SMSDeleteAll);
                IsBusy = false;
            });
        }

    }
}
