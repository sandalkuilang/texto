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
using Sockets; 
using GSMServerModel; 
using GSMCommunication; 
using GSMServer.Contracts.InternalLogging;

namespace GSMServer.Contracts
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SMSAttribute : Attribute, ISMS
    {
    
        public SMSAttribute()
        {
        }

        public BaseResult<GenericTypeResult<bool>> DeleteAll(ParameterizedMap map)
        {
            UnboxParameterizedMap param = new UnboxParameterizedMap(map);
            GSMCommunication.Feature.SMS sms = new GSMCommunication.Feature.SMS(param.TryGet<BasicInformation>("base"));
            BaseResult<GenericTypeResult<bool>> result = sms.DeleteAll();
            result.ID = param.TryGet<string>("id");

            IInternalLogging logging = ObjectPool.Instance.Resolve<IInternalLogging>();
            if (logging != null) 
                logging.Write(result);

            return result;
        }
         
        public BaseResult<GenericTypeResult<bool>> Delete(ParameterizedMap map)
        {
            UnboxParameterizedMap param = new UnboxParameterizedMap(map);
            GSMCommunication.Feature.SMS sms = new GSMCommunication.Feature.SMS(param.TryGet<BasicInformation>("base"));
            BaseResult<GenericTypeResult<bool>> result = sms.Delete(param.TryGet<int>("index"));
            result.ID = param.TryGet<string>("id");

            IInternalLogging logging = ObjectPool.Instance.Resolve<IInternalLogging>();
            if (logging != null)
                logging.Write(result);

            return result;
        }

        public List<BaseResult<SMSReadResult>> ReadAll(ParameterizedMap map)
        {
            UnboxParameterizedMap param = new UnboxParameterizedMap(map);
            GSMCommunication.Feature.SMS sms = new GSMCommunication.Feature.SMS(param.TryGet<BasicInformation>("base"));
            List<BaseResult<SMSReadResult>> result = sms.Read(SMSStatus.AllMessages);

            IInternalLogging logging = ObjectPool.Instance.Resolve<IInternalLogging>();
            ISMSLogging smsLogging = ObjectPool.Instance.Resolve<ISMSLogging>();
             
            if (smsLogging != null)
                smsLogging.Read(result); 

            return result;
        }

        public List<BaseResult<SMSReadResult>> Read(ParameterizedMap map)
        {
            UnboxParameterizedMap param = new UnboxParameterizedMap(map);
            GSMCommunication.Feature.SMS sms = new GSMCommunication.Feature.SMS(param.TryGet<BasicInformation>("base"));
            List<BaseResult<SMSReadResult>> list = sms.Read(SMSStatus.ReceivedUnreadMessage);

            IInternalLogging logging = ObjectPool.Instance.Resolve<IInternalLogging>();
            ISMSLogging smsLogging = ObjectPool.Instance.Resolve<ISMSLogging>();

            if (logging != null) 
                logging.Write(list);
            if (smsLogging != null)
                smsLogging.Read(list); 
  
            return list;
        }

        public BaseResult<SMSSendResult> Send(ParameterizedMap map)
        {
            UnboxParameterizedMap param = new UnboxParameterizedMap(map);
            GSMCommunication.Feature.SMS sms = new GSMCommunication.Feature.SMS(param.TryGet<BasicInformation>("base"));
            BaseResult<SMSSendResult> result = null;
            result = sms.Send(param.TryGet<string>("message"), param.TryGet<string>("number"));
            if (result != null)
            {
                result.ID = param.TryGet<string>("id");

                IInternalLogging logging = ObjectPool.Instance.Resolve<IInternalLogging>();
                ISMSLogging smsLogging = ObjectPool.Instance.Resolve<ISMSLogging>();

                if (logging != null)
                    logging.Write(result);
                if (smsLogging != null)
                    smsLogging.Send(result); 

            }
                
            return result;
        }
    }
}
