using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;

//CELOX
namespace Esms
{    
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "CeloxEvent2", Location = "PLC1", Destination = "ESMS2")]
    [DBGroup(UnitNumber = 2)]
    public class CeloxEvent : EsmsBaseEvent
    {
        //185 Celox время замера
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,DT844")]
        public DateTime MeasurementTime { set; get; } 
        //186 Celox температура
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL852 ")]
        public float Temperature { get; set; }        //DB824.DBD1388
        //187 Celox EMF
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL856 ")]
        public float EMF { get; set; }        //DB824.DBD1392
        //188 Celox кислород
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL860 ")]
        public float Oxygen { get; set; }        //DB824.DBD1396
        //189 Celox углерод
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL864 ")]
        public float Carbon { get; set; }        //DB824.DBD1400
    }
}    
