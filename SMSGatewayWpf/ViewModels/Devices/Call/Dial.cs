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
    public class Dial : BaseBindableModel
    {
        public ICommand DialCommand { get; set; }

        private string phoneNumber;

        public string PhoneNumber
        {
            get
            {
                return phoneNumber;
            }
            set
            {
                NotifyIfChanged(ref this.phoneNumber, value);
            }
        }

        public Dial()
        {
            DialCommand = new DelegateCommand(new Action(OnDial));
            IsEnabled = false;
        }

        public void OnDial()
        {
            Task.Factory.StartNew(() =>
            {
                IGatewayService service = GatewayManager.Instance.GetService();
                IsBusy = true;
                Response = service.Execute(string.Format(GSMClient.Command.CommandCollection.CallDial, phoneNumber));
                IsBusy = false;
            });
        }
         
        public override string this[string columnName]
        {
            get
            {
                if (columnName.Equals("PhoneNumber") && (string.IsNullOrEmpty(PhoneNumber)))
                {
                    return "You must enter phone number";
                }
                if (!string.IsNullOrEmpty(PhoneNumber))
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
