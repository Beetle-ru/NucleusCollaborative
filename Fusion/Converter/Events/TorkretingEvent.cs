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
    // Данные по торкретированию	
    [Serializable]
    [DataContract]
    [PLCGroup(Location = "test_PLC", Destination = "Blowing")]
    [PLCGroup(Location = "PLC11", Destination = "Converter1")]
    [PLCGroup(Location = "PLC21", Destination = "Converter2")]
    [PLCGroup(Location = "PLC31", Destination = "Converter3")]
    [DBGroup(UnitNumber = 1)]
    [DBGroup(UnitNumber = 2)]
    [DBGroup(UnitNumber = 3)]
    public class TorkretingEvent : ConverterBaseEvent
    {
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,W14")]
        public int O2TorkVol { set; get; }                // O2 расход на торкретирование           # ACT_CX_O2VOLTORK

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,W26")]
        public int TorkretingFlag { set; get; }           // торкретирование нач(1)/кон(0)          # ACT_CX_SKULLCUTTING

    }
}
