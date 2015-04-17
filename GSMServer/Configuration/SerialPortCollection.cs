using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace GSMServer.Configuration
{
    [ConfigurationCollection(typeof(SerialPort), AddItemName = "add")]
    public class SerialPortCollection : ConfigurationElementCollection
    {  
        protected override ConfigurationElement CreateNewElement()
        {
            return new SerialPort();
        }
         
        protected override object GetElementKey(ConfigurationElement element)
        {
            var l_configElement = element as SerialPort;
            if (l_configElement != null)
                return l_configElement.PortName;
            else
                return null;
        }

        public SerialPort this[int index]
        {
            get
            {
                return BaseGet(index) as SerialPort;
            }
        }

        public new IEnumerator<SerialPort> GetEnumerator()
        {
            return (from i in Enumerable.Range(0, this.Count)
                    select this[i])
                    .GetEnumerator();
        }
    }
}
