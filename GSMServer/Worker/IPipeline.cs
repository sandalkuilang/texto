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
using GSMServer.Contracts;
using Sockets;

namespace GSMServer.Worker
{
    internal interface IPipeline
    {
        string BeforePush(ParameterizedMap map);
        string Push(ParameterizedMap map);
        string AfterPush(ParameterizedMap map);
        string BeforePull(ParameterizedMap map);
        string Pull(ParameterizedMap map);
        string AfterPull(ParameterizedMap map);
    }
}
