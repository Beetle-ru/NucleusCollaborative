using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;

//Пальцы
namespace Esms
{  
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "FingersEvent2", Location = "PLC1", Destination = "ESMS2")]
    [DBGroup(UnitNumber = 2)]
    public class FingersEvent : EsmsBaseEvent
    {
        //1	Пальцы открыты
		[DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,BYTE0", IsBoolean = true, BitNumber = 0)]
        public bool FingersOpen { get; set; }        //DB822.DBX283.3
        //2	Пальцы почти открыты
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE0", IsBoolean = true, BitNumber = 1)]
        public bool AlmostOpen { get; set; }        //DB822.DBX283.4
        //3	Пальцы почти закрыты
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE0", IsBoolean = true, BitNumber = 2)]
        public bool AlmostClose { get; set; }        //DB822.DBX283.5
        //4	Пальцы закрыты
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,BYTE0", IsBoolean = true, BitNumber = 3)]
        public bool FingersClose { get; set; }        //DB822.DBX283.6
        //5 Бадья открыта
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,BYTE0", IsBoolean = true, BitNumber = 4)]
        public bool TubOpen { get; set; }
        //6 Бадья закрыта
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,BYTE0", IsBoolean = true, BitNumber = 5)]
        public bool TubClose { get; set; }
        //7 Резерв 1
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE0", IsBoolean = true, BitNumber = 6)]
        public bool Reserve1 { get; set; }
        //8 Резерв 2
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE0", IsBoolean = true, BitNumber = 7)]
        public bool Reserve2 { get; set; }	
        //9 Резерв 3
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE1", IsBoolean = true, BitNumber = 0)]
        public bool Reserve3 { get; set; }	
        //10 Резерв 4
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE1", IsBoolean = true, BitNumber = 1)]
        public bool Reserve4 { get; set; }	
        //11 Резерв 5
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE1", IsBoolean = true, BitNumber = 2)]
        public bool Reserve5 { get; set; }	
        //12 Резерв 6
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE1", IsBoolean = true, BitNumber = 3)]
        public bool Reserve6 { get; set; }	
        //13 Резерв 7
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE1", IsBoolean = true, BitNumber = 4)]
        public bool Reserve7 { get; set; }	
        //14 Резерв 8
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE1", IsBoolean = true, BitNumber = 5)]
        public bool Reserve8 { get; set; }	
        //15 Резерв 9
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE1", IsBoolean = true, BitNumber = 6)]
        public bool Reserve9 { get; set; }	
        //16 Резерв 10
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE1", IsBoolean = true, BitNumber = 7)]
        public bool Reserve10 { get; set; }	
		//193 Позиция пальцев со стороны выпуска 
        [DataMember]
        [PLCPoint(Location = "DB550,REAL880")]
        public float PositionOutput { get; set; }        //DB824.DBD156
        //194 Позиция пальцев со стороны шлака 
        [DataMember]
        [PLCPoint(Location = "DB550,REAL884")]
        public float PositionSlag { get; set; }        //DB824.DBD160

    }
}    
