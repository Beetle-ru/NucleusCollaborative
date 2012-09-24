using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;

//������ ����������� 1
namespace Esms
{     
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "FurnaceSwitch1Event2", Location = "PLC1", Destination = "ESMS2")]
    [DBGroup(UnitNumber = 2)]
    public class FurnaceSwitch1Event : EsmsBaseEvent
    {
        //54 ������ ����������� 1, ���������� ���� 1, �� 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL310")]
        public float VoltagePhase1 { get; set; }        //DB824.DBD940
        //55 ������ ����������� 1, ���������� ���� 2, �� 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL314")]
        public float VoltagePhase2 { get; set; }        //DB824.DBD944
        //56 ������ ����������� 1, ���������� ���� 3, �� 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL318")]
        public float VoltagePhase3 { get; set; }        //DB824.DBD948
        //57 ������ ����������� 1, ��� ���� 1, � 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL322")]
        public float AmperagePhase1 { get; set; }        //DB824.DBD952
        //58 ������ ����������� 1, ��� ���� 2, � 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL326")]
        public float AmperagePhase2 { get; set; }        //DB824.DBD956
        //59 ������ ����������� 1, ��� ���� 3, � 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL330")]
        public float AmperagePhase3 { get; set; }        //DB824.DBD960
        //60 ������ ����������� 1, �������� �������� 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL334")]
        public float ActiveEnergy { get; set; }        //DB824.DBD964
        //61 ������ ����������� 1, ���������� �������� 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL338")]
        public float ReactiveEnergy { get; set; }        //DB824.DBD968
        //62 ������ ����������� 1, ������� �� 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL342")]
        public float CosFi { get; set; }        //DB824.DBD972
        //63 ������ ����������� 1, �������� ������� 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL346")]
        public float ActiveEnergyToo { get; set; }        //DB824.DBD976
        //64 ������ ����������� 1, ���������� ������� 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL350")]
        public float ReactiveEnergyToo { get; set; }        //DB824.DBD980
        //65 ������ 1 
        [DataMember]
        //[DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL354")]
        public float Reserve { get; set; }
    }
}    
