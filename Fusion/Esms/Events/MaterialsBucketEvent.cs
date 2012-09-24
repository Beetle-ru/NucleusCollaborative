using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;

//��������� ����������� � ����
namespace Esms
{ 
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "MaterialsBucketEvent2", Location = "PLC3", Destination = "ESMS2")]
    [DBGroup(UnitNumber = 2)]
    public class MaterialsBucketEvent : EsmsBaseEvent
    {
        //102 ��� ����������� � ���� �� ������� 1-16
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL390")]
        public float WeightBunker1t16 { get; set; }        //DB91.DBD0
        //103 ��� ����������� � ���� �� ������� 2-16
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL394")]
        public float WeightBunker2t16 { get; set; }        //DB91.DBD4
        //104 ��� ����������� � ���� �� ������� 3-16
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL398")]
        public float WeightBunker3t16 { get; set; }        //DB91.DBD8
        //105 ��� ����������� � ���� �� ������� 4-16
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL402")]
        public float WeightBunker4t16 { get; set; }        //DB91.DBD12
        //106 ��� ����������� � ���� �� ������� 5-16
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL406")]
        public float WeightBunker5t16 { get; set; }        //DB91.DBD16
        //107 ��� ����������� � ���� �� ������� 6-16
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL410")]
        public float WeightBunker6t16 { get; set; }        //DB91.DBD20
        //108 ��� ����������� � ���� �� ������� 7-16
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL414")]
        public float WeightBunker7t16 { get; set; }        //DB91.DBD24
        //109 ��� ����������� � ���� �� ������� 8-16
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL418")]
        public float WeightBunker8t16 { get; set; }        //DB91.DBD28
        //110 ��� ����������� � ���� �� ������� 9-16
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL422")]
        public float WeightBunker9t16 { get; set; }        //DB91.DBD32
        //111 ��� ����������� � ���� �� ������� 10-16
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL426")]
        public float WeightBunker10t16 { get; set; }        //DB91.DBD36
        //112 ��� ����������� � ���� �� ������� 1-17
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL430")]
        public float WeightBunker1t17 { get; set; }        //DB91.DBD40
        //113 ��� ����������� � ���� �� ������� 2-17
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL434")]
        public float WeightBunker2t17{ get; set; }        //DB91.DBD44
        //114 ��� ����������� � ���� �� ������� 3-17
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL438")]
        public float WeightBunker3t17 { get; set; }        //DB91.DBD48
        //115 ��� ����������� � ���� �� ������� 4-17
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL442")]
        public float WeightBunker4t17 { get; set; }        //DB91.DBD52
    }
}
