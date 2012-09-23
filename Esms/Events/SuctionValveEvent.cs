using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;
 
//Аспирационные задвижки
namespace Esms
{
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "SuctionValveEvent2", Location = "PLC3", Destination = "ESMS2")]
    public class SuctionValveEvent : EsmsBaseEvent
    {
        //57 Статус задвижки 6Р13 2141
        [DataMember]
        [PLCPoint(Location = "DB550,INT300")]
        public int StatusValve6Р13n2141 { set; get; }        //DB26.DBW0
        //58 Статус задвижки 6Р13 2142
        [DataMember]
        [PLCPoint(Location = "DB550,INT302")]
        public int StatusValve6Р13n2142 { set; get; }        //DB26.DBW2
        //59 Статус задвижки 6Р13 2143
        [DataMember]
        [PLCPoint(Location = "DB550,INT304")]
        public int StatusValve6Р13n2143 { set; get; }        //DB26.DBW4
        //60 Статус задвижки 6Р13 2144
        [DataMember]
        [PLCPoint(Location = "DB550,INT306")]
        public int StatusValve6Р13n2144 { set; get; }        //DB26.DBW6
        //61 Статус задвижки 6Р13 2145
        [DataMember]
        [PLCPoint(Location = "DB550,INT308")]
        public int StatusValve6Р13n2145 { set; get; }        //DB26.DBW8
        //62 Статус задвижки 6Р14 2147
        [DataMember]
        [PLCPoint(Location = "DB550,INT312")]
        public int StatusValve6Р14n2147 { set; get; }        //DB26.DBW12
        //63 Статус задвижки 6Р14 2148
        [DataMember]
        [PLCPoint(Location = "DB550,INT314")]
        public int StatusValve6Р14n2148 { set; get; }        //DB26.DBW14
        //64 Статус задвижки 6Р14 2149
        [DataMember]
        [PLCPoint(Location = "DB550,INT316")]
        public int StatusValve6Р14n2149 { set; get; }        //DB26.DBW16
        //65 Статус задвижки 6Р14 2150
        [DataMember]
        [PLCPoint(Location = "DB550,INT318")]
        public int StatusValve6Р14n2150 { set; get; }        //DB26.DBW18
        //66 Статус задвижки 6Р15 2160
        [DataMember]
        [PLCPoint(Location = "DB550,INT320")]
        public int StatusValve6Р15n2160 { set; get; }        //DB26.DBW20
        //67 Статус задвижки 6Р16 2161
        [DataMember]
        [PLCPoint(Location = "DB550,INT322")]
        public int StatusValve6Р16n2161 { set; get; }        //DB26.DBW22
    }
}  
