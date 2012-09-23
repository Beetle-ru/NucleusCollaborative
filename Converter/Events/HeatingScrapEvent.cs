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
    // Данные по сушке скрапа
    [Serializable]
    [DataContract]
    [PLCGroup(Location = "test_PLC", Destination = "Blowing")]
    [PLCGroup(Location = "PLC11", Destination = "Converter1")]
    [PLCGroup(Location = "PLC21", Destination = "Converter2")]
    [PLCGroup(Location = "PLC31", Destination = "Converter3")]
    [DBGroup(UnitNumber = 1)]
    [DBGroup(UnitNumber = 2)]
    [DBGroup(UnitNumber = 3)]
    public class HeatingScrapEvent : ConverterBaseEvent
    {

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,W56")]
        public int HeatingScrapFlag { set; get; }         // Сушка лома (1=сушка,2=нет сушки) # ACT_CX_HEATING_SCRAP

    }
}
