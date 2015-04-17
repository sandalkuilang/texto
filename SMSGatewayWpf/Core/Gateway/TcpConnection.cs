using Cryptography;
using GSMClient;
using SMSGatewayWpf.Core.Gateway;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SMSGatewayWpf.Core.Gateway
{
    public class TcpConnection : BaseGatewayConnection
    { 

        private string ipAddress;
        public string IPAddress
        {
            get
            {
                return this.ipAddress;
            }
            set
            {
                this.ipAddress = value;
                ((GSMClient.TcpConnection)gsmCommand.Connection).HostName = value;
                NotifyPropertyChanged("IPAddress");
            }
        }

        private int port;
        public int Port
        {
            get
            {
                return this.port;
            }
            set
            {
                this.port = value;
                ((GSMClient.TcpConnection)gsmCommand.Connection).Port = value; 
                NotifyPropertyChanged("Port");
            }
        } 

        public TcpConnection(BaseGSMCommand gsmCommand)
            : base(gsmCommand)
        {
        
        } 
         
    }
}
