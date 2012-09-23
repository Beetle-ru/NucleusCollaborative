using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace OPC
{
    public class Point
    {
        public string Location { set; get; }

        public Type Type { set; get; }

        public Object Value { set; get; }

        public string FieldName { set; get; }

        public string Encoding { get; set; }

        public int ServerHandle { get; set; }

        public bool IsBoolean { get; set; }

        public int BitNumber { get; set; }

        public string OPCLocation
        {
            get
            {
                //PLC01:DB2,W0 => S7:[PLC01]DB2,int0
                //                S7:
                return Location.Replace("W", "int").Replace("STRING", "CHAR");
               // return Location.Replace("W", "int");
            }
        }
    }
}
