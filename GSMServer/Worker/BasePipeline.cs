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
using GSMCommunication.Feature;
using GSMServer.Contracts;
using Sockets;

namespace GSMServer.Worker
{
    internal abstract class BasePipeline : IPipeline
    {
        protected bool Validate(ParameterizedMap map)
        {
            if ((map != null))
            {
                BasicInformation connection = map.TryGet<BasicInformation>("base");
                if (connection != null)
                {
                    return true;
                }
            }
            return false;
        }

        public string BeforePush(ParameterizedMap map)
        {
            if (Validate(map))
            {
                return OnBeforePush(map);
            }
            return string.Empty;
        }

        public string Push(ParameterizedMap map)
        {
            if (Validate(map))
            {
                return OnPush(map);
            }
            return string.Empty;
        }

        public string AfterPush(ParameterizedMap map)
        {
            if (Validate(map))
            {
                return OnAfterPush(map);
            }
            return string.Empty;
        }

        public string BeforePull(ParameterizedMap map)
        {
            if (Validate(map))
            {
                return OnBeforePull(map);
            }
            return string.Empty;
        }

        public string Pull(ParameterizedMap map)
        {
            if (Validate(map))
            {
                return OnPull(map);
            }
            return string.Empty;
        }

        public string AfterPull(ParameterizedMap map)
        {
            if (Validate(map))
            {
                return OnAfterPull(map);
            }
            return string.Empty;
        }
        public abstract string OnBeforePush(ParameterizedMap map);
        public abstract string OnPush(Contracts.ParameterizedMap map);
        public abstract string OnAfterPush(Contracts.ParameterizedMap map);
        public abstract string OnBeforePull(Contracts.ParameterizedMap map);
        public abstract string OnPull(Contracts.ParameterizedMap map);
        public abstract string OnAfterPull(Contracts.ParameterizedMap map); 
    }
}
