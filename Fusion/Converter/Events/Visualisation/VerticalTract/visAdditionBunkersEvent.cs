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
    // Данные по бункерам добавочных материалов вертикального тракта	
    [Serializable]
    [DataContract]
    [PLCGroup(Location = "test_PLC", Destination = "Additions")]
    [PLCGroup(Location = "PLC12", Destination = "Converter1")]
    [PLCGroup(Location = "PLC22", Destination = "Converter2")]
    [PLCGroup(Location = "PLC32", Destination = "Converter3")]
    [DBGroup(UnitNumber = 1)]
    [DBGroup(UnitNumber = 2)]
    [DBGroup(UnitNumber = 3)]
    public class visAdditionBunkersEvent : ConverterBaseEvent
    {
        [DataMember]
        [DBPoint(IsStored = true, MaxSize = 15)]
        [PLCPoint(Location = "DB12,STRING172,8", Encoding = "x-cp1251")]
        public string Bunker12MaterialName { set; get; }                    // Наименование материала в расходном бункере 12 # AS32/AVARIYA.MATER_12

        [DataMember]
        [DBPoint(IsStored = true, MaxSize = 15)]
        [PLCPoint(Location = "DB12,STRING162,8", Encoding = "x-cp1251")]
        public string Bunker11MaterialName { set; get; }                    // Наименование материала в расходном бункере 11 # AS32/AVARIYA.MATER_11

        [DataMember]
        [DBPoint(IsStored = true, MaxSize = 15)]
        [PLCPoint(Location = "DB12,STRING152,8", Encoding = "x-cp1251")]
        public string Bunker10MaterialName { set; get; }                    // Наименование материала в расходном бункере 10 # AS32/AVARIYA.MATER_10

        [DataMember]
        [DBPoint(IsStored = true, MaxSize = 15)]
        [PLCPoint(Location = "DB12,STRING142,8", Encoding = "x-cp1251")]
        public string Bunker9_2MaterialName { set; get; }                    // Наименование материала в расходном бункере 9.2 # AS32/AVARIYA.MATER_9

        [DataMember]
        [DBPoint(IsStored = true, MaxSize = 15)]
        [PLCPoint(Location = "DB12,STRING142,8", Encoding = "x-cp1251")]
        public string Bunker9_1MaterialName { set; get; }                    // Наименование материала в расходном бункере 9.1 # AS32/AVARIYA.MATER_9

        [DataMember]
        [DBPoint(IsStored = true, MaxSize = 15)]
        [PLCPoint(Location = "DB12,STRING132,8", Encoding = "x-cp1251")]
        public string Bunker8_2MaterialName { set; get; }                    // Наименование материала в расходном бункере 8.2 # AS32/AVARIYA.MATER_8

        [DataMember]
        [DBPoint(IsStored = true, MaxSize = 15)]
        [PLCPoint(Location = "DB12,STRING132,8", Encoding = "x-cp1251")]
        public string Bunker8_1MaterialName { set; get; }                    // Наименование материала в расходном бункере 8.1 # AS32/AVARIYA.MATER_8

        [DataMember]
        [DBPoint(IsStored = true, MaxSize = 15)]
        [PLCPoint(Location = "DB12,STRING122,8", Encoding = "x-cp1251")]
        public string Bunker7MaterialName { set; get; }                      // Наименование материала в расходном бункере 7 # AS32/AVARIYA.MATER_7

        [DataMember]
        [DBPoint(IsStored = true, MaxSize = 15)]
        [PLCPoint(Location = "DB12,STRING112,8", Encoding = "x-cp1251")]
        public string Bunker6MaterialName { set; get; }                      // Наименование материала в расходном бункере 6 # AS32/AVARIYA.MATER_6

        [DataMember]
        [DBPoint(IsStored = true, MaxSize = 15)]
        [PLCPoint(Location = "DB12,STRING102,8", Encoding = "x-cp1251")]
        public string Bunker5MaterialName { set; get; }                      // Наименование материала в расходном бункере 5 # AS32/AVARIYA.MATER_5

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE384", IsBoolean=true, BitNumber=3)]
        public bool Bunker12FeederVibrating { set; get; }                    // Индикация работы вибропитателя расходного бункера 12 # AS32/DATA_OS.Q3_VIBB12

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE384", IsBoolean = true, BitNumber = 2)]
        public bool Bunker11FeederVibrating { set; get; }                    // Индикация работы вибропитателя расходного бункера 11 # AS32/DATA_OS.Q3_VIBB11

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE384", IsBoolean = true, BitNumber = 1)]
        public bool Bunker10FeederVibrating { set; get; }                    // Индикация работы вибропитателя расходного бункера 10 # AS32/DATA_OS.Q3_VIBB10

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE384", IsBoolean = true, BitNumber = 0)]
        public bool Bunker9_2FeederVibrating { set; get; }                    // Индикация работы вибропитателя расходного бункера 9_2 # AS32/DATA_OS.Q3_VIBB9_2

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE383", IsBoolean = true, BitNumber = 7)]
        public bool Bunker9_1FeederVibrating { set; get; }                    // Индикация работы вибропитателя расходного бункера 9_1 # AS32/DATA_OS.Q3_VIBB9_1

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE383", IsBoolean = true, BitNumber = 6)]
        public bool Bunker8_2FeederVibrating { set; get; }                    // Индикация работы вибропитателя расходного бункера 8_2 # AS32/DATA_OS.Q3_VIBB8_2

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE383", IsBoolean = true, BitNumber = 5)]
        public bool Bunker8_1FeederVibrating { set; get; }                    // Индикация работы вибропитателя расходного бункера 8_1 # AS32/DATA_OS.Q3_VIBB8_1

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE383", IsBoolean = true, BitNumber = 4)]
        public bool Bunker7FeederVibrating { set; get; }                      // Индикация работы вибропитателя расходного бункера 7 # AS32/DATA_OS.Q3_VIBB7

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE383", IsBoolean = true, BitNumber = 3)]
        public bool Bunker6FeederVibrating { set; get; }                      // Индикация работы вибропитателя расходного бункера 6 # AS32/DATA_OS.Q3_VIBB6

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE383",IsBoolean=true, BitNumber=2)]
        public bool Bunker5FeederVibrating { set; get; }                      // Индикация работы вибропитателя расходного бункера 5 # AS32/DATA_OS.Q3_VIBB5

    }
}
