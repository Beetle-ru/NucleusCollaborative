using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;
 
//Инжектор 3
namespace Esms
{
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "Injector3Event2", Location = "PLC2", Destination = "ESMS2")]
    [DBGroup(UnitNumber = 2)]
    public class Injector3Event : EsmsBaseEvent
    {
        //146 Тест утечки газа работает, инжектор 3
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE14", IsBoolean = true, BitNumber = 0)]
        public bool TestGasLeak { set; get; }        //DB902.DBX536.6
        //Резерв 1
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE14", IsBoolean = true, BitNumber = 1)]
        public bool Reserv1 { set; get; }     
        //Резерв 2
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE14", IsBoolean = true, BitNumber = 2)]
        public bool Reserv2 { set; get; }         
        //Резерв 3
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE14", IsBoolean = true, BitNumber = 3)]
        public bool Reserv3 { set; get; }         
        //Резерв 4
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE14", IsBoolean = true, BitNumber = 4)]
        public bool Reserv4 { set; get; }         
        //Резерв 5
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE14", IsBoolean = true, BitNumber = 5)]
        public bool Reserv5 { set; get; }         
        //Резерв 6
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE14", IsBoolean = true, BitNumber = 6)]
        public bool Reserv6 { set; get; }         
        //Резерв 7
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE14", IsBoolean = true, BitNumber = 7)]
        public bool Reserv7 { set; get; }         
        //Резерв 8
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE15", IsBoolean = true, BitNumber = 0)]
        public bool Reserv8 { set; get; }         
        //Резерв 9
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE15", IsBoolean = true, BitNumber = 1)]
        public bool Reserv9 { set; get; }         
        //Резерв 10
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE15", IsBoolean = true, BitNumber = 2)]
        public bool Reserv10 { set; get; }         
        //Резерв 11
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE15", IsBoolean = true, BitNumber = 3)]
        public bool Reserv11 { set; get; }         
        //Резерв 12
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE15", IsBoolean = true, BitNumber = 4)]
        public bool Reserv12 { set; get; }         
        //Резерв 13
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE15", IsBoolean = true, BitNumber = 5)]
        public bool Reserv13 { set; get; }         
        //Резерв 14
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE15", IsBoolean = true, BitNumber = 6)]
        public bool Reserv14 { set; get; } 
        //Резерв 15
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE15", IsBoolean = true, BitNumber = 7)]
        public bool Reserv15 { set; get; }
        //129 Текущий расход газа, инжектор 3, м3/ч
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL576")]
        public float CurrentGasFlow { get; set; }        //DB904.DBD1468
        //130 Текущая уставка по расходу природного газа, инжектор 3, м3/ч
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL580")]
        public float SetpointGasFlow { get; set; }        //DB904.DBD1388
        //131 Расход газа с начала плавки, инжектор 3, м3
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL584")]
        public float GasFlow { get; set; }        //DB904.DBD1484
        //132 Текущий расход кислорода (режим горелки), инжектор 3, м3/ч
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL588")]
        public float CurrentOxygenFlowBurner { get; set; }        //DB904.DBD1460
        //133 Текущая уставка по расходу кислорода (режим горелки), инжектор 3, м3/ч
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL592")]
        public float SetpointOxygenFlowBurner { get; set; }        //DB904.DBD1380
        //134 Расход кислорода с начала плавки (режим горелки), инжектор 3, м3
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL596")]
        public float OxygenFlowBurner { get; set; }        //DB904.DBD1476
        //135 Текущий расход кислорода (режим инжектора), инжектор 3, м3/ч
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL600")]
        public float CurrentOxygenFlowInjector { get; set; }        //DB904.DBD1464
        //136 Текущая уставка по расходу кислорода (режим инжектора), инжектор 3, м3/ч
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL604")]
        public float SetpointOxygenFlowIngector { get; set; }        //DB904.DBD1384
        //137 Расход кислорода с начала плавки (режим инжектора), инжектор 3, м3
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL608")]
        public float OxygenFlowIngector { get; set; }        //DB904.DBD1480
        //138 Соотношение газ/кислород (коэф-т)
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL612")]
        public float RatioGasOxygen { get; set; }        //DB904.DBD1372
        //139 Программа инжектора 3
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,INT616")]
        public int Programm { set; get; }        //DB901.DBW44
        //140 Статус инжектора 3
        [DataMember]
        [PLCPoint(Location = "DB550,INT618")]
        public int Status { set; get; }        //DB902.DBW522
        //141 Запорный клапан 1 газа, инжектор 3, статус
        [DataMember]
        [PLCPoint(Location = "DB550,INT620")]
        public int StatusGasShutOffValve1 { set; get; }        //DB902.DBW510
        //142 Запорный клапан 2 газа, инжектор 3, статус
        [DataMember]
        [PLCPoint(Location = "DB550,INT622")]
        public int StatusGasShutOffValve2 { set; get; }        //DB902.DBW512
        //143 Положение регулирующего клапана газа, %
        [DataMember]
        [PLCPoint(Location = "DB550,REAL624")]
        public float PositionControlValveGas { get; set; }        //DB904.DBD1448
        //144 Регулирующий клапан газа, инжектор 3, статус управляющего вентиля
        [DataMember]
        [PLCPoint(Location = "DB550,INT628")]
        public int StatusControlVentilGas { set; get; }        //DB902.DBW626
        //145 Регулирующий клапан газа, инжектор 3, статус
        [DataMember]
        [PLCPoint(Location = "DB550,INT630")]
        public int StatusControlValveGas { set; get; }        //DB902.DBW520
        //147 Положение регулирующего клапана кислорода на горение инжектор 3, %
        [DataMember]
        [PLCPoint(Location = "DB550,REAL632")]
        public float PositionControlValveOxygenBurning { get; set; }        //DB904.DBD1440
        //148 Регулирующий клапан кислорода на горение, инжектор 3, статус управляющего вентиля
        [DataMember]
        [PLCPoint(Location = "DB550,INT636")]
        public int StatusControlVentilOxygenBurning { set; get; }        //DB902.DBW638
        //149 Регулирующий клапан кислорода на горение, инжектор 3, статус
        [DataMember]
        [PLCPoint(Location = "DB550,INT638")]
        public int StatusControlValveOxygenBurning { set; get; }        //DB902.DBW516
        //150 Положение регулирующего клапана подачи кислорода на рафиниров-е, %
        [DataMember]
        [PLCPoint(Location = "DB550,REAL640")]
        public float PositionControlValveOxygenRefining { get; set; }        //DB904.DBD1444
        //151 Регулирующий клапан кислорода на рафинирование, инжектор 3, статус управляющего вентиля
        [DataMember]
        [PLCPoint(Location = "DB550,INT644")]
        public int StatusControlVentilOxygenRefining { set; get; }        //DB902.DBW640
        //152 Регулирующий клапан кислорода на рафинирование, инжектор 3, статус
        [DataMember]
        [PLCPoint(Location = "DB550,INT646")]
        public int StatusControlValveOxygenRefining { set; get; }        //DB902.DBW518
        //153 Байпасный клапан кислорода инжектора 3, статус
        [DataMember]
        [PLCPoint(Location = "DB550,INT648")]
        public int BypassValveOxygen { set; get; }        //DB902.DBW514
        //Время работы за плавку в режиме горелки в секундах
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL650")]
        public float BurnerWorkTime { set; get; }	
        //Время работы за плавку в режиме фурмы (низ.) в секундах
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL654")]
        public float LanceWorkTime1 { set; get; }	
        //Время работы за плавку в режиме фурмы (низ.) в секундах
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL658")]
        public float LanceWorkTime2 { set; get; }	
        //Резерв 16
        [DataMember]
        [PLCPoint(Location = "DB550,REAL662")]
        public float Reserv16 { set; get; }  	
        //Резерв 17
        [DataMember]
        [PLCPoint(Location = "DB550,REAL666")]
        public float Reserv17 { set; get; }
    }
}    
