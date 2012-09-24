using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;

//Электроды
namespace Esms
{ 
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "ElectrodesEvent2", Location = "PLC1", Destination = "ESMS2")]
    public class ElectrodesEvent : EsmsBaseEvent
    {
        //81 Электрод A вверху
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE10", IsBoolean = true, BitNumber = 0)]
        public bool UpA { get; set; }         //DB822.DBX346.0
        //82 Электрод A выпуск
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE10", IsBoolean = true, BitNumber = 1)]
        public bool OutputA { get; set; }         //DB822.DBX346.1
        //83 Электрод A внизу
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE10", IsBoolean = true, BitNumber = 2)]
        public bool DownA { get; set; }         //DB822.DBX346.2
        //84 Электрод B вверху
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE10", IsBoolean = true, BitNumber = 3)]
        public bool UpB { get; set; }         //DB822.DBX346.3
        //85 Электрод B выпуск
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE10", IsBoolean = true, BitNumber = 4)]
        public bool OutputB { get; set; }         //DB822.DBX346.4
        //86 Электрод B внизу
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE10", IsBoolean = true, BitNumber = 5)]
        public bool DownB { get; set; }         //DB822.DBX346.5
        //87 Электрод C вверху
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE10", IsBoolean = true, BitNumber = 6)]
        public bool UpC { get; set; }         //DB822.DBX346.6
        //88 Электрод C выпуск
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE10", IsBoolean = true, BitNumber = 7)]
        public bool OutputC { get; set; }         //DB822.DBX346.7
        //89 Электрод C внизу
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE11", IsBoolean = true, BitNumber = 0)]
        public bool DownC { get; set; }         //DB822.DBX347.0
        //90 Резерв 1
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE11", IsBoolean = true, BitNumber = 1)]
        public bool Reserve1 { get; set; }  
        //91 Резерв 2
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE11", IsBoolean = true, BitNumber = 2)]
        public bool Reserve2 { get; set; }  
        //92 Резерв 3
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE11", IsBoolean = true, BitNumber = 3)]
        public bool Reserve3 { get; set; }  
        //93 Резерв 4
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE11", IsBoolean = true, BitNumber = 4)]
        public bool Reserve4 { get; set; }  
        //94 Резерв 5
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE11", IsBoolean = true, BitNumber = 5)]
        public bool Reserve5 { get; set; }  
        //95 Резерв 6
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE11", IsBoolean = true, BitNumber = 6)]
        public bool Reserve6 { get; set; }  
        //96 Резерв 7
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE11", IsBoolean = true, BitNumber = 7)]
        public bool Reserve7 { get; set; }  
        //197 Позиция электрода А 
        [DataMember]
        [PLCPoint(Location = "DB550,REAL896")]
        public float PositionA { get; set; }        //DB824.DBD216
        //198 Позиция электрода В 
        [DataMember]
        [PLCPoint(Location = "DB550,REAL900")]
        public float PositionB { get; set; }        //DB824.DBD220
        //199 Позиция электрода С 
        [DataMember]
        [PLCPoint(Location = "DB550,REAL904")]
        public float PositionC { get; set; }        //DB824.DBD224
    }
}    
