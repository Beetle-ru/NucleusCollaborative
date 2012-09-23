using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;

//������ � ����/�����
namespace Esms
{ 
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "SubmissionEvent2", Location = "PLC3", Destination = "ESMS2")]
    [DBGroup(UnitNumber = 2)]
    public class SubmissionEvent : EsmsBaseEvent
    {
        //75 �������� 6��4 2009 ��������
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE3", IsBoolean = true, BitNumber = 0)]
        public bool Conveyor6��4n2009Work { set; get; }        //IX65.1
        //76 ����� � ���������� ������ � ��������� �� ����
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,BYTE3", IsBoolean = true, BitNumber = 1)]
        public bool SchieberPositionFurnace { set; get; }        //IX122.0
        //77 ����� � ���������� ������ � ��������� �� �����
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,BYTE3", IsBoolean = true, BitNumber = 2)]
        public bool SchieberPositionBucket { set; get; }        //IX122.3
        //80 �������� ���������� �� �������� 1-17�4-17 �����
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,BYTE3", IsBoolean = true, BitNumber = 3)]
        public bool LoadMaterials1t17n4t17Start { set; get; }        //DB14.DBX4.0
        //81 �������� ���������� �� �������� 1-16�10-16 �����
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,BYTE3", IsBoolean = true, BitNumber = 4)]
        public bool LoadMaterials1t16n10t16Start { set; get; }        //DB14.DBX4.1
        //P����� 1
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE3", IsBoolean = true, BitNumber = 5)]
        public bool Reserv1 { set; get; }	
        //P����� 2
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE3", IsBoolean = true, BitNumber = 6)]
        public bool Reserv2 { set; get; }	
        //P����� 3
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE3", IsBoolean = true, BitNumber = 7)]
        public bool Reserv3 { set; get; }	
        //P����� 4
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE4", IsBoolean = true, BitNumber = 0)]
        public bool Reserv4 { set; get; }	
        //P����� 5
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE4", IsBoolean = true, BitNumber = 1)]
        public bool Reserv5 { set; get; }	
        //P����� 6
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE4", IsBoolean = true, BitNumber = 2)]
        public bool Reserv6 { set; get; }	
        //P����� 7
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE4", IsBoolean = true, BitNumber = 3)]
        public bool Reserv7 { set; get; }	
        //P����� 8
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE4", IsBoolean = true, BitNumber = 4)]
        public bool Reserv8 { set; get; }
        //P����� 9
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE4", IsBoolean = true, BitNumber = 5)]
        public bool Reserv9 { set; get; }	
        //P����� 10
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE4", IsBoolean = true, BitNumber = 6)]
        public bool Reserv10 { set; get; }	
        //P����� 11
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE4", IsBoolean = true, BitNumber = 7)]
        public bool Reserv11 { set; get; }	

        
        //78 �������� ���������� �� �������� 1-17�4-17, ����� ������ (0 � � ����, 1 � � ����)
        [DataMember]
        [PLCPoint(Location = "DB550,INT330")]
        public int LoadMaterials1t17n4t17 { set; get; }        //DB14.DBW0
        //79 �������� ���������� �� �������� 1-16�10-16, ����� ������ (0 � ������, 1 � � ����, 2 � � ����, 3 - ���������)
        [DataMember]
        [PLCPoint(Location = "DB550,INT332")]
        public int LoadMaterials1t16n10t16 { set; get; }        //DB14.DBW2
        
        
        
        
    }
}  
