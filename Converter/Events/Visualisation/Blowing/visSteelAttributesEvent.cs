using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Reflection;
using Core;
namespace Converter
{

    // факт.данные от PLC x.3 (Визиуализация)
    // Von:	PLC x.3	(x=номер конвертера)
    // Данные по номеру плавки текущей продувки	
    [Serializable]
    [DataContract]
    [PLCGroup(Location = "test_PLC", Destination = "Blockings")]
    [PLCGroup(Location = "PLC13", Destination = "Converter1")]
    [PLCGroup(Location = "PLC23", Destination = "Converter2")]
    [PLCGroup(Location = "PLC33", Destination = "Converter3")]
    [DBGroup(UnitNumber = 1)]
    [DBGroup(UnitNumber = 2)]
    [DBGroup(UnitNumber = 3)]
    public class visSteelAttributesEvent : ConverterBaseEvent
    {
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB22,STRING26,16", Encoding = "x-cp1251")]
        public string Grade { set; get; }                                   // Марка стали # AS33/T3_povalka.StType

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB22,W128")]
        public int PlannedTemperature { set; get; }                      // Заданная температура # DB22_TempDst
    }
}
