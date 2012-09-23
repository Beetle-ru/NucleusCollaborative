using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;

//ВЖ3 (виброжёлоб)
namespace Esms
{ 
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "VibratingChannel3Event2", Location = "PLC3", Destination = "ESMS2")]
    [DBGroup(UnitNumber = 2)]
    public class VibratingChannel3Event : EsmsBaseEvent
    {
        //83 Aaeaaoaeu 1, aeiaie?aneee A?3 (к печи) aee??ai
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,BYTE5", IsBoolean = true, BitNumber = 0)]
        public bool Pump1VG3Furnace { set; get; }        //IX86.0
        //84 Aaeaaoaeu 2, aeiaie?aneee A?3 (к печи) aee??ai
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,BYTE5", IsBoolean = true, BitNumber = 1)]
        public bool Pump2VG3Furnace { set; get; }        //IX86.1
        //Pезерв 1
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE5", IsBoolean = true, BitNumber = 2)]
        public bool Reserv1 { set; get; }
        //Pезерв 2
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE5", IsBoolean = true, BitNumber = 3)]
        public bool Reserv2 { set; get; }	
        //Pезерв 3
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE5", IsBoolean = true, BitNumber = 4)]
        public bool Reserv3 { set; get; }	
        //Pезерв 4
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE5", IsBoolean = true, BitNumber = 5)]
        public bool Reserv4 { set; get; }	
        //Pезерв 5
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE5", IsBoolean = true, BitNumber = 6)]
        public bool Reserv5 { set; get; }	
        //Pезерв 6
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE5", IsBoolean = true, BitNumber = 7)]
        public bool Reserv6 { set; get; }	
    }
}  