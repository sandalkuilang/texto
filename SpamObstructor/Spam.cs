/*
    SMS Gateway
 
    Copyright (C) 2015 Yudha - yudha_hyp@yahoo.com

    This library is free software; you can redistribute it and/or
    modify it, in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  
*/

using GSMCommunication.Feature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Texaco.Database;
using Texaco.Database.Petapoco;
using Texaco.Database.SqlLoader;

namespace SpamObstructor
{
    public class Spam
    {
        public void Main()
        {
            ConfigurationSettings settings = new ConfigurationSettings();
            ObjectPool.Instance.Register<ConfigurationSettings>().ImplementedBy(settings);

            PetapocoDbManager dbManager = new Texaco.Database.Petapoco.PetapocoDbManager(null, null);
            dbManager.AddSqlLoader(new ResourceSqlLoader("filesql", "SqlFiles", Assembly.GetCallingAssembly()));
            dbManager.ConnectionDescriptor.Add(new ConnectionDescriptor()
            {
                ConnectionString = settings.ConnectionString.ConnectionString,
                IsDefault = true,
                Name = settings.ConnectionString.Name,
                ProviderName = settings.ConnectionString.ProviderName
            });

            ObjectPool.Instance.Register<PetapocoDbManager>().ImplementedBy(dbManager);
        }

        public void OnDataReceived(string data)
        {
            try
            {
                if (data != "[]")
                {
                    if (data.StartsWith("[") && data.EndsWith("]") && data.Contains("SMSReadResult"))
                    {
                        ConfigurationSettings configuration = ObjectPool.Instance.Resolve<ConfigurationSettings>();
                        List<BaseResult<SMSReadResult>> list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<BaseResult<SMSReadResult>>>(data);
                        IDbManager dbManager = ObjectPool.Instance.Resolve<PetapocoDbManager>();
                        IDataCommand db = dbManager.GetDatabase("SMSGW");
                        foreach (BaseResult<SMSReadResult> read in list)
                        {
                            db.Execute("Obstruction", null);
                        }
                        db.Close();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            } 
        }

        public void OnDataSent(string data)
        {

        }

        public void Dispose()
        {

        }
    }
}
