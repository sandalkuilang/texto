using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSGatewayWpf.Core
{
    public abstract class SymmetricEncryption : IEncryptionAgent
    {
        public string Key { get; set; }
        public string IV { get; set; }

        public SymmetricEncryption()
        {

        }

        public SymmetricEncryption(string key, string iv)
        {
            this.Key = key;
            this.IV = iv;
        }

        public abstract string Encrypt(string value);

        public abstract string Decrypt(string value);
    }
}
