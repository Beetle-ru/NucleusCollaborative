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
    [DBGroup(UnitNumber = 1, DisplayName = "Анализ отходящих газов", IsTrendGroup = true, BindingPropertyName = "OffGasAnalysisHistory")]
    [DBGroup(UnitNumber = 2, DisplayName = "Анализ отходящих газов", IsTrendGroup = true, BindingPropertyName = "OffGasAnalysisHistory")]
    [DBGroup(UnitNumber = 3, DisplayName = "Анализ отходящих газов", IsTrendGroup = true, BindingPropertyName = "OffGasAnalysisHistory")]
    public class OffGasAnalysisEvent : ConverterBaseEvent
    {
        [DataMember]
        [DBPoint(IsStored = true, DisplayName = "H2, %", IsTrendPoint = true, MinValue = 0, MaxValue = 100)]
        public double H2 { get; set; }

        [DataMember]
        [DBPoint(IsStored = true, DisplayName = "O2, %", IsTrendPoint = true, MinValue = 0, MaxValue = 100)]
        public double O2 { get; set; }

        [DataMember]
        [DBPoint(IsStored = true, DisplayName = "CO, %", IsTrendPoint = true, MinValue = 0, MaxValue = 100)]
        public double CO { get; set; }

        [DataMember]
        [DBPoint(IsStored = true, DisplayName = "CO2, %", IsTrendPoint = true, MinValue = 0, MaxValue = 100)]
        public double CO2 { get; set; }

        [DataMember]
        [DBPoint(IsStored = true, DisplayName = "N2, %", IsTrendPoint = true, MinValue = 0, MaxValue = 100)]
        public double N2 { get; set; }

        [DataMember]
        [DBPoint(IsStored = true, DisplayName = "Ar, %", IsTrendPoint = true, MinValue = 0, MaxValue = 100)]
        public double Ar { get; set; }

    }
}
