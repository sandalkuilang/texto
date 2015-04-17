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
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Sockets; 
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using GSMServer.Contracts;
using Cryptography;
using System.Configuration;
using System.Diagnostics;
using System.Threading;
using GSMServerModel;
using GSMServer.Contracts.InternalLogging;
using GSMServer.Configuration;
using Newtonsoft.Json;

namespace GSMServer.Security
{
    internal class AuthorizationRequestFilterAttribute : Attribute, IRequestFilter
    {
        private static readonly  object s_InternalSyncObject = new object();

        private GSMServer.Configuration.IConfiguration configuration;
        private string signature;
        private string encodingName;
        private Encoding encoding;
        private Crypter c;

        public AuthorizationRequestFilterAttribute()
        {
            c = new Crypter(new DefaultConfigurationKey()); 
        }

        public bool OnRequesting(PacketEventArgs requestFilter)
        {
            //// filtering header
            //// 1. get string using encoding ibm850
            //// 2. decrypt using rijndael
            //// 3. split with "</>"
            if (configuration == null)
            {
                configuration = ObjectPool.Instance.Resolve<GSMServer.Configuration.IConfiguration>();
            }
            if (string.IsNullOrEmpty(signature) || (string.IsNullOrEmpty(encodingName) || (encoding == null)))
            {
                signature = ((ApplicationSettings)configuration).General.SMSGWSignature;
                encodingName = ((ApplicationSettings)configuration).General.DefaultEncoding;
                encoding = Encoding.GetEncoding(encodingName);
            }
            
            lock (s_InternalSyncObject)
            {
                string dataFilter = "";
                if (requestFilter.Data.Length > 0)
                {
                    try
                    {
                        dataFilter = c.Decrypt(encoding.GetString(requestFilter.Data));
                    }
                    catch(FormatException fe)
                    {
                        IErrorLogging log = ObjectPool.Instance.Resolve<IErrorLogging>();
                        log.Write(fe.Message + " - on requesting");
                        return false;
                    }
                    requestFilter.Data = encoding.GetBytes(dataFilter);
                }
                if (!string.IsNullOrEmpty(dataFilter))
                {
                    Request request = JsonConvert.DeserializeObject<Request>(dataFilter, new JsonSerializerSettings
                                        {
                                            TypeNameHandling = TypeNameHandling.Objects
                                        });
                    request.RemoteEndPoint = (IPEndPoint)requestFilter.Client.RemoteEndPoint;

                    if (request.Header != null)
                    {
                        if (request.Header.Signature != null)
                        {
                            if (!request.Header.Signature.Equals(signature))
                            {
                                request.Cancel = true;
                                return false;
                            } 
                        }
                    }
                    else
                        return false; 
                }
            }
            return true;
        }
    }
}
