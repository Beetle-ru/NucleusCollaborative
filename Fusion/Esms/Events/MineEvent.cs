using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;

// �����
namespace Esms
{ 
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "MineEvent2", Location = "PLC1", Destination = "ESMS2")]
    public class MineEvent : EsmsBaseEvent
    {
        //33 ����� ������
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE4", IsBoolean = true, BitNumber = 0)]
        public bool Up { get; set; }         //DB822.DBX281.4
        //34 ����� � ������� �������
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE4", IsBoolean = true, BitNumber = 1)]
        public bool Output { get; set; }         //DB822.DBX281.5
        //35 ����� �����
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE4", IsBoolean = true, BitNumber = 2)]
        public bool Down { get; set; }         //DB822.DBX281.6
        //36 ����� �����������
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE4", IsBoolean = true, BitNumber = 3)]
        public bool GoUp { get; set; }         //DB822.DBX281.7
        //37 ����� ����������
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE4", IsBoolean = true, BitNumber = 4)]
        public bool GoDown { get; set; }         //DB822.DBX282.0
        //38 ������ 1
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE4", IsBoolean = true, BitNumber = 5)]
        public bool Reserve1 { get; set; } 
        //39 ������ 2
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE4", IsBoolean = true, BitNumber = 6)]
        public bool Reserve2 { get; set; } 
        //40 ������ 3
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE4", IsBoolean = true, BitNumber = 7)]
        public bool Reserve3 { get; set; }  
        //41 ������ 4
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE5", IsBoolean = true, BitNumber = 0)]
        public bool Reserve4 { get; set; }  
        //42 ������ 5
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE5", IsBoolean = true, BitNumber = 1)]
        public bool Reserve5 { get; set; }  
        //43 ������ 6
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE5", IsBoolean = true, BitNumber = 2)]
        public bool Reserve6 { get; set; }  
        //44 ������ 7
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE5", IsBoolean = true, BitNumber = 3)]
        public bool Reserve7 { get; set; }  
        //45 ������ 8
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE5", IsBoolean = true, BitNumber = 4)]
        public bool Reserve8 { get; set; }  
        //46 ������ 9
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE5", IsBoolean = true, BitNumber = 5)]
        public bool Reserve9 { get; set; }  
        //47 ������ 10
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE5", IsBoolean = true, BitNumber = 6)]
        public bool Reserve10 { get; set; }  
        //48 ������ 11
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE5", IsBoolean = true, BitNumber = 7)]
        public bool Reserve11 { get; set; }  
        //190 ������� ����� ������� ������� 
        [DataMember]
        [PLCPoint(Location = "DB550,REAL868")]
        public float PositionOutput { get; set; }        //DB824.DBD144
        //191 ������� ����� ������� ����� 
        [DataMember]
        [PLCPoint(Location = "DB550,REAL872")]
        public float PositionSlag { get; set; }        //DB824.DBD148
        //192 ������� ����� (�������) 
        [DataMember]
        [PLCPoint(Location = "DB550,REAL876")]
        public float SkewedMine { get; set; }        //DB824.DBD152
    }
}    
