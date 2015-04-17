using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSGatewayWpf.ViewModels.Configuration.Client
{
    public class DatabaseSettings : ConfigurationSection, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        [ConfigurationProperty("server")]
        public string Server
        {
            get
            {
                return (string)this["server"];
            }
            set
            {
                this["server"] = value;
                OnPropertyChanged("Server");
            }
        }

        [ConfigurationProperty("name")]
        public string Name
        {
            get
            {
                return (string)this["name"];
            }
            set
            {
                this["name"] = value;
                OnPropertyChanged("Name");
            }
        }


        [ConfigurationProperty("userName")]
        public string Username
        {
            get
            {
                return (string)this["userName"];
            }
            set
            {
                this["userName"] = value;
                OnPropertyChanged("Username");
            }
        }

        [ConfigurationProperty("password")]
        public string Password
        {
            get
            {
                return (string)this["password"];
            }
            set
            {
                this["password"] = value;
                OnPropertyChanged("Password");
            }
        }

        [ConfigurationProperty("providerName")]
        public string ProviderName
        {
            get
            {
                return (string)this["providerName"];
            }
            set
            {
                this["providerName"] = value;
                OnPropertyChanged("ProviderName");
            }
        }

    }
}
