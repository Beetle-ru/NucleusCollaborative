using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class PLCPoint : Attribute
    {
        public string Location { get; set; }
        public string Encoding { get; set; }
        public bool IsBoolean { get; set; }
        public bool IsWritable { get; set; }
        public int BitNumber { get; set; }
    }
}
