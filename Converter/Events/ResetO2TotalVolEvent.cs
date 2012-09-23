using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Reflection;
using Core;

namespace Converter
{

    // факт.данные от PLC x.1						
    // Von:	PLC x.1	(x=номер конвертера)
    // Данные по продувке
    [Serializable]
    [DataContract]
    [PLCGroup(Location = "test_PLC", Destination = "Blowing")]
    [PLCGroup(Location = "PLC11", Destination = "Converter1")]
    [PLCGroup(Location = "PLC21", Destination = "Converter2")]
    [PLCGroup(Location = "PLC31", Destination = "Converter3")]
    [DBGroup(UnitNumber = 1)]
    [DBGroup(UnitNumber = 2)]
    [DBGroup(UnitNumber = 3)]
    public class ResetO2TotalVolEvent : ConverterBaseEvent
    {

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,W46")]
        public int O2TotalVol { set; get; }               // сброшенный общий O2 расход # ACT_CX_O2VOL_TOTAL_RUE

    }
}
