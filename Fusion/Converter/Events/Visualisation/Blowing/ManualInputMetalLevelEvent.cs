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
    // Данные по уровню металла	
    [Serializable]
    [DataContract]
    [PLCGroup(Location = "test_PLC", Destination = "Blockings")]
    [PLCGroup(Location = "PLC11", Destination = "Converter1")]
    [PLCGroup(Location = "PLC21", Destination = "Converter2")]
    [PLCGroup(Location = "PLC31", Destination = "Converter3")]
    [DBGroup(UnitNumber = 1)]
    [DBGroup(UnitNumber = 2)]
    [DBGroup(UnitNumber = 3)]
    public class ManualInputMetalLevelEvent : ConverterBaseEvent
    {

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB5,W10")]
        public int ManualInputMetalLevel { set; get; }                   // Уровень металла в конвертере (вводимый параметр)# AS31/REC_S31.L3_metal_lev



    }
}
