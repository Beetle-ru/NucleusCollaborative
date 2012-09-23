using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;

//������
namespace Esms
{ 
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "PortalEvent2", Location = "PLC1", Destination = "ESMS2")]
    public class PortalEvent : EsmsBaseEvent
    {
        //97 ������ � ������� ��������
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE12", IsBoolean = true, BitNumber = 0)]
        public bool PositionParking { get; set; }         //DB822.DBX370.0
        //98 ������ ����� �������� ��������
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE12", IsBoolean = true, BitNumber = 1)]
        public bool BeforePositionParking { get; set; }         //DB822.DBX370.1
        //99 ������ ����� �������� ����
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE12", IsBoolean = true, BitNumber = 2)]
        public bool BeforePositionFurnace { get; set; }         //DB822.DBX370.2
        //100 ������ � ������� ����
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE12", IsBoolean = true, BitNumber = 3)]
        public bool PositionFurnace { get; set; }         //DB822.DBX370.3
        //101 ������ 1
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE12", IsBoolean = true, BitNumber = 4)]
        public bool Reserve1 { get; set; } 
        //102 ������ 2
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE12", IsBoolean = true, BitNumber = 5)]
        public bool Reserve2 { get; set; }   
        //103 ������ 3
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE12", IsBoolean = true, BitNumber = 6)]
        public bool Reserve3 { get; set; }   
        //104 ������ 4
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE12", IsBoolean = true, BitNumber = 7)]
        public bool Reserve4 { get; set; }   
        //105 ������ 5
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE13", IsBoolean = true, BitNumber = 0)]
        public bool Reserve5 { get; set; }   
        //106 ������ 6
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE13", IsBoolean = true, BitNumber = 1)]
        public bool Reserve6 { get; set; }   
        //107 ������ 7
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE13", IsBoolean = true, BitNumber = 2)]
        public bool Reserve7 { get; set; }   
        //108 ������ 8
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE13", IsBoolean = true, BitNumber = 3)]
        public bool Reserve8 { get; set; }   
        //109 ������ 9
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE13", IsBoolean = true, BitNumber = 4)]
        public bool Reserve9 { get; set; }  
        //110 ������ 10
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE13", IsBoolean = true, BitNumber = 5)]
        public bool Reserve10 { get; set; }  
        //111 ������ 11
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE13", IsBoolean = true, BitNumber = 6)]
        public bool Reserve11 { get; set; }  
        //112 ������ 12
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE13", IsBoolean = true, BitNumber = 7)]
        public bool Reserve12 { get; set; }  
        //200 ������� ������� 
        [DataMember]
        [PLCPoint(Location = "DB550,REAL908")]
        public float Position { get; set; }        //DB824.DBD276
    }
}    
