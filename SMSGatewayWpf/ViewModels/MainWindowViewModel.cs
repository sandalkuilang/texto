using SMSGatewayWpf.Core;
using SMSGatewayWpf.ViewModels.Configuration.Client;
using SMSGatewayWpf.ViewModels.Configuration.Server;
using SMSGatewayWpf.ViewModels.Devices;
using SMSGatewayWpf.ViewModels.Message;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSGatewayWpf.ViewModels
{
    public class MainWindowViewModel : BaseBindableModel
    {
       
        public MainWindowViewModel(ServerConfiguration serverConfiguration, MessageCollaborator messageCollection, ApplicationSettings applicationSettings, GeneralView generalView, CallView callView,
            PortView portView, SMSView smsView)
        {
            this.serverConfiguration = serverConfiguration;
            this.messageCollection = messageCollection;
            this.applicationSettings = applicationSettings;
            this.generalView = generalView;
            this.callView = callView;
            this.portView = portView;
            this.smsView = smsView; 
        }

        private ServerConfiguration serverConfiguration;
        public ServerConfiguration ServerConfiguration
        {
            get
            {
                return serverConfiguration;
            }
            set
            {
                NotifyIfChanged(ref serverConfiguration, value);
            }
        }
        
         
        private MessageCollaborator messageCollection;
        public MessageCollaborator MessageCollection
        {
            get
            {
                return messageCollection;
            }
            set
            {
                NotifyIfChanged(ref messageCollection, value);
            }
        }

        private ApplicationSettings applicationSettings;
        public ApplicationSettings ApplicationSettings
        {
            get
            {
                return applicationSettings;
            }
            set
            {
                NotifyIfChanged(ref applicationSettings, value);
            }
        }

        private GeneralView generalView;
        public GeneralView GeneralView
        {
            get
            {
                return generalView;
            }
            set
            {
                NotifyIfChanged(ref generalView, value);
            }
        }

        private CallView callView;
        public CallView CallView
        {
            get
            {
                return callView;
            }
            set
            {
                NotifyIfChanged(ref callView, value);
            }
        }

        private PortView portView;
        public PortView PortView
        {
            get
            {
                return portView;
            }
            set
            {
                NotifyIfChanged(ref portView, value);
            }
        }

        private SMSView smsView;
        public SMSView SMSView
        {
            get
            {
                return smsView;
            }
            set
            {
                NotifyIfChanged(ref smsView, value);
            }
        }
    }
}
