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
    [PLCGroup(Name = "MaterialsFurnaceEvent2", Location = "PLC3", Destination = "ESMS2")]
    [DBGroup(UnitNumber = 2)]
    public class MaterialsFurnaceEvent : EsmsBaseEvent
    {
        //88 ��� ����������� � ���� �� ������� 1-16
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL334")]
        public float WeightBunker1t16 { get; set; }        //DB90.DBD0
        //89 ��� ����������� � ���� �� ������� 2-16
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL338")]
        public float WeightBunker2t16 { get; set; }        //DB90.DBD4
        //90 ��� ����������� � ���� �� ������� 3-16
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL342")]
        public float WeightBunker3t16 { get; set; }        //DB90.DBD8
        //91 ��� ����������� � ���� �� ������� 4-16
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL346")]
        public float WeightBunker4t16 { get; set; }        //DB90.DBD12
        //92 ��� ����������� � ���� �� ������� 5-16
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL350")]
        public float WeightBunker5t16 { get; set; }        //DB90.DBD16
        //93 ��� ����������� � ���� �� ������� 6-16
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL354")]
        public float WeightBunker6t16 { get; set; }        //DB90.DBD20
        //94 ��� ����������� � ���� �� ������� 7-16
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL358")]
        public float WeightBunker7t16 { get; set; }        //DB90.DBD24
        //95 ��� ����������� � ���� �� ������� 8-16
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL362")]
        public float WeightBunker8t16 { get; set; }        //DB90.DBD28
        //96 ��� ����������� � ���� �� ������� 9-16
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL366")]
        public float WeightBunker9t16 { get; set; }        //DB90.DBD32
        //97 ��� ����������� � ���� �� ������� 10-16
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL370")]
        public float WeightBunker10t16 { get; set; }        //DB90.DBD36
        //98 ��� ����������� � ���� �� ������� 1-17
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL374")]
        public float WeightBunker1t17 { get; set; }        //DB90.DBD40
        //99 ��� ����������� � ���� �� ������� 2-17
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL378")]
        public float WeightBunker2t17 { get; set; }        //DB90.DBD44
        //100 ��� ����������� � ���� �� ������� 3-17
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL382")]
        public float WeightBunker3t17 { get; set; }        //DB90.DBD48
        //101 ��� ����������� � ���� �� ������� 4-17
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL386")]
        public float WeightBunker4t17 { get; set; }        //DB90.DBD52
    }
}  
