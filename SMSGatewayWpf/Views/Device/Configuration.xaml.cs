using SMSGatewayWpf.ViewModels.Configuration;
using SMSGatewayWpf.ViewModels.Configuration.Client;
using SMSGatewayWpf.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SMSGatewayWpf.ViewModels.Configuration.Server;
using SMSGatewayWpf.Views.Dialogs;
using SMSGatewayWpf.ViewModels;

namespace SMSGatewayWpf.Views.Device
{
    /// <summary>
    /// Interaction logic for Configuration.xaml
    /// </summary>
    public partial class Configuration : UserControl
    {
         
        public Configuration()
        {
            InitializeComponent();
            ServerConfiguration model = ObjectPool.Instance.Resolve<ServerConfiguration>();
            if (this.DataContext == null && model != null)
            {
                this.DataContext = model;
            }
        }

        private void ShowDialog(Window window)
        {
            if (window == null)
            {
                window.Closed += (o, args) => window = null;
            }
            window.ShowDialog();
        }

        private void AddSerialPort_Click(object sender, RoutedEventArgs e)
        {
            ShowDialog(new SerialPort());
        }

        private void DeleteSerialPort_Click(object sender, RoutedEventArgs e)
        {

        }
         
        private void AddPlugin_Click(object sender, RoutedEventArgs e)
        {
            ShowDialog(new Plugin());
        }

        private void DeletePlugin_Click(object sender, RoutedEventArgs e)
        {

        }
         
    }
}
