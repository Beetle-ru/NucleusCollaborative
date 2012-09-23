using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;

//Охлаждающая вода на газоход
namespace Esms
{ 
    //Охлаждающая вода на газоход
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "WaterCoolingFlueEvent2", Location = "PLC1", Destination = "ESMS2")]
    [DBGroup(UnitNumber = 2)]
    public class WaterCoolingFlueEvent : EsmsBaseEvent
    {
        //104 Расход охлаждающей воды на выходе газоотв-го тракта
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL502")]
        public float Flow1WaterOutputFlue { get; set; }        //DB824.DBD792
        //105 Температура охлаждающей воды на выходе газоотв-го тракта
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL506")]
        public float Temp1WaterOutputFlue { get; set; }        //DB824.DBD796
        //106 Температура охлаждающей воды на выходе газоотв-го тракта
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL510")]
        public float Temp2WaterOutputFlue { get; set; }        //DB824.DBD800
        //107 Температура охлаждающей воды на выходе газоотв-го тракта
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL514")]
        public float Temp3WaterOutputFlue { get; set; }        //DB824.DBD804
        //108 Температура 1 охлаждающей воды на выходе газоотв-го тракта (2/3)
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL518")]
        public float Temp1WaterOutputFlue2div3 { get; set; }        //DB824.DBD808
        //109 Температура 2 охлаждающей воды на выходе газоотв-го тракта (2/3)
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL522")]
        public float Temp2WaterOutputFlue2div3 { get; set; }        //DB824.DBD812
        //110 Температура 3 охлаждающей воды на выходе газоотв-го тракта (2/3)
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL526")]
        public float Temp3WaterOutputFlue2div3 { get; set; }        //DB824.DBD816
        //111 Температура 4 охлаждающей воды на выходе газоотв-го тракта (2/3)
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL530")]
        public float Temp4WaterOutputFlue2div3 { get; set; }        //DB824.DBD820
        //112 Температура 5 охлаждающей воды на выходе газоотв-го тракта (2/3)
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL534")]
        public float Temp5WaterOutputFlue2div3 { get; set; }        //DB824.DBD824
        //113 Температура 6 охлаждающей воды на выходе газоотв-го тракта (2/3)
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL538")]
        public float Temp6WaterOutputFlue2div3 { get; set; }        //DB824.DBD828
        //114 Расход охлаждающей воды на выходе газоотв-го тракта
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL542")]
        public float Flow2WaterOutputFlue { get; set; }        //DB824.DBD832
        //115 Расход охлаждающей воды на выходе газоотв-го тракта
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL546")]
        public float Flow3WaterOutputFlue { get; set; }        //DB824.DBD836
    }
}    
