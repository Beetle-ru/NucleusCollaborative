using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;

//����� �������
namespace Esms
{ 
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "SchieberEvent2", Location = "PLC3", Destination = "ESMS2")]
    [DBGroup(UnitNumber = 2)]
    public class SchieberEvent : EsmsBaseEvent
    {
        //87 ����� ������� ������
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,BYTE7", IsBoolean = true, BitNumber = 0)]
        public bool SchieberOpen { set; get; }        //DB83.DBX6.3
        //87 ����� ������� ������
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,BYTE7", IsBoolean = true, BitNumber = 1)]
        public bool SchieberClose { set; get; }        //DB83.DBX6.4
        //P����� 1
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE7", IsBoolean = true, BitNumber = 2)]
        public bool Reserv1 { set; get; }
        //P����� 2
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE7", IsBoolean = true, BitNumber = 3)]
        public bool Reserv2 { set; get; }	
        //P����� 3
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE7", IsBoolean = true, BitNumber = 4)]
        public bool Reserv3 { set; get; }	
        //P����� 4
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE7", IsBoolean = true, BitNumber = 5)]
        public bool Reserv4 { set; get; }	
        //P����� 5
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE7", IsBoolean = true, BitNumber = 6)]
        public bool Reserv5 { set; get; }	
        //P����� 6
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE7", IsBoolean = true, BitNumber = 7)]
        public bool Reserv6 { set; get; }	
    }
}  