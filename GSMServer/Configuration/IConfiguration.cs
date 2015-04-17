using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSMServer.Configuration
{
    public interface IConfiguration
    {   
        string GetRawXml(string section);
        bool SetRawXml(string section, string raw);
        bool Save();
    }
}
