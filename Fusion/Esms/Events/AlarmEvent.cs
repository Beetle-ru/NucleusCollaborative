using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;

//Подача к печи/ковшу
namespace Esms
{ 
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "AlarmEvent2", Location = "PLC3", Destination = "ESMS2")]
    public class AlarmEvent : EsmsBaseEvent
    {
        //Привод вибропитателя 6П4 (привод 2201) общ. неиспр.
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE10", IsBoolean = true, BitNumber = 0)]
        public bool Drive2201 { set; get; }        //DB9.DBX2.0
        //Привод вибропитателя 6П4 (привод 2202) общ. неиспр.
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE10", IsBoolean = true, BitNumber = 1)]
        public bool Drive2202 { set; get; }        //DB9.DBX2.1
        //Привод вибропитателя 6П4 (привод 2203) общ. неиспр.
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE10", IsBoolean = true, BitNumber = 2)]
        public bool Drive2203 { set; get; }        //DB9.DBX2.2
        //Привод вибропитателя 6П4 (привод 2204) общ. неиспр.
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE10", IsBoolean = true, BitNumber = 3)]
        public bool Drive2204 { set; get; }        //DB9.DBX2.3
        //Привод вибропитателя 6П4 (привод 2205) общ. неиспр.
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE10", IsBoolean = true, BitNumber = 4)]
        public bool Drive2205 { set; get; }        //DB9.DBX2.4
        //Привод вибропитателя 6П4 (привод 2206) общ. неиспр.
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE10", IsBoolean = true, BitNumber = 5)]
        public bool Drive2206 { set; get; }        //DB9.DBX2.5
        //Привод вибропитателя 6П4 (привод 2207) общ. неиспр.
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE10", IsBoolean = true, BitNumber = 6)]
        public bool Drive2207 { set; get; }        //DB9.DBX2.6
        //Привод вибропитателя 6П4 (привод 2208) общ. неиспр.
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE10", IsBoolean = true, BitNumber = 7)]
        public bool Drive2208 { set; get; }        //DB9.DBX2.7
        //Привод вибропитателя 6П4 (привод 2209) общ. неиспр.
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE11", IsBoolean = true, BitNumber = 0)]
        public bool Drive2209 { set; get; }        //DB9.DBX3.0
        //Привод вибропитателя 6П4 (привод 2210) общ. неиспр.
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE11", IsBoolean = true, BitNumber = 1)]
        public bool Drive2210 { set; get; }        //DB9.DBX3.1
        //Привод задвижки 6Р13 (привод 2141) общ. неиспр.
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE11", IsBoolean = true, BitNumber = 2)]
        public bool Drive2141 { set; get; }        //DB9.DBX3.2
        //Привод задвижки 6Р13 (привод 2142) общ. неиспр.
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE11", IsBoolean = true, BitNumber = 3)]
        public bool Drive2142 { set; get; }        //DB9.DBX3.3
        //Привод задвижки 6Р13 (привод 2143) общ. неиспр.
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE11", IsBoolean = true, BitNumber = 4)]
        public bool Drive2143 { set; get; }        //DB9.DBX3.4
        //Привод задвижки 6Р13 (привод 2144) общ. неиспр.
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE11", IsBoolean = true, BitNumber = 5)]
        public bool Drive2144 { set; get; }        //DB9.DBX3.5
        //Привод задвижки 6Р13 (привод 2145) общ. неиспр.
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE11", IsBoolean = true, BitNumber = 6)]
        public bool Drive2145 { set; get; }        //DB9.DBX3.6
        //Привод задвижки 6Р13 (привод 2146) общ. неиспр.
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE11", IsBoolean = true, BitNumber = 7)]
        public bool Drive2146 { set; get; }        //DB9.DBX3.7
        //Привод задвижки 6Р14 (привод 2147) общ. неиспр.
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE12", IsBoolean = true, BitNumber = 0)]
        public bool Drive2147 { set; get; }        //DB9.DBX4.0
        //Привод задвижки 6Р14 (привод 2148) общ. неиспр.
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE12", IsBoolean = true, BitNumber = 1)]
        public bool Drive2148 { set; get; }        //DB9.DBX4.1
        //Привод задвижки 6Р14 (привод 2149) общ. неиспр.
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE12", IsBoolean = true, BitNumber = 2)]
        public bool Drive2149 { set; get; }        //DB9.DBX4.2
        //Привод задвижки 6Р14 (привод 2150) общ. неиспр.
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE12", IsBoolean = true, BitNumber = 3)]
        public bool Drive2150 { set; get; }        //DB9.DBX4.3
        //Привод задвижки 6Р15 (привод 2160) общ. неиспр.
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE12", IsBoolean = true, BitNumber = 4)]
        public bool Drive2160 { set; get; }        //DB9.DBX4.4
        //Привод задвижки 6Р16 (привод 2161) общ. неиспр.
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE12", IsBoolean = true, BitNumber = 5)]
        public bool Drive2161 { set; get; }        //DB9.DBX4.5
        //Привод конвейера 6КВ4 (привод 2009) общ. неиспр.
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE12", IsBoolean = true, BitNumber = 6)]
        public bool Drive2009 { set; get; }        //DB9.DBX4.6
        //Привод весового питателя 6В5 (привод 2211) общ. неиспр.
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE12", IsBoolean = true, BitNumber = 7)]
        public bool Drive2211 { set; get; }        //DB9.DBX4.7
        //Привод весового питателя 6В5 (привод 2212) общ. неиспр.
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE13", IsBoolean = true, BitNumber = 0)]
        public bool Drive2212 { set; get; }        //DB9.DBX5.0
        //Привод весового питателя 6В5 (привод 2213) общ. неиспр.
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE13", IsBoolean = true, BitNumber = 1)]
        public bool Drive2213 { set; get; }        //DB9.DBX5.1
        //Привод весового питателя 6В5 (привод 2214) общ. неиспр.
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE13", IsBoolean = true, BitNumber = 2)]
        public bool Drive2214 { set; get; }        //DB9.DBX5.2
    }
}