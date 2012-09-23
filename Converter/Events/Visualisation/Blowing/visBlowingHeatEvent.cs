using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Reflection;
using Core;
namespace Converter
{

    // факт.данные от PLC x.1 (Визиуализация)
    // Von:	PLC x.1	(x=номер конвертера)
    // Данные по номеру плавки текущей продувки	
    [Serializable]
    [DataContract]
    [PLCGroup(Location = "test_PLC", Destination = "Blowing")]
    [PLCGroup(Location = "PLC11", Destination = "Converter1")]
    [PLCGroup(Location = "PLC21", Destination = "Converter2")]
    [PLCGroup(Location = "PLC31", Destination = "Converter3")]
    [DBGroup(UnitNumber = 1)]
    [DBGroup(UnitNumber = 2)]
    [DBGroup(UnitNumber = 3)]
    public class visBlowingHeatEvent : ConverterBaseEvent
    {
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,W138")]
        public int CurrentHeatNumber { set; get; }                      // Номер плавки # AS31/REC_S31.BloN
    }
}
