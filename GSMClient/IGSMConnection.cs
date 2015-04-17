using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSMClient
{
    public interface IGSMConnection : IDisposable
    {
        bool IsOpen { get; }
        void Open();
        void Close();

    }
}
