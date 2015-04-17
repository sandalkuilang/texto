using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSGatewayWpf.ViewModels.Configuration.Client
{
    public class GatewaySettings : ConfigurationSection, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        [ConfigurationProperty("ipAddress")]
        public string IPAddress
        {
            get
            {
                return (string)this["ipAddress"];
            }
            set
            {
                this["ipAddress"] = value;
                OnPropertyChanged("IPAddress");
            }
        }

        [ConfigurationProperty("port")] 
        public int Port
        {
            get
            {
                return (int)this["port"];
            }
            set
            {
                this["port"] = value;
                OnPropertyChanged("Port");
            }
        }
    }
}
