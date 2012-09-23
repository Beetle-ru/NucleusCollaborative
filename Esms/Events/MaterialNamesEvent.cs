using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;

//Коды материалов
namespace Esms
{
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "MaterialNamesEvent2", Location = "PLC3", Destination = "ESMS2")]
    [DBGroup(UnitNumber = 2)]
    public class MaterialNamesEvent : EsmsBaseEvent
    {
        //29 Название материала в бункере 1-16
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,STRING76,16", Encoding = "x-cp1251")]
        public string Bunker1t16 { set; get; }        //DB153.S28,16
        //30 Название материала в бункере 2-16
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,STRING92,16", Encoding = "x-cp1251")]
        public string Bunker2t16 { set; get; }        //DB153.S44,16
        //31 Название материала в бункере 3-16
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,STRING108,16", Encoding = "x-cp1251")]
        public string Bunker3t16 { set; get; }        //DB153.S60,16
        //32 Название материала в бункере 4-16
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,STRING124,16", Encoding = "x-cp1251")]
        public string Bunker4t16 { set; get; }        //DB153.S76,16
        //33 Название материала в бункере 5-16
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,STRING140,16", Encoding = "x-cp1251")]
        public string Bunker5t16 { set; get; }        //DB153.S92,16
        //34 Название материала в бункере 6-16
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,STRING156,16", Encoding = "x-cp1251")]
        public string Bunker6t16 { set; get; }        //DB153.S108,16
        //35 Название материала в бункере 7-16
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,STRING172,16", Encoding = "x-cp1251")]
        public string Bunker7t16 { set; get; }        //DB153.S124,16
        //36 Название материала в бункере 8-16
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,STRING188,16", Encoding = "x-cp1251")]
        public string Bunker8t16 { set; get; }        //DB153.S140,16
        //37 Название материала в бункере 9-16
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,STRING204,16", Encoding = "x-cp1251")]
        public string Bunker9t16 { set; get; }        //DB153.S156,16
        //38 Название материала в бункере 10-16
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,STRING220,16", Encoding = "x-cp1251")]
        public string Bunker10t16 { set; get; }        //DB153.S172,16
        //53. Название материала в бункере 1-17
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,STRING236,16", Encoding = "x-cp1251")]
        public string Bunker1t17 { set; get; }        //DB153.S188,16
        //54 Название материала в бункере 2-17
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,STRING252,16", Encoding = "x-cp1251")]
        public string Bunker2t17 { set; get; }        //DB153.S204,16
        //55 Название материала в бункере 3-17
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,STRING268,16", Encoding = "x-cp1251")]
        public string Bunker3t17 { set; get; }        //DB153.S220,16
        //56 Название материала в бункере 4-17
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,STRING284,16", Encoding = "x-cp1251")]
        public string Bunker4t17 { set; get; }        //DB153.S236,16
    }
}  
