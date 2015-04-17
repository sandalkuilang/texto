using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace GSMServer.Configuration
{
    public class GeneralSettings : System.Configuration.ConfigurationSection
    {

        [ConfigurationProperty("defaultPort")]
        public string DefaultPort
        {
            get
            {
                return (string)this["defaultPort"];
            }
            set
            {
                this["defaultPort"] = value;
            }
        }

        [ConfigurationProperty("defaultIP")]
        public string DefaultIP
        {
            get
            {
                return (string)this["defaultIP"];
            }
            set
            {
                this["defaultIP"] = value;
            }
        }

        [ConfigurationProperty("defaultEncoding")]
        public string DefaultEncoding
        {
            get
            {
                return (string)this["defaultEncoding"];
            }
            set
            {
                this["defaultEncoding"] = value;
            }
        }

        [ConfigurationProperty("countryCode")]
        public string CountryCode
        {
            get
            {
                return (string)this["countryCode"];
            }
            set
            {
                this["countryCode"] = value;
            }
        }

        [ConfigurationProperty("smsGWSignature")]
        public string SMSGWSignature
        {
            get
            {
                return (string)this["smsGWSignature"];
            }
            set
            {
                this["smsGWSignature"] = value;
            }
        }

        [ConfigurationProperty("prefixOwnNumber")]
        public string PrefixOwnNumber
        {
            get
            {
                return (string)this["prefixOwnNumber"];
            }
            set
            {
                this["prefixOwnNumber"] = value;
            }
        }

        [ConfigurationProperty("intervalWorkerQueue")]
        public int IntervalWorkerQueue
        {
            get
            {
                return (int)this["intervalWorkerQueue"];
            }
            set
            {
                this["intervalWorkerQueue"] = value;
            }
        }

        [ConfigurationProperty("intervalReadMessage")]
        public int IntervalReadMessage
        {
            get
            {
                return (int)this["intervalReadMessage"];
            }
            set
            {
                this["intervalReadMessage"] = value;
            }
        }
         
    }
}
