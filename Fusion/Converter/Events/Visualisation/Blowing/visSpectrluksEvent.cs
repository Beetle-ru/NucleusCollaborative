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
    // Данные химия со спектролюкса
    [Serializable]
    [DataContract]
    [PLCGroup(Location = "test_PLC", Destination = "Blowing")]
    [PLCGroup(Location = "PLC13", Destination = "Converter1")]
    [PLCGroup(Location = "PLC23", Destination = "Converter2")]
    [PLCGroup(Location = "PLC33", Destination = "Converter3")]
    [DBGroup(UnitNumber = 1)]
    [DBGroup(UnitNumber = 2)]
    [DBGroup(UnitNumber = 3)]
    public class visSpectrluksEvent : ConverterBaseEvent
    {
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB22,D44")]
        public Int64 HeatNumber { set; get; }                      // Номер плавки химанализа со "Спектролюкса" # DB22_StType_Steel

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB22,REAL48")]
        public double C { set; get; }                             // "С" со "Спектролюкса" # DB22_StAnal_C

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB22,REAL52")]
        public double Si { set; get; }                            // "Si" со "Спектролюкса" # DB22_StAnal_Si
        
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB22,REAL56")]
        public double Mn { set; get; }                            // "Mn" со "Спектролюкса" # DB22_StAnal_Mc
        
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB22,REAL60")]
        public double P { set; get; }                             // "P" со "Спектролюкса" # DB22_StAnal_P

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB22,REAL64")]
        public double S { set; get; }                             // "S" со "Спектролюкса" # DB22_StAnal_S

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB22,REAL134")]
        public double N2 { set; get; }                            // "N2" со "Спектролюкса" # DB22_StAnal_N2

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB22,REAL142")]
        public double Sn { set; get; }                            // "Sn" со "Спектролюкса" # DB22_StAnal_Sn
       
    }
}
