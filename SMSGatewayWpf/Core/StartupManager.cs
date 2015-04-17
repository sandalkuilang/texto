using Microsoft.Win32;
using SMSGatewayWpf.ViewModels.Configuration.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SMSGatewayWpf.Core
{
    public class StartUpManager
    {
        public static void AddApplicationToCurrentUserStartup()
        {
            ApplicationSettings settings = ObjectPool.Instance.Resolve<ApplicationSettings>();
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                key.SetValue(settings.General.Signature, "\"" + System.Reflection.Assembly.GetExecutingAssembly().Location + "\"");
            }
        }

        public static void AddApplicationToAllUserStartup()
        {
            ApplicationSettings settings = ObjectPool.Instance.Resolve<ApplicationSettings>();
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                key.SetValue(settings.General.Signature, "\"" + System.Reflection.Assembly.GetExecutingAssembly().Location + "\"");
            }
        }

        public static void RemoveApplicationFromCurrentUserStartup()
        {
            ApplicationSettings settings = ObjectPool.Instance.Resolve<ApplicationSettings>();
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                key.DeleteValue(settings.General.Signature, false);
            }
        }

        public static void RemoveApplicationFromAllUserStartup()
        {
            ApplicationSettings settings = ObjectPool.Instance.Resolve<ApplicationSettings>();
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                key.DeleteValue(settings.General.Signature, false);
            }
        }

        public static bool IsUserAdministrator()
        {
            //bool value to hold our return value
            bool isAdmin;
            try
            {
                //get the currently logged in user
                WindowsIdentity user = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(user);
                isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch (UnauthorizedAccessException)
            {
                isAdmin = false;
            }
            catch (Exception)
            {
                isAdmin = false;
            }
            return isAdmin;
        }


    }



}
