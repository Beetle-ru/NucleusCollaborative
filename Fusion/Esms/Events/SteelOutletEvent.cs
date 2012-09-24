using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;

//Сталевыпускное отверстие
namespace Esms
{
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "SteelOutletEvent2", Location = "PLC1", Destination = "ESMS2")]
    [DBGroup(UnitNumber = 2)]
    public class SteelOutletEvent : EsmsBaseEvent
    {
        //65 Шибер сталевыпускного отверстия открыт 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,BYTE8", IsBoolean = true, BitNumber = 0)]
        public bool GateOpen { set; get; }        //DB822.DBX414.3
        //66 Шибер сталевыпускного отверстия закрыт 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,BYTE8", IsBoolean = true, BitNumber = 1)]
        public bool GateClose { set; get; }        //DB822.DBX414.4
        //67 Засыпка сталевыпускного отверстия ОК 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,BYTE8", IsBoolean = true, BitNumber = 2)]
        public bool BackfillOk { set; get; }        //DB822.DBX415.3
        //68 Выпуск
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,BYTE8", IsBoolean = true, BitNumber = 3)]
        public bool Output { get; set; }   
        //69 Резерв 1
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE8", IsBoolean = true, BitNumber = 4)]
        public bool Reserve1 { get; set; }   
        //70 Резерв 2
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE8", IsBoolean = true, BitNumber = 5)]
        public bool Reserve2 { get; set; }   
        //71 Резерв 3
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE8", IsBoolean = true, BitNumber = 6)]
        public bool Reserve3 { get; set; }   
        //72 Резерв 4
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE8", IsBoolean = true, BitNumber = 7)]
        public bool Reserve4 { get; set; }   
        //73 Резерв 5
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE8", IsBoolean = true, BitNumber = 0)]
        public bool Reserve5 { get; set; }   
        //74 Резерв 6
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE8", IsBoolean = true, BitNumber = 1)]
        public bool Reserve6 { get; set; }   
        //75 Резерв 7
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE8", IsBoolean = true, BitNumber = 2)]
        public bool Reserve7 { get; set; }   
        //76 Резерв 8
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE8", IsBoolean = true, BitNumber = 3)]
        public bool Reserve8 { get; set; }   
        //77 Резерв 9
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE8", IsBoolean = true, BitNumber = 4)]
        public bool Reserve9 { get; set; }   
        //78 Резерв 10
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE8", IsBoolean = true, BitNumber = 5)]
        public bool Reserve10 { get; set; }   
        //79 Резерв 11
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE8", IsBoolean = true, BitNumber = 6)]
        public bool Reserve11 { get; set; }   
        //80 Резерв 12
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE8", IsBoolean = true, BitNumber = 7)]
        public bool Reserve12 { get; set; }  
    }
}

