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
    public class HardwareVersion : BaseBindableModel
    {
        public ICommand HardwareVersionCommand { get; set; }
        public HardwareVersion()
        {
            HardwareVersionCommand = new DelegateCommand(new Action(OnGetHWVersion));
        }

        private void OnGetHWVersion()
        {
            Task.Factory.StartNew(() => 
            {
                IGatewayService service = GatewayManager.Instance.GetService();
                IsBusy = true;
                Response = service.Execute(GSMClient.Command.CommandCollection.GeneralGetHardwareVersion);
                IsBusy = false;
            });
        }

    }
}
