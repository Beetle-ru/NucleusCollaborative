using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;

//������� � �������������
namespace Esms
{ 
    //������� � �������������
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "ReactorTransformerEvent2", Location = "PLC1", Destination = "ESMS2")]
    [DBGroup(UnitNumber = 2)]
    public class ReactorTransformerEvent : EsmsBaseEvent
    {
        //116 ������� ��������������
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL550")]
        public float TransformerStage { get; set; }        //DB824.DBD880
        //117 ������������� ����������
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL554")]
        public float TransformerVoltage { get; set; }        //DB824.DBD884
        //118 ������� ��������
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL558")]
        public float ReactorStage { get; set; }        //DB824.DBD1044
        //119 ������� ����������
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL562")]
        public float ReactorVoltage { get; set; }        //DB824.DBD1048
        //120 ������ 1
        [DataMember]
        [PLCPoint(Location = "DB550,REAL566")]
        public float Reserve1 { get; set; }
        //121 ������ 2
        [DataMember]
        [PLCPoint(Location = "DB550,REAL570")]
        public float Reserve2 { get; set; } 
        //122 ������ 3
        [DataMember]
        [PLCPoint(Location = "DB550,REAL574")]
        public float Reserve3 { get; set; } 
        //123 ������ 4
        [DataMember]
        [PLCPoint(Location = "DB550,REAL578")]
        public float Reserve4 { get; set; } 
    }
}    
