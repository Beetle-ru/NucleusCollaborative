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
    public class OffGasEvent : ConverterBaseEvent
    {
        [DataMember]
        [DBPoint(IsStored = true, DisplayName = "Обьем отходящих газов, нм3/ч", DisplayShortName= "V отх. газов", IsTrendPoint = true, MinValue = 0, MaxValue = 800000)]
        [PLCPoint(Location = "DB2,REAL26")]
        public double OffGasFlow { get; set; }

        [DataMember]
        [DBPoint(IsStored = true, DisplayName = "Температура отходящих газов, °C", DisplayShortName = "Т отх. газов", IsTrendPoint = true, MinValue = 0, MaxValue = 2000)]
        [PLCPoint(Location = "DB2,W30")]
        public int OffGasTemp { get; set; }
        [DataMember]
        //14	Позиция регулирующего конуса	статус	flag	0	2	0	ACT_CX_GASFILTERCONTROLPOS	"
        //0=без дожигания
        //1=част.дожигание
        //2=с дожиганием
        [PLCPoint(Location = "DB2,W44")]
        [DBPoint(IsStored = true)]
        public int OffGasHoodPos { get; set; }
        [DataMember]
        [DBPoint(IsStored = true)]
        //Позиция юбки	статус	flag	0	2	0	ACT_CX_HOODPOS	
        //0 = верх
        //1 = середина
        //2 = низ
        [PLCPoint(Location = "DB2,W32")]
        public int OffGasFilterControlPos { get; set; }

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,D50")]
        public int OffGasCounter { get; set; }

    }
}
