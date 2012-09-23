using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;

//������ ����������� 2
namespace Esms
{       
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "FurnaceSwitch2Event2", Location = "PLC1", Destination = "ESMS2")]
    [DBGroup(UnitNumber = 2)]
    public class FurnaceSwitch2Event : EsmsBaseEvent
    {
        //66 ������ ����������� 2, ���������� ���� 1, ��
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL358")]
        public float VoltagePhase1 { get; set; }        //DB824.DBD992
        //67 ������ ����������� 2, ���������� ���� 2, ��
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL362")]
        public float VoltagePhase2 { get; set; }        //DB824.DBD996
        //68 ������ ����������� 2, ���������� ���� 3, ��
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL366")]
        public float VoltagePhase3 { get; set; }        //DB824.DBD1000
        //69 ������ ����������� 2, ��� ���� 1, �
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL370")]
        public float AmperagePhase1 { get; set; }        //DB824.DBD1004
        //70 ������ ����������� 2, ��� ���� 2, �
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL374")]
        public float AmperagePhase2 { get; set; }        //DB824.DBD1008
        //71 ������ ����������� 2 ��� ���� 3, �
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL378")]
        public float AmperagePhase3 { get; set; }        //DB824.DBD1012
        //72 ������ ����������� 2, �������� ��������
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL382")]
        public float ActiveEnergy { get; set; }        //DB824.DBD1016
        //73 ������ ����������� 2, ���������� ��������
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL386")]
        public float ReactiveEnergy { get; set; }        //DB824.DBD1020
        //74 ������ ����������� 2, ������� ��
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL390")]
        public float CosFi { get; set; }        //DB824.DBD1024
        //75 ������ ����������� 2, �������� �������
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL394")]
        public float ActiveEnergyToo { get; set; }        //DB824.DBD1028
        //76 ������ ����������� 2, ���������� �������
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL398")]
        public float ReactiveEnergyToo{ get; set; }        //DB824.DBD1032
        //77 ������ 1
        [DataMember]
        //[DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL402")]
        public float Reserve { get; set; } 
    }
}    
