using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;

//��� ����������� �� ��������
namespace Esms
{
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "WeighBunkersEvent2", Location = "PLC3", Destination = "ESMS2")]
    [DBGroup(UnitNumber = 2)]
    public class WeighBunkersEvent : EsmsBaseEvent
    {
        //1 ���, ����������� � ������� ������� �� ������� 1-16
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL20")]
        public float WeighBunker1t16 { get; set; }        //DB29.DBD0
        //2 ���, ����������� � ������� ������� �� ������� 2-16
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL24")]
        public float WeighBunker2t16 { get; set; }        //DB29.DBD4
        //3 ���, ����������� � ������� ������� �� ������� 3-16
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL28")]
        public float WeighBunker3t16 { get; set; }        //DB29.DBD8
        //4 ���, ����������� � ������� ������� �� ������� 4-16
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL32")]
        public float WeighBunker4t16 { get; set; }        //DB29.DBD12
        //5 ���, ����������� � ������� ������� �� ������� 5-16
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL36")]
        public float WeighBunker5t16 { get; set; }        //DB29.DBD16
        //6 ���, ����������� � ������� ������� �� ������� 6-16
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL40")]
        public float WeighBunker6t16 { get; set; }        //DB29.DBD20
        //7 ���, ����������� � ������� ������� �� ������� 7-16
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL44")]
        public float WeighBunker7t16 { get; set; }        //DB29.DBD24
        //8 ���, ����������� � ������� ������� �� ������� 8-16
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL48")]
        public float WeighBunker8t16 { get; set; }        //DB29.DBD28
        //9 ���, ����������� � ������� ������� �� ������� 9-16
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL52")]
        public float WeighBunker9t16 { get; set; }        //DB29.DBD32
        //10 ���, ����������� � ������� ������� �� ������� 10-16
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL56")]
        public float WeighBunker10t16 { get; set; }        //DB29.DBD36
        //11 ���, ����������� �� ������� 1-17
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL60")]
        public float WeighBunker1t17 { get; set; }        //DB29.DBD40
        //12 ���, ����������� �� ������� 2-17
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL64")]
        public float WeighBunker2t17 { get; set; }        //DB29.DBD44
        //13 ���, ����������� �� ������� 3-17
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL68")]
        public float WeighBunker3t17 { get; set; }        //DB29.DBD48
        //14 ���, ����������� �� ������� 4-17
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL72")]
        public float WeighBunker4t17 { get; set; }        //DB29.DBD52
    }
}  
