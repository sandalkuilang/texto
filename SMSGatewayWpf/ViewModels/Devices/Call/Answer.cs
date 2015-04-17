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
    public class Answer : BaseBindableModel
    {
        public ICommand AnswerCommand { get; set; }

        public Answer()
        {
            AnswerCommand = new DelegateCommand(new Action(OnAnswer));
        }

        private void OnAnswer()
        {
            Task.Factory.StartNew(() => 
            {
                IGatewayService service = GatewayManager.Instance.GetService();
                IsBusy = true;
                Response = service.Execute(GSMClient.Command.CommandCollection.CallAnswer);
                IsBusy = false;
            });
        }
    }
}
