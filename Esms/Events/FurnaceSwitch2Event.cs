using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;

//Печной выключатель 2
namespace Esms
{       
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "FurnaceSwitch2Event2", Location = "PLC1", Destination = "ESMS2")]
    [DBGroup(UnitNumber = 2)]
    public class FurnaceSwitch2Event : EsmsBaseEvent
    {
        //66 Печной выключатель 2, напряжение фаза 1, кВ
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL358")]
        public float VoltagePhase1 { get; set; }        //DB824.DBD992
        //67 Печной выключатель 2, напряжение фаза 2, кВ
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL362")]
        public float VoltagePhase2 { get; set; }        //DB824.DBD996
        //68 Печной выключатель 2, напряжение фаза 3, кВ
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL366")]
        public float VoltagePhase3 { get; set; }        //DB824.DBD1000
        //69 Печной выключатель 2, ток фаза 1, А
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL370")]
        public float AmperagePhase1 { get; set; }        //DB824.DBD1004
        //70 Печной выключатель 2, ток фаза 2, А
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL374")]
        public float AmperagePhase2 { get; set; }        //DB824.DBD1008
        //71 Печной выключатель 2 ток фаза 3, А
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL378")]
        public float AmperagePhase3 { get; set; }        //DB824.DBD1012
        //72 Печной выключатель 2, активная мощность
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL382")]
        public float ActiveEnergy { get; set; }        //DB824.DBD1016
        //73 Печной выключатель 2, реактивная мощность
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL386")]
        public float ReactiveEnergy { get; set; }        //DB824.DBD1020
        //74 Печной выключатель 2, косинус фи
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL390")]
        public float CosFi { get; set; }        //DB824.DBD1024
        //75 Печной выключатель 2, активная энергия
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL394")]
        public float ActiveEnergyToo { get; set; }        //DB824.DBD1028
        //76 Печной выключатель 2, реактивная энергия
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL398")]
        public float ReactiveEnergyToo{ get; set; }        //DB824.DBD1032
        //77 Резерв 1
        [DataMember]
        //[DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL402")]
        public float Reserve { get; set; } 
    }
}    
