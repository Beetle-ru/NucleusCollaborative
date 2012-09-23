using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;

//���������. ����� �������
namespace Esms
{ 
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "InjectorsCommonEvent2", Location = "PLC2", Destination = "ESMS2")]
    public class InjectorsCommonEvent : EsmsBaseEvent
    {
        //179 ���������� �� ������ ����������
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE18", IsBoolean = true, BitNumber = 0)]
        public bool WorkPermit { set; get; }        //DB902.DBX440.0
        //180 ������� (��������� � ���*�) ������ �����������, ��� ��������� ���������� � ������ �������
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE18", IsBoolean = true, BitNumber = 1)]
        public bool EnergyBurner { set; get; }        //DB902.DBX440.1 
        //181 ������� (��������� � ���*�) ������ �����������, ��� ��������� ���������� � ������ �������������
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE18", IsBoolean = true, BitNumber = 2)]
        public bool EnergyInjector { set; get; }        //DB902.DBX440.2
        //������ 1
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE18", IsBoolean = true, BitNumber = 3)]
        public bool Reserv1 { set; get; }     
        //������ 2
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE18", IsBoolean = true, BitNumber = 4)]
        public bool Reserv2 { set; get; }         
        //������ 3
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE18", IsBoolean = true, BitNumber = 5)]
        public bool Reserv3 { set; get; }         
        //������ 4
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE18", IsBoolean = true, BitNumber = 6)]
        public bool Reserv4 { set; get; }         
        //������ 5
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE18", IsBoolean = true, BitNumber = 7)]
        public bool Reserv5 { set; get; }         
        //������ 6
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE19", IsBoolean = true, BitNumber = 0)]
        public bool Reserv6 { set; get; }         
        //������ 7
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE19", IsBoolean = true, BitNumber = 1)]
        public bool Reserv7 { set; get; }         
        //������ 8
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE19", IsBoolean = true, BitNumber = 2)]
        public bool Reserv8 { set; get; }         
        //������ 9
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE19", IsBoolean = true, BitNumber = 3)]
        public bool Reserv9 { set; get; }         
        //������ 10
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE19", IsBoolean = true, BitNumber = 4)]
        public bool Reserv10 { set; get; }         
        //������ 11
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE19", IsBoolean = true, BitNumber = 5)]
        public bool Reserv11 { set; get; }         
        //������ 12
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE19", IsBoolean = true, BitNumber = 6)]
        public bool Reserv12 { set; get; }         
        //������ 13
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE19", IsBoolean = true, BitNumber = 7)]
        public bool Reserv13 { set; get; }         
    }
}    
