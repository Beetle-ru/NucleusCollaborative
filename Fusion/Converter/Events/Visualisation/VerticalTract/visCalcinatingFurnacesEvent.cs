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
    // Данные по печам прокаливания вертикального тракта	
    [Serializable]
    [DataContract]
    [PLCGroup(Location = "test_PLC", Destination = "Additions")]
    [PLCGroup(Location = "PLC12", Destination = "Converter1")]
    [PLCGroup(Location = "PLC22", Destination = "Converter2")]
    [PLCGroup(Location = "PLC32", Destination = "Converter3")]
    [DBGroup(UnitNumber = 1)]
    [DBGroup(UnitNumber = 2)]
    [DBGroup(UnitNumber = 3)]
    public class visCalcinatingFurnacesEvent : ConverterBaseEvent
    {
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE394", IsBoolean = true, BitNumber = 3)]
        public bool SlidingTrapOnFurnace4 { set; get; }      // Положение шибера на печь 4 # AS32/DATA_OS.Q3_Z2P4

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE394", IsBoolean = true, BitNumber = 2)]
        public bool SlidingTrapOnFurnace3 { set; get; }      // Положение шибера на печь 3 # AS32/DATA_OS.Q3_Z2P3

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE393", IsBoolean = true, BitNumber = 1)]
        public bool SlidingTrapOnFurnace2 { set; get; }      // Положение шибера на печь 2 # AS32/DATA_OS.Q3_Z2P2

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE393", IsBoolean = true, BitNumber = 0)]
        public bool SlidingTrapOnFurnace1 { set; get; }      // Положение шибера на печь 1 # AS32/DATA_OS.Q3_Z2P1

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE395", IsBoolean = true, BitNumber = 6)]
        public bool Furnace4SpoutOnScales11 { set; get; }    // Индикация течки печи прокаливания 4 "на весы 11" # AS32/DATA_OS.Q3_TP4W11

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE395", IsBoolean = true, BitNumber = 7)]
        public bool Furnace4Spout { set; get; }              // Индикация течки печи прокаливания 4 "в печь 4" # AS32/DATA_OS.Q3_TP4

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE395", IsBoolean = true, BitNumber = 4)]
        public bool Furnace3SpoutOnScales11 { set; get; }    // Индикация течки печи прокаливания 3 "на весы 11" # AS32/DATA_OS.Q3_TP3W11

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE395", IsBoolean = true, BitNumber = 5)]
        public bool Furnace3Spout { set; get; }              // Индикация течки печи прокаливания 3 "в печь 3" # AS32/DATA_OS.Q3_TP3

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE392", IsBoolean = true, BitNumber = 6)]
        public bool Furnace2SpoutOnScales10 { set; get; }    // Индикация течки печи прокаливания 2 "на весы 10" # AS32/DATA_OS.Q3_TP2W10

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE392", IsBoolean = true, BitNumber = 7)]
        public bool Furnace2Spout { set; get; }              // Индикация течки печи прокаливания 2 "в печь 2" # AS32/DATA_OS.Q3_TP2

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE392", IsBoolean = true, BitNumber = 4)]
        public bool Furnace1SpoutOnScales10 { set; get; }    // Индикация течки печи прокаливания 1 "на весы 10" # AS32/DATA_OS.Q3_TP1W10

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE392", IsBoolean = true, BitNumber = 5)]
        public bool Furnace1Spout { set; get; }              // Индикация течки печи прокаливания 1 "в печь 1" # AS32/DATA_OS.Q3_TP1

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE394", IsBoolean = true, BitNumber = 5)]
        public bool Furnace4ArchOpened { set; get; }         // Индикация открытия свода печи 4 "открыт" # AS32/DATA_OS.Q3_SVP4

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE394", IsBoolean = true, BitNumber = 7)]
        public bool Furnace4ArchClosed { set; get; }         // Индикация открытия свода печи 4 "закрыт" # AS32/DATA_OS.Q3_SVP4ZAK

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE394", IsBoolean = true, BitNumber = 4)]
        public bool Furnace3ArchOpened { set; get; }         // Индикация открытия свода печи 3 "открыт" # AS32/DATA_OS.Q3_SVP3

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE394", IsBoolean = true, BitNumber = 6)]
        public bool Furnace3ArchClosed { set; get; }         // Индикация открытия свода печи 3 "закрыт" # AS32/DATA_OS.Q3_SVP3ZAK

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE393", IsBoolean = true, BitNumber = 3)]
        public bool Furnace2ArchOpened { set; get; }         // Индикация открытия свода печи 2 "открыт" # AS32/DATA_OS.Q3_SVP2

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE393", IsBoolean = true, BitNumber = 5)]
        public bool Furnace2ArchClosed { set; get; }         // Индикация открытия свода печи 2 "закрыт" # AS32/DATA_OS.Q3_SVP2ZAK

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE393", IsBoolean = true, BitNumber = 2)]
        public bool Furnace1ArchOpened { set; get; }         // Индикация открытия свода печи 1 "открыт" # AS32/DATA_OS.Q3_SVP1

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE393", IsBoolean = true, BitNumber = 4)]
        public bool Furnace1ArchClosed { set; get; }         // Индикация открытия свода печи 1 "закрыт" # AS32/DATA_OS.Q3_SVP1ZAK

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE390", IsBoolean = true, BitNumber = 1)]
        public bool Furnace4Unloading { set; get; }         // Адрес выгрузки в печь 4 # AS32/DATA_OS.Q3_SIGP4

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE390", IsBoolean = true, BitNumber = 0)]
        public bool Furnace3Unloading { set; get; }         // Адрес выгрузки в печь 3 # AS32/DATA_OS.Q3_SIGP3

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE389", IsBoolean = true, BitNumber = 7)]
        public bool Furnace2Unloading { set; get; }         // Адрес выгрузки в печь 2 # AS32/DATA_OS.Q3_SIGP2

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE389", IsBoolean = true, BitNumber = 6)]
        public bool Furnace1Unloading { set; get; }         // Адрес выгрузки в печь 1 # AS32/DATA_OS.Q3_SIGP1

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE395", IsBoolean = true, BitNumber = 1)]
        public bool Furnace4BadOpened { set; get; }      // Индикация пода печи 4 "открыт" # AS32/DATA_OS.Q3_PODP4

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE395", IsBoolean = true, BitNumber = 3)]
        public bool Furnace4BadClosed { set; get; }      // Индикация пода печи 4 "закрыт" # AS32/DATA_OS.Q3_PODP4Z

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE395", IsBoolean = true, BitNumber = 0)]
        public bool Furnace3BadOpened { set; get; }      // Индикация пода печи 3 "открыт" # AS32/DATA_OS.Q3_PODP3

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE395", IsBoolean = true, BitNumber = 2)]
        public bool Furnace3BadClosed { set; get; }      // Индикация пода печи 3 "закрыт" # AS32/DATA_OS.Q3_PODP3Z

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE393", IsBoolean = true, BitNumber = 7)]
        public bool Furnace2BadOpened { set; get; }      // Индикация пода печи 2 "открыт" # AS32/DATA_OS.Q3_PODP2

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE394", IsBoolean = true, BitNumber = 1)]
        public bool Furnace2BadClosed { set; get; }      // Индикация пода печи 2 "закрыт" # AS32/DATA_OS.Q3_PODP2Z

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE393", IsBoolean = true, BitNumber = 6)]
        public bool Furnace1BadOpened { set; get; }      // Индикация пода печи 1 "открыт" # AS32/DATA_OS.Q3_PODP1

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE394", IsBoolean = true, BitNumber = 0)]
        public bool Furnace1BadClosed { set; get; }      // Индикация пода печи 1 "закрыт" # AS32/DATA_OS.Q3_PODP1Z

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE391", IsBoolean = true, BitNumber = 3)]
        public bool Scales11OverSpout4 { set; get; }      // Адрес выгрузки в весы 11 через течку печи 4 # AS32/DATA_OS.Q3_SIGW11P4

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE391", IsBoolean = true, BitNumber = 2)]
        public bool Scales11OverSpout3 { set; get; }      // Адрес выгрузки в весы 11 через течку печи 3 # AS32/DATA_OS.Q3_SIGW11P3

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE390", IsBoolean = true, BitNumber = 3)]
        public bool Scales10OverSpout2 { set; get; }      // Адрес выгрузки в весы 10 через течку печи 2 # AS32/DATA_OS.Q3_SIGW10P2

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE390", IsBoolean = true, BitNumber = 2)]
        public bool Scales10OverSpout1 { set; get; }      // Адрес выгрузки в весы 10 через течку печи 1 # AS32/DATA_OS.Q3_SIGW10P1
    }
}
