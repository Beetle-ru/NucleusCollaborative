using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using System.Runtime.Serialization;

namespace Converter
{
    // Данные с PLC x.3	
    // Замер измерительного зонда
    [Serializable]
    [DataContract]
    [PLCGroup(Location = "test_PLC", Destination = "Blockings")]
    [PLCGroup(Location = "PLC13", Destination = "Converter1")]
    [PLCGroup(Location = "PLC23", Destination = "Converter2")]
    [PLCGroup(Location = "PLC33", Destination = "Converter3")]
    [DBGroup(UnitNumber = 1)]
    [DBGroup(UnitNumber = 2)]
    [DBGroup(UnitNumber = 3)]
    public class SublanceStartEvent : ConverterBaseEvent
    {
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,W22")]
        public int SublanceStartFlag { set; get; }            // замер изм. зонда 1-начат/0-закончен     # ACT_CX_SUBLANCE
    }
}
