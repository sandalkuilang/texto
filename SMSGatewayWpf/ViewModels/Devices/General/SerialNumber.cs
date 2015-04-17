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
    public class SerialNumber : BaseBindableModel
    {
        public ICommand SerialNumberCommand { get; set; }

        public SerialNumber()
        {
            SerialNumberCommand = new DelegateCommand(new Action(OnGetSerialNumber));
        }

        private void OnGetSerialNumber()
        {
            Task.Factory.StartNew(() => 
            {
                IGatewayService service = GatewayManager.Instance.GetService();
                IsBusy = true;
                Response = service.Execute(GSMClient.Command.CommandCollection.GeneralGetSerialNumber);
                IsBusy = false;
            });
        }
    }
}
