﻿using System;
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
    // Данные по следующим за главной продувкам
    [Serializable]
    [DataContract]
    [PLCGroup(Location = "test_PLC", Destination = "Blowing")]
    [PLCGroup(Location = "PLC11", Destination = "Converter1")]
    [PLCGroup(Location = "PLC21", Destination = "Converter2")]
    [PLCGroup(Location = "PLC31", Destination = "Converter3")]
    [DBGroup(UnitNumber = 1)]
    [DBGroup(UnitNumber = 2)]
    [DBGroup(UnitNumber = 3)]
    public class ReBlowingEvent : ConverterBaseEvent
    {
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,W0")]
        public int O2TotalVol { set; get; }               // общий O2 расход                                        # ACT_CX_O2VOL_TOTAL

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,W22")]
        public int BlowingFlag { set; get; }              // продувка нач/кон 1=старт продувки, 0=продувка продувки  # ACT_CX_REBLOWING
    }
}
