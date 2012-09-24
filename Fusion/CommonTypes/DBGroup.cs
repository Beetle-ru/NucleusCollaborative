using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DBGroup : Attribute
    {
        public string DisplayName { get; set; }
        public string TableName { set; get; }
        public int UnitNumber { get; set; }
        public bool IsTrendGroup { get; set; }
        public string BindingPropertyName { get; set; }
    }
}
