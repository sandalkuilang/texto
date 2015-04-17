using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Texaco.Container;

namespace SpamObstructor
{
    internal class ObjectPool
    {
        private static IContainer instance;
        public static IContainer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Container("texto-container");
                }
                return instance;
            }
        }
    }
}
