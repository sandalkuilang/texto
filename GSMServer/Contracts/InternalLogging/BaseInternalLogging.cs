using System;
/*
    SMS Gateway
 
    Copyright (C) 2015 Yudha - yudha_hyp@yahoo.com

    This library is free software; you can redistribute it and/or
    modify it, in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  
*/

using System.Collections.Generic;
using System.Linq;
using System.Text;
using Texaco.Database;

namespace GSMServer.Contracts.InternalLogging
{
    internal class BaseInternalLogging : IInternalLogging
    {
        public void Write(object value)
        {
            if (value == null)
                return;

            System.Reflection.PropertyInfo pi = value.GetType().GetProperty("ID"); 
            
            if (pi != null)
            { 
                IDbManager dbManager = ObjectPool.Instance.Resolve<IDbManager>();
                IDataCommand db = dbManager.GetDatabase(DatabaseNames.SMSGW);

                string propValue = (string)pi.GetValue(value, null);
                if (!string.IsNullOrEmpty(propValue))
                {
                    StringBuilder responseJson = new StringBuilder(Newtonsoft.Json.JsonConvert.SerializeObject(value,
                        Newtonsoft.Json.Formatting.None,
                        new Newtonsoft.Json.JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                        }));

                    db.Execute("InsertResultWorkItem", new
                    {
                        SeqNbr = propValue,
                        Response = responseJson.ToString()
                    });
                    db.Close();
                } 

            }
        }
    }
}
