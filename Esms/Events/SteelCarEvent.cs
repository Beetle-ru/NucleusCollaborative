using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;

//Сталевоз
namespace Esms
{
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "SteelCarEvent2", Location = "PLC1", Destination = "ESMS2")]
    public class SteelCarEvent : EsmsBaseEvent
    {
        //49 Сталевоз в позиции «печь» 
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE6", IsBoolean = true, BitNumber = 0)]
        public bool PositoinFurnace { set; get; }        //DB822.DBX426.0
        //50 Сталевоз в позиции «кран» 
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE6", IsBoolean = true, BitNumber = 1)]
        public bool PositoinCrane { set; get; }        //DB822.DBX426.3
        //51 Резерв 1 
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE6", IsBoolean = true, BitNumber = 2)]
        public bool Reserve1 { get; set; }  
        //52 Резерв 2 
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE6", IsBoolean = true, BitNumber = 3)]
        public bool Reserve2 { get; set; }  
        //53 Резерв 3 
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE6", IsBoolean = true, BitNumber = 4)]
        public bool Reserve3 { get; set; }  
        //54 Резерв 4 
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE6", IsBoolean = true, BitNumber = 5)]
        public bool Reserve4 { get; set; }  
        //55 Резерв 5 
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE6", IsBoolean = true, BitNumber = 6)]
        public bool Reserve5 { get; set; }  
        //56 Резерв 6 
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE6", IsBoolean = true, BitNumber = 7)]
        public bool Reserve6 { get; set; }  
        //57 Резерв 7 
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE7", IsBoolean = true, BitNumber = 0)]
        public bool Reserve7 { get; set; }  
        //58 Резерв 8 
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE7", IsBoolean = true, BitNumber = 1)]
        public bool Reserve8 { get; set; }  
        //59 Резерв 9 
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE7", IsBoolean = true, BitNumber = 2)]
        public bool Reserve9 { get; set; }  
        //60 Резерв 10 
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE7", IsBoolean = true, BitNumber = 3)]
        public bool Reserve10 { get; set; }  
        //61 Резерв 11 
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE7", IsBoolean = true, BitNumber = 4)]
        public bool Reserve11 { get; set; }  
        //62 Резерв 12 
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE7", IsBoolean = true, BitNumber = 5)]
        public bool Reserve12 { get; set; }  
        //63 Резерв 13 
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE7", IsBoolean = true, BitNumber = 6)]
        public bool Reserve13 { get; set; }  
        //64 Резерв 14 
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE7", IsBoolean = true, BitNumber = 7)]
        public bool Reserve14 { get; set; }  

    }
}