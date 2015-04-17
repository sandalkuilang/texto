using SMSGatewayWpf.Core;
using SMSGatewayWpf.Navigation;
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

namespace SMSGatewayWpf.Views
{
    /// <summary>
    /// Interaction logic for Devices.xaml
    /// </summary>
    public partial class Devices : UserControl
    {
        public Devices()
        {
            InitializeComponent();
            InitializeMenu();
        }

        private void InitializeMenu()
        {
            //DevicesMenuNavigation selector = ViewLocator.Instance.Get<DevicesMenuNavigation>();
            //if (selector == null)
            //{
            //    selector = new DevicesMenuNavigation();
            //    selector.Templates.Add("Port", this.Resources["Port"]);
            //    selector.Templates.Add("Configuration", this.Resources["Configuration"]);
            //    selector.Templates.Add("General", this.Resources["General"]);
            //    selector.Templates.Add("Phonebook", this.Resources["Phonebook"]);
            //    selector.Templates.Add("Call", this.Resources["Call"]);
            //    selector.Templates.Add("SMS", this.Resources["SMS"]);
            //    ViewLocator.Instance.Add(typeof(DevicesMenuNavigation), selector);
            //    ViewLocator.Instance.Add("TransitionContentDevices", this.transitionContentDevices);
            //}
            //this.transitionContentDevices.ContentTemplateSelector = selector;

        }
         
    }
}
