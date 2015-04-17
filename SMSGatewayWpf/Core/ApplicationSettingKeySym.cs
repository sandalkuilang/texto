using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cryptography;
using SMSGatewayWpf.ViewModels.Configuration.Client;

namespace SMSGatewayWpf.Core
{
    internal class ApplicationSettingKeySym : IKeySym
    {
        public byte[] GetKey()
        {
            ApplicationSettings settings = ObjectPool.Instance.Resolve<ApplicationSettings>();
            string[] KeySym = settings.Encryption.Key.Split(',');
            byte[] publicKey = new byte[KeySym.Length];
            for (int i = 0; i < KeySym.Length; i++)
            {
                publicKey[i] = Convert.ToByte(KeySym[i]);
            }
            return publicKey; 
        }
    }
}
