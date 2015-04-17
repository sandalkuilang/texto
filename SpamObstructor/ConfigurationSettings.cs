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
using System.Configuration;
using System.Linq;
using System.Text;

namespace SpamObstructor
{
    internal class ConfigurationSettings
    { 
        public ConnectionStringSettings ConnectionString { get; private set; }

        public ConfigurationSettings()
        {
            string name = ConfigurationManager.ConnectionStrings["SMSGW"].Name;
            string connectionString = ConfigurationManager.ConnectionStrings["SMSGW"].ConnectionString;
            string providerName = ConfigurationManager.ConnectionStrings["SMSGW"].ProviderName;
            this.ConnectionString = new ConnectionStringSettings(name, connectionString, providerName);
        }
    }
}
