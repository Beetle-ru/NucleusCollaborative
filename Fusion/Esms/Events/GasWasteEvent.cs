using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;

//Отходящий газ
namespace Esms
{ 
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "GasWasteEvent2", Location = "PLC1", Destination = "ESMS2")]
    [DBGroup(UnitNumber = 2)]
    public class GasWasteEvent : EsmsBaseEvent
    {
        //203 Уставка на заслонку отходящего газа 
        [DataMember]
        [PLCPoint(Location = "DB550,REAL920")]
        public float SettingThrottle { get; set; }        //DB824.DBD428
        //204 Положение заслонки отходящего газа 
        [DataMember]
        [PLCPoint(Location = "DB550,REAL924")]
        public float PositionThrottle { get; set; }        //DB824.DBD436
        //205 Температура отходящего газа 1 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL928")]
        public float Temperature1 { get; set; }        //DB824.DBD1080
        //206 Температура отходящего газа 2 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL932")]
        public float Temperature2 { get; set; }        //DB824.DBD1084
    }
}    
    