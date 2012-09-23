using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Reflection;
using Core;
namespace Converter
{

    // факт.данные от PLC x.1 (Визиуализация)
    // Von:	PLC x.1	(x=номер конвертера)
    // Данные по текущей продувке
    [Serializable]
    [DataContract]
    [PLCGroup(Location = "test_PLC", Destination = "Blowing")]
    [PLCGroup(Location = "PLC11", Destination = "Converter1")]
    [PLCGroup(Location = "PLC21", Destination = "Converter2")]
    [PLCGroup(Location = "PLC31", Destination = "Converter3")]
    [DBGroup(UnitNumber = 1)]
    [DBGroup(UnitNumber = 2)]
    [DBGroup(UnitNumber = 3)]
    public class visBlowingEvent : ConverterBaseEvent
    {
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB6,REAL86")]
        public double O2Pressure { set; get; }                      // Давление кислорода на продувку рабочей фурмы # AS31/SEN_S31.PO2_gen

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB6,W32")]
        public int RealLanceHeight { set; get; }                    // Заданная координата положения фурмы # AS31/SEN_S31.L3_zd_crd

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB6,W34")]
        public int RealO2Flow { set; get; }                         // Заданное значение расхода кислорода # AS31/SEN_S31.O3_QO2_zd

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB7,REAL92")]
        public double ValveValue { set; get; }                      // Индикация значения управляющего сигнала на регулирующий клапан # DB7_kr

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB6,REAL98")]
        public double NSlagBlowingPressure { set; get; }            // Давление азота на раздувку шлака # AS31/SEN_S31.PN2

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB6,W44")]
        public int NSlagBlowingFlow { set; get; }                   // Расход азота на раздувку шлака # AS31/SEN_S31.L3_zd_crd
        
    }
}
