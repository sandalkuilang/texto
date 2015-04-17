using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using SMSGatewayWpf.Core;
using SMSGatewayWpf.Navigation;
using SMSGatewayWpf.ViewModels;
using SMSGatewayWpf.ViewModels.Configuration.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SMSGatewayWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private bool _shutdown;
        private System.Collections.Generic.Stack<string> history;
        private bool firstLoaded = false;
        public MainWindow()
        {
            InitializeComponent();
            InitializeMenu();
            history = new Stack<string>();
            ApplicationSettings settings = ObjectPool.Instance.Resolve<ApplicationSettings>();
            settings.ConfigurationLoaded += settings_ConfigurationLoaded;
            if (!firstLoaded)
            {
                firstLoaded = true;
                 

                if (settings.General.LaunchMinimized)
                    this.WindowState = WindowState.Minimized;                    

                if (settings.General.ShowTaskbarIcon)
                    this.ShowInTaskbar = true;
                else
                    this.ShowInTaskbar = false;

                ObjectPool.Instance.Register<Window>().ImplementedBy(this);
            } 
        }

        void settings_ConfigurationLoaded(object sender, EventArgs e)
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                HomeNavigation home = ViewLocator.Instance.Get<HomeNavigation>();
                TransitioningContentControl selector = ViewLocator.Instance.Get<TransitioningContentControl>("TransitionContentHome");
                selector.Transition = TransitionType.Right;
                selector.Content = home.Templates["Settings"];
                selector.Content = home.Templates["Home"];
                selector.Transition = TransitionType.Left;
            }));
        }
         
        private void InitializeMenu()
        {
            HomeNavigation selector = ViewLocator.Instance.Get<HomeNavigation>();
            if (selector == null)
            {
                selector = new HomeNavigation();
                selector.Templates.Add("Settings", this.Resources["Settings"]);
                selector.Templates.Add("About", this.Resources["About"]);
                selector.Templates.Add("Home", this.Resources["Home"]);
                ViewLocator.Instance.Add(typeof(HomeNavigation), selector);
                ViewLocator.Instance.Add("TransitionContentHome", this.transitionContentHome);
            }
            this.transitionContentHome.ContentTemplateSelector = selector;
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        { 
            HomeNavigation selector = ViewLocator.Instance.Get<HomeNavigation>();
            this.transitionContentHome.Content = selector.Templates["Settings"];

            if (history.FirstOrDefault() != "about")
                this.transitionContentHome.AbortTransition();

            history.Push("settings");
        }

        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            HomeNavigation selector = ViewLocator.Instance.Get<HomeNavigation>();
            this.transitionContentHome.Content = selector.Templates["About"];

            if (history.FirstOrDefault() != "settings")
                this.transitionContentHome.AbortTransition();

            history.Push("about");
        }

        private async void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            e.Cancel = !_shutdown;
            if (_shutdown) return;

            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Yes",
                NegativeButtonText = "No",
                AnimateShow = true,
                AnimateHide = false
            };

            var result = await this.ShowMessageAsync("",
                "Are you sure want to quit application?",
                MessageDialogStyle.AffirmativeAndNegative, mySettings);

            _shutdown = result == MessageDialogResult.Affirmative;

            if (_shutdown)
                Application.Current.Shutdown();
        } 
         
    }
}
