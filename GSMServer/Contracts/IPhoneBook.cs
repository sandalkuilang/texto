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

namespace GSMServer.Contracts
{
    [Contract]
    public interface IPhoneBook
    {
        BaseResult<GenericTypeResult<List<PhoneNumberInfo>>> FindPhoneBook(ParameterizedMap map);
        BaseResult<GenericTypeResult<List<PhoneNumberInfo>>> ReadPhoneBook(ParameterizedMap map);
        BaseResult<GenericTypeResult<PhoneBookInfo>> GetInfo(ParameterizedMap map);
        BaseResult<GenericTypeResult<bool>> SetPhoneBookMemory(ParameterizedMap map);
        BaseResult<GenericTypeResult<bool>> WritePhoneBook(ParameterizedMap map);
    }
}
