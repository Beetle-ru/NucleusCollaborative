using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Core;

//Используется одна группа, т.е. все переменные опрашиваются одновременно.
namespace Esms
{ 
    [Serializable]
    [DataContract]
    [PLCGroup(Name = "CoalInjectionEvent2", Location = "PLC4", Destination = "ESMS2")]
    [DBGroup(UnitNumber = 2)]
    public class CoalInjectionEvent : EsmsBaseEvent
    {
        //1 Клапан Y9 открыт
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE0", IsBoolean = true, BitNumber = 0)]
        public bool ValveY9Open { set; get; }        //DB183.DBX8.5
        //2 Клапан Y8 открыт
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE0", IsBoolean = true, BitNumber = 1)]
        public bool ValveY8Open { set; get; }        //DB183.DBX8.4
        //5 Клапан Y5.2 открыт
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE0", IsBoolean = true, BitNumber = 2)]
        public bool ValveY5p2Open{ set; get; }        //DB183.DBX7.5
        //6 Клапан Y5.1 открыт
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE0", IsBoolean = true, BitNumber = 3)]
        public bool ValveY5p1Open { set; get; }        //DB183.DBX7.4
        //7 Пропорциональный клапан (Y5, давление в камере) включен
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE0", IsBoolean = true, BitNumber = 4)]
        public bool ValveY5IsOn { set; get; }        //DB183.DBX5.4
        //8 Двигатель 1 включен (подача кокса из накопительного бункера)
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE0", IsBoolean = true, BitNumber = 5)]
        public bool Engine1IsOn { set; get; }        //DB183.DBX8.0
        //9 Уровень в накопительном бункере макс-й
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE0", IsBoolean = true, BitNumber = 6)]
        public float LevelBunkerMax { get; set; }        //DB183.DBX7.1
        //10 Уровень в накопительном бункере средний
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE0", IsBoolean = true, BitNumber = 7)]
        public bool LevelBunkerAvg { set; get; }        //DB183.DBX7.3
        //11 Уровень в накопительном бункере мин-й
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE1", IsBoolean = true, BitNumber = 0)]
        public bool LevelBunkerMin { set; get; }        //DB183.DBX7.2
        //12 Поворотный клапан накопительного бункера (Y2) открыт
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE1", IsBoolean = true, BitNumber = 1)]
        public bool ValveY2Open { set; get; }        //DB183.DBX4.1
        //13 Поворотный клапан накопительного бункера (Y2) закрыт
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE1", IsBoolean = true, BitNumber = 2)]
        public bool ValveY2Close { set; get; }        //DB183.DBX4.2
        //14 Поворотный клапан накопительного бункера (Y2) сигнал «открыть»
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE1", IsBoolean = true, BitNumber = 3)]
        public bool ValveY2BeepOpen { set; get; }        //DB184.DBX18.0
        //15 Двигатель 2 включен (подача кокса из накопительного бункера)
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE1", IsBoolean = true, BitNumber = 4)]
        public bool Engine2IsOn { set; get; }        //DB183.DBX8.2
        //16 Поворотный клапан UNIDOS (Y3) открыт
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE1", IsBoolean = true, BitNumber = 5)]
        public bool ValveY3UNIDOSOpen { set; get; }        //DB183.DBX4.3
        //17 Поворотный клапан UNIDOS (Y3) закрыт
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE1", IsBoolean = true, BitNumber = 6)]
        public bool ValveY3UNIDOSClose { set; get; }        //DB183.DBX4.4
        //18 Поворотный клапан UNIDOS (Y3)сигнал «открыть»
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE1", IsBoolean = true, BitNumber = 7)]
        public bool ValveY3UNIDOSBeepOpen { set; get; }        //DB184.DBX18.2
        //19 Клапан декомпрессии (Y6) открыт
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE2", IsBoolean = true, BitNumber = 0)]
        public bool ValveY6Open { set; get; }        //DB183.DBX5.5
        //20 Давление в камере больше 0
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE2", IsBoolean = true, BitNumber = 1)]
        public bool PressureChamberLargeZero { set; get; }        //DB183.DBX5.6
        //21 Уровень в UNIDOS максимальный
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE2", IsBoolean = true, BitNumber = 2)]
        public bool LevelUNIDOSMax { set; get; }        //DB183.DBX6.2
        //24 Клапан аэрации накопительного бункера (Y1) открыт
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE2", IsBoolean = true, BitNumber = 3)]
        public bool ValveY1Open { set; get; }        //DB183.DBX7.0
        //25 Клапан аэрации UNIDOS (Y1) открыт
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE2", IsBoolean = true, BitNumber = 4)]
        public bool ValveY1UNIDOSOpen { set; get; }        //DB183.DBX5.7
        //26 Клапан давления подачи линия 2 (Y21) открыт
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE2", IsBoolean = true, BitNumber = 5)]
        public bool ValveY21Open { set; get; }        //DB183.DBX6.1
        //27 Клапан продувки линии 2 открыт
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE2", IsBoolean = true, BitNumber = 6)]
        public bool ValveBlowingLine2Open { set; get; }        //DB183.DBX7.7
        //28 Клапан давления подачи линия 1 (Y11) открыт
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE2", IsBoolean = true, BitNumber = 7)]
        public bool ValveY11Open { set; get; }        //DB183.DBX6.0
        //29 Клапан продувки линии 1 открыт
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE3", IsBoolean = true, BitNumber = 0)]
        public bool ValveBlowingLine1Open { set; get; }        //DB183.DBX7.6
        //30 Клапан подачи материала из UNIDOS, линия 2 (Y22) открыт
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE3", IsBoolean = true, BitNumber = 1)]
        public bool ValveY22UNIDOSOpen { set; get; }        //DB183.DBX5.2
        //31 Клапан подачи материала из UNIDOS, линия 2 (Y22) закрыт
        [DataMember]
         [PLCPoint(Location = "DB80,BYTE3", IsBoolean = true, BitNumber = 2)]
        public bool ValveY22UNIDOSClose { set; get; }        //DB183.DBX5.3
        //32 Клапан подачи материала из UNIDOS, линия 2 (Y22) команда «открыть»
        [DataMember]
         [PLCPoint(Location = "DB80,BYTE3", IsBoolean = true, BitNumber = 3)]
        public bool ValveY22UNIDOSBeepOpen { set; get; }        //DB184.DBX18.6
        //33 Клапан подачи материала из UNIDOS, линия 2 (Y12) открыт
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE3", IsBoolean = true, BitNumber = 4)]
        public bool ValveY12UNIDOSOpen{ set; get; }        //DB183.DBX4.7
        //34 Клапан подачи материала из UNIDOS, линия 2 (Y12) закрыт
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE3", IsBoolean = true, BitNumber = 5)]
        public bool ValveY12UNIDOSClose  { set; get; }        //DB183.DBX5.0
        //35 Клапан подачи материала из UNIDOS, линия 2 (Y12) команда «открыть»
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE3", IsBoolean = true, BitNumber = 6)]
        public bool ValveY12UNIDOSBeepOpen { set; get; }        //DB184.DBX18.4
        //38 Автоматическое управление установкой
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE3", IsBoolean = true, BitNumber = 7)]
        public bool AutoControl { set; get; }        //DB183.DBX6.5
        //39 Ручное управление установкой
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE4", IsBoolean = true, BitNumber = 0)]
        public bool ManualControl { set; get; }        //DB183.DBX6.4
        //40 Готовность установки к работе
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE4", IsBoolean = true, BitNumber = 1)]
        public bool ReadyToGo { set; get; }        //DB183.DBX6.6
        //41 Установка – суммарная ошибка
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE4", IsBoolean = true, BitNumber = 2)]
        public bool TotalError { set; get; }        //DB183.DBX6.7
        //45 Выбрана линия 1
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE4", IsBoolean = true, BitNumber = 3)]
        public bool SelectLine1 { set; get; }        //DB183.DBX74.4
        //46 Выбрана линия 2
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE4", IsBoolean = true, BitNumber = 4)]
        public bool SelectLine2 { set; get; }        //DB183.DBX74.5
        //47 Наполнение работает
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE4", IsBoolean = true, BitNumber = 5)]
        public bool FillingWorks { set; get; }        //DB183.DBX9.0
        //48 Вдувание старт
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE4", IsBoolean = true, BitNumber = 6)]
        public bool BlowingStart { set; get; }        //DB183.DBX74.0
        //49 Вдувание стоп
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE4", IsBoolean = true, BitNumber = 7)]
        public bool BlowingStop { set; get; }        //DB183.DBX74.1
        //Резерв 1
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE5", IsBoolean = true, BitNumber = 0)]
        public bool Reserv1 { set; get; }
        //Резерв 2
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE5", IsBoolean = true, BitNumber = 1)]
        public bool Reserv2 { set; get; }
        //Резерв 3
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE5", IsBoolean = true, BitNumber = 2)]
        public bool Reserv3 { set; get; }
        //Резерв 4
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE5", IsBoolean = true, BitNumber = 3)]
        public bool Reserv4{ set; get; }
        //Резерв 5
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE5", IsBoolean = true, BitNumber = 4)]
        public bool Reserv5{ set; get; }
        //Резерв 6
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE5", IsBoolean = true, BitNumber = 5)]
        public bool Reserv6{ set; get; }
        //Резерв 7
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE5", IsBoolean = true, BitNumber = 6)]
        public bool Reserv7{ set; get; }
        //Резерв 8
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE5", IsBoolean = true, BitNumber = 7)]
        public bool Reserv8{ set; get; }
        //Резерв 9
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE6", IsBoolean = true, BitNumber = 0)]
        public bool Reserv9 { set; get; }
        //Резерв 10
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE6", IsBoolean = true, BitNumber = 1)]
        public bool Reserv10 { set; get; }
        //Резерв 11
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE6", IsBoolean = true, BitNumber = 2)]
        public bool Reserv11 { set; get; }
        //Резерв 12
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE6", IsBoolean = true, BitNumber = 3)]
        public bool Reserv12 { set; get; }
        //Резерв 13
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE6", IsBoolean = true, BitNumber = 4)]
        public bool Reserv13 { set; get; }
        //Резерв 14
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE6", IsBoolean = true, BitNumber = 5)]
        public bool Reserv14 { set; get; }
        //Резерв 15
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE6", IsBoolean = true, BitNumber = 6)]
        public bool Reserv15 { set; get; }
        //Резерв 16
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE6", IsBoolean = true, BitNumber = 7)]
        public bool Reserv16 { set; get; }
        //Резерв 17
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE7", IsBoolean = true, BitNumber = 0)]
        public bool Reserv17 { set; get; }
        //Резерв 18
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE7", IsBoolean = true, BitNumber = 1)]
        public bool Reserv18 { set; get; }
        //Резерв 19
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE7", IsBoolean = true, BitNumber = 2)]
        public bool Reserv19 { set; get; }
        //Резерв 20
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE7", IsBoolean = true, BitNumber = 3)]
        public bool Reserv20 { set; get; }
        //Резерв 21
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE7", IsBoolean = true, BitNumber = 4)]
        public bool Reserv21 { set; get; }
        //Резерв 22
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE7", IsBoolean = true, BitNumber = 5)]
        public bool Reserv22 { set; get; }
        //Резерв 23
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE7", IsBoolean = true, BitNumber = 6)]
        public bool Reserv23 { set; get; }
        //Резерв 24
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE7", IsBoolean = true, BitNumber = 7)]
        public bool Reserv24 { set; get; }
        //Резерв 25
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE8", IsBoolean = true, BitNumber = 0)]
        public bool Reserv25 { set; get; }
        //Резерв 26
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE8", IsBoolean = true, BitNumber = 1)]
        public bool Reserv26 { set; get; }
        //Резерв 27
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE8", IsBoolean = true, BitNumber = 2)]
        public bool Reserv27 { set; get; }
        //Резерв 28
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE8", IsBoolean = true, BitNumber = 3)]
        public bool Reserv28 { set; get; }
        //Резерв 29
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE8", IsBoolean = true, BitNumber = 4)]
        public bool Reserv29 { set; get; }
        //Резерв 30
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE8", IsBoolean = true, BitNumber = 5)]
        public bool Reserv30 { set; get; }
        //Резерв 31
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE8", IsBoolean = true, BitNumber = 6)]
        public bool Reserv31 { set; get; }
        //Резерв 32
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE8", IsBoolean = true, BitNumber = 7)]
        public bool Reserv32 { set; get; }
        //Резерв 33
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE9", IsBoolean = true, BitNumber = 0)]
        public bool Reserv33 { set; get; }
        //Резерв 34
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE9", IsBoolean = true, BitNumber = 1)]
        public bool Reserv34 { set; get; }
        //Резерв 35
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE9", IsBoolean = true, BitNumber = 2)]
        public bool Reserv35 { set; get; }
        //Резерв 36
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE9", IsBoolean = true, BitNumber = 3)]
        public bool Reserv36 { set; get; }
        //Резерв 37
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE9", IsBoolean = true, BitNumber = 4)]
        public bool Reserv37 { set; get; }
        //Резерв 38
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE9", IsBoolean = true, BitNumber = 5)]
        public bool Reserv38 { set; get; }
        //Резерв 39
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE9", IsBoolean = true, BitNumber = 6)]
        public bool Reserv39 { set; get; }
        //Резерв 40
        [DataMember]
        [PLCPoint(Location = "DB80,BYTE9", IsBoolean = true, BitNumber = 7)]
        public bool Reserv40 { set; get; }
        //3 Текущее давление в камере
        [DataMember]
        [PLCPoint(Location = "DB80,REAL10")]
        public float CurrentPressureChamber { get; set; }        //DB183.DBD10
        //4 Уставка давления в камере
        [DataMember]
        [PLCPoint(Location = "DB80,REAL14")]
        public float SetpiontPressureChamber { get; set; }        //DB183.DBD14
        //22 Вес материала в UNIDOS брутто
        [DataMember]
        [PLCPoint(Location = "DB80,REAL18")]
        public float WeightUNIDOSBrutto { get; set; }        //DB183.DBD18
        //23 Вес нетто, отданный с 00:00
        [DataMember]
        [PLCPoint(Location = "DB80,REAL22")]
        public float WeightNetto { get; set; }        //DB183.DBD22
         //36 Давление в линии 2
        [DataMember]
        [PLCPoint(Location = "DB80,REAL26")]
        public float PressureLine2 { get; set; }        //DB183.DBD46
        //37 Давление в линии 1
        [DataMember]
        [PLCPoint(Location = "DB80,REAL30")]
        public float PressureLine1 { get; set; }        //DB183.DBD42
        //42 Расход кокса за плавку, кг
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB80,REAL34")]
        public float CokeFlow { get; set; }        //DB183.DBD26
        //43 Интенсивность вдувания кокса, кг/мин
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB80,REAL38")]
        public float CokeBlowing { get; set; }        //DB183.DBD30
        //44 Уставка расхода кокса, кг/мин
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB80,REAL42")]
        public float SetpiontCokeFlow { get; set; }        //DB183.DBD34
        //Продолжительность вдувания кокса за плавку, секунды
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB80,REAL46")]
        public float CokeBlowingTime { get; set; }	
    }
}  
