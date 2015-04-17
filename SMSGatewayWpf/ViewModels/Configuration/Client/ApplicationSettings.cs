using SMSGatewayWpf.Core;
using SMSGatewayWpf.Core.Gateway;
using SMSGatewayWpf.ViewModels.Configuration.Server;
using SMSGatewayWpf.ViewModels.Contact;
using SMSGatewayWpf.ViewModels.Devices;
using SMSGatewayWpf.ViewModels.Message;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Texaco.Database;

namespace SMSGatewayWpf.ViewModels.Configuration.Client
{
    public class ApplicationSettings 
    {
        
        public GeneralSettings General { get; set; }
        public EncryptionSettings Encryption { get; set; }
        public DatabaseSettings Database { get; set; }
        public GatewaySettings Gateway { get; set; }
        public AudioSettings Audio { get; set; }
        public ApplicationBindingCommands Commands { get; set; }

        public System.Configuration.Configuration Configuration { get; private set; }

        public event EventHandler ConfigurationLoaded;
        public ApplicationSettings()
        {
            Commands = new ApplicationBindingCommands(); 
            Commands.SaveCommand = new DelegateCommand(new Action(Save));
            Commands.RouteConnectionCommand = new DelegateCommand(new Action(RouteConnection)); 
            Commands.ButtonContent = "Connect";
            Commands.IsEnabled = true;

            Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            General = (SMSGatewayWpf.ViewModels.Configuration.Client.GeneralSettings)Configuration.Sections["generalSettings"];
            Encryption = (SMSGatewayWpf.ViewModels.Configuration.Client.EncryptionSettings)Configuration.Sections["encryptionSettings"];
            Gateway = (SMSGatewayWpf.ViewModels.Configuration.Client.GatewaySettings)Configuration.Sections["gatewaySettings"];
            Database = (SMSGatewayWpf.ViewModels.Configuration.Client.DatabaseSettings)Configuration.Sections["databaseSettings"];
            Audio = (SMSGatewayWpf.ViewModels.Configuration.Client.AudioSettings)Configuration.Sections["audioSettings"]; 
        }

        public void Save()
        {
            Configuration.Save(ConfigurationSaveMode.Full);
        }

        public void Connect()
        {
            Task.Factory.StartNew(() =>
            {
                Commands.IsBusy = true;
                IGatewayService service = GatewayManager.Instance.GetService();
                if (service != null)
                {
                    if (!service.Connection.Connected)
                    {
                        ApplicationSettings settings = ObjectPool.Instance.Resolve<ApplicationSettings>(); 
                        ((TcpConnection)service.Connection).IPAddress = settings.Gateway.IPAddress;
                        ((TcpConnection)service.Connection).Port = settings.Gateway.Port;

                        if (service.Connection.Connect())
                        {
                            if (LoadServerConfiguration(service))
                                Commands.ButtonContent = "Disconnect";
                        }
                    }
                }
                Commands.IsBusy = false;
            }).Wait();
        }

        public void RouteConnection()
        {
            Task.Factory.StartNew(() =>
            {
                Commands.IsBusy = true;
                IGatewayService service = GatewayManager.Instance.GetService();
                if (service != null)
                {
                    if (!service.Connection.Connected)
                    {
                        ApplicationSettings settings = ObjectPool.Instance.Resolve<ApplicationSettings>();
                        ((TcpConnection)service.Connection).IPAddress = settings.Gateway.IPAddress;
                        ((TcpConnection)service.Connection).Port = settings.Gateway.Port;
                        if (service.Connection.Connect())
                        {
                            if (LoadServerConfiguration(service))
                                Commands.ButtonContent = "Disconnect";
                        }
                    }
                    else
                    {
                        if (service.Connection.Disconnect())
                        {
                            /// Unregister all, excepts IDbManager, IAudioService, IDialogService, ApplicationSettings
                            MessageCollaborator message = ObjectPool.Instance.Resolve<MessageCollaborator>();
                            message.StopSyncronizing();

                            ObjectPool.Instance.Unregister<SMSView>();
                            ObjectPool.Instance.Unregister<CallView>();
                            ObjectPool.Instance.Unregister<PortView>();
                            ObjectPool.Instance.Unregister<GeneralView>();

                            ObjectPool.Instance.Unregister<ServerConfiguration>();
                            ObjectPool.Instance.Unregister<MessageCollaborator>();
                            ObjectPool.Instance.Unregister<DatabaseCollectionViewSource>();  

                            Commands.ButtonContent = "Connect";
                        }
                    }
                }
                Commands.IsBusy = false;
            });
        }

        public void Disconnect()
        {
            Task.Factory.StartNew(() =>
            {
                Commands.IsBusy = true;
                IGatewayService smsService = GatewayManager.Instance.GetService();
                if (smsService != null)
                {
                    if (smsService.Connection.Connected)
                    { 
                        if (smsService.Connection.Disconnect())
                        {
                            Commands.ButtonContent = "Connect";
                        }
                    }
                }
                Commands.IsBusy = false;
            });
        }

        internal bool LoadServerConfiguration(IGatewayService service)
        {
            Server.ServerConfiguration serverConfiguration;
            IDataSyncronize message;
            IDataSyncronize contact;  
            
            serverConfiguration = ObjectPool.Instance.Resolve<Server.ServerConfiguration>();

            message = ObjectPool.Instance.Resolve<MessageCollaborator>();
            contact = ObjectPool.Instance.Resolve<DatabaseCollectionViewSource>();

            //if (serverConfiguration == null) // comment this if you want refresh connection
            {
                if (serverConfiguration == null)
                    serverConfiguration = new Server.ServerConfiguration(service);

                if (message == null)
                    message = new MessageCollaborator();

                if (contact == null)
                    contact = new DatabaseCollectionViewSource();

                message.StartSyncronizing();
                contact.StartSyncronizing();

                ObjectPool.Instance.Register<SMSGatewayWpf.ViewModels.Devices.SMSView>().ImplementedBy(new SMSGatewayWpf.ViewModels.Devices.SMSView());
                ObjectPool.Instance.Register<SMSGatewayWpf.ViewModels.Devices.CallView>().ImplementedBy(new SMSGatewayWpf.ViewModels.Devices.CallView());
                ObjectPool.Instance.Register<SMSGatewayWpf.ViewModels.Devices.PortView>().ImplementedBy(new SMSGatewayWpf.ViewModels.Devices.PortView());
                ObjectPool.Instance.Register<SMSGatewayWpf.ViewModels.Devices.GeneralView>().ImplementedBy(new SMSGatewayWpf.ViewModels.Devices.GeneralView());
            }

            ObjectPool.Instance.Register<Server.ServerConfiguration>().ImplementedBy(serverConfiguration);
            ObjectPool.Instance.Register<MessageCollaborator>().ImplementedBy(message);
            ObjectPool.Instance.Register<DatabaseCollectionViewSource>().ImplementedBy(contact);

            OnConfigurationLoaded();
                
            // comment due to disabled load configuration server
            //if (serverConfiguration.General == null)
            //    return false;
            //else
            return true;
        }


        private void OnConfigurationLoaded()
        {
            if (ConfigurationLoaded != null)
                ConfigurationLoaded(this, null);
        }
    }
}
