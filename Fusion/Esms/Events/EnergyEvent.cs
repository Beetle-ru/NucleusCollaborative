using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;
 
//Подача энергоносителей
namespace Esms
{
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "EnergyEvent2", Location = "PLC2", Destination = "ESMS2")]
    [DBGroup(UnitNumber = 2)]
    public class EnergyEvent : EsmsBaseEvent
    {
        //182 Давление газа перед главным клапаном
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE20", IsBoolean = true, BitNumber = 0)]
        public bool PressureGasBeforeMainValve { set; get; }        //DB902.DBX236.0
        //183 Давление газа после главного клапана
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE20", IsBoolean = true, BitNumber = 1)]
        public bool PressureGasAfterMainValve { set; get; }        //DB902.DBX236.1
        //187 Давление кислорода перед главным клапаном
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE20", IsBoolean = true, BitNumber = 2)]
        public bool PressureOxygenBeforeMainValve { set; get; }        //DB902.DBX228.0
        //191 Давление воздуха на продувку
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE20", IsBoolean = true, BitNumber = 3)]
        public bool PressureAirBlowing { set; get; }        //DB902.DBX220.1
        //193 Давление воздуха (для управления пневмоклапанами) 
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE20", IsBoolean = true, BitNumber = 4)]
        public bool PressureAirControlPneumatic  { set; get; }        //DB902.DBX220.0
        //Резерв 1
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE20", IsBoolean = true, BitNumber = 5)]
        public bool Reserv1 { set; get; }         
        //Резерв 2
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE20", IsBoolean = true, BitNumber = 6)]
        public bool Reserv2 { set; get; }         
        //Резерв 3
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE20", IsBoolean = true, BitNumber = 7)]
        public bool Reserv3 { set; get; }         
        //Резерв 4
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE21", IsBoolean = true, BitNumber = 0)]
        public bool Reserv4 { set; get; }         
        //Резерв 5
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE21", IsBoolean = true, BitNumber = 1)]
        public bool Reserv5 { set; get; }         
        //Резерв 6
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE21", IsBoolean = true, BitNumber = 2)]
        public bool Reserv6 { set; get; }         
        //Резерв 7
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE21", IsBoolean = true, BitNumber = 3)]
        public bool Reserv7 { set; get; }         
        //Резерв 8
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE21", IsBoolean = true, BitNumber = 4)]
        public bool Reserv8 { set; get; }         
        //Резерв 9
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE21", IsBoolean = true, BitNumber = 5)]
        public bool Reserv9 { set; get; }         
        //Резерв 10
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE21", IsBoolean = true, BitNumber = 6)]
        public bool Reserv10 { set; get; }         
        //Резерв 11
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE21", IsBoolean = true, BitNumber = 7)]
        public bool Reserv11 { set; get; }
        //184 Главный клапан газа, статус
        [DataMember]
        [PLCPoint(Location = "DB550,INT764")]
        public int StatusMainValveGas { set; get; }        //DB902.DBW230
        //185 Содержание газа, %
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL766")]
        public float ContetntGas { get; set; }        //DB904.DBD176
        //186 Давление газа, бар
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL770")]
        public float PressureGas { get; set; }        //DB904.DBD164
        //188 Главный клапан кислорода, статус
        [DataMember]
        [PLCPoint(Location = "DB550,INT774")]
        public int StatusMainValveOxygen{ set; get; }        //DB902.DBW222
        //189 Содержание кислорода, %
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL776")]
        public float ContentOxygen { get; set; }        //DB904.DBD172
        //190 Давление кислорода, бар
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL780")]
        public float PressureOxygen { get; set; }        //DB904.DBD160
        //192 Главный клапан воздуха, статус
        [DataMember]
        [PLCPoint(Location = "DB550,INT784")]
        public int StatusMainValveAir { set; get; }        //DB902.DBW256
        
        
        
    }
}  
