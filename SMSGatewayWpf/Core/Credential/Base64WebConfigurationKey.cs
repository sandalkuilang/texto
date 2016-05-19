using Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSGatewayWpf.Core
{
    public class Base64WebConfigurationKey : IKeySym
    {
        public byte[] GetKey()
        {
            string value = System.Configuration.ConfigurationManager.AppSettings["PublicKey"];
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("App setting \'PublicKey\' cannot be null.");
            else
            {
                return Convert.FromBase64String(value);
            }
        }
    }
}
