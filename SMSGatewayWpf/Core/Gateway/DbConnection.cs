using GSMClient;
using SMSGatewayWpf.Core.Gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSGatewayWpf.Core.Gateway
{
    public class DbConnection : BaseGatewayConnection
    {

        private string connectionString;
        public string ConnectionString
        {
            get
            {
                return connectionString;
            }
            set
            {
                connectionString = value;
                ((GSMClient.DbConnection)gsmCommand.Connection).ConnectionString = value;
                NotifyPropertyChanged("ConnectionString");
            }
        }

        private string providerName;
        public string ProviderName
        {
            get
            {
                return providerName;
            }
            set
            {
                providerName = value;
                ((GSMClient.DbConnection)gsmCommand.Connection).ProviderName = value; 
                NotifyPropertyChanged("ProviderName");
            }
        }

        public DbConnection(BaseGSMCommand gsmCommand)
            : base(gsmCommand)
        { 

        }
         
    }
}
