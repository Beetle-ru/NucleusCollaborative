using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;

 //Приводы бункеров
namespace Esms
{
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "DrivesBunkersEvent2", Location = "PLC3", Destination = "ESMS2")]
    public class DrivesBunkersEvent : EsmsBaseEvent
    {
        //15 Привод 2201 бункера 1-16 работает
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE0", IsBoolean = true, BitNumber = 0)]
        public bool Drive2201Bunker1t16 { set; get; }        //IX2.0
        //16 Привод 2202 бункера 2-16 работает
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE0", IsBoolean = true, BitNumber = 1)]
        public bool Drive2202Bunker2t16 { set; get; }        //IX4.0
        //17 Привод 2203 бункера 3-16 работает
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE0", IsBoolean = true, BitNumber = 2)]
        public bool Drive2203Bunker3t16{ set; get; }        //IX6.0
        //18 Привод 2204 бункера 4-16 работает
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE0", IsBoolean = true, BitNumber = 3)]
        public bool Drive2204Bunker4t16 { set; get; }        //IX8.0
        //19 Привод 2205 бункера 5-16 работает
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE0", IsBoolean = true, BitNumber = 4)]
        public bool Drive2205Bunker5t16 { set; get; }        //IX10.0
        //20 Привод 2206 бункера 6-16 работает
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE0", IsBoolean = true, BitNumber = 5)]
        public bool Drive2206Bunker6t16 { set; get; }        //IX12.0
        //21 Привод 2207 бункера 7-16 работает
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE0", IsBoolean = true, BitNumber = 6)]
        public bool Drive2207Bunker7t16 { set; get; }        //IX14.0
        //22 Привод 2208 бункера 8-16 работает
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE0", IsBoolean = true, BitNumber = 7)]
        public bool Drive2208Bunker8t16 { set; get; }        //IX16.0
        //23 Привод 2209 бункера 9-16 работает
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE1", IsBoolean = true, BitNumber = 0)]
        public bool Drive2209Bunker9t16 { set; get; }        //IX18.0
        //24 Привод 2210 бункера 10-16 работает
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE1", IsBoolean = true, BitNumber = 1)]
        public bool Drive2210Bunker10t16 { set; get; }        //IX20.0
        //25 Привод 2211 бункера 1-17 работает
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE1", IsBoolean = true, BitNumber = 2)]
        public bool Drive2211Bunker1t17 { set; get; }        //QX50.0
        //26 Привод 2212 бункера 2-17 работает
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE1", IsBoolean = true, BitNumber = 3)]
        public bool Drive2212Bunker2t17 { set; get; }        //QX54.0
        //27 Привод 2213 бункера 3-17 работает
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE1", IsBoolean = true, BitNumber = 4)]
        public bool Drive2213Bunker3t17 { set; get; }        //QX58.0
        //28 Привод 2214 бункера 4-17 работает
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE1", IsBoolean = true, BitNumber = 5)]
        public bool Drive2214Bunker4t17 { set; get; }        //QX62.0
        //Резерв 1
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE1", IsBoolean = true, BitNumber = 6)]
        public bool Reserv1 { set; get; }        
        //Резерв 2
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE1", IsBoolean = true, BitNumber = 7)]
        public bool Reserv2 { set; get; }        
    }
}  
