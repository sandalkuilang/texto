using SMSGatewayWpf.Core;
using SMSGatewayWpf.Core.Gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSGatewayWpf.ViewModels.Devices
{
    public class PortView
    {
        public List<BasicInformation> Connections { get; set; }

        public PortView()
        {
            IGatewayService service = GatewayManager.Instance.GetService();
            string response;
            if (service != null)
            {
                response= service.Execute(GSMClient.Command.CommandCollection.ServerGetAvailableConnections);
                Connections = Newtonsoft.Json.JsonConvert.DeserializeObject<List<BasicInformation>>(response);
            }
        }
    }
}
