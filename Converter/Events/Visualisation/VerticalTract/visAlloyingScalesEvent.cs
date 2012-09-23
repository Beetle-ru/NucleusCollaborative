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
    // Данные по весам легирующих вертикального тракта	
    [Serializable]
    [DataContract]
    [PLCGroup(Location = "test_PLC", Destination = "Additions")]
    [PLCGroup(Location = "PLC12", Destination = "Converter1")]
    [PLCGroup(Location = "PLC22", Destination = "Converter2")]
    [PLCGroup(Location = "PLC32", Destination = "Converter3")]
    [DBGroup(UnitNumber = 1)]
    [DBGroup(UnitNumber = 2)]
    [DBGroup(UnitNumber = 3)]
    public class visAlloyingScalesEvent : ConverterBaseEvent
    {
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,REAL36")]
        public double Scale9Weight { set; get; }               // Значение веса материала на весах 9 # AS32/DATA_OS.QP_DOZA_B14

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,REAL32")]
        public double Scale8Weight { set; get; }               // Значение веса материала на весах 8 # AS32/DATA_OS.QP_DOZA_B13

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,REAL20")]
        public double Scale3Weight { set; get; }               // Значение веса материала на весах 3 # AS32/DATA_OS.QP_DOZA_B5

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,REAL12")]
        public double Scale2Weight { set; get; }               // Значение веса материала на весах 2 # AS32/DATA_OS.QP_DOZA_B2

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,REAL4")]
        public double Scale1Weight { set; get; }               // Значение веса материала на весах 1 # AS32/DATA_OS.QP_DOZA_B1

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,REAL72")]
        public double Scale11Weight { set; get; }               // Значение веса материала на весах 11 # 32/DATA_OS.QP_DOZA_W11

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,REAL68")]
        public double Scale10Weight { set; get; }               // Значение веса материала на весах 10 # 32/DATA_OS.QP_DOZA_W10

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE382", IsBoolean = true, BitNumber = 0)]
        public bool Scale9Opened { set; get; }                 // Индикация выгрузки весов 9 "1" - выгрузка # AS32/DATA_OS.Q3_VIBW9

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE381", IsBoolean = true, BitNumber = 7)]
        public bool Scale8Opened { set; get; }                 // Индикация выгрузки весов 8 "1" - выгрузка # AS32/DATA_OS.Q3_VIBW8

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE382", IsBoolean = true, BitNumber = 1)]
        public bool Scale3Opened { set; get; }                 // Индикация выгрузки весов 3 "открыты" # AS32/DATA_OS.Q3_VIBW3

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB12,BYTE351", IsBoolean = true, BitNumber = 0)]
        public bool Scale3Closed { set; get; }                 // Индикация выгрузки весов 3 "закрыты" # DB12_W3_closed
        
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE381", IsBoolean = true, BitNumber = 6)]
        public bool Scale2Opened { set; get; }                 // Индикация выгрузки весов 2 "1" - выгрузка # AS32/DATA_OS.Q3_VIBW2

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE381", IsBoolean = true, BitNumber = 5)]
        public bool Scale1Opened { set; get; }                 // Индикация выгрузки весов 1 "1" - выгрузка # AS32/DATA_OS.Q3_VIBW1

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE396", IsBoolean = true, BitNumber = 2)]
        public bool Scale11Opened { set; get; }                 // Индикация затвора весов 11 "открыт" # AS32/DATA_OS.Q3_ZATW11

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE396", IsBoolean = true, BitNumber = 3)]
        public bool Scale11Closed { set; get; }                 // Индикация затвора весов 11 "закрыт" # AS32/DATA_OS.Q3_ZATW11Z

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE396", IsBoolean = true, BitNumber = 0)]
        public bool Scale10Opened { set; get; }                 // Индикация затвора весов 10 "открыт" # AS32/DATA_OS.Q3_ZATW10

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE396", IsBoolean = true, BitNumber = 1)]
        public bool Scale10Closed { set; get; }                 // Индикация затвора весов 10 "закрыт" # AS32/DATA_OS.Q3_ZATW10Z


    }
}
