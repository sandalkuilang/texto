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
    public class NetworkRegistration : BaseBindableModel
    {
        public ICommand NetworkRegistrationCommand { get; set; }
        public NetworkRegistration()
        {
            NetworkRegistrationCommand = new DelegateCommand(new Action(OnGetNetworkRegistration));
        }

        private void OnGetNetworkRegistration()
        {
            Task.Factory.StartNew(() => 
            {
                IGatewayService service = GatewayManager.Instance.GetService();
                IsBusy = true;
                Response = service.Execute(GSMClient.Command.CommandCollection.GeneralGetRegistrationStatus);
                IsBusy = false;
            });
        }
    }
}
