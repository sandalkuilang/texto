using SMSGatewayWpf.Core;
using SMSGatewayWpf.Core.Gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SMSGatewayWpf.ViewModels.Devices
{
    public class Hangup : BaseBindableModel
    {
        public ICommand HangupCommand { get; set; }

        public Hangup()
        {
            HangupCommand = new DelegateCommand(new Action(OnHangup));
        }

        public void OnHangup()
        {
            Task.Factory.StartNew(() =>
            {
                IGatewayService service = GatewayManager.Instance.GetService();
                IsBusy = true;
                Response = service.Execute(GSMClient.Command.CommandCollection.CallHang);
                IsBusy = false;
            });
        }
         
    }
}
