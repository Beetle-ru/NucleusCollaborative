using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using System.Runtime.Serialization;

namespace Converter
{
    // Данные с PLC x.3	
    // Засыпка скрапа
    [Serializable]
    [DataContract]
    [PLCGroup(Location = "test_PLC", Destination = "Blockings")]
    [PLCGroup(Location = "PLC13", Destination = "Converter1")]
    [PLCGroup(Location = "PLC23", Destination = "Converter2")]
    [PLCGroup(Location = "PLC33", Destination = "Converter3")]
    [DBGroup(UnitNumber = 1)]
    [DBGroup(UnitNumber = 2)]
    [DBGroup(UnitNumber = 3)]
    public class ScrapChargingEvent : ConverterBaseEvent
    {
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,W42")]
        public int ScrapChargingFlag { set; get; }            // засыпка скрапа в конвертер 1-начата/0-закончен а     # ACT_CX_SCRCHARGING
    }
}
