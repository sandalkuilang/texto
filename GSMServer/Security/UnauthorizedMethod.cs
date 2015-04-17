using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace GSMServer.Security
{
    internal class UnauthorizedCall
    {
        private static UnauthorizedCall instance;

        public static UnauthorizedCall Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UnauthorizedCall();
                }
                return instance;
            } 
            
        }
        private List<string> blockingMethods;
        private UnauthorizedCall()
        {
            blockingMethods = new List<string>();
            blockingMethods.Add("invokemethod");
        }

        public bool IsAuthorized(){
            StackTrace stackTrace = new StackTrace();           // get call stack
            StackFrame[] stackFrames = stackTrace.GetFrames();  // get method calls (frames) 
            string callerStack = blockingMethods.Where(x => x.ToLower() == stackFrames[2].GetMethod().Name.ToLower()).SingleOrDefault();
            if (!string.IsNullOrEmpty(callerStack))
            {
                return false;
            }
            return true;
        }
    }
}
