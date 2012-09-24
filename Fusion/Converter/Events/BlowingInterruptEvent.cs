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
    // Данные по прерываниям продувки
    [Serializable]
    [DataContract]
    [PLCGroup(Location = "test_PLC", Destination = "Blowing")]
    [PLCGroup(Location = "PLC11", Destination = "Converter1")]
    [PLCGroup(Location = "PLC21", Destination = "Converter2")]
    [PLCGroup(Location = "PLC31", Destination = "Converter3")]
    [DBGroup(UnitNumber = 1)]
    [DBGroup(UnitNumber = 2)]
    [DBGroup(UnitNumber = 3)]
    public class BlowingInterruptEvent : ConverterBaseEvent
    {

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,W0")]
        public int O2TotalVol { set; get; }               // общий O2 расход                   # ACT_CX_O2VOL_TOTAL

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,W24")]
        public int BlowingInterruptFlag { set; get; }     // прерывание продувки нач(1)/кон(0) # ACT_CX_BLOWINTERRUPT

    }
}
