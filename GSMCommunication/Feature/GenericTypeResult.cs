using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSMCommunication.Feature
{
    public class GenericTypeResult<T> : Result
    {
        public T Result { get; set; }

        public override string TypeName
        {
            get { return this.GetType().Name; }
        }
    }
}
