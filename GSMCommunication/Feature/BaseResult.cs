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

namespace GSMCommunication.Feature
{
    public class BaseResult<T> where T : new()
    {
        public BaseResult()
        {
            Response = new T();
        }
        public string ID { get; set; }
        public T Response { get; set; } 
        public TimeSpan ExecutionTime { get; set; } 
    }
}
