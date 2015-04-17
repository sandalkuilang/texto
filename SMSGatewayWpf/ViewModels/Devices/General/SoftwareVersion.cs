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
    public class SoftwareVersion : BaseBindableModel
    {
        public ICommand SoftwareVersionCommand { get; set; }
        public SoftwareVersion()
        {
            SoftwareVersionCommand = new DelegateCommand(new Action(OnGetSoftwareVersion));
        }

        private void OnGetSoftwareVersion()
        {
            Task.Factory.StartNew(() => 
            {
                IGatewayService service = GatewayManager.Instance.GetService();
                IsBusy = true;
                Response = service.Execute(GSMClient.Command.CommandCollection.GeneralGetSoftwareVersion);
                IsBusy = false;
            });
        }
    }
}
