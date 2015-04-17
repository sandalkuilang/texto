using SMSGatewayWpf.Core;
using SMSGatewayWpf.ViewModels;
using SMSGatewayWpf.ViewModels.Devices;
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

namespace SMSGatewayWpf.Views.Device
{
    /// <summary>
    /// Interaction logic for Port.xaml
    /// </summary>
    public partial class Port : UserControl
    {
        public Port()
        {
            InitializeComponent();
            PortView model = ObjectPool.Instance.Resolve<PortView>();
            if (this.DataContext == null && model != null)
            {
                this.DataContext = model;
            }
        }
    }
}
