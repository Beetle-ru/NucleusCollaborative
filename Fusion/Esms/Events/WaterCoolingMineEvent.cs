using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;

//Охлаждающая вода на свод, шахту
namespace Esms
{      
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "WaterCoolingMineEvent2", Location = "PLC1", Destination = "ESMS2")]
    [DBGroup(UnitNumber = 2)]
    public class WaterCoolingMineEvent : EsmsBaseEvent
    {
        //92 Температура охлаждающей воды на входе свода
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL454")]
        public float TempWaterInputCrest { get; set; }        //DB824.DBD600
        //93 Температура охлаждающей воды на выходе свода
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL458")]
        public float TempWaterOutputCrest { get; set; }        //DB824.DBD604
        //94 Температура охлаждающей воды центр Pete, замыкание 1
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL462")]
        public float TempWaterPete1 { get; set; }        //DB824.DBD608
        //95 Температура охлаждающей воды центр Pete, замыкание 2
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL466")]
        public float TempWaterPete2 { get; set; }        //DB824.DBD612
        //96 Температура охлаждающей воды на выходе шахты и свода
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL470")]
        public float TempWaterOutputMineCrest { get; set; }        //DB824.DBD616
        //97 Расход охлаждающей воды на выходе шахты и свода
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL474")]
        public float FlowWaterOutputMineCrest { get; set; }        //DB824.DBD620
        //98 Температура охлаждающей воды на входе шахты и воронки
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL478")]
        public float TempWaterOutputMineFunnel { get; set; }        //DB824.DBD624
        //99 Температура охлаждающей воды на выходе шахты и воронки
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL482")]
        public float TempWaterOutputMineFunnelToo { get; set; }        //DB824.DBD628
        //100 Температура охлаждающей воды на входе шахты и колпака
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL486")]
        public float TempWaterOutputMineHood { get; set; }        //DB824.DBD632
        //101 Температура охлаждающей воды на выходе шахты и колпака
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL490")]
        public float TempWaterOutputMineHoodToo { get; set; }        //DB824.DBD636
        //102 Температура охлаждающей воды на выходе пальцев
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL494")]
        public float TempWaterOutputFingers { get; set; }        //DB824.DBD640
        //103 Расход охлаждающей воды на выходе пальцев
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL498")]
        public float FlowWaterOutputFingers { get; set; }        //DB824.DBD644
    }
}    
