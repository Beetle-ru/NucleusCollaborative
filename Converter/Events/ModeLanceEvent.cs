using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Reflection;
using Core;
namespace Converter
{

    // факт.данные от PLC x.1						
    // Von:	PLC x.1	(x=номер конвертера)
    // Режимы фурмы	
    [Serializable]
    [DataContract]
    [PLCGroup(Location = "test_PLC", Destination = "Blowing")]
    [PLCGroup(Location = "PLC11", Destination = "Converter1")]
    [PLCGroup(Location = "PLC21", Destination = "Converter2")]
    [PLCGroup(Location = "PLC31", Destination = "Converter3")]
    [DBGroup(UnitNumber = 1)]
    [DBGroup(UnitNumber = 2)]
    [DBGroup(UnitNumber = 3)]
    public class ModeLanceEvent : ConverterBaseEvent
    {
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,W30")]
        public int LanceMode { set; get; }                // Режим работы управления фурмой 1=ручной, 2=автомат, 3=компьютер # ACT_CX_OPLANCE

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,W32")]
        public int O2FlowMode { set; get; }               // Режим работы регулирования O2 1=ручной, 2=автомат, 3=компьютер  # ACT_CX_OPO2FLOW
    }
}
