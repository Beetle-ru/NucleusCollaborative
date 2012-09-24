using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaterialSpecificationsReferenceWindow
{
    public class MaterialReference
    {
        public int ID { set; get; }
        public string NameEnglish { set; get; }
        public string NameOther { set; get; }
    }

    public class MaterialAnalysesReference
    {
        public int ID { set; get; }
        public string ElementName { set; get; }
        public double ElementValue { set; get; }
    }
}

