using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;

//Печь
namespace Esms
{    
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "FurnaceEvent2", Location = "PLC1", Destination = "ESMS2")]
    [DBGroup(UnitNumber = 2)]
    public class FurnaceEvent : EsmsBaseEvent
    {
        //201 Угол наклона печи (в градусах) 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL912")]
        public float TiltAngle { get; set; }        //DB824.DBD308
        //202 Давление в печи 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL916")]
        public float Pressure { get; set; }        //DB824.DBD432
    }
}    
