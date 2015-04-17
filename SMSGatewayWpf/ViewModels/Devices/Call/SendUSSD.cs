using SMSGatewayWpf.Core;
using SMSGatewayWpf.Core.Gateway;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SMSGatewayWpf.ViewModels.Devices
{
    public class SendUSSD : BaseBindableModel, IDataErrorInfo
    {
        public ICommand SendUSSDCommand { get; set; }

        private string ussdCode;

        public string USSD
        {
            get
            {
                return ussdCode;
            }
            set
            {
                NotifyIfChanged(ref this.ussdCode, value);
            }
        }

        public SendUSSD()
        {
            SendUSSDCommand = new DelegateCommand(new Action(OnSendUSSD));
            IsEnabled = false; 
        }

        private void OnSendUSSD()
        {
            Task.Factory.StartNew(() =>
            {
                IGatewayService service = GatewayManager.Instance.GetService();
                IsBusy = true;
                Response = service.Execute(string.Format(GSMClient.Command.CommandCollection.CallSendUSSD, ussdCode));
                IsBusy = false;
            });
        }
         
        public override string this[string columnName]
        {
            get
            {
                if (columnName.Equals("USSD") && string.IsNullOrEmpty(USSD))
                {
                    return "You must enter USSD code";
                }
                if (!string.IsNullOrEmpty(USSD))
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
