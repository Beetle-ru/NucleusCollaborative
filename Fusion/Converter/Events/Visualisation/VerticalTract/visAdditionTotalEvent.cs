using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Reflection;
using Core;
namespace Converter
{

    // факт.данные от PLC x.2 (Визиуализация)
    // Von:	PLC x.2	(x=номер конвертера)
    // Данные по весам отданным в конвертер из бункеров нарастающим итогом	
    [Serializable]
    [DataContract]
    [PLCGroup(Location = "test_PLC", Destination = "Additions")]
    [PLCGroup(Location = "PLC12", Destination = "Converter1")]
    [PLCGroup(Location = "PLC22", Destination = "Converter2")]
    [PLCGroup(Location = "PLC32", Destination = "Converter3")]
    [DBGroup(UnitNumber = 1)]
    [DBGroup(UnitNumber = 2)]
    [DBGroup(UnitNumber = 3)]
    public class visAdditionTotalEvent : ConverterBaseEvent
    {
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "MREAL1384")]
        public double RB5TotalWeight { set; get; }               // Суммарный вес материала, отданного в конвертер из бункера 5 # M_B5MB1C

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "MREAL1388")]
        public double RB6TotalWeight { set; get; }               // Суммарный вес материала, отданного в конвертер из бункера 6 # M_B6MB1C

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "MREAL1392")]
        public double RB7TotalWeight { set; get; }               // Суммарный вес материала, отданного в конвертер из бункера 7 # M_B7MB1C

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "MREAL1396")]
        public double RB8TotalWeight { set; get; }               // Суммарный вес материала, отданного в конвертер из бункера 8 # M_B8MB1C

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "MREAL1400")]
        public double RB9TotalWeight { set; get; }               // Суммарный вес материала, отданного в конвертер из бункера 9 # M_B9MB2C

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "MREAL1404")]
        public double RB10TotalWeight { set; get; }               // Суммарный вес материала, отданного в конвертер из бункера 10 # M_B10MB2C

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "MREAL1408")]
        public double RB11TotalWeight { set; get; }               // Суммарный вес материала, отданного в конвертер из бункера 11 # M_B11MB2C

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "MREAL1412")]
        public double RB12TotalWeight { set; get; }               // Суммарный вес материала, отданного в конвертер из бункера 12 # M_B12MB2C
    }
}
