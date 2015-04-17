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
    public class ServiceCenter : BaseBindableModel
    {
        public ICommand ServiceCenterCommand { get; set; }
        public ServiceCenter()
        {
            ServiceCenterCommand = new DelegateCommand(new Action(OnGetServiceCenter));
        }

        private void OnGetServiceCenter()
        {
            Task.Factory.StartNew(() => 
            {
                IGatewayService service = GatewayManager.Instance.GetService();
                IsBusy = true;
                Response = service.Execute(GSMClient.Command.CommandCollection.GeneralGetServiceCenter);
                IsBusy = false;
            });
        }
    }
}
