using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;
 
//Инжектор 4
namespace Esms
{
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "Injector4Event2", Location = "PLC2", Destination = "ESMS2")]
    [DBGroup(UnitNumber = 2)]
    public class Injector4Event : EsmsBaseEvent
    {
        //171 Тест утечки газа работает, инжектор 4
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE16", IsBoolean = true, BitNumber = 0)]
        public bool TestGasLeak { set; get; }        //DB902.DBX568.6
        //Резерв 1
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE16", IsBoolean = true, BitNumber = 1)]
        public bool Reserv1 { set; get; }     
        //Резерв 2
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE16", IsBoolean = true, BitNumber = 2)]
        public bool Reserv2 { set; get; }         
        //Резерв 3
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE16", IsBoolean = true, BitNumber = 3)]
        public bool Reserv3 { set; get; }         
        //Резерв 4
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE16", IsBoolean = true, BitNumber = 4)]
        public bool Reserv4 { set; get; }         
        //Резерв 5
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE16", IsBoolean = true, BitNumber = 5)]
        public bool Reserv5 { set; get; }         
        //Резерв 6
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE16", IsBoolean = true, BitNumber = 6)]
        public bool Reserv6 { set; get; }         
        //Резерв 7
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE16", IsBoolean = true, BitNumber = 7)]
        public bool Reserv7 { set; get; }         
        //Резерв 8
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE17", IsBoolean = true, BitNumber = 0)]
        public bool Reserv8 { set; get; }         
        //Резерв 9
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE17", IsBoolean = true, BitNumber = 1)]
        public bool Reserv9 { set; get; }         
        //Резерв 10
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE17", IsBoolean = true, BitNumber = 2)]
        public bool Reserv10 { set; get; }         
        //Резерв 11
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE17", IsBoolean = true, BitNumber = 3)]
        public bool Reserv11 { set; get; }         
        //Резерв 12
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE17", IsBoolean = true, BitNumber = 4)]
        public bool Reserv12 { set; get; }         
        //Резерв 13
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE17", IsBoolean = true, BitNumber = 5)]
        public bool Reserv13 { set; get; }         
        //Резерв 14
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE17", IsBoolean = true, BitNumber = 6)]
        public bool Reserv14 { set; get; } 
        //Резерв 15
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE17", IsBoolean = true, BitNumber = 7)]
        public bool Reserv15 { set; get; }
        //154 Текущий расход газа, инжектор 4, м3/ч
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL670")]
        public float CurrentGasFlow { get; set; }        //DB904.DBD1608
        //155 Текущая уставка по расходу природного газа, инжектор 4, м3/ч
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL674")]
        public float SetpointGasFlow { get; set; }        //DB904.DBD1528
        //156 Расход газа с начала плавки, инжектор 4, м3
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL678")]
        public float GasFlow { get; set; }        //DB904.DBD1624
        //157 Текущий расход кислорода (режим горелки), инжектор 4, м3/ч
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL682")]
        public float CurrentOxygenFlowBurner { get; set; }        //DB904.DBD1600
        //158 Текущая уставка по расходу кислорода (режим горелки), инжектор 4, м3/ч
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL686")]
        public float SetpointOxygenFlowBurner { get; set; }        //DB904.DBD1520
        //159 Расход кислорода с начала плавки (режим горелки), инжектор 4, м3
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL690")]
        public float OxygenFlowBurner { get; set; }        //DB904.DBD1616
        //160 Текущий расход кислорода (режим инжектора), инжектор 4, м3/ч
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL694")]
        public float CurrentOxygenFlowInjector { get; set; }        //DB904.DBD1604
        //161 Текущая уставка по расходу кислорода (режим инжектора), инжектор 4, м3/ч
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL698")]
        public float SetpointOxygenFlowIngector { get; set; }        //DB904.DBD1524
        //162 Расход кислорода с начала плавки (режим инжектора), инжектор 4, м3
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL702")]
        public float OxygenFlowIngector { get; set; }        //DB904.DBD1620
        //163 Соотношение газ/кислород (коэф-т)
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL706")]
        public float RatioGasOxygen { get; set; }        //DB904.DBD1512
        //164 Программа инжектора 4
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,INT710")]
        public int Programm { set; get; }        //DB901.DBW46
        //165 Статус инжектора 4
        [DataMember]
        [PLCPoint(Location = "DB550,INT712")]
        public int Status { set; get; }        //DB902.DBW554
        //166 Запорный клапан 1 газа, инжектор 4, статус
        [DataMember]
        [PLCPoint(Location = "DB550,INT714")]
        public int StatusGasShutOffValve1 { set; get; }        //DB902.DBW542
        //167 Запорный клапан 2 газа, инжектор 4, статус
        [DataMember]
        [PLCPoint(Location = "DB550,INT716")]
        public int StatusGasShutOffValve2 { set; get; }        //DB902.DBW544
        //168 Положение регулирующего клапана газа, %
        [DataMember]
        [PLCPoint(Location = "DB550,REAL718")]
        public float PositionControlValveGas { get; set; }        //DB904.DBD1588
        //169 Регулирующий клапан газа, инжектор 4, статус управляющего вентиля
        [DataMember]
        [PLCPoint(Location = "DB550,INT722")]
        public int StatusControlVentilGas { set; get; }        //DB902.DBW628
        //170 Регулирующий клапан газа, инжектор 4, статус
        [DataMember]
        [PLCPoint(Location = "DB550,INT724")]
        public int StatusControlValveGas { set; get; }        //DB902.DBW552
        //172 Положение регулирующего клапана кислорода на горение инжектор 4, %
        [DataMember]
        [PLCPoint(Location = "DB550,REAL726")]
        public float PositionControlValveOxygenBurning { get; set; }        //DB904.DBD1580
        //173 Регулирующий клапан кислорода на горение, инжектор 4, статус управляющего вентиля
        [DataMember]
        [PLCPoint(Location = "DB550,INT730")]
        public int StatusControlVentilOxygenBurning { set; get; }        //DB902.DBW642
        //174 Регулирующий клапан кислорода на горение, инжектор 4, статус
        [DataMember]
        [PLCPoint(Location = "DB550,INT732")]
        public int StatusControlValveOxygenBurning { set; get; }        //DB902.DBW548
        //175 Положение регулирующего клапана подачи кислорода на рафиниров-е, %
        [DataMember]
        [PLCPoint(Location = "DB550,REAL734")]
        public float PositionControlValveOxygenRefining { get; set; }        //DB904.DBD1584
        //176 Регулирующий клапан кислорода на рафинирование, инжектор 4, статус управляющего вентиля
        [DataMember]
        [PLCPoint(Location = "DB550,INT738")]
        public int StatusControlVentilOxygenRefining { set; get; }        //DB902.DBW644
        //177 Регулирующий клапан кислорода на рафинирование, инжектор 4, статус
        [DataMember]
        [PLCPoint(Location = "DB550,INT740")]
        public int StatusControlValveOxygenRefining { set; get; }        //DB902.DBW550
        //178 Байпасный клапан кислорода инжектора 4, статус
        [DataMember]
        [PLCPoint(Location = "DB550,INT742")]
        public int BypassValveOxygen { set; get; }        //DB902.DBW546
        //Время работы за плавку в режиме горелки в секундах
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL744")]
        public float BurnerWorkTime { set; get; }	
        //Время работы за плавку в режиме фурмы (низ.) в секундах
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL748")]
        public float LanceWorkTime1 { set; get; }	
        //Время работы за плавку в режиме фурмы (низ.) в секундах
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL752")]
        public float LanceWorkTime2 { set; get; }	
        //Резерв 16
        [DataMember]
        [PLCPoint(Location = "DB550,REAL756")]
        public float Reserv16 { set; get; }  	
        //Резерв 17
        [DataMember]
        [PLCPoint(Location = "DB550,REAL760")]
        public float Reserv17 { set; get; }
    }
}    
