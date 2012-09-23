using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;

//Горелка4
namespace Esms
{ 
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "Burner4Event2", Location = "PLC2", Destination = "ESMS2")]
    [DBGroup(UnitNumber = 2)]
    public class Burner4Event : EsmsBaseEvent
    {
        //67 Давление сжатого воздуха на продувку сопла горелки 4
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE6", IsBoolean = true, BitNumber = 0)]
        public bool PressureAirBlowing { set; get; }        //DB902.DBX374.0
        //70 Тест утечки газа работает, горелка 4
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE6", IsBoolean = true, BitNumber = 1)]
        public bool TestGasLeak { set; get; }        //DB902.DBX374.6
        //Резерв 1
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE6", IsBoolean = true, BitNumber = 2)]
        public bool Reserv1 { set; get; }     
        //Резерв 2
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE6", IsBoolean = true, BitNumber = 3)]
        public bool Reserv2 { set; get; }         
        //Резерв 3
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE6", IsBoolean = true, BitNumber = 4)]
        public bool Reserv3 { set; get; }         
        //Резерв 4
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE6", IsBoolean = true, BitNumber = 5)]
        public bool Reserv4 { set; get; }         
        //Резерв 5
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE6", IsBoolean = true, BitNumber = 6)]
        public bool Reserv5 { set; get; }         
        //Резерв 6
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE6", IsBoolean = true, BitNumber = 7)]
        public bool Reserv6 { set; get; }         
        //Резерв 7
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE7", IsBoolean = true, BitNumber = 0)]
        public bool Reserv7 { set; get; }         
        //Резерв 8
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE7", IsBoolean = true, BitNumber = 1)]
        public bool Reserv8 { set; get; }         
        //Резерв 9
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE7", IsBoolean = true, BitNumber = 2)]
        public bool Reserv9 { set; get; }         
        //Резерв 10
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE7", IsBoolean = true, BitNumber = 3)]
        public bool Reserv10 { set; get; }         
        //Резерв 11
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE7", IsBoolean = true, BitNumber = 4)]
        public bool Reserv11 { set; get; }         
        //Резерв 12
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE7", IsBoolean = true, BitNumber = 5)]
        public bool Reserv12 { set; get; }         
        //Резерв 13
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE7", IsBoolean = true, BitNumber = 6)]
        public bool Reserv13 { set; get; }         
        //Резерв 14
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE7", IsBoolean = true, BitNumber = 7)]
        public bool Reserv14 { set; get; }
        //58 Текущий расход природного газа, горелка 4, м3/ч
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL316")]
        public float CurrentNaturalGasFlow { get; set; }        //DB904.DBD724
        //59 Текущая уставка по расходу природного газа, горелка 4, м3/ч
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL320")]
        public float CurrentSetpointNaturalGasFlow { get; set; }        //DB904.DBD644
        //60 Расход природного газа с начала плавки, горелка 4, м3
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL324")]
        public float NaturalGasFlow { get; set; }        //DB904.DBD744
        //61 Текущий расход кислорода, горелка 4, м3/ч
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL328")]
        public float CurrentOxygenFlow { get; set; }        //DB904.DBD720
        //62 Текущая уставка по расходу кислорода, горелка 4, м3/ч
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL332")]
        public float SetpointOxygenFlow { get; set; }        //DB904.DBD640
        //63 Расход кислорода с начала плавки, горелка 4, м3
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL336")]
        public float OxygenFlow  { get; set; }        //DB904.DBD740
        //64 Соотношение газ/кислород (коэф-т)
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL340")]
        public float RatioGasOxygen { get; set; }        //DB904.DBD632
        //65 Программа горелки 4
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,INT344")]
        public int Program { set; get; }        //DB901.DBW26
        //66 Статус горелки 4
        [DataMember]
        [PLCPoint(Location = "DB550,INT346")]
        public int Status { set; get; }        //DB902.DBW360
        //68 Запорный клапан 1 газа, горелка 4, статус
        [DataMember]
        [PLCPoint(Location = "DB550,INT348")]
        public int StatusGasShutOffValve1 { set; get; }        //DB902.DBW348
        //69 Запорный клапан 2 газа, горелка 4, статус
        [DataMember]
        [PLCPoint(Location = "DB550,INT350")]
        public int StatusGasShutOffValve2 { set; get; }        //DB902.DBW350
        //71 Положение регулирующего клапана подачи газа, %
        [DataMember]
        [PLCPoint(Location = "DB550,REAL352")]
        public float PositionControlValveGas { get; set; }        //DB904.DBD704
        //72 Регулирующий клапан газа, горелка 4, статус управляющего вентиля
        [DataMember]
        [PLCPoint(Location = "DB550,INT356")]
        public int StatusControlVentilGas { set; get; }        //DB902.DBW612
        //73 Регулирующий клапан газа, горелка 4, статус
        [DataMember]
        [PLCPoint(Location = "DB550,INT358")]
        public int StatusControlValveGas { set; get; }        //DB902.DBW356
        //74 Положение регулирующего клапана подачи кислорода, горелка 4, %
        [DataMember]
        [PLCPoint(Location = "DB550,REAL360")]
        public float PositionControlValveOxygen { get; set; }        //DB904.DBD700
        //75 Регулирующий клапан кислорода, горелка 4, статус управляющего вентиля
        [DataMember]
        [PLCPoint(Location = "DB550,INT364")]
        public int StatusControlVentilOxygen { set; get; }        //DB902.DBW620
        //76 Регулирующий клапан кислорода, горелка 4, статус
        [DataMember]
        [PLCPoint(Location = "DB550,INT366")]
        public int StatusControlValveOxygen { set; get; }        //DB902.DBW354
        //Время работы горелки за плавку в секундах 
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL368")]
        public float BurnerWorkTime { get; set; }
        //Резерв 15
        [DataMember]
        [PLCPoint(Location = "DB550,REAL372")]
        public float Reserv15 { get; set; }	
        //Резерв 16
        [DataMember]
        [PLCPoint(Location = "DB550,REAL376")]
        public float Reserv16 { get; set; }	
        //Резерв 17
        [DataMember]
        [PLCPoint(Location = "DB550,REAL380")]
        public float Reserv17 { get; set; }	
        //Резерв 18
        [DataMember]
        [PLCPoint(Location = "DB550,REAL384")]
        public float Reserv18 { get; set; }	
    }
}    
