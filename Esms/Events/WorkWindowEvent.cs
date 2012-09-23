using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;

//������� ����

namespace Esms
{
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "WorkWindowEvent2", Location = "PLC1", Destination = "ESMS2")]
    [DBGroup(UnitNumber = 2)]
    public class WorkWindowEvent : EsmsBaseEvent
    {
        //113 ������� ���� �������
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,BYTE14", IsBoolean = true, BitNumber = 0)]
        public bool Open  { set; get; }        //IX120.4
        //114 ������� ���� �������
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,BYTE14", IsBoolean = true, BitNumber = 1)]
        public bool Close  { set; get; }        //IX120.4
        //115 ������ 1
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE14", IsBoolean = true, BitNumber = 2)]
        public bool Reserve1 { get; set; }    
        //116 ������ 2
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE14", IsBoolean = true, BitNumber = 3)]
        public bool Reserve2 { get; set; }    
        //117 ������ 3
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE14", IsBoolean = true, BitNumber = 4)]
        public bool Reserve3 { get; set; }    
        //118 ������ 4
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE14", IsBoolean = true, BitNumber = 5)]
        public bool Reserve4 { get; set; }    
        //119 ������ 5
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE14", IsBoolean = true, BitNumber = 6)]
        public bool Reserve5 { get; set; }    
        //120 ������ 6
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE14", IsBoolean = true, BitNumber = 7)]
        public bool Reserve6 { get; set; }    
        //121 ������ 7
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE15", IsBoolean = true, BitNumber = 0)]
        public bool Reserve7 { get; set; }    
        //122 ������ 8
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE15", IsBoolean = true, BitNumber = 1)]
        public bool Reserve8 { get; set; }    
        //123 ������ 9
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE15", IsBoolean = true, BitNumber = 2)]
        public bool Reserve9 { get; set; }    
        //124 ������ 10
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE15", IsBoolean = true, BitNumber = 3)]
        public bool Reserve10 { get; set; }    
        //125 ������ 11
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE15", IsBoolean = true, BitNumber = 4)]
        public bool Reserve11 { get; set; }    
        //126 ������ 12
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE15", IsBoolean = true, BitNumber = 5)]
        public bool Reserve12 { get; set; }    
        //127 ������ 13
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE15", IsBoolean = true, BitNumber = 6)]
        public bool Reserve13 { get; set; }    
        //128 ������ 14
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE15", IsBoolean = true, BitNumber = 7)]
        public bool Reserve14 { get; set; }   
        }
}
