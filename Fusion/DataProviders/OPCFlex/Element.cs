using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConnectionProvider;
using Converter;
using Implements;

namespace OPCFlex
{
    public class Element
    {
        public int sHandle, cHandle;
        public object val;
        public string opcItemID;

        public Element(string ItemID)
        {
            opcItemID = ItemID;
            sHandle = Int32.MinValue;
            cHandle = Int32.MinValue;
        }
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("hClient {0}; hServer {1}; opcItemID \"{2}\"", cHandle, sHandle, opcItemID);
            if (val != null)
            {
                sb.AppendFormat(";\nval {0}", val);
            }
            return "{" + sb.ToString() + "}";
        }
    }
}
