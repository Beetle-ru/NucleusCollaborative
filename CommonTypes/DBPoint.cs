using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class DBPoint : Attribute
    {
        public string DisplayName { set; get; }
        public string DisplayShortName { set; get; }
        public bool IsTrendPoint { set; get; }
        public bool IsStored { get; set; }
        public int MaxSize { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
    }
}
