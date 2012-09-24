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
    // Данные по промышленным бункерам добавочных материалов вертикального тракта	
    [Serializable]
    [DataContract]
    [PLCGroup(Location = "test_PLC", Destination = "Additions")]
    [PLCGroup(Location = "PLC12", Destination = "Converter1")]
    [PLCGroup(Location = "PLC22", Destination = "Converter2")]
    [PLCGroup(Location = "PLC32", Destination = "Converter3")]
    [DBGroup(UnitNumber = 1)]
    [DBGroup(UnitNumber = 2)]
    [DBGroup(UnitNumber = 3)]
    public class visIndustrialBunkersEvent : ConverterBaseEvent
    {
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE392", IsBoolean = true, BitNumber = 3)]
        public bool Bunker2Minimum { set; get; }                                    // Индикация минимального сигнала промбункера 2 # AS32/DATA_OS.Q3_MINB22

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE392", IsBoolean = true, BitNumber = 2)]
        public bool Bunker1Minimum { set; get; }                                    // Индикация минимального сигнала промбункера 1 # AS32/DATA_OS.Q3_MINB21

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE385", IsBoolean = true, BitNumber = 2)]
        public bool Bunker2Opened { set; get; }                                     // Индикация затвора промбункера 2 "открыт" # AS32/DATA_OS.Q3_B22OTK

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE385", IsBoolean = true, BitNumber = 3)]
        public bool Bunker2Closed { set; get; }                                     // Индикация затвора промбункера 2 "закрыт" # AS32/DATA_OS.Q3_B22ZAK

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE385", IsBoolean = true, BitNumber = 0)]
        public bool Bunker1Opened { set; get; }                                     // Индикация затвора промбункера 1 "открыт" # AS32/DATA_OS.Q3_B21OTK

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE385", IsBoolean = true, BitNumber = 1)]
        public bool Bunker1Closed { set; get; }                                     // Индикация затвора промбункера 1 "закрыт" # AS32/DATA_OS.Q3_B21ZAK
    }
}
