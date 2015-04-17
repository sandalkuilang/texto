using MahApps.Metro.Controls;
using SMSGatewayWpf.ViewModels.Configuration.Client;
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
using SMSGatewayWpf.ViewModels;

namespace SMSGatewayWpf.Views
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : UserControl
    { 
        public Settings()
        {
            InitializeComponent();
            ApplicationSettings model = ObjectPool.Instance.Resolve<ApplicationSettings>();
            if (this.DataContext == null && model != null)
            {
                this.DataContext = model;
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            HomeNavigation home = ViewLocator.Instance.Get<HomeNavigation>();
            TransitioningContentControl selector = ViewLocator.Instance.Get<TransitioningContentControl>("TransitionContentHome");
            selector.Transition = TransitionType.Right;
            selector.Content = home.Templates["Home"];
            selector.Transition = TransitionType.Left;
        }
         
    }
}
