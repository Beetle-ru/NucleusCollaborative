using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Reflection;
using Core;
namespace Converter
{

    /// <summary>
    /// Событие происходит при расчете углерода содержащегося в конверторе
    /// </summary>
    [Serializable]
    [DataContract]
    [DBGroup(UnitNumber = 1)]
    [DBGroup(UnitNumber = 2)]
    [DBGroup(UnitNumber = 3)]
    public class CalculatedCarboneEvent : ConverterBaseEvent
    {
        /// <summary>
        /// Расчетный процент углерода в железе
        /// </summary>
        [DataMember]
        [DBPoint(IsStored = true)]
        public double CarbonePercent { set; get; }
        
        /// <summary>
        /// Расчетная масса углерода в конверторе
        /// </summary>
        [DataMember]
        [DBPoint(IsStored = true)]
        public double CarboneMass { set; get; }

        /// <summary>
        /// Название модели по которой происходит
        /// </summary>
        [DataMember]
        [DBPoint(IsStored = true)]
        public string model { set; get; }
        
        public CalculatedCarboneEvent()
        {
            CarbonePercent = 0.0;
            CarboneMass = 0.0;
            model = "0";
        }
    }
}