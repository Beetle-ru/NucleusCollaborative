using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;

//�������. ����� �������.
namespace Esms
{ 
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "BurnersCommonEvent2", Location = "PLC2", Destination = "ESMS2")]
    public class BurnersCommonEvent : EsmsBaseEvent
    {
        //77 ���������� �� ������ �������
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE8", IsBoolean = true, BitNumber = 0)]
        public bool WorkPermit { set; get; }        //DB902.DBX247.0
        //78 ������� (��������� � ���*�) ������ �����������, ��� ��������� �������
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE8", IsBoolean = true, BitNumber = 1)]
        public bool Energy { set; get; }        //DB902.DBX247.1
        //������ 1
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE8", IsBoolean = true, BitNumber = 2)]
        public bool Reserv1 { set; get; }     
        //������ 2
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE8", IsBoolean = true, BitNumber = 3)]
        public bool Reserv2 { set; get; }         
        //������ 3
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE8", IsBoolean = true, BitNumber = 4)]
        public bool Reserv3 { set; get; }         
        //������ 4
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE8", IsBoolean = true, BitNumber = 5)]
        public bool Reserv4 { set; get; }         
        //������ 5
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE8", IsBoolean = true, BitNumber = 6)]
        public bool Reserv5 { set; get; }         
        //������ 6
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE8", IsBoolean = true, BitNumber = 7)]
        public bool Reserv6 { set; get; }         
        //������ 7
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE9", IsBoolean = true, BitNumber = 0)]
        public bool Reserv7 { set; get; }         
        //������ 8
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE9", IsBoolean = true, BitNumber = 1)]
        public bool Reserv8 { set; get; }         
        //������ 9
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE9", IsBoolean = true, BitNumber = 2)]
        public bool Reserv9 { set; get; }         
        //������ 10
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE9", IsBoolean = true, BitNumber = 3)]
        public bool Reserv10 { set; get; }         
        //������ 11
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE9", IsBoolean = true, BitNumber = 4)]
        public bool Reserv11 { set; get; }         
        //������ 12
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE9", IsBoolean = true, BitNumber = 5)]
        public bool Reserv12 { set; get; }         
        //������ 13
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE9", IsBoolean = true, BitNumber = 6)]
        public bool Reserv13 { set; get; }         
        //������ 14
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE9", IsBoolean = true, BitNumber = 7)]
        public bool Reserv14 { set; get; }
    }
}    
