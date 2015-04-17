using SMSGatewayWpf.Core;
using SMSGatewayWpf.Core.Gateway;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace SMSGatewayWpf.ViewModels.Configuration.Server
{
    public class ServerConfiguration
    {
        private PluginCollection plugins;
        private SerialPortCollection serialPorts;
        private GeneralSettings general;
        public PluginCollection Plugins 
        { 
            get
            {
                return plugins;
            }
            internal set { }
        }

        public SerialPortCollection SerialPorts 
        {
            get
            {
                return serialPorts;
            }
            internal set { }
        }

        public GeneralSettings General 
        {
            get
            {
                return general;
            }
            internal set { } 
        }

        public ServerConfiguration()
        {
            this.Initialize(null); 
        }

        public bool Enable { get; set; }

        public ServerConfiguration(IGatewayService service)
        {
            this.Enable = false; // set true if want to load server configuration 
            this.Initialize(service);
        }

        private void Initialize(IGatewayService service)
        {
            if (!Enable)
                return;

            if (service == null)
                service = GatewayManager.Instance.GetService();

            string generalXml = service.Execute(string.Format(GSMClient.Command.CommandCollection.ConfigGetRawXml, "general"));
            general = LoadFromXml<GeneralSettings>(generalXml);

            string serialPortsXml = service.Execute(string.Format(GSMClient.Command.CommandCollection.ConfigGetRawXml, "serialports"));
            serialPorts = LoadFromXml<SerialPortCollection>(serialPortsXml);

            string pluginXml = service.Execute(string.Format(GSMClient.Command.CommandCollection.ConfigGetRawXml, "plugin"));
            plugins = LoadFromXml<PluginCollection>(pluginXml);
             
        }

        protected T LoadFromXml<T>(string raw)
        {
            if (!string.IsNullOrEmpty(raw))
            {
                /// remove quote string "
                raw = raw.Substring(1, raw.Length - 2);
                raw = raw.Replace(@"\" + (char)34, ((char)34).ToString());
                raw = raw.Replace(@"\r\n", "");

                try
                { 
                    StringReader reader = new StringReader(raw);
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    T deObject = (T)serializer.Deserialize(reader);
                    reader.Dispose();
                    return deObject;
                }
                catch (Exception)
                { }
               
            }
            return default(T);
        }
    }
}
