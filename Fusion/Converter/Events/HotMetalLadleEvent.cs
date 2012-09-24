using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using System.Runtime.Serialization;

namespace Converter
{
    // Данные с PLC 0.1	
    // Чугуновозные ковши
    [Serializable]
    [DataContract]
    [PLCGroup(Location = "test_PLC", Destination = "Common")]
    [PLCGroup(Location = "PLC01", Destination = "Converter1", FilterPropertyName = "ConverterNumber", FilterPropertyValue = "1")]
    [PLCGroup(Location = "PLC01", Destination = "Converter2", FilterPropertyName = "ConverterNumber", FilterPropertyValue = "2")]
    [PLCGroup(Location = "PLC01", Destination = "Converter3", FilterPropertyName = "ConverterNumber", FilterPropertyValue = "3")]
    [DBGroup(UnitNumber = 1)]
    [DBGroup(UnitNumber = 2)]
    [DBGroup(UnitNumber = 3)]
    public class HotMetalLadleEvent : ConverterBaseEvent
    {
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,W2")]
        public int LadleNumber { set; get; }                    // № чугуновозного ковша

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,W6")]
        public int MixerNumberPortion1 { set; get; }            //  № миксера. порция 1 

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,W8")]
        public int MixerNumberPortion2 { set; get; }            //  № миксера. порция 2 

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,W10")]
        public int MixerNumberPortion3 { set; get; }            //  № миксера. порция 3 

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,D12")]
        public int WeightPortion1 { set; get; }                 //  веспорция 1       

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,D16")]
        public int WeightPortion2 { set; get; }                 //  веспорция 2       

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,D20")]
        public int WeightPortion3 { set; get; }                 //  веспорция 3       

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,D30")]
        public int HotMetalTotalWeight { set; get; }            //  общ.вес           

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,W42")]
        public int HotMetalTemperature { set; get; }            //  T чугуна

        [PLCPoint(Location = "DB2,W104")]
        [DBPoint(IsStored = false)]
        public int ConverterNumber { set; get; }                // номер конвертера

    }
}
