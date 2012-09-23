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
    // Данные по расходу азота на раздувку шлака	
    [Serializable]
    [DataContract]
    [PLCGroup(Location = "test_PLC", Destination = "Blowing")]
    [PLCGroup(Location = "PLC11", Destination = "Converter1")]
    [PLCGroup(Location = "PLC21", Destination = "Converter2")]
    [PLCGroup(Location = "PLC31", Destination = "Converter3")]
    [DBGroup(UnitNumber = 1)]
    [DBGroup(UnitNumber = 2)]
    [DBGroup(UnitNumber = 3)]
    public class SlagBlowingEvent : ConverterBaseEvent
    {
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,W16")]
        public int NVol { set; get; }                     // N расход азота на раздувку шлака       # ACT_CX_NVOLSLAGBLOW

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,W28")]
        public int SlagBlowingFlag { set; get; }          // N раздувка шлака нач(1)/кон(0)         # ACT_CX_SLAGBLOWING
    }
}
