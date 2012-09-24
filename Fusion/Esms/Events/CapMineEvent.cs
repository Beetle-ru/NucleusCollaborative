using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;
     
//Колпак шахты
namespace Esms
{  
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "CapMineEvent2", Location = "PLC1", Destination = "ESMS2")]
    public class CapMineEvent : EsmsBaseEvent
    {        
        //17 Колпак шахты открыт
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE2", IsBoolean = true, BitNumber = 0)]
        public bool Open { get; set; }        //DB822.DBX282.1
        //18 Колпак шахты почти открыт
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE2", IsBoolean = true, BitNumber = 1)]
        public bool AlmostOpen { get; set; }        //DB822.DBX282.2
        //19 Колпак шахты почти закрыт
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE2", IsBoolean = true, BitNumber = 2)]
        public bool AlmostClose { get; set; }        //DB822.DBX282.3
        //20 Колпак шахты закрыт
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE2", IsBoolean = true, BitNumber = 3)]
        public bool Close { get; set; }        //DB822.DBX282.4
        //21 Колпак шахты вверху
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE2", IsBoolean = true, BitNumber = 4)]
        public bool Up { get; set; }        //DB822.DBX282.5
        //22 Колпак шахты внизу
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE2", IsBoolean = true, BitNumber = 5)]
        public bool Down { get; set; }        //DB822.DBX282.6
        //23 Резерв 1
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE2", IsBoolean = true, BitNumber = 6)]
        public bool Reserve1 { get; set; }
        //24 Резерв 2 
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE2", IsBoolean = true, BitNumber = 7)]
        public bool Reserve2 { get; set; } 
        //25 Резерв 3
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE3", IsBoolean = true, BitNumber = 0)]
        public bool Reserve3 { get; set; } 
        //26 Резерв 4
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE3", IsBoolean = true, BitNumber = 1)]
        public bool Reserve4 { get; set; } 
        //27 Резерв 5
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE3", IsBoolean = true, BitNumber = 2)]
        public bool Reserve5 { get; set; } 
        //28 Резерв 6
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE3", IsBoolean = true, BitNumber = 3)]
        public bool Reserve6 { get; set; } 
        //29 Резерв 7
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE3", IsBoolean = true, BitNumber = 4)]
        public bool Reserve7 { get; set; } 
        //30 Резерв 8
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE3", IsBoolean = true, BitNumber = 5)]
        public bool Reserve8 { get; set; } 
        //31 Резерв 9
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE3", IsBoolean = true, BitNumber = 6)]
        public bool Reserve9 { get; set; } 
        //32 Резерв 10
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE3", IsBoolean = true, BitNumber = 7)]
        public bool Reserve10 { get; set; } 
		//195 Позиция колпака шахты со стороны выпуска 
        [DataMember]
        [PLCPoint(Location = "DB550,REAL888")]
        public float PositonOutput { get; set; }        //DB824.DBD164
        //196 Позиция колпака шахты со стороны шлака 
        [DataMember]
        [PLCPoint(Location = "DB550,REAL892")]
        public float PositonSlag { get; set; }        //DB824.DBD168

    }
}    
