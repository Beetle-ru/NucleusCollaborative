using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;

//Инжектор 1
namespace Esms
{ 
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "Injector1Event2", Location = "PLC2", Destination = "ESMS2")]
    [DBGroup(UnitNumber = 2)]
    public class Injector1Event : EsmsBaseEvent
    {
        //93 Тест утечки газа работает, инжектор 1
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE10", IsBoolean = true, BitNumber = 0)]
        public bool TestGasLeak { set; get; }        //DB902.DBX472.6
        //Резерв 1
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE10", IsBoolean = true, BitNumber = 1)]
        public bool Reserv1 { set; get; }     
        //Резерв 2
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE10", IsBoolean = true, BitNumber = 2)]
        public bool Reserv2 { set; get; }         
        //Резерв 3
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE10", IsBoolean = true, BitNumber = 3)]
        public bool Reserv3 { set; get; }         
        //Резерв 4
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE10", IsBoolean = true, BitNumber = 4)]
        public bool Reserv4 { set; get; }         
        //Резерв 5
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE10", IsBoolean = true, BitNumber = 5)]
        public bool Reserv5 { set; get; }         
        //Резерв 6
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE10", IsBoolean = true, BitNumber = 6)]
        public bool Reserv6 { set; get; }         
        //Резерв 7
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE10", IsBoolean = true, BitNumber = 7)]
        public bool Reserv7 { set; get; }         
        //Резерв 8
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE11", IsBoolean = true, BitNumber = 0)]
        public bool Reserv8 { set; get; }         
        //Резерв 9
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE11", IsBoolean = true, BitNumber = 1)]
        public bool Reserv9 { set; get; }         
        //Резерв 10
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE11", IsBoolean = true, BitNumber = 2)]
        public bool Reserv10 { set; get; }         
        //Резерв 11
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE11", IsBoolean = true, BitNumber = 3)]
        public bool Reserv11 { set; get; }         
        //Резерв 12
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE11", IsBoolean = true, BitNumber = 4)]
        public bool Reserv12 { set; get; }         
        //Резерв 13
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE11", IsBoolean = true, BitNumber = 5)]
        public bool Reserv13 { set; get; }         
        //Резерв 14
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE11", IsBoolean = true, BitNumber = 6)]
        public bool Reserv14 { set; get; } 
        //Резерв 15
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE11", IsBoolean = true, BitNumber = 7)]
        public bool Reserv15 { set; get; }         
        //79 Текущий расход газа, инжектор 1, м3/ч
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL388")]
        public float CurrentGasFlow { get; set; }        //DB904.DBD1188
        //80 Текущая уставка по расходу природного газа, инжектор 1, м3/ч
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL392")]
        public float SetpointGasFlow { get; set; }        //DB904.DBD1108
        //81 Расход газа с начала плавки, инжектор 1, м3
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL396")]
        public float GasFlow { get; set; }        //DB904.DBD1204
        //82 Текущий расход кислорода (режим горелки), инжектор 1, м3/ч
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL400")]
        public float CurrentOxygenFlowBurner { get; set; }        //DB904.DBD1180
        //83 Текущая уставка по расходу кислорода (режим горелки), инжектор 1, м3/ч
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL404")]
        public float SetpointOxygenFlowBurner { get; set; }        //DB904.DBD1100
        //84 Расход кислорода с начала плавки (режим горелки), инжектор 1, м3
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL408")]
        public float OxygenFlowBurner { get; set; }        //DB904.DBD1196
        //85 Текущий расход кислорода (режим инжектора), инжектор 1, м3/ч
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL412")]
        public float CurrentOxygenFlowInjector { get; set; }        //DB904.DBD1184
        //86 Текущая уставка по расходу кислорода (режим инжектора), инжектор 1, м3/ч
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL416")]
        public float SetpointOxygenFlowIngector { get; set; }        //DB904.DBD1104
        //87 Расход кислорода с начала плавки (режим инжектора), инжектор 1, м3
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL420")]
        public float OxygenFlowIngector { get; set; }        //DB904.DBD1200
        //88 Соотношение газ/кислород (коэф-т)
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL424")]
        public float RatioGasOxygen { get; set; }        //DB904.DBD1092
        //89 Программа инжектора 1
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,INT428")]
        public int Programm { set; get; }        //DB901.DBW40
        //90 Статус инжектора 1
        [DataMember]
        [PLCPoint(Location = "DB550,INT430")]
        public int Status { set; get; }        //DB902.DBW458
        //91 Запорный клапан 1 газа, инжектор 1, статус
        [DataMember]
        [PLCPoint(Location = "DB550,INT432")]
        public int StatusGasShutOffValve1 { set; get; }        //DB902.DBW446
        //92 Запорный клапан 2 газа, инжектор 1, статус
        [DataMember]
        [PLCPoint(Location = "DB550,INT434")]
        public int StatusGasShutOffValve2 { set; get; }        //DB902.DBW448
        //94 Положение регулирующего клапана газа, %
        [DataMember]
        [PLCPoint(Location = "DB550,REAL436")]
        public float PositionControlValveGas { get; set; }        //DB904.DBD1168
        //95 Регулирующий клапан газа, инжектор 1, статус управляющего вентиля
        [DataMember]
        [PLCPoint(Location = "DB550,INT440")]
        public int StatusControlVentilGas { set; get; }        //DB902.DBW622
        //96 Регулирующий клапан газа, инжектор 1, статус
        [DataMember]
        [PLCPoint(Location = "DB550,INT442")]
        public int StatusControlValveGas { set; get; }        //DB902.DBW456
        //97 Положение регулирующего клапана кислорода на горение инжектор 1, %
        [DataMember]
        [PLCPoint(Location = "DB550,REAL444")]
        public float PositionControlValveOxygenBurning { get; set; }        //DB904.DBD1160
        //98 Регулирующий клапан кислорода на горение, инжектор 1, статус управляющего вентиля
        [DataMember]
        [PLCPoint(Location = "DB550,INT448")]
        public int StatusControlVentilOxygenBurning { set; get; }        //DB902.DBW630
        //99 Регулирующий клапан кислорода на горение, инжектор 1, статус
        [DataMember]
        [PLCPoint(Location = "DB550,INT450")]
        public int StatusControlValveOxygenBurning { set; get; }        //DB902.DBW452
        //100 Положение регулирующего клапана подачи кислорода на рафинирование, %
        [DataMember]
        [PLCPoint(Location = "DB550,REAL452")]
        public float PositionControlValveOxygenRefining { get; set; }        //DB904.DBD1164
        //101 Регулирующий клапан кислорода на рафинирование, инжектор 1, статус управляющего вентиля
        [DataMember]
        [PLCPoint(Location = "DB550,INT456")]
        public int StatusControlVentilOxygenRefining { set; get; }        //DB902.DBW632
        //102 Регулирующий клапан кислорода на рафинирование, инжектор 1, статус
        [DataMember]
        [PLCPoint(Location = "DB550,INT458")]
        public int StatusControlValveOxygenRefining { set; get; }        //DB902.DBW454
        //103 Байпасный клапан кислорода инжектора 1, статус
        [DataMember]
        [PLCPoint(Location = "DB550,INT460")]
        public int BypassValveOxygen { set; get; }        //DB902.DBW450
        
        //Время работы за плавку в режиме горелки в секундах
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL462")]
        public float BurnerWorkTime { set; get; }	
        //Время работы за плавку в режиме фурмы (низ.) в секундах
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL466")]
        public float LanceWorkTime1 { set; get; }	
        //Время работы за плавку в режиме фурмы (низ.) в секундах
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL470")]
        public float LanceWorkTime2 { set; get; }	
        //Резерв 16
        [DataMember]
        [PLCPoint(Location = "DB550,REAL474")]
        public float Reserv16 { set; get; }  	
        //Резерв 17
        [DataMember]
        [PLCPoint(Location = "DB550,REAL478")]
        public float Reserv17 { set; get; }  	

    }
}    
