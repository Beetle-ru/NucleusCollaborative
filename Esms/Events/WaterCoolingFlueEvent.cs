using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;

//����������� ���� �� �������
namespace Esms
{ 
    //����������� ���� �� �������
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "WaterCoolingFlueEvent2", Location = "PLC1", Destination = "ESMS2")]
    [DBGroup(UnitNumber = 2)]
    public class WaterCoolingFlueEvent : EsmsBaseEvent
    {
        //104 ������ ����������� ���� �� ������ �������-�� ������
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL502")]
        public float Flow1WaterOutputFlue { get; set; }        //DB824.DBD792
        //105 ����������� ����������� ���� �� ������ �������-�� ������
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL506")]
        public float Temp1WaterOutputFlue { get; set; }        //DB824.DBD796
        //106 ����������� ����������� ���� �� ������ �������-�� ������
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL510")]
        public float Temp2WaterOutputFlue { get; set; }        //DB824.DBD800
        //107 ����������� ����������� ���� �� ������ �������-�� ������
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL514")]
        public float Temp3WaterOutputFlue { get; set; }        //DB824.DBD804
        //108 ����������� 1 ����������� ���� �� ������ �������-�� ������ (2/3)
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL518")]
        public float Temp1WaterOutputFlue2div3 { get; set; }        //DB824.DBD808
        //109 ����������� 2 ����������� ���� �� ������ �������-�� ������ (2/3)
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL522")]
        public float Temp2WaterOutputFlue2div3 { get; set; }        //DB824.DBD812
        //110 ����������� 3 ����������� ���� �� ������ �������-�� ������ (2/3)
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL526")]
        public float Temp3WaterOutputFlue2div3 { get; set; }        //DB824.DBD816
        //111 ����������� 4 ����������� ���� �� ������ �������-�� ������ (2/3)
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL530")]
        public float Temp4WaterOutputFlue2div3 { get; set; }        //DB824.DBD820
        //112 ����������� 5 ����������� ���� �� ������ �������-�� ������ (2/3)
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL534")]
        public float Temp5WaterOutputFlue2div3 { get; set; }        //DB824.DBD824
        //113 ����������� 6 ����������� ���� �� ������ �������-�� ������ (2/3)
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL538")]
        public float Temp6WaterOutputFlue2div3 { get; set; }        //DB824.DBD828
        //114 ������ ����������� ���� �� ������ �������-�� ������
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL542")]
        public float Flow2WaterOutputFlue { get; set; }        //DB824.DBD832
        //115 ������ ����������� ���� �� ������ �������-�� ������
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL546")]
        public float Flow3WaterOutputFlue { get; set; }        //DB824.DBD836
    }
}    
