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
    public class Manufacturer : BaseBindableModel
    { 
        public ICommand ManufacturerCommand { get; set; }
        public Manufacturer()
        {
            ManufacturerCommand = new DelegateCommand(new Action(OnGetManufacturer));
        }

        private void OnGetManufacturer()
        {
            Task.Factory.StartNew(() => 
            {
                IGatewayService service = GatewayManager.Instance.GetService();
                IsBusy = true;
                Response = service.Execute(GSMClient.Command.CommandCollection.GeneralGetManufacturer);
                IsBusy = false;
            });
        }
    }
}
