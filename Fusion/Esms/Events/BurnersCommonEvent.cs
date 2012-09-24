using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;

//Горелки. Общие сигналы.
namespace Esms
{ 
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "BurnersCommonEvent2", Location = "PLC2", Destination = "ESMS2")]
    public class BurnersCommonEvent : EsmsBaseEvent
    {
        //77 Разрешение на работу горелок
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE8", IsBoolean = true, BitNumber = 0)]
        public bool WorkPermit { set; get; }        //DB902.DBX247.0
        //78 Энергия (наработка в МВт*ч) больше минимальной, для включения горелок
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE8", IsBoolean = true, BitNumber = 1)]
        public bool Energy { set; get; }        //DB902.DBX247.1
        //Резерв 1
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE8", IsBoolean = true, BitNumber = 2)]
        public bool Reserv1 { set; get; }     
        //Резерв 2
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE8", IsBoolean = true, BitNumber = 3)]
        public bool Reserv2 { set; get; }         
        //Резерв 3
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE8", IsBoolean = true, BitNumber = 4)]
        public bool Reserv3 { set; get; }         
        //Резерв 4
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE8", IsBoolean = true, BitNumber = 5)]
        public bool Reserv4 { set; get; }         
        //Резерв 5
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE8", IsBoolean = true, BitNumber = 6)]
        public bool Reserv5 { set; get; }         
        //Резерв 6
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE8", IsBoolean = true, BitNumber = 7)]
        public bool Reserv6 { set; get; }         
        //Резерв 7
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE9", IsBoolean = true, BitNumber = 0)]
        public bool Reserv7 { set; get; }         
        //Резерв 8
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE9", IsBoolean = true, BitNumber = 1)]
        public bool Reserv8 { set; get; }         
        //Резерв 9
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE9", IsBoolean = true, BitNumber = 2)]
        public bool Reserv9 { set; get; }         
        //Резерв 10
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE9", IsBoolean = true, BitNumber = 3)]
        public bool Reserv10 { set; get; }         
        //Резерв 11
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE9", IsBoolean = true, BitNumber = 4)]
        public bool Reserv11 { set; get; }         
        //Резерв 12
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE9", IsBoolean = true, BitNumber = 5)]
        public bool Reserv12 { set; get; }         
        //Резерв 13
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE9", IsBoolean = true, BitNumber = 6)]
        public bool Reserv13 { set; get; }         
        //Резерв 14
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE9", IsBoolean = true, BitNumber = 7)]
        public bool Reserv14 { set; get; }
    }
}    
