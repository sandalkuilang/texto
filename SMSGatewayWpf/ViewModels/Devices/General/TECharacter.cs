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
    public class TECharacter : BaseBindableModel
    {
        public ICommand TECharacterCommand { get; set; }
        public string Value { get; set; }
        public TECharacter()
        {
            TECharacterCommand = new DelegateCommand(new Action(OnGetTECharacter));
        }

        private void OnGetTECharacter()
        {
            Task.Factory.StartNew(() => 
            {
                IGatewayService service = GatewayManager.Instance.GetService();
                IsBusy = true;
                if (string.IsNullOrEmpty(Value))
                {
                    Response = service.Execute(GSMClient.Command.CommandCollection.GeneralGetCharacterSet);
                }
                else
                {
                    Response = service.Execute(string.Format(GSMClient.Command.CommandCollection.GeneralSetCharacterSet, Value));
                }
                IsBusy = false;
            });
        }
         
    }
}
