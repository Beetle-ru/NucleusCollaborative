using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;

//Печной выключатель общие сигналы
namespace Esms
{ 
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "FurnaceSwitchCommonEvent2", Location = "PLC1", Destination = "ESMS2")]
    [DBGroup(UnitNumber = 2)]
    public class FurnaceSwitchCommonEvent : EsmsBaseEvent
    {
        //129 Печной выключатель готов к включению 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,BYTE16", IsBoolean = true, BitNumber = 0)]
        public bool Ready  { set; get; }        //DB822.DBX768.0
        //130 Печной выключатель авария 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,BYTE16", IsBoolean = true, BitNumber = 1)]
        public bool Failure { set; get; }        //DB822.DBX768.1
        //131 Печной выключатель ошибка 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,BYTE16", IsBoolean = true, BitNumber = 2)]
        public bool Error  { set; get; }        //DB822.DBX768.2
        //132 Печной выключатель замкнут (печь включена 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,BYTE16", IsBoolean = true, BitNumber = 3)]
        public bool Close  { set; get; }        //DB822.DBX768.3
        //133 Печной выключатель разомкнут (печь выключена) 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,BYTE16", IsBoolean = true, BitNumber = 4)]
        public bool Open  { set; get; }        //DB822.DBX768.4
        //134 Выбран печной выключатель №1 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,BYTE16", IsBoolean = true, BitNumber = 5)]
        public bool Switch1 { set; get; }        //DB822.DBX754.2
        //135 Выбран печной выключатель №2 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,BYTE16", IsBoolean = true, BitNumber = 6)]
        public bool Switch2 { set; get; }        //DB822.DBX761.2
        //136 Резерв 1 
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE16", IsBoolean = true, BitNumber = 7)]
        public bool Reserve1 { get; set; } 
        //137 Резерв 2 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,BYTE17", IsBoolean = true, BitNumber = 0)]
        public bool Reserve2 { get; set; } 
        //138 Резерв 3 
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE17", IsBoolean = true, BitNumber = 1)]
        public bool Reserve3 { get; set; } 
        //139 Резерв 4 
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE17", IsBoolean = true, BitNumber = 2)]
        public bool Reserve4 { get; set; } 
        //140 Резерв 5 
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE17", IsBoolean = true, BitNumber = 3)]
        public bool Reserve5 { get; set; } 
        //141 Резерв 6 
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE17", IsBoolean = true, BitNumber = 4)]
        public bool Reserve6 { get; set; } 
        //142 Резерв 7 
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE17", IsBoolean = true, BitNumber = 5)]
        public bool Reserve7 { get; set; } 
        //143 Резерв 8 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,BYTE17", IsBoolean = true, BitNumber = 6)]
        public bool Reserve8 { get; set; } 
        //144 Резерв 9 
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE17", IsBoolean = true, BitNumber = 7)]
        public bool Reserve9 { get; set; } 

    }
}