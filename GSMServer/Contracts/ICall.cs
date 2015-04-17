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
    public interface ICall
    {
        BaseResult<GenericTypeResult<string>> Dial(ParameterizedMap map);
        BaseResult<GenericTypeResult<bool>> Hang(ParameterizedMap map);
        BaseResult<GenericTypeResult<bool>> Answer(ParameterizedMap map);
        BaseResult<USSDResult> SendUSSD(ParameterizedMap map);
    }
}
