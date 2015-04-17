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
    public class ReportME : BaseBindableModel
    {
        public ICommand ReportMECommand { get; set; }
        public string Value { get; set; }

        public ReportME()
        {
            ReportMECommand = new DelegateCommand(new Action(OnReportME));
        }

        private void OnReportME()
        {
            Task.Factory.StartNew(() => 
            {
                IGatewayService service = GatewayManager.Instance.GetService();
                IsBusy = true;
                if (string.IsNullOrEmpty(Value))
                {
                    Response = service.Execute(GSMClient.Command.CommandCollection.GeneralGetErrorMessageFormat);
                }
                else
                {
                    Response = service.Execute(string.Format(GSMClient.Command.CommandCollection.GeneralSetErrorMessageFormat, Value));
                }
                IsBusy = false;
            });
        }
         
    }
}
