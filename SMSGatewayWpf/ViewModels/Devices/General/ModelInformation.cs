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
    public class ModelInformation : BaseBindableModel
    {
        public ICommand ModelInformationCommand { get; set; }
        public ModelInformation()
        {
            ModelInformationCommand = new DelegateCommand(new Action(OnGetModelInformation));
        }

        private void OnGetModelInformation()
        {
            Task.Factory.StartNew(() => 
            {
                IGatewayService service = GatewayManager.Instance.GetService();
                IsBusy = true;
                Response = service.Execute(GSMClient.Command.CommandCollection.GeneralGetModelInformation);
                IsBusy = false;
            });
        }
    }
}
