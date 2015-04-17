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
using GSMServer.Contracts.InternalLogging;

namespace GSMServer.Contracts
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ActionGeneral : Attribute, IGeneral
    {

        public BaseResult<GenericTypeResult<bool>> SetFunctionality(ParameterizedMap map)
        {
            UnboxParameterizedMap param = new UnboxParameterizedMap(map);
            BasicInformation basic = param.TryGet<BasicInformation>("base");
            BaseResult<GenericTypeResult<bool>> result = basic.SetFunctionality((FunctionalityLevel)Convert.ToInt32(param.TryGet<string>("level")));
            result.ID = param.TryGet<string>("id");

            IInternalLogging logging = ObjectPool.Instance.Resolve<IInternalLogging>();
            if (logging != null)
            {
                logging.Write(result);
            }
            return result;
        }

        public BaseResult<GenericTypeResult<RegistrationStatus>> GetRegistrationStatus(ParameterizedMap map)
        {
            UnboxParameterizedMap param = new UnboxParameterizedMap(map);
            BasicInformation basic = param.TryGet<BasicInformation>("base");
            BaseResult<GenericTypeResult<RegistrationStatus>> result = basic.GetRegistrationStatus();
            result.ID = param.TryGet<string>("id");

            IInternalLogging logging = ObjectPool.Instance.Resolve<IInternalLogging>();
            if (logging != null)
            {
                logging.Write(result);
            }
            return result;
        }

        public BaseResult<GenericTypeResult<int>> GetSignalQuality(ParameterizedMap map)
        {
            UnboxParameterizedMap param = new UnboxParameterizedMap(map);
            BasicInformation basic = param.TryGet<BasicInformation>("base");
            BaseResult<GenericTypeResult<int>> result = basic.GetSignalQuality();
            result.ID = param.TryGet<string>("id");

            IInternalLogging logging = ObjectPool.Instance.Resolve<IInternalLogging>();
            if (logging != null)
            {
                logging.Write(result);
            }
            return result;
        }

        public BaseResult<GenericTypeResult<bool>> SetErrorMessageFormat(ParameterizedMap map)
        {
            UnboxParameterizedMap param = new UnboxParameterizedMap(map);
            BasicInformation basic = param.TryGet<BasicInformation>("base");
            BaseResult<GenericTypeResult<bool>> result = basic.SetErrorMessageFormat(param.TryGet<int>("format"));
            result.ID = param.TryGet<string>("id");

            IInternalLogging logging = ObjectPool.Instance.Resolve<IInternalLogging>();
            if (logging != null)
            {
                logging.Write(result);
            }
            return result;
        }

        public BaseResult<GenericTypeResult<List<string>>> GetPossibleCharacterSet(ParameterizedMap map)
        {
            UnboxParameterizedMap param = new UnboxParameterizedMap(map);
            BasicInformation basic = param.TryGet<BasicInformation>("base");
            BaseResult<GenericTypeResult<List<string>>> result = basic.GetPossibleCharacterSet();
            result.ID = param.TryGet<string>("id");

            IInternalLogging logging = ObjectPool.Instance.Resolve<IInternalLogging>();
            if (logging != null)
            {
                logging.Write(result);
            }
            return result;
        }

        public BaseResult<GenericTypeResult<string>> GetCharacterSet(ParameterizedMap map)
        {
            UnboxParameterizedMap param = new UnboxParameterizedMap(map);
            BasicInformation basic = param.TryGet<BasicInformation>("base");
            BaseResult<GenericTypeResult<string>> result = basic.GetCharacterSet();
            result.ID = param.TryGet<string>("id");

            IInternalLogging logging = ObjectPool.Instance.Resolve<IInternalLogging>();
            if (logging != null)
            {
                logging.Write(result);
            }
            return result;
        }

        public BaseResult<GenericTypeResult<bool>> SetCharacterSet(ParameterizedMap map)
        {
            UnboxParameterizedMap param = new UnboxParameterizedMap(map);
            BasicInformation basic = param.TryGet<BasicInformation>("base");
            BaseResult<GenericTypeResult<bool>> result = basic.SetCharacterSet(param.TryGet<string>("characterSet"));
            result.ID = param.TryGet<string>("id");

            IInternalLogging logging = ObjectPool.Instance.Resolve<IInternalLogging>();
            if (logging != null)
            {
                logging.Write(result);
            }
            return result;
        }

        public BaseResult<GenericTypeResult<int>> GetErrorMessageFormat(ParameterizedMap map)
        {
            UnboxParameterizedMap param = new UnboxParameterizedMap(map);
            BasicInformation basic = param.TryGet<BasicInformation>("base");
            BaseResult<GenericTypeResult<int>> result = basic.GetErrorMessageFormat();
            result.ID = param.TryGet<string>("id");

            IInternalLogging logging = ObjectPool.Instance.Resolve<IInternalLogging>();
            if (logging != null)
            {
                logging.Write(result);
            }
            return result;
        }

        public BaseResult<GenericTypeResult<string>> GetManufacturer(ParameterizedMap map)
        {
            UnboxParameterizedMap param = new UnboxParameterizedMap(map);
            BasicInformation basic = param.TryGet<BasicInformation>("base");
            BaseResult<GenericTypeResult<string>> result = basic.GetManufacturer();
            result.ID = param.TryGet<string>("id");

            IInternalLogging logging = ObjectPool.Instance.Resolve<IInternalLogging>();
            if (logging != null)
            {
                logging.Write(result);
            }
            return result;
        }

        public BaseResult<GenericTypeResult<string>> GetServiceCenter(ParameterizedMap map)
        {
            UnboxParameterizedMap param = new UnboxParameterizedMap(map);
            BasicInformation basic = param.TryGet<BasicInformation>("base");
            BaseResult<GenericTypeResult<string>> result = basic.GetServiceCenter();
            result.ID = param.TryGet<string>("id");

            IInternalLogging logging = ObjectPool.Instance.Resolve<IInternalLogging>();
            if (logging != null)
            {
                logging.Write(result);
            }
            return result;
        }

        public BaseResult<GenericTypeResult<string>> GetSoftwareVersion(ParameterizedMap map)
        {
            UnboxParameterizedMap param = new UnboxParameterizedMap(map);
            BasicInformation basic = param.TryGet<BasicInformation>("base");
            BaseResult<GenericTypeResult<string>> result = basic.GetSoftwareVersion();
            result.ID = param.TryGet<string>("id");

            IInternalLogging logging = ObjectPool.Instance.Resolve<IInternalLogging>();
            if (logging != null)
            {
                logging.Write(result);
            }
            return result;
        }

        public BaseResult<GenericTypeResult<string>> GetModelInformation(ParameterizedMap map)
        {
            UnboxParameterizedMap param = new UnboxParameterizedMap(map);
            BasicInformation basic = param.TryGet<BasicInformation>("base");
            BaseResult<GenericTypeResult<string>> result = basic.GetModelInformation();
            result.ID = param.TryGet<string>("id");

            IInternalLogging logging = ObjectPool.Instance.Resolve<IInternalLogging>();
            if (logging != null)
            {
                logging.Write(result);
            }
            return result;
        }

        public BaseResult<GenericTypeResult<string>> GetIMSI(ParameterizedMap map)
        {
            UnboxParameterizedMap param = new UnboxParameterizedMap(map);
            BasicInformation basic = param.TryGet<BasicInformation>("base");
            BaseResult<GenericTypeResult<string>> result = basic.GetIMSI();
            result.ID = param.TryGet<string>("id");

            IInternalLogging logging = ObjectPool.Instance.Resolve<IInternalLogging>();
            if (logging != null)
            {
                logging.Write(result);
            }
            return result;

        }

        public BaseResult<GenericTypeResult<string>> GetOperator(ParameterizedMap map)
        {
            UnboxParameterizedMap param = new UnboxParameterizedMap(map);
            BasicInformation basic = param.TryGet<BasicInformation>("base");
            BaseResult<GenericTypeResult<string>> result = basic.GetOperator();
            result.ID = param.TryGet<string>("id");

            IInternalLogging logging = ObjectPool.Instance.Resolve<IInternalLogging>();
            if (logging != null)
            {
                logging.Write(result);
            }
            return result;
        }

        public BaseResult<GenericTypeResult<PhoneActivityStatus>> GetActivityStatus(ParameterizedMap map)
        {
            UnboxParameterizedMap param = new UnboxParameterizedMap(map);
            BasicInformation basic = param.TryGet<BasicInformation>("base");
            BaseResult<GenericTypeResult<PhoneActivityStatus>> result = basic.GetActivityStatus();
            result.ID = param.TryGet<string>("id");

            IInternalLogging logging = ObjectPool.Instance.Resolve<IInternalLogging>();
            if (logging != null)
            {
                logging.Write(result);
            }
            return result;
        }

        public BaseResult<GenericTypeResult<string>> GetSerialNumber(ParameterizedMap map)
        {
            UnboxParameterizedMap param = new UnboxParameterizedMap(map);
            BasicInformation basic = param.TryGet<BasicInformation>("base");
            BaseResult<GenericTypeResult<string>> result = basic.GetSerialNumber();
            result.ID = param.TryGet<string>("id");

            IInternalLogging logging = ObjectPool.Instance.Resolve<IInternalLogging>();
            if (logging != null)
            {
                logging.Write(result);
            }
            return result;
        }

        public BaseResult<GenericTypeResult<string>> GetHardwareVersion(ParameterizedMap map)
        {
            UnboxParameterizedMap param = new UnboxParameterizedMap(map);
            BasicInformation basic = param.TryGet<BasicInformation>("base");
            BaseResult<GenericTypeResult<string>> result = basic.GetHardwareVersion();
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
