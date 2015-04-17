using SMSGatewayWpf.ViewModels.Configuration.Client;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSGatewayWpf.Core.Gateway
{
    public class GatewayManager : IGatewayManager
    {
        private static IGatewayManager instance;
        private List<IGatewayService> bags;
        public static IGatewayManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GatewayManager();
                }
                return instance;
            }
        }

        public GatewayManager()
        {
            string formattedConnectionString;
            string connectionString;
            
            bags = new List<IGatewayService>();

            ApplicationSettings settings = ObjectPool.Instance.Resolve<ApplicationSettings>();
            formattedConnectionString = ConfigurationManager.AppSettings.Get("FormattedConnectionString");
            connectionString = string.Format(formattedConnectionString, settings.Database.Server, settings.Database.Name, settings.Database.Username, settings.Database.Password);

            AddService(new TcpService(settings.Gateway.IPAddress, settings.Gateway.Port));
            AddService(new DbService(connectionString, settings.Database.ProviderName));
        }
         
        public void AddService(IGatewayService service)
        { 
            bags.Add(service);
        }

        public IGatewayService GetService()
        { 
            return bags[0];
        } 
        public IGatewayService GetService<T>()
        {
            for (int i = 0; i < bags.Count; i++)
            {
                if (bags[i].GetType() == typeof(T))
                {
                    return bags[i];
                }
            }
            return null;
        }

        public void Clear()
        {
            bags.Clear();
        }
    }
}
