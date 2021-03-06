﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Reflection;
using Core;
namespace Converter
{

    // факт.данные от PLC x.3						
    // Von:	PLC x.3	(x=номер конвертера)
    // Данные по сливу стали	
    [Serializable]
    [DataContract]
    [PLCGroup(Location = "test_PLC", Destination = "Blockings")]
    [PLCGroup(Location = "PLC13", Destination = "Converter1")]
    [PLCGroup(Location = "PLC23", Destination = "Converter2")]
    [PLCGroup(Location = "PLC33", Destination = "Converter3")]
    [DBGroup(UnitNumber = 1)]
    [DBGroup(UnitNumber = 2)]
    [DBGroup(UnitNumber = 3)]
    public class TappingEvent : ConverterBaseEvent
    {
        
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,W34")]
        public int TappingFlag { set; get; }           // слив стали нач(1)/кон(0)          # ACT_CX_TAPPING

    }
}
