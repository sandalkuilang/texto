using Crypto;
using GSMClient;
using GSMServerModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSGatewayWpf.Core.Gateway
{
    public abstract class BaseGatewayService : IGatewayService
    {
        protected BaseGSMCommand gsmCommand;
        protected Crypter crypter;
        protected Header header;

        public Request Request { get; set; }

        public BaseGatewayService()
        {
            string signature = ConfigurationManager.AppSettings["Signature"];
            header = new Header(signature, "texto-app", "");
            Request = new Request(null, header, null);
            Request.QueueWorkItem = new QueueWorkItem();

            crypter = new Crypto.Crypter(new ApplicationSettingKeySym());
        }

        public abstract BaseGatewayConnection Connection { get; set; }

        public abstract string Execute(string command);

        public abstract string Execute(Request request);
    }
}
