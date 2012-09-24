using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NordSteel.Data
{
    public class TrendPoint
    {
        private string _ShortName;
        public string Name { get; set; }
        public string ShortName { get { return string.IsNullOrEmpty(_ShortName) ? Name: _ShortName;  } set { _ShortName = value; } }
        public string PropertyName { get; set; }

        public double MinValue { get; set; }
        public double MaxValue { get; set; }

        public TrendGroup ParentGroup { get; set; }

        

    }
}
