using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSMClient
{
    interface IGSMCommand
    {
        IGSMConnection Connection { get; set; } 
        int Write(string data); 
        int Write(byte[] data, int startIndex, int length);
    }
}
