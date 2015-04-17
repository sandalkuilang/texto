/*
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
using System.IO;
using System.Security.Cryptography;

namespace Cryptography
{
    public class Hash : IHash
    {
        private HashAlgorithm hashAlgorithm;

        public Hash(HashAlgorithm Algorithm)
        {
            if (Algorithm != null)
                hashAlgorithm = Algorithm;
        }

        [System.Diagnostics.DebuggerStepThrough]
        public byte[] ComputeHash(Stream stream)
        {
            byte[] hash;
            hash = hashAlgorithm.ComputeHash(stream);
            return hash;
        }

        [System.Diagnostics.DebuggerStepThrough]
        public byte[] ComputeHash(string file)
        {
            byte[] hash;
            using (Stream stream = new FileStream(file, FileMode.Open))
            {
                hash = ComputeHash(stream);
            }
            return hash;
        }

        [System.Diagnostics.DebuggerStepThrough]
        public void Dispose()
        {
            hashAlgorithm.Dispose();
        }
    }
}
