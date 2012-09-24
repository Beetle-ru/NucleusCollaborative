using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;

//��4 (���������)
namespace Esms
{ 
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "VibratingChannel4Event2", Location = "PLC3", Destination = "ESMS2")]
    [DBGroup(UnitNumber = 2)]
    public class VibratingChannel4Event : EsmsBaseEvent
    {
        //85 Aaeaaoaeu 1, aeiaie?aneee A?4 (� �����) aee??ai
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,BYTE6", IsBoolean = true, BitNumber = 0)]
        public bool Pump1VG4Bucket { set; get; }        //IX72.0
        //86 Aaeaaoaeu 2, aeiaie?aneee A?4 (� �����) aee??ai
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,BYTE6", IsBoolean = true, BitNumber = 1)]
        public bool Pump2VG4Bucket { set; get; }        //IX72.1
        //P����� 1
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE6", IsBoolean = true, BitNumber = 2)]
        public bool Reserv1 { set; get; }
        //P����� 2
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE6", IsBoolean = true, BitNumber = 3)]
        public bool Reserv2 { set; get; }	
        //P����� 3
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE6", IsBoolean = true, BitNumber = 4)]
        public bool Reserv3 { set; get; }	
        //P����� 4
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE6", IsBoolean = true, BitNumber = 5)]
        public bool Reserv4 { set; get; }	
        //P����� 5
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE6", IsBoolean = true, BitNumber = 6)]
        public bool Reserv5 { set; get; }	
        //P����� 6
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE6", IsBoolean = true, BitNumber = 7)]
        public bool Reserv6 { set; get; }	
    }
}  