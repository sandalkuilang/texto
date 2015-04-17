using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace GSMServer.Configuration
{
    public class SerialPortSettings : ConfigurationSection
    {
        [ConfigurationProperty("serialPort")]
        [ConfigurationCollection(typeof(SerialPortCollection))]
        public SerialPortCollection Items
        {
            get 
            {
                return ((SerialPortCollection)(base["serialPort"])); 
            }
            set 
            {
                base["serialPort"] = value;
            }
        }
    }
}
