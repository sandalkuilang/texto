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
using GSMCommunication.Feature;
using GSMServer.Contracts.InternalLogging;

namespace GSMServer.Contracts
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PhoneBookAttribute : Attribute, IPhoneBook
    {

        public PhoneBookAttribute()
        {
        }

        public BaseResult<GenericTypeResult<List<PhoneNumberInfo>>> FindPhoneBook(ParameterizedMap map)
        {
            UnboxParameterizedMap param = new UnboxParameterizedMap(map);
            GSMCommunication.Feature.PhoneBook pb = new GSMCommunication.Feature.PhoneBook(param.TryGet<BasicInformation>("base"));
            BaseResult<GenericTypeResult<List<PhoneNumberInfo>>> result = pb.FindPhoneBook(param.TryGet<string>("memory"), param.TryGet<string>("name"));
            result.ID = param.TryGet<string>("id");

            IInternalLogging logging = ObjectPool.Instance.Resolve<IInternalLogging>();
            if (logging != null)
            {
                logging.Write(result);
            }
            return result;
        }

        public BaseResult<GenericTypeResult<List<PhoneNumberInfo>>> ReadPhoneBook(ParameterizedMap map)
        {
            UnboxParameterizedMap param = new UnboxParameterizedMap(map);
            GSMCommunication.Feature.PhoneBook pb = new GSMCommunication.Feature.PhoneBook(param.TryGet<BasicInformation>("base"));
            BaseResult<GenericTypeResult<List<PhoneNumberInfo>>> result = pb.ReadPhoneBook(param.TryGet<string>("memory"), param.TryGet<int>("fromindex"), param.TryGet<int>("toindex"));
            result.ID = param.TryGet<string>("id");

            IInternalLogging logging = ObjectPool.Instance.Resolve<IInternalLogging>();
            if (logging != null)
            {
                logging.Write(result);
            }
            return result;
        }

        public BaseResult<GenericTypeResult<GSMCommunication.Feature.PhoneBookInfo>> GetInfo(ParameterizedMap map)
        {
            UnboxParameterizedMap param = new UnboxParameterizedMap(map);
            GSMCommunication.Feature.PhoneBook pb = new GSMCommunication.Feature.PhoneBook(param.TryGet<BasicInformation>("base"));
            BaseResult<GenericTypeResult<GSMCommunication.Feature.PhoneBookInfo>> result = pb.GetInfo();
            result.ID = param.TryGet<string>("id");

            IInternalLogging logging = ObjectPool.Instance.Resolve<IInternalLogging>();
            if (logging != null)
            {
                logging.Write(result);
            }
            return result;
        }

        public BaseResult<GenericTypeResult<bool>> SetPhoneBookMemory(ParameterizedMap map)
        {
            UnboxParameterizedMap param = new UnboxParameterizedMap(map);
            GSMCommunication.Feature.PhoneBook pb = new GSMCommunication.Feature.PhoneBook(param.TryGet<BasicInformation>("base"));
            BaseResult<GenericTypeResult<bool>> result = pb.SetPhoneBookMemory(param.TryGet<string>("memory"));
            result.ID = param.TryGet<string>("id");

            IInternalLogging logging = ObjectPool.Instance.Resolve<IInternalLogging>();
            if (logging != null)
            {
                logging.Write(result);
            }
            return result;
        }

        public BaseResult<GenericTypeResult<bool>> WritePhoneBook(ParameterizedMap map)
        {
            UnboxParameterizedMap param = new UnboxParameterizedMap(map);
            GSMCommunication.Feature.PhoneBook pb = new GSMCommunication.Feature.PhoneBook(param.TryGet<BasicInformation>("base"));
            BaseResult<GenericTypeResult<bool>> result = pb.WritePhoneBook(param.TryGet<string>("memory"), param.TryGet<string>("index"), param.TryGet<string>("name"), param.TryGet<string>("phonenumber"));
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
