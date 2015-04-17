using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSMServer.Worker.Model
{
    public class WeeklyTrigger
    { 
        public string ID { get; set; }
        public int RecursEvery { get; set; } 
        public int Sunday { get; set; } 
        public int Monday { get; set; } 
        public int Tuesday { get; set; } 
        public int Wednesday { get; set; } 
        public int Thursday { get; set; } 
        public int Friday { get; set; } 
        public int Saturday { get; set; } 
    }
}
