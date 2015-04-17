using GSMServer.Configuration;
/*
    SMS Gateway
 
    Copyright (C) 2013 Yudha - yudha_hyp@yahoo.com

    This library is free software; you can redistribute it and/or
    modify it, in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSMServer.Contracts
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ConfigAttribute : Attribute, IConfiguration
    { 
        public bool Save(ParameterizedMap map)
        {
            GSMServer.Configuration.IConfiguration config = ObjectPool.Instance.Resolve<GSMServer.Configuration.IConfiguration>();
            return config.Save();
        }

        public string GetRawXml(ParameterizedMap map)
        {
            UnboxParameterizedMap param = new UnboxParameterizedMap(map);
            GSMServer.Configuration.IConfiguration config = ObjectPool.Instance.Resolve<GSMServer.Configuration.IConfiguration>();
            string section = param.TryGet<string>("section"); 
            return config.GetRawXml(section); 
        }

        public bool SetRawXml(ParameterizedMap map)
        {
            UnboxParameterizedMap param = new UnboxParameterizedMap(map);
            GSMServer.Configuration.IConfiguration config = ObjectPool.Instance.Resolve<GSMServer.Configuration.IConfiguration>();
            string section = param.TryGet<string>("section"); 
            string raw = param.TryGet<string>("raw");
            return config.SetRawXml(section, raw);
        }
    }
}
