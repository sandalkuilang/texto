using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSMCommunication.Feature
{
    public class StringResult : Result
    {
        public string Result { get; set; }

        public override string TypeName
        {
            get { return this.GetType().Name; }
        }
    }
}
