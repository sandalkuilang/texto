using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSGatewayWpf.Core
{
    public interface IEncryptionAgent
    {
        string Encrypt(string value);
        string Decrypt(string value);
    }
}
