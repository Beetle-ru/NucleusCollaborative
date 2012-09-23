using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;
 
//Инжектор 2
namespace Esms
{
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "Injector2Event2", Location = "PLC2", Destination = "ESMS2")]
    [DBGroup(UnitNumber = 2)]
    public class Injector2Event : EsmsBaseEvent
    {
        //128 Тест утечки газа работает, инжектор 2
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE12", IsBoolean = true, BitNumber = 0)]
        public bool TestGasLeak { set; get; }        //DB902.DBX504.6
        //Резерв 1
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE12", IsBoolean = true, BitNumber = 1)]
        public bool Reserv1 { set; get; }     
        //Резерв 2
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE12", IsBoolean = true, BitNumber = 2)]
        public bool Reserv2 { set; get; }         
        //Резерв 3
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE12", IsBoolean = true, BitNumber = 3)]
        public bool Reserv3 { set; get; }         
        //Резерв 4
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE12", IsBoolean = true, BitNumber = 4)]
        public bool Reserv4 { set; get; }         
        //Резерв 5
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE12", IsBoolean = true, BitNumber = 5)]
        public bool Reserv5 { set; get; }         
        //Резерв 6
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE12", IsBoolean = true, BitNumber = 6)]
        public bool Reserv6 { set; get; }         
        //Резерв 7
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE12", IsBoolean = true, BitNumber = 7)]
        public bool Reserv7 { set; get; }         
        //Резерв 8
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE13", IsBoolean = true, BitNumber = 0)]
        public bool Reserv8 { set; get; }         
        //Резерв 9
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE13", IsBoolean = true, BitNumber = 1)]
        public bool Reserv9 { set; get; }         
        //Резерв 10
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE13", IsBoolean = true, BitNumber = 2)]
        public bool Reserv10 { set; get; }         
        //Резерв 11
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE13", IsBoolean = true, BitNumber = 3)]
        public bool Reserv11 { set; get; }         
        //Резерв 12
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE13", IsBoolean = true, BitNumber = 4)]
        public bool Reserv12 { set; get; }         
        //Резерв 13
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE13", IsBoolean = true, BitNumber = 5)]
        public bool Reserv13 { set; get; }         
        //Резерв 14
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE13", IsBoolean = true, BitNumber = 6)]
        public bool Reserv14 { set; get; } 
        //Резерв 15
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE13", IsBoolean = true, BitNumber = 7)]
        public bool Reserv15 { set; get; }
        //104 Текущий расход газа, инжектор 2, м3/ч
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL482")]
        public float CurrentGasFlow { get; set; }        //DB904.DBD1328
        //105 Текущая уставка по расходу природного газа, инжектор 2, м3/ч
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL486")]
        public float SetpointGasFlow { get; set; }        //DB904.DBD1248
        //106 Расход газа с начала плавки, инжектор 2, м3
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL490")]
        public float GasFlow { get; set; }        //DB904.DBD1344
        //107 Текущий расход кислорода (режим горелки), инжектор 2, м3/ч
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL494")]
        public float CurrentOxygenFlowBurner { get; set; }        //DB904.DBD1320
        //108 Текущая уставка по расходу кислорода (режим горелки), инжектор 2, м3/ч
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL498")]
        public float SetpointOxygenFlowBurner { get; set; }        //DB904.DBD1240
        //109 Расход кислорода с начала плавки (режим горелки), инжектор 2, м3
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL502")]
        public float OxygenFlowBurner { get; set; }        //DB904.DBD1336
        //110 Текущий расход кислорода (режим инжектора), инжектор 2, м3/ч
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL506")]
        public float CurrentOxygenFlowInjector { get; set; }        //DB904.DBD1324
        //111 Текущая уставка по расходу кислорода (режим инжектора), инжектор 2, м3/ч
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL510")]
        public float SetpointOxygenFlowIngector { get; set; }        //DB904.DBD1244
        //112 Расход кислорода с начала плавки (режим инжектора), инжектор 2, м3
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL514")]
        public float OxygenFlowIngector { get; set; }        //DB904.DBD1340
        //113 Соотношение газ/кислород (коэф-т)
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL518")]
        public float RatioGasOxygen { get; set; }        //DB904.DBD1232
        //114 Программа инжектора 2
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,INT522")]
        public int Programm { set; get; }        //DB901.DBW42
        //115 Статус инжектора 2
        [DataMember]
        [PLCPoint(Location = "DB550,INT524")]
        public int Status { set; get; }        //DB902.DBW490
        //116 Запорный клапан 1 газа, инжектор 2, статус
        [DataMember]
        [PLCPoint(Location = "DB550,INT526")]
        public int StatusGasShutOffValve1 { set; get; }        //DB902.DBW478
        //117 Запорный клапан 2 газа, инжектор 2, статус
        [DataMember]
        [PLCPoint(Location = "DB550,INT528")]
        public int StatusGasShutOffValve2 { set; get; }        //DB902.DBW480
        //118 Положение регулирующего клапана газа, %
        [DataMember]
        [PLCPoint(Location = "DB550,REAL530")]
        public float PositionControlValveGas { get; set; }        //DB904.DBD1308
        //119 Регулирующий клапан газа, инжектор 2, статус управляющего вентиля
        [DataMember]
        [PLCPoint(Location = "DB550,INT534")]
        public int StatusControlVentilGas { set; get; }        //DB902.DBW624
        //120 Регулирующий клапан газа, инжектор 2, статус
        [DataMember]
        [PLCPoint(Location = "DB550,INT536")]
        public int StatusControlValveGas { set; get; }        //DB902.DBW488
        //121 Положение регулирующего клапана кислорода на горение инжектор 2, %
        [DataMember]
        [PLCPoint(Location = "DB550,REAL538")]
        public float PositionControlValveOxygenBurning { get; set; }        //DB904.DBD1300
        //122 Регулирующий клапан кислорода на горение, инжектор 2, статус управляющего вентиля
        [DataMember]
        [PLCPoint(Location = "DB550,INT542")]
        public int StatusControlVentilOxygenBurning { set; get; }        //DB902.DBW634
        //123 Регулирующий клапан кислорода на горение, инжектор 2, статус
        [DataMember]
        [PLCPoint(Location = "DB550,INT544")]
        public int StatusControlValveOxygenBurning { set; get; }        //DB902.DBW484
        //124 Положение регулирующего клапана подачи кислорода на рафинир-е, %
        [DataMember]
        [PLCPoint(Location = "DB550,REAL546")]
        public float PositionControlValveOxygenRefining { get; set; }        //DB904.DBD1304
        //125 Регулирующий клапан кислорода на рафинирование, инжектор 2, статус управляющего вентиля
        [DataMember]
        [PLCPoint(Location = "DB550,INT550")]
        public int StatusControlVentilOxygenRefining { set; get; }        //DB902.DBW636
        //126 Регулирующий клапан кислорода на рафинирование, инжектор 2, статус
        [DataMember]
        [PLCPoint(Location = "DB550,INT552")]
        public int StatusControlValveOxygenRefining { set; get; }        //DB902.DBW486
        //127 Байпасный клапан кислорода инжектора 2, статус
        [DataMember]
        [PLCPoint(Location = "DB550,INT554")]
        public int BypassValveOxygen { set; get; }        //DB902.DBW492
        //Время работы за плавку в режиме горелки в секундах
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL556")]
        public float BurnerWorkTime { set; get; }	
        //Время работы за плавку в режиме фурмы (низ.) в секундах
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL560")]
        public float LanceWorkTime1 { set; get; }	
        //Время работы за плавку в режиме фурмы (низ.) в секундах
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL564")]
        public float LanceWorkTime2 { set; get; }	
        //Резерв 16
        [DataMember]
        [PLCPoint(Location = "DB550,REAL568")]
        public float Reserv16 { set; get; }  	
        //Резерв 17
        [DataMember]
        [PLCPoint(Location = "DB550,REAL572")]
        public float Reserv17 { set; get; }
        
    }
}    
