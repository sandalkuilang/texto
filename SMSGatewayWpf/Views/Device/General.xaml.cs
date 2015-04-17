using SMSGatewayWpf.Core;
using SMSGatewayWpf.ViewModels;
using SMSGatewayWpf.ViewModels.Configuration.Server;
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
    /// Interaction logic for Generic.xaml
    /// </summary>
    public partial class General : UserControl
    {
        public General()
        {
            InitializeComponent();
            GeneralView model = ObjectPool.Instance.Resolve<GeneralView>();
            if (this.DataContext == null && model != null)
            {
                this.DataContext = model;
            }
        }
    }
}
