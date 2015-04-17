using SMSGatewayWpf.ViewModels.Configuration.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texaco.Database;
using Texaco.Database.SqlLoader;
using Texaco.Container;
using Texaco.Database.Petapoco;
using System.Threading;
using System.Reflection;
using SMSGatewayWpf.ViewModels.Message;
using SMSGatewayWpf.ViewModels;
using SMSGatewayWpf.Core.Gateway;
using System.Windows;

namespace SMSGatewayWpf.Core
{
    public class ApplicationStartup
    {
        ApplicationSettings applicationSettings = new ApplicationSettings();

        public void Run()
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            InitializeConfiguration();
            InitializeDatabase();

            // add your additional initialize below
            InitializeDialog();
            InitializeAudio(); 
        }

        private void InitializeAudio()
        {
            IAudioService audio = new DefaultAudioService();
            ApplicationSettings settings = ObjectPool.Instance.Resolve<ApplicationSettings>();
            
            if (!string.IsNullOrEmpty(settings.Audio.SendMessage))
            {
                audio.Register(new Audio() 
                { 
                    Url = string.Join("", System.AppDomain.CurrentDomain.BaseDirectory, settings.Audio.SendMessage),
                    Type = AudioEnum.SendMessage
                });
            }

            if (!string.IsNullOrEmpty(settings.Audio.ReceiveMessage))
            {
                audio.Register(new Audio()
                {
                    Url = string.Join("", System.AppDomain.CurrentDomain.BaseDirectory, settings.Audio.ReceiveMessage),
                    Type = AudioEnum.ReceiveMessage
                });
            } 

            if (!string.IsNullOrEmpty(settings.Audio.Call))
            {
                audio.Register(new Audio()
                {
                    Url = string.Join("", System.AppDomain.CurrentDomain.BaseDirectory, settings.Audio.Call),
                    Type = AudioEnum.Call
                });
            }

            if (!string.IsNullOrEmpty(settings.Audio.Hangup))
            {
                audio.Register(new Audio()
                {
                    Url = string.Join("", System.AppDomain.CurrentDomain.BaseDirectory, settings.Audio.Hangup),
                    Type = AudioEnum.Hangup
                });
            }

            if (!string.IsNullOrEmpty(settings.Audio.IncomingCall))
            {
                audio.Register(new Audio()
                {
                    Url = string.Join("", System.AppDomain.CurrentDomain.BaseDirectory, settings.Audio.IncomingCall),
                    Type = AudioEnum.IncomingCall
                });
            }

            ObjectPool.Instance.Register<IAudioService>().ImplementedBy(audio);   
        }

        private void InitializeDialog()
        {
            IDialogService dialog = new DialogService();

            dialog.Register(new Views.Dialogs.LookupContact());
            dialog.Register(new Views.Dialogs.ComposeContact());
            dialog.Register(new Views.Dialogs.ComposeMessage());
            dialog.Register(new Views.Dialogs.Plugin());
            dialog.Register(new Views.Dialogs.SerialPort());

            ObjectPool.Instance.Register<IDialogService>().ImplementedBy(dialog);        
        }

        private void InitializeConfiguration()
        {
            ObjectPool.Instance.Register<ApplicationSettings>().ImplementedBy(applicationSettings);
             
            Task.Factory.StartNew(() =>
            {
                while(true)
                { 
                    Thread.Sleep(5000);
                    applicationSettings.Connect();

                    if (GatewayManager.Instance.GetService().Connection.Connected)
                        break;
                }
            });
        }

        private void InitializeDatabase()
        {
            Task.Factory.StartNew(() => 
            {
                ApplicationSettings settings = null;
                string formattedConnectionString;
                string connectionString;
                IDbManager dbManager;

                while (settings == null)
                {
                    settings = ObjectPool.Instance.Resolve<ApplicationSettings>();

                    if (settings != null)
                    {
                        formattedConnectionString = ConfigurationManager.AppSettings.Get("FormattedConnectionString");
                        connectionString = string.Format(formattedConnectionString, settings.Database.Server, settings.Database.Name, settings.Database.Username, settings.Database.Password);

                        dbManager = new Texaco.Database.Petapoco.PetapocoDbManager(null, null);
                        dbManager.AddSqlLoader(new ResourceSqlLoader("filesql", "SMSGatewayWpf.SqlFiles", Assembly.GetAssembly(this.GetType())));
                        dbManager.ConnectionDescriptor.Add(new ConnectionDescriptor()
                        {
                            ConnectionString = connectionString,
                            IsDefault = true,
                            Name = settings.Database.Name,
                            ProviderName = settings.Database.ProviderName
                        });

                        ObjectPool.Instance.Register<IDbManager>().ImplementedBy(dbManager);
                        break;
                    }

                    Thread.Sleep(5000);
                }
                
            });
        }
 
    }
}
