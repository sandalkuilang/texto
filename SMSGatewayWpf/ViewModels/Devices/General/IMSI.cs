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
    public class IMSI : BaseBindableModel
    {
        public ICommand IMSICommand { get; set; }
        public IMSI()
        {
            IMSICommand = new DelegateCommand(new Action(OnGetIMSI));
        }

        private void OnGetIMSI()
        {
            Task.Factory.StartNew(() => 
            {
                IGatewayService service = GatewayManager.Instance.GetService();
                IsBusy = true;
                Response = service.Execute(GSMClient.Command.CommandCollection.GeneralGetIMSI);
                IsBusy = false;
            });
        }
    }
}
