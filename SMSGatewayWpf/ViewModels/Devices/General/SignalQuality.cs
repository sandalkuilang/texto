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
    public class SignalQuality : BaseBindableModel
    {
        public ICommand SignalQualityCommand { get; set; }
        public SignalQuality()
        {
            SignalQualityCommand = new DelegateCommand(new Action(OnGetSignalQuality));
        }

        private void OnGetSignalQuality()
        {
            Task.Factory.StartNew(() => 
            {
                IGatewayService service = GatewayManager.Instance.GetService();
                IsBusy = true;
                Response = service.Execute(GSMClient.Command.CommandCollection.GeneralGetSignalQuality);
                IsBusy = false;
            });
        }
    }
}
