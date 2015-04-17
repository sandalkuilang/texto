﻿using GSMServer.Configuration;
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
using System.Xml;
using System.Xml.Linq;

namespace GSMServer.Contracts
{
    [Contract]
    public interface IConfiguration
    { 
        string GetRawXml(ParameterizedMap map);
        bool SetRawXml(ParameterizedMap map);
        bool Save(ParameterizedMap map); 
    }
}
