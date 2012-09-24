using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Reflection;
using Core;
namespace Converter
{
    // Коммутатор О2 или симулятор О2 (bool) W4					

    // PLC:	PLC x.2	(x=номер конвертера)	
    // Событие			

    [Serializable]
    [DataContract]
    [PLCGroup(Location = "test_PLC", Destination = "Additions")]
    [PLCGroup(Location = "PLC12", Destination = "Converter1")]
    [PLCGroup(Location = "PLC22", Destination = "Converter2")]
    [PLCGroup(Location = "PLC32", Destination = "Converter3")]
    [DBGroup(UnitNumber = 1)]
    [DBGroup(UnitNumber = 2)]
    [DBGroup(UnitNumber = 3)]
    public class comSelectOxygenModeW4Event : ConverterBaseEvent
    {
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB1,INT498", IsWritable = true)]
        public int SelectOxygenSimulation { set; get; }                   // 1 - симуляция; 0 - реальный кислород               // # SP_CX_O2_SELECTW4

    }
}