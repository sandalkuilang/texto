﻿/*
    Cryptography
 
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
using System.Security.Cryptography;
using System.IO;

namespace Cryptography
{
    public class SHA512Hash : IHash, IDisposable
    {
        private Hash hash;

        public SHA512Hash()
        {
            hash = new Hash(new SHA512Managed());
        }

        [System.Diagnostics.DebuggerStepThrough]
        public byte[] ComputeHash(Stream stream)
        {
            return hash.ComputeHash(stream);
        }

        [System.Diagnostics.DebuggerStepThrough]
        public byte[] ComputeHash(string file)
        {
            return hash.ComputeHash(file);
        }

        [System.Diagnostics.DebuggerStepThrough]
        public void Dispose()
        {
            hash.Dispose();
        }
    }
}