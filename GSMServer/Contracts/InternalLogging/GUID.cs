using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSMServer.Contracts.InternalLogging
{
    internal class GUID : BaseRandomizeGenerator
    {
        private const string LetterArray = "WECBUFGHAJKLYRQOPTIXVDMZFGHAJJKLYRQOPTIXVDMZ";
        private const string NumberArray = "809426315709463158094631942631263157094631";
        public static string GenerateID(string prefix)
        {
            if (string.IsNullOrEmpty(prefix))
                throw new ArgumentNullException("prefix");
             
            string result = Generate(Guid.NewGuid().ToString().Replace("-", ""), NumberArray, 14);

            return string.Join("", new string[] 
            { 
                prefix, 
                DateTime.Now.Ticks.ToString().Substring(13), 
                result
            });
        }

    }
}
