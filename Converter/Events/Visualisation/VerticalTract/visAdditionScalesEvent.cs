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
    // Данные по весам добавочных материалов вертикального тракта	
    [Serializable]
    [DataContract]
    [PLCGroup(Location = "test_PLC", Destination = "Additions")]
    [PLCGroup(Location = "PLC12", Destination = "Converter1")]
    [PLCGroup(Location = "PLC22", Destination = "Converter2")]
    [PLCGroup(Location = "PLC32", Destination = "Converter3")]
    [DBGroup(UnitNumber = 1)]
    [DBGroup(UnitNumber = 2)]
    [DBGroup(UnitNumber = 3)]
    public class visAdditionScalesEvent : ConverterBaseEvent
    {
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,REAL56")]
        public double Scale7Weight { set; get; }               // Значение веса материала на весах 7 # AS32/DATA_OS.QP_DOZA_B10

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,REAL52")]
        public double Scale6Weight { set; get; }               // Значение веса материала на весах 6 # AS32/DATA_OS.QP_DOZA_B9

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,REAL48")]
        public double Scale5Weight { set; get; }               // Значение веса материала на весах 5 # AS32/DATA_OS.QP_DOZA_B8

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,REAL28")]
        public double Scale4Weight { set; get; }               // Значение веса материала на весах 4 # AS32/DATA_OS.QP_DOZA_B7

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,REAL20")]
        public double Scale3Weight { set; get; }               // Значение веса материала на весах 3 # AS32/DATA_OS.QP_DOZA_B6

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE382", IsBoolean = true, BitNumber=5 )]
        public bool Scale7Opened { set; get; }                 // Индикация выгрузки весов 7 "открыты"

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB12,BYTE351", IsBoolean = true, BitNumber = 4)]
        public bool Scale7Closed { set; get; }                 // Индикация выгрузки весов 7 "закрыты"

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE382", IsBoolean = true, BitNumber = 4)]
        public bool Scale6Opened { set; get; }                 // Индикация выгрузки весов 6 "открыты"

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB12,BYTE351", IsBoolean=true, BitNumber=3)]
        public bool Scale6Closed { set; get; }                 // Индикация выгрузки весов 6 "закрыты"

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE382", IsBoolean=true, BitNumber=3)]
        public bool Scale5Opened { set; get; }                 // Индикация выгрузки весов 5 "открыты"

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB12,BYTE351", IsBoolean=true, BitNumber=2)]
        public bool Scale5Closed { set; get; }                 // Индикация выгрузки весов 5 "закрыты"

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE382", IsBoolean=true, BitNumber=2)]
        public bool Scale4Opened { set; get; }                 // Индикация выгрузки весов 4 "открыты"

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB12,BYTE351", IsBoolean=true, BitNumber=1)]
        public bool Scale4Closed { set; get; }                 // Индикация выгрузки весов 4 "закрыты"

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE382", IsBoolean=true, BitNumber=1)]
        public bool Scale3Opened { set; get; }                 // Индикация выгрузки весов 3 "открыты"

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB12,BYTE351", IsBoolean=true, BitNumber=0)]
        public bool Scale3Closed { set; get; }                 // Индикация выгрузки весов 3 "закрыты"
    }
}
