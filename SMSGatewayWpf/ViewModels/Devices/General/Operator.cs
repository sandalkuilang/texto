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
    public class Operator : BaseBindableModel
    {
        public ICommand OperatorCommand { get; set; }
        public Operator()
        {
            OperatorCommand = new DelegateCommand(new Action(OnGetOperator));
        }

        private void OnGetOperator()
        {
            Task.Factory.StartNew(() => 
            {
                IGatewayService service = GatewayManager.Instance.GetService();
                IsBusy = true;
                Response = service.Execute(GSMClient.Command.CommandCollection.GeneralGetOperator);
                IsBusy = false;
            });
        }
    }
}
