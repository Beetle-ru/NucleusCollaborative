using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;

//Печной выключатель 1
namespace Esms
{     
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "FurnaceSwitch1Event2", Location = "PLC1", Destination = "ESMS2")]
    [DBGroup(UnitNumber = 2)]
    public class FurnaceSwitch1Event : EsmsBaseEvent
    {
        //54 Печной выключатель 1, напряжение фаза 1, кВ 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL310")]
        public float VoltagePhase1 { get; set; }        //DB824.DBD940
        //55 Печной выключатель 1, напряжение фаза 2, кВ 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL314")]
        public float VoltagePhase2 { get; set; }        //DB824.DBD944
        //56 Печной выключатель 1, напряжение фаза 3, кВ 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL318")]
        public float VoltagePhase3 { get; set; }        //DB824.DBD948
        //57 Печной выключатель 1, ток фаза 1, А 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL322")]
        public float AmperagePhase1 { get; set; }        //DB824.DBD952
        //58 Печной выключатель 1, ток фаза 2, А 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL326")]
        public float AmperagePhase2 { get; set; }        //DB824.DBD956
        //59 Печной выключатель 1, ток фаза 3, А 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL330")]
        public float AmperagePhase3 { get; set; }        //DB824.DBD960
        //60 Печной выключатель 1, активная мощность 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL334")]
        public float ActiveEnergy { get; set; }        //DB824.DBD964
        //61 Печной выключатель 1, реактивная мощность 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL338")]
        public float ReactiveEnergy { get; set; }        //DB824.DBD968
        //62 Печной выключатель 1, косинус фи 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL342")]
        public float CosFi { get; set; }        //DB824.DBD972
        //63 Печной выключатель 1, активная энергия 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL346")]
        public float ActiveEnergyToo { get; set; }        //DB824.DBD976
        //64 Печной выключатель 1, реактивная энергия 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL350")]
        public float ReactiveEnergyToo { get; set; }        //DB824.DBD980
        //65 Резерв 1 
        [DataMember]
        //[DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL354")]
        public float Reserve { get; set; }
    }
}    
