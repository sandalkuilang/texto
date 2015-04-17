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
    public class ReadSMS : BaseBindableModel
    {
        public ICommand ReadCommand { get; set; }

        public ReadSMS()
        {
            ReadCommand = new DelegateCommand(new Action(OnRead));
        }

        private void OnRead()
        {
            Task.Factory.StartNew(() =>
            {
                IGatewayService service = GatewayManager.Instance.GetService();
                this.IsBusy = true;
                this.IsEnabled = false;
                Response = service.Execute(GSMClient.Command.CommandCollection.SMSRead);
                this.IsEnabled = true;
                this.IsBusy = false;
            });
        }
         
    }
}
