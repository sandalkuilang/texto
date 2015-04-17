using SMSGatewayWpf.ViewModels.Configuration.Client;
using SMSGatewayWpf.Core;
using SMSGatewayWpf.Navigation;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows; 
using GSMClient;

namespace SMSGatewayWpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e); 
            var bootstrapper = new ApplicationStartup();
            bootstrapper.Run(); 
        }
      
    }
}
