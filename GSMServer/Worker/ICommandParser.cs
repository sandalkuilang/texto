using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSMServer.Worker
{
    internal interface ICommandParser<T>
    {
        T Parse(string input);
    }
}
