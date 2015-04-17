using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace GSMServer.Configuration
{
    public class SerialPort : ConfigurationSection
    {
        [ConfigurationProperty("portName")]
        public string PortName
        {
            get
            {
                return (string)this["portName"];
            }
            set
            {
                this["portName"] = value;
            }
        }

        [ConfigurationProperty("baudRate")]
        public int BaudRate
        {
            get
            {
                return (int)this["baudRate"];
            }
            set
            {
                this["baudRate"] = value;
            }
        }

        [ConfigurationProperty("parity")]
        public string Parity
        {
            get
            {
                return (string)this["parity"];
            }
            set
            {
                this["parity"] = value;
            }
        }

        [ConfigurationProperty("dataBits")]
        public int DataBits
        {
            get
            {
                return (int)this["dataBits"];
            }
            set
            {
                this["dataBits"] = value;
            }
        }

        [ConfigurationProperty("stopBits")]
        public string StopBits
        {
            get
            {
                return (string)this["stopBits"];
            }
            set
            {
                this["stopBits"] = value;
            }
        }

        [ConfigurationProperty("handshake")]
        public string Handshake
        {
            get
            {
                return (string)this["handshake"];
            }
            set
            {
                this["handshake"] = value;
            }
        }

        [ConfigurationProperty("serviceCenter")]
        public string ServiceCenter
        {
            get
            {
                return (string)this["serviceCenter"];
            }
            set
            {
                this["serviceCenter"] = value;
            }
        }

        [ConfigurationProperty("pduMode")]
        public bool PDUMode
        {
            get
            {
                return (bool)this["pduMode"];
            }
            set
            {
                this["pduMode"] = value;
            }
        }
    }
}
