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
using GSMCommunication;
using Sockets;
using GSMCommunication.Feature;
using GSMServer.Contracts.InternalLogging;

namespace GSMServer.Contracts
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CallAttribute : Attribute, ICall
    {
        public CallAttribute()
        {
        }

        public BaseResult<GenericTypeResult<string>> Dial(ParameterizedMap map)
        {
            UnboxParameterizedMap param = new UnboxParameterizedMap(map);
            GSMCommunication.Feature.Call call = new GSMCommunication.Feature.Call(param.TryGet<BasicInformation>("base"));
            BaseResult<GenericTypeResult<string>> result = call.Dial(param.TryGet<string>("number"));
            result.ID = param.TryGet<string>("id"); 

            IInternalLogging logging = ObjectPool.Instance.Resolve<IInternalLogging>();
            if (logging != null)
            {
                logging.Write(result);
            }
            return result;
        }

        public BaseResult<GenericTypeResult<bool>> Hang(ParameterizedMap map)
        {
            UnboxParameterizedMap param = new UnboxParameterizedMap(map);
            GSMCommunication.Feature.Call call = new GSMCommunication.Feature.Call(param.TryGet<BasicInformation>("base"));
            BaseResult<GenericTypeResult<bool>> result = call.Hang(param.TryGet<string>("number"));
            result.ID = param.TryGet<string>("id");

            IInternalLogging logging = ObjectPool.Instance.Resolve<IInternalLogging>();
            if (logging != null)
            {
                logging.Write(result);
            }
            return result;
        }

        public BaseResult<GenericTypeResult<bool>> Answer(ParameterizedMap map)
        {
            UnboxParameterizedMap param = new UnboxParameterizedMap(map);
            GSMCommunication.Feature.Call call = new GSMCommunication.Feature.Call(param.TryGet<BasicInformation>("base"));
            BaseResult<GenericTypeResult<bool>> result = call.Answer();
            result.ID = param.TryGet<string>("id");

            IInternalLogging logging = ObjectPool.Instance.Resolve<IInternalLogging>();
            if (logging != null)
            {
                logging.Write(result);
            }
            return result;
        }

        public BaseResult<USSDResult> SendUSSD(ParameterizedMap map)
        {
            UnboxParameterizedMap param = new UnboxParameterizedMap(map);
            GSMCommunication.Feature.Call call = new GSMCommunication.Feature.Call(param.TryGet<BasicInformation>("base"));
            BaseResult<USSDResult> result = call.SendUSSD(param.TryGet<string>("code"), param.TryGet<string>("number"));
            result.ID = param.TryGet<string>("id");

            IInternalLogging logging = ObjectPool.Instance.Resolve<IInternalLogging>();
            if (logging != null)
            {
                logging.Write(result);
            }
            return result;
        }
    }
}
