using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using System.Runtime.Serialization;

namespace Converter
{
    [Serializable]
    [DataContract]
    [PLCGroup(Location = "test_PLC", Destination = "Blowing")]
    [PLCGroup(Location = "PLC11", Destination = "Converter1")]
    [PLCGroup(Location = "PLC21", Destination = "Converter2")]
    [PLCGroup(Location = "PLC31", Destination = "Converter3")]
    [DBGroup(UnitNumber = 1)]
    [DBGroup(UnitNumber = 2)]
    [DBGroup(UnitNumber = 3)]
    public class HeatChangeEvent : ConverterBaseEvent
    {
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,D40")]
        public Int64 HeatNumber { set; get; }
    }
}
