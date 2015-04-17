using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSGatewayWpf.ViewModels.Configuration.Client
{
    public class AudioSettings : ConfigurationSection, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        [ConfigurationProperty("sendMessage")]
        public string SendMessage
        {
            get
            {
                return (string)this["sendMessage"];
            }
            set
            {
                this["sendMessage"] = value;
                OnPropertyChanged("Send");
            }
        }

        [ConfigurationProperty("receiveMessage")]
        public string ReceiveMessage
        {
            get
            {
                return (string)this["receiveMessage"];
            }
            set
            {
                this["receiveMessage"] = value;
                OnPropertyChanged("Receive");
            }
        }

        [ConfigurationProperty("call")]
        public string Call
        {
            get
            {
                return (string)this["call"];
            }
            set
            {
                this["call"] = value;
                OnPropertyChanged("Call");
            }
        }

        [ConfigurationProperty("hangup")]
        public string Hangup
        {
            get
            {
                return (string)this["hangup"];
            }
            set
            {
                this["hangup"] = value;
                OnPropertyChanged("Hangup");
            }
        }

        [ConfigurationProperty("incomingCall")]
        public string IncomingCall
        {
            get
            {
                return (string)this["incomingCall"];
            }
            set
            {
                this["incomingCall"] = value;
                OnPropertyChanged("IncomingCall");
            }
        }

    }
}
