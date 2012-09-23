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
    // Данные по бункерам легирующих вертикального тракта	
    [Serializable]
    [DataContract]
    [PLCGroup(Location = "test_PLC", Destination = "Additions")]
    [PLCGroup(Location = "PLC12", Destination = "Converter1")]
    [PLCGroup(Location = "PLC22", Destination = "Converter2")]
    [PLCGroup(Location = "PLC32", Destination = "Converter3")]
    [DBGroup(UnitNumber = 1)]
    [DBGroup(UnitNumber = 2)]
    [DBGroup(UnitNumber = 3)]
    public class visAlloyingBunkersEvent : ConverterBaseEvent
    {
        [DataMember]
        [DBPoint(IsStored = true, MaxSize = 15)]
        [PLCPoint(Location = "DB12,STRING212,8", Encoding = "x-cp1251")]
        public string Bunker16MaterialName { set; get; }                    // Наименование материала в расходном бункере 16 # AS32/AVARIYA.MATER_16

        [DataMember]
        [DBPoint(IsStored = true, MaxSize = 15)]
        [PLCPoint(Location = "DB12,STRING192,8", Encoding = "x-cp1251")]
        public string Bunker14MaterialName { set; get; }                    // Наименование материала в расходном бункере 14 # AS32/AVARIYA.MATER_14

        [DataMember]
        [DBPoint(IsStored = true, MaxSize = 15)]
        [PLCPoint(Location = "DB12,STRING202,8", Encoding = "x-cp1251")]
        public string Bunker15MaterialName { set; get; }                    // Наименование материала в расходном бункере 15 # AS32/AVARIYA.MATER_15

        [DataMember]
        [DBPoint(IsStored = true, MaxSize = 15)]
        [PLCPoint(Location = "DB12,STRING182,8", Encoding = "x-cp1251")]
        public string Bunker13MaterialName { set; get; }                    // Наименование материала в расходном бункере 13 # AS32/AVARIYA.MATER_13

        [DataMember]
        [DBPoint(IsStored = true, MaxSize = 15)]
        [PLCPoint(Location = "DB12,STRING92,8", Encoding = "x-cp1251")]
        public string Bunker4MaterialName { set; get; }                      // Наименование материала в расходном бункере 4 # AS32/AVARIYA.MATER_4

        [DataMember]
        [DBPoint(IsStored = true, MaxSize = 15)]
        [PLCPoint(Location = "DB12,STRING72,8", Encoding = "x-cp1251")]
        public string Bunker2MaterialName { set; get; }                      // Наименование материала в расходном бункере 2 # AS32/AVARIYA.MATER_2

        [DataMember]
        [DBPoint(IsStored = true, MaxSize = 15)]
        [PLCPoint(Location = "DB12,STRING82,8", Encoding = "x-cp1251")]
        public string Bunker3MaterialName { set; get; }                      // Наименование материала в расходном бункере 3 # AS32/AVARIYA.MATER_3

        [DataMember]
        [DBPoint(IsStored = true, MaxSize = 15)]
        [PLCPoint(Location = "DB12,STRING62,8", Encoding = "x-cp1251")]
        public string Bunker1MaterialName { set; get; }                      // Наименование материала в расходном бункере 1 # AS32/AVARIYA.MATER_1

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE384", IsBoolean=true, BitNumber=7)]
        public bool Bunker16FeederVibrating { set; get; }                    // Индикация работы вибропитателя расходного бункера 16 # AS32/DATA_OS.Q3_VIBB16

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE384", IsBoolean = true, BitNumber = 5)]
        public bool Bunker14FeederVibrating { set; get; }                    // Индикация работы вибропитателя расходного бункера 14 # AS32/DATA_OS.Q3_VIBB14

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE384", IsBoolean = true, BitNumber = 6)]
        public bool Bunker15FeederVibrating { set; get; }                    // Индикация работы вибропитателя расходного бункера 15 # AS32/DATA_OS.Q3_VIBB15

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE384", IsBoolean = true, BitNumber = 4)]
        public bool Bunker13FeederVibrating { set; get; }                    // Индикация работы вибропитателя расходного бункера 13 # AS32/DATA_OS.Q3_VIBB13

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE383", IsBoolean = true, BitNumber = 1)]
        public bool Bunker4FeederVibrating { set; get; }                     // Индикация работы вибропитателя расходного бункера 4 # AS32/DATA_OS.Q3_VIBB4

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE382", IsBoolean = true, BitNumber = 7)]
        public bool Bunker2FeederVibrating { set; get; }                      // Индикация работы вибропитателя расходного бункера 2 # AS32/DATA_OS.Q3_VIBB2

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE383", IsBoolean = true, BitNumber = 0)]
        public bool Bunker3FeederVibrating { set; get; }                      // Индикация работы вибропитателя расходного бункера 3 # AS32/DATA_OS.Q3_VIBB3

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE382", IsBoolean = true, BitNumber = 6)]
        public bool Bunker1FeederVibrating { set; get; }                      // Индикация работы вибропитателя расходного бункера 1 # AS32/DATA_OS.Q3_VIBB1

    }
}
