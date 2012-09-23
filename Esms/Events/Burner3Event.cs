using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;

//Горелка3
namespace Esms
{ 
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "Burner3Event2", Location = "PLC2", Destination = "ESMS2")]
    [DBGroup(UnitNumber = 2)]
    public class Burner3Event : EsmsBaseEvent
    {
        //48 Давление сжатого воздуха на продувку сопла горелки 3
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE4", IsBoolean = true, BitNumber = 0)]
        public bool PressureAirBlowing { set; get; }        //DB902.DBX342.0
        //51 Тест утечки газа работает, горелка 3
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE4", IsBoolean = true, BitNumber = 1)]
        public bool TestGasLeak { set; get; }        //DB902.DBX342.6
        //Резерв 1
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE4", IsBoolean = true, BitNumber = 2)]
        public bool Reserv1 { set; get; }     
        //Резерв 2
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE4", IsBoolean = true, BitNumber = 3)]
        public bool Reserv2 { set; get; }         
        //Резерв 3
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE4", IsBoolean = true, BitNumber = 4)]
        public bool Reserv3 { set; get; }         
        //Резерв 4
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE4", IsBoolean = true, BitNumber = 5)]
        public bool Reserv4 { set; get; }         
        //Резерв 5
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE4", IsBoolean = true, BitNumber = 6)]
        public bool Reserv5 { set; get; }         
        //Резерв 6
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE4", IsBoolean = true, BitNumber = 7)]
        public bool Reserv6 { set; get; }         
        //Резерв 7
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE5", IsBoolean = true, BitNumber = 0)]
        public bool Reserv7 { set; get; }         
        //Резерв 8
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE5", IsBoolean = true, BitNumber = 1)]
        public bool Reserv8 { set; get; }         
        //Резерв 9
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE5", IsBoolean = true, BitNumber = 2)]
        public bool Reserv9 { set; get; }         
        //Резерв 10
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE5", IsBoolean = true, BitNumber = 3)]
        public bool Reserv10 { set; get; }         
        //Резерв 11
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE5", IsBoolean = true, BitNumber = 4)]
        public bool Reserv11 { set; get; }         
        //Резерв 12
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE5", IsBoolean = true, BitNumber = 5)]
        public bool Reserv12 { set; get; }         
        //Резерв 13
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE5", IsBoolean = true, BitNumber = 6)]
        public bool Reserv13 { set; get; }         
        //Резерв 14
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE5", IsBoolean = true, BitNumber = 7)]
        public bool Reserv14 { set; get; }         
        //39 Текущий расход природного газа, горелка 3, м3/ч
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL244")]
        public float CurrentNaturalGasFlow { get; set; }        //DB904.DBD584
        //40 Текущая уставка по расходу природного газа, горелка 3, м3/ч
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL248")]
        public float SetpointNaturalGasFlow { get; set; }        //DB904.DBD504
        //41 Расход природного газа с начала плавки, горелка 3, м3
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL252")]
        public float NaturalGasFlow { get; set; }        //DB904.DBD604
        //42 Текущий расход кислорода, горелка 3, м3/ч
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL256")]
        public float CurrentOxygenFlow { get; set; }        //DB904.DBD580
        //43 Текущая уставка по расходу кислорода, горелка 3, м3/ч
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL260")]
        public float SetpointOxygenFlow { get; set; }        //DB904.DBD500
        //44 Расход кислорода с начала плавки, горелка 3, м3
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL264")]
        public float OxygenFlow  { get; set; }        //DB904.DBD600
        //45 Соотношение газ/кислород (коэф-т)
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL268")]
        public float RatioGasOxygen { get; set; }        //DB904.DBD492
        //46 Программа горелки 3
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,INT272")]
        public int Program { set; get; }        //DB901.DBW24
        //47 Статус горелки 3
        [DataMember]
        [PLCPoint(Location = "DB550,INT274")]
        public int Status { set; get; }        //DB902.DBW328
        //49 Запорный клапан 1 газа, горелка 3, статус
        [DataMember]
        [PLCPoint(Location = "DB550,INT276")]
        public int StatusGasShutOffValve1 { set; get; }        //DB902.DBW316
        //50 Запорный клапан 2 газа, горелка 3, статус
        [DataMember]
        [PLCPoint(Location = "DB550,INT278")]
        public int StatusGasShutOffValve2 { set; get; }        //DB902.DBW318
        //52 Положение регулирующего клапана подачи газа, %
        [DataMember]
        [PLCPoint(Location = "DB550,REAL280")]
        public float PositionControlValveGas { get; set; }        //DB904.DBD564
        //53 Регулирующий клапан газа, горелка 3, статус управляющего вентиля
        [DataMember]
        [PLCPoint(Location = "DB550,INT284")]
        public int StatusControlVentilGas { set; get; }        //DB902.DBW610
        //54 Регулирующий клапан газа, горелка 3, статус
        [DataMember]
        [PLCPoint(Location = "DB550,INT286")]
        public int StatusControlValveGas { set; get; }        //DB902.DBW324
        //55 Положение регулирующего клапана подачи кислорода, горелка 3, %
        [DataMember]
        [PLCPoint(Location = "DB550,REAL288")]
        public float PositionControlValveOxygen { get; set; }        //DB904.DBD560
        //56 Регулирующий клапан кислорода, горелка 3, статус управляющего вентиля
        [DataMember]
        [PLCPoint(Location = "DB550,INT292")]
        public int StatusControlVentilOxygen { set; get; }        //DB902.DBW618
        //57 Регулирующий клапан кислорода, горелка 3, статус
        [DataMember]
        [PLCPoint(Location = "DB550,INT294")]
        public int StatusControlValveOxygen { set; get; }        //DB902.DBW322
        //Время работы горелки за плавку в секундах 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL296")]
        public float BurnerWorkTime { get; set; }
        //Резерв 15
        [DataMember]
        [PLCPoint(Location = "DB550,REAL300")]
        public float Reserv15 { get; set; }	
        //Резерв 16
        [DataMember]
        [PLCPoint(Location = "DB550,REAL304")]
        public float Reserv16 { get; set; }	
        //Резерв 17
        [DataMember]
        [PLCPoint(Location = "DB550,REAL308")]
        public float Reserv17 { get; set; }	
        //Резерв 18
        [DataMember]
        [PLCPoint(Location = "DB550,REAL312")]
        public float Reserv18 { get; set; }	
    }
}    
