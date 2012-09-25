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
    [PLCGroup(Location = "test_PLC", Destination = "Blockings")]
    [PLCGroup(Location = "PLC13", Destination = "Converter1")]
    [PLCGroup(Location = "PLC23", Destination = "Converter2")]
    [PLCGroup(Location = "PLC33", Destination = "Converter3")]
    [DBGroup(UnitNumber = 1, DisplayName = "Обьем и температура отходящих газов", IsTrendGroup = true, BindingPropertyName = "OffGasHistory")]
    [DBGroup(UnitNumber = 2, DisplayName = "Обьем и температура отходящих газов", IsTrendGroup = true, BindingPropertyName = "OffGasHistory")]
    [DBGroup(UnitNumber = 3, DisplayName = "Обьем и температура отходящих газов", IsTrendGroup = true, BindingPropertyName = "OffGasHistory")]
    public class O2Event : ConverterBaseEvent
    {
        [DataMember]
        [DBPoint(IsStored = true, DisplayName = "Обьем отходящих газов, нм3/ч", DisplayShortName= "V отх. газов", IsTrendPoint = true, MinValue = 0, MaxValue = 800000)]
        [PLCPoint(Location = "DB2,REAL26")]
        public double OffGasFlow { get; set; }
    }
}
