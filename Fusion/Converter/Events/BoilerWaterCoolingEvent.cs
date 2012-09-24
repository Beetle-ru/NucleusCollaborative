using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Reflection;
using Core;

namespace Converter
{
    [Serializable]
    [DataContract]
    [DBGroup(UnitNumber = 1)]
    [DBGroup(UnitNumber = 2)]
    [DBGroup(UnitNumber = 3)]
    public class BoilerWaterCoolingEvent : ConverterBaseEvent
    {
        [DataMember]
        [DBPoint(IsStored = true)]
        public float GasTemperatureOnExit { get; set; }         // температура газов на выходе из котла-охладителя

        [DataMember]
        [DBPoint(IsStored = true)]
        public float PrecollingGasTemperature { get; set; }     // температура отходящих газов после предварительного охлаждения

        [DataMember]
        [DBPoint(IsStored = true)]
        public float GasTemperatureAfter1Step { get; set; }     // температура отходящих газов после первой ступени

        [DataMember]
        [DBPoint(IsStored = true)]
        public float GasTemperatureAfter2Step { get; set; }     // температура отходящих газов после второй ступени

        [DataMember]
        [DBPoint(IsStored = true)]
        public float O2Vol { get; set; }                        // расход О2

    }
}
