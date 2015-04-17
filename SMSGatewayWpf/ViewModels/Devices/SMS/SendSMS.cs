using SMSGatewayWpf.Core;
using SMSGatewayWpf.Core.Gateway;
using SMSGatewayWpf.ViewModels.Configuration.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SMSGatewayWpf.ViewModels.Devices.SMS 
{
    public class SendSMS : BaseBindableModel, IDataErrorInfo
    {
        public ICommand SendCommand { get; set; } 
         
        public string PhoneNumber { get; set; }
         
        public string Message { get; set; } 

        private void OnSendSMS()
        { 
            Task.Factory.StartNew(() =>
            {
                IGatewayService service = GatewayManager.Instance.GetService();
                
                this.IsBusy = true;
                ApplicationSettings settings = ObjectPool.Instance.Resolve<ApplicationSettings>();
                if (settings.General.Sounds)
                {
                    IAudioService audio = ObjectPool.Instance.Resolve<IAudioService>();
                    audio.Play(AudioEnum.SendMessage);
                }
                Response = service.Execute(string.Format(GSMClient.Command.CommandCollection.SMSSend, PhoneNumber, Message));
                this.IsBusy = false;

            });
        }

        public SendSMS()
        {
            SendCommand = new DelegateCommand(new System.Action(OnSendSMS));
            IsEnabled = false;
        }
          
        public override string this[string columnName]
        {
            get
            {
                switch(columnName)
                {
                    case "PhoneNumber":
                        if (string.IsNullOrEmpty(PhoneNumber))
                            return "Phone number cannot be empty";
                        break;
                    case "Message":
                        if (string.IsNullOrEmpty(PhoneNumber))
                            return "You must enter Message";
                        break;
                }
                if (!string.IsNullOrEmpty(PhoneNumber) && (!string.IsNullOrEmpty(Message)))
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
