using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;

//Горелка 1
namespace Esms
{ 
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "Burner1Event2", Location = "PLC2", Destination = "ESMS2")]
    [DBGroup(UnitNumber = 2)]
    public class Burner1Event : EsmsBaseEvent
    {
        //10 Давление сжатого воздуха на продувку сопла горелки 1 
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE0", IsBoolean = true, BitNumber = 0)]
        public bool PressureAirBlowing { set; get; }         //DB902.DBX278.0
        //13 Тест утечки газа работает, горелка 1 
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE0", IsBoolean = true, BitNumber = 1)]
        public bool TestGasLeak { set; get; }         //DB902.DBX278.6
        //Резерв 1
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE0", IsBoolean = true, BitNumber = 2)]
        public bool Reserv1 { set; get; }     
        //Резерв 2
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE0", IsBoolean = true, BitNumber = 3)]
        public bool Reserv2 { set; get; }         
        //Резерв 3
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE0", IsBoolean = true, BitNumber = 4)]
        public bool Reserv3 { set; get; }         
        //Резерв 4
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE0", IsBoolean = true, BitNumber = 5)]
        public bool Reserv4 { set; get; }         
        //Резерв 5
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE0", IsBoolean = true, BitNumber = 6)]
        public bool Reserv5 { set; get; }         
        //Резерв 6
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE0", IsBoolean = true, BitNumber = 7)]
        public bool Reserv6 { set; get; }         
        //Резерв 7
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE1", IsBoolean = true, BitNumber = 0)]
        public bool Reserv7 { set; get; }         
        //Резерв 8
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE1", IsBoolean = true, BitNumber = 1)]
        public bool Reserv8 { set; get; }         
        //Резерв 9
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE1", IsBoolean = true, BitNumber = 2)]
        public bool Reserv9 { set; get; }         
        //Резерв 10
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE1", IsBoolean = true, BitNumber = 3)]
        public bool Reserv10 { set; get; }         
        //Резерв 11
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE1", IsBoolean = true, BitNumber = 4)]
        public bool Reserv11 { set; get; }         
        //Резерв 12
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE1", IsBoolean = true, BitNumber = 5)]
        public bool Reserv12 { set; get; }         
        //Резерв 13
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE1", IsBoolean = true, BitNumber = 6)]
        public bool Reserv13 { set; get; }         
        //Резерв 14
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE1", IsBoolean = true, BitNumber = 7)]
        public bool Reserv14 { set; get; }         
        //1 Текущий расход природного газа, горелка 1, м3/ч 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL100")]
        public float CurrentNaturalGasFlow { get; set; }        //DB904.DBD304
        //2 Текущая уставка по расходу природного газа, горелка 1, м3/ч 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL104")]
        public float SetpointNaturalGasFlow { get; set; }        //DB904.DBD224
        //3 Расход природного газа с начала плавки, горелка 1, м3 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL108")]
        public float NaturalGasFlow { get; set; }        //DB904.DBD324
        //4 Текущий расход кислорода, горелка 1, м3/ч 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL112")]
        public float CurrentOxygenFlow { get; set; }        //DB904.DBD300
        //5 Текущая уставка по расходу кислорода, горелка 1, м3/ч 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL116")]
        public float SetpointOxygenFlow { get; set; }        //DB904.DBD220
        //6 Расход кислорода с начала плавки, горелка 1, м3 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL120")]
        public float OxygenFlow { get; set; }        //DB904.DBD320
        //7 Соотношение газ/кислород (коэф-т) 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL124")]
        public float RatioGasOxygen { get; set; }        //DB904.DBD212
        //8 Программа горелки 1 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,INT128")]
        public int Program { set; get; }        //DB901.DBW20
        //9 Статус горелки 1 
        [DataMember]
        [PLCPoint(Location = "DB550,INT130")]
        public int Status { set; get; }        //DB902.DBW264
        //11 Запорный клапан 1 газа, горелка 1, статус 
        [DataMember]
        [PLCPoint(Location = "DB550,INT132")]
        public int StatusGasShutOffValve1 { set; get; }        //DB902.DBW252
        //12 Запорный клапан 2 газа, горелка 1, статус 
        [DataMember]
        [PLCPoint(Location = "DB550,INT134")]
        public int StatusGasShutOffValve2 { set; get; }        //DB902.DBW254
        //14 Положение регулирующего клапана подачи газа, % 
        [DataMember]
        [PLCPoint(Location = "DB550,REAL136")]
        public float PositionControlValveGas { get; set; }        //DB904.DBD284
        //15 Регулирующий клапан газа, горелка 1, статус управляющего вентиля 
        [DataMember]
        [PLCPoint(Location = "DB550,INT140")]
        public int StatusControlVentilGas { set; get; }        //DB902.DBW606
        //16 Регулирующий клапан газа, горелка 1, статус 
        [DataMember]
        [PLCPoint(Location = "DB550,INT142")]
        public int StatusControlValveGas { set; get; }        //DB902.DBW260
        //17 Положение регулирующего клапана подачи кислорода, горелка 1, % 
        [DataMember]
        [PLCPoint(Location = "DB550,REAL144")]
        public float PositionControlValveOxygen { get; set; }        //DB904.DBD280
        //18 Регулирующий клапан кислорода, горелка 1, статус управляющего вентиля 
        [DataMember]
        [PLCPoint(Location = "DB550,INT148")]
        public int StatusControlVentilOxygen { set; get; }        //DB902.DBW614
        //19 Регулирующий клапан кислорода, горелка 1, статус 
        [DataMember]
        [PLCPoint(Location = "DB550,INT150")]
        public int StatusControlValveOxygen { set; get; }        //DB902.DBW258
        //Время работы горелки за плавку в секундах 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL152")]
        public float BurnerWorkTime { get; set; }
        //Резерв 15
        [DataMember]
        [PLCPoint(Location = "DB550,REAL156")]
        public float Reserv15 { get; set; }	
        //Резерв 16
        [DataMember]
        [PLCPoint(Location = "DB550,REAL160")]
        public float Reserv16 { get; set; }	
        //Резерв 17
        [DataMember]
        [PLCPoint(Location = "DB550,REAL164")]
        public float Reserv17 { get; set; }	
        //Резерв 18
        [DataMember]
        [PLCPoint(Location = "DB550,REAL168")]
        public float Reserv18 { get; set; }	

    }
}    
