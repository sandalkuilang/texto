using GSMClient;
using SMSGatewayWpf.Core.Gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSGatewayWpf.Core.Gateway
{
    public class DbService : BaseGatewayService
    {
        public override BaseGatewayConnection Connection { get; set; }

        public DbService(string connectionString, string providerName)
        {
            if (string.IsNullOrEmpty(providerName))
            {
                throw new ArgumentNullException("providerName");
            }
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("connectionString");
            }

            gsmCommand = new DbCommand(connectionString, providerName);
            Connection = new DbConnection(gsmCommand); 
        }

        public override string Execute(string command)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(command))
            {
                Connection.Connect();
                Request.QueueWorkItem.Command = command;
                Request.QueueWorkItem.Enabled = true;

                gsmCommand.Request = Request;
                result = gsmCommand.Write();
            }
            Connection.Disconnect();
            return result;
        }

        public override string Execute(GSMServerModel.Request request)
        {
            string result = string.Empty;
            if (request != null)
            {
                Connection.Connect();
                gsmCommand.Request = request;
                result = gsmCommand.Write();
            }
            Connection.Disconnect();
            return result;
        }
    }
}
 