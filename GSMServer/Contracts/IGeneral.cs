/*
    SMS Gateway
 
    Copyright (C) 2013 Yudha - yudha_hyp@yahoo.com

    This library is free software; you can redistribute it and/or
    modify it, in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  
*/
using GSMCommunication.Feature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSMServer.Contracts
{
    [Contract]
    public interface IGeneral
    {
        BaseResult<GenericTypeResult<bool>> SetFunctionality(ParameterizedMap map);
        BaseResult<GenericTypeResult<RegistrationStatus>> GetRegistrationStatus(ParameterizedMap map);
        BaseResult<GenericTypeResult<int>> GetSignalQuality(ParameterizedMap map);
        BaseResult<GenericTypeResult<bool>> SetErrorMessageFormat(ParameterizedMap map);
        BaseResult<GenericTypeResult<List<string>>> GetPossibleCharacterSet(ParameterizedMap map);
        BaseResult<GenericTypeResult<string>> GetCharacterSet(ParameterizedMap map);
        BaseResult<GenericTypeResult<bool>> SetCharacterSet(ParameterizedMap map);
        BaseResult<GenericTypeResult<int>> GetErrorMessageFormat(ParameterizedMap map);
        BaseResult<GenericTypeResult<string>> GetManufacturer(ParameterizedMap map);
        BaseResult<GenericTypeResult<string>> GetServiceCenter(ParameterizedMap map);
        BaseResult<GenericTypeResult<string>> GetSoftwareVersion(ParameterizedMap map);
        BaseResult<GenericTypeResult<string>> GetModelInformation(ParameterizedMap map);
        BaseResult<GenericTypeResult<string>> GetIMSI(ParameterizedMap map);
        BaseResult<GenericTypeResult<string>> GetOperator(ParameterizedMap map);
        BaseResult<GenericTypeResult<PhoneActivityStatus>> GetActivityStatus(ParameterizedMap map);
        BaseResult<GenericTypeResult<string>> GetSerialNumber(ParameterizedMap map);
        BaseResult<GenericTypeResult<string>> GetHardwareVersion(ParameterizedMap map);

    }
}
