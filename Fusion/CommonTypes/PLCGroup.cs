using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class PLCGroup : Attribute
    {
        public string Name { set; get; }
        public string Location { get; set; }
        public string Destination { get; set; }
        public string FilterPropertyName { get; set; }
        public string FilterPropertyValue { get; set; }
    }
}
