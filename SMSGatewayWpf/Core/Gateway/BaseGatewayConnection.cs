using GSMClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SMSGatewayWpf.Core.Gateway
{
    public abstract class BaseGatewayConnection : IGatewayConnection, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected BaseGSMCommand gsmCommand;  

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public BaseGatewayConnection(BaseGSMCommand gsmCommand)
        {
            this.gsmCommand = gsmCommand;
        }
         

        private bool isConnected;
        public bool Connected
        {
            get { return isConnected; }
        }


        /// <summary>
        /// Start connecting to gateway sever
        /// </summary>
        /// <param name="ipAddress">the specified local IP address</param>
        /// <param name="port">the port number
        public virtual bool Connect()
        {
            try
            {
                gsmCommand.Connection.Open();
                isConnected = true;
                return true;
            }
            catch (System.Net.Sockets.SocketException)
            {
                isConnected = false;
                return false;
            }
        }


        /// <summary>
        /// Disconnect from gateway server
        /// </summary>
        public virtual bool Disconnect()
        {
            gsmCommand.Connection.Close();
            isConnected = false;
            return true;
        }
         
         
    }
}
