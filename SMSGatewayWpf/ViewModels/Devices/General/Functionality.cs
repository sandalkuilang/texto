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
    public class Functionality : BaseBindableModel
    {
        public ICommand FunctionalityCommand { get; set; }
        public string Value { get; set; }
        public Functionality()
        {
            FunctionalityCommand = new DelegateCommand(new Action(OnSetFunctionality));
        }

        private void OnSetFunctionality()
        {
            Task.Factory.StartNew(() => 
            {
                IGatewayService service = GatewayManager.Instance.GetService();
                IsBusy = true;
                Response = service.Execute(string.Format(GSMClient.Command.CommandCollection.GeneralSetFunctionality, Value));
                IsBusy = false;
            });
        }

        public override string this[string columnName]
        {
            get
            {
                if (columnName.Equals("Value") && (string.IsNullOrEmpty(Value)))
                {
                    return "You must enter functionality value, integer";
                }
                if (!string.IsNullOrEmpty(Value))
                {
                    IsEnabled = true;
                }
                else
                {
                    IsEnabled = false;
                }
                return string.Empty;
            }
        } 
    }
}
