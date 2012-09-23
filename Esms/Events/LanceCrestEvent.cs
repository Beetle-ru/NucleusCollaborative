using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;

//Сводовая фурма
namespace Esms
{    
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "LanceCrestEvent2", Location = "PLC1", Destination = "ESMS2")]
    [DBGroup(UnitNumber = 2)]
    public class LanceCrestEvent : EsmsBaseEvent
    {
        //145 Фурма свода вверху
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE18", IsBoolean = true, BitNumber = 0)]
        public bool Up { get; set; }         //DB822.DBX464.5
        //146 Фурма свода в позиции продувки
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE18", IsBoolean = true, BitNumber = 1)]
        public bool PositionInjection { get; set; }         //DB822.DBX463.6
        //147 Фурма свода внизу
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE18", IsBoolean = true, BitNumber = 2)]
        public bool Down { get; set; }         //DB822.DBX464.4
        //148 Резерв 1
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE18", IsBoolean = true, BitNumber = 3)]
        public bool Reserve1 { get; set; }
        //149 Резерв 2
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE18", IsBoolean = true, BitNumber = 4)]
        public bool Reserve2 { get; set; }
        //150 Резерв 3
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE18", IsBoolean = true, BitNumber = 5)]
        public bool Reserve3 { get; set; }
        //151 Резерв 4
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE18", IsBoolean = true, BitNumber = 6)]
        public bool Reserve4 { get; set; }
        //152 Резерв 5
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE18", IsBoolean = true, BitNumber = 7)]
        public bool Reserve5 { get; set; }
        //153 Резерв 6
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE19", IsBoolean = true, BitNumber = 0)]
        public bool Reserve6{ get; set; }
        //154 Резерв 7
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE19", IsBoolean = true, BitNumber = 1)]
        public bool Reserve7 { get; set; }
        //155 Резерв 8
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE19", IsBoolean = true, BitNumber = 2)]
        public bool Reserve8 { get; set; }
        //156 Резерв 9
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE19", IsBoolean = true, BitNumber = 3)]
        public bool Reserve9 { get; set; }
        //157 Резерв 10
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE19", IsBoolean = true, BitNumber = 4)]
        public bool Reserve10 { get; set; }
        //158 Резерв 11
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE19", IsBoolean = true, BitNumber = 5)]
        public bool Reserve11 { get; set; }
        //159 Резерв 12
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE19", IsBoolean = true, BitNumber = 6)]
        public bool Reserve12 { get; set; }
        //160 Резерв 13
        [DataMember]
        [PLCPoint(Location = "DB550,BYTE19", IsBoolean = true, BitNumber = 7)]
        public bool Reserve13 { get; set; }  
        //78 Сводовая фурма, положение, мм
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL406")]
        public float Position { get; set; }        //DB824.DBD400
        //79 Сводовая фурма, текущий расход кислорода, м3/ч
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL410")]
        public float CurrentOxygenFlow { get; set; }        //DB824.DBD404
        //80 Температура охлаждающей воды на входе сводовой фурмы
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL414")]
        public float TempWaterInput { get; set; }        //DB824.DBD408
        //81 Температура охлаждающей воды на выходе сводовой фурмы
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL418")]
        public float TempOtputWater { get; set; }        //DB824.DBD412
        //82 Сводовая фурма, уставка расхода кислорода, м3/ч
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL422")]
        public float SetpointOxygenFlow { get; set; }        //DB826.DBD868
        //83 Сводовая фурма, суммарный расход с начала плавки, м3
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL426")]
        public float TotalOxygenFlow { get; set; }        //DB824.DBD364
        //84 Регулирующий клапан кислорода фурмы, статус
        [DataMember]
        [PLCPoint(Location = "DB550,INT430")]
        public int StatusControlValveOxygen { set; get; }        //DB822.DBW438
        //85 Статус отсечного клапана кислорода фурмы
        [DataMember]
        [PLCPoint(Location = "DB550,INT432")]
        public int StatusShutOffValveOxygen { set; get; }        //DB822.DBW440
        //86 Регулирующий клапан кислорода фурмы, статус управляющего вентиля
        [DataMember]
        [PLCPoint(Location = "DB550,INT434")]
        public int StatusControlVentilOxygen { set; get; }        //DB822.DBW442
        //87 Фурма статус
        [DataMember]
        [PLCPoint(Location = "DB550,INT436")]
        public int StatusLanceCrest { set; get; }        //DB822.DBW444
        //88 Время вдувания кислорода через фурму, секунды
        [DataMember]
        //[DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL438")]
        public float InjectionOxygenTime { get; set; } 
        //89 Резерв 14  
        [DataMember]
        //[DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL442")]
        public float Reserve14 { get; set; } 
        //90 Резерв 15  
        [DataMember]
        //[DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL446")]
        public float Reserve15 { get; set; } 
        //91 Резерв 16  
        [DataMember]
        //[DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB550,REAL450")]
        public float Reserve16 { get; set; } 
    }
}    
