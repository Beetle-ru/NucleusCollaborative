using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Reflection;
using Core;
namespace Converter
{
    // 1004	Заданный расход O2 на главную продувку,фазу замера изм.фурмой и додувку									

    //   PLC:	PLC x.1	(x=номер конвертера)							
    //   Событие									
    //   a) После выполнения предварительного расчета								
    //   b) После выполнения расчета по изм.фурме								
    //   c) После выполнения расчета додудвки								

    //   Примечание									
    //   Предварительный расчет подтверждается,по-видимому, после начала продувки								
    //   Заданный расход O2 действует,пока не будет заменен новым								

    //   "(A1) Заданный общий расход O2
    //   Пересчитывается в каждом цикле расчета модели"								

    //   "(A2) Заданный общий расход O2 для замера изм.фурмой
    //   Величина передается в начале главной продувки."								


    [Serializable]
    [DataContract]
    [PLCGroup(Location = "test_PLC", Destination = "Blowing")]
    [PLCGroup(Location = "PLC11", Destination = "Converter1")]
    [PLCGroup(Location = "PLC21", Destination = "Converter2")]
    [PLCGroup(Location = "PLC31", Destination = "Converter3")]
    [DBGroup(UnitNumber = 1)]
    [DBGroup(UnitNumber = 2)]
    [DBGroup(UnitNumber = 3)]
    public class comO2FlowRateEvent : ConverterBaseEvent
    {
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT168")]
        public int O2TotalVol { set; get; }               // общий O2 расход                                              # SP_CX_O2VOL_TOTAL

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT170")]
        public int SublanceStartO2Vol { set; get; }       // O2 расход для замера изм.фурмой                              # SP_CX_O2VOL_INBLOWSTART
    }
}