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
    public class ActivityStatus : BaseBindableModel
    {
        public ICommand ActivityStatusCommand { get; set; }
        public ActivityStatus()
        {
            ActivityStatusCommand = new DelegateCommand(new Action(OnGetActivityStatus));
        }

        private void OnGetActivityStatus()
        {
            Task.Factory.StartNew(() => 
            {
                IGatewayService service = GatewayManager.Instance.GetService();
                IsBusy = true;
                Response = service.Execute(GSMClient.Command.CommandCollection.GeneralGetActivityStatus);
                IsBusy = false;
            });
        }

    }
}
