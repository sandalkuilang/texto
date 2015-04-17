using SMSGatewayWpf.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SMSGatewayWpf.ViewModels.Configuration.Client
{
    public class GeneralSettings : ConfigurationSection, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        [ConfigurationProperty("signature", DefaultValue = "")]
        public string Signature
        {
            get
            {
                return (string)this["signature"];
            }
            set
            {
                this["signature"] = value;
                OnPropertyChanged("Signature");
            }
        }

        [ConfigurationProperty("desktopNotifications", DefaultValue = "true")]
        public bool DesktopNotifications
        {
            get
            {
                return (bool)this["desktopNotifications"];
            }
            set
            {
                this["desktopNotifications"] = value;
                OnPropertyChanged("DesktopNotifications");
            }
        }

        [ConfigurationProperty("sounds", DefaultValue = "true")]
        public bool Sounds
        {
            get
            {
                return (bool)this["sounds"];
            }
            set
            {
                this["sounds"] = value;
                OnPropertyChanged("Sounds");
            }
        }

        [ConfigurationProperty("showTrayIcon", DefaultValue = "true")]
        public bool ShowTrayIcon
        {
            get
            {
                return (bool)this["showTrayIcon"];
            }
            set
            {
                this["showTrayIcon"] = value;
                OnPropertyChanged("ShowTrayIcon");
            }
     
        }

        [ConfigurationProperty("showTaskbarIcon", DefaultValue = "true")]
        public bool ShowTaskbarIcon
        {
            get
            {
                return (bool)this["showTaskbarIcon"];
            }
            set
            {
                Window mainWindow = ObjectPool.Instance.Resolve<Window>();
                if (mainWindow != null)
                    mainWindow.ShowInTaskbar = value;

                this["showTaskbarIcon"] = value;
                OnPropertyChanged("ShowTaskbarIcon");
            }
        }

        [ConfigurationProperty("launchWhenSystemStarts", DefaultValue = "true")]
        public bool LaunchWhenSystemStarts
        {
            get
            {
                return (bool)this["launchWhenSystemStarts"];
            }
            set
            {
                if (value)
                    StartUpManager.AddApplicationToCurrentUserStartup();
                else
                    StartUpManager.RemoveApplicationFromCurrentUserStartup();

                this["launchWhenSystemStarts"] = value;
                OnPropertyChanged("LaunchWhenSystemStarts");
            }
        }

        [ConfigurationProperty("launchMinimized", DefaultValue = "false")]
        public bool LaunchMinimized
        {
            get
            {
                return (bool)this["launchMinimized"];
            }
            set
            {
                this["launchMinimized"] = value;
                OnPropertyChanged("LaunchMinimized");
            }
        }
    }
}
