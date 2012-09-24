using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Reflection;
using Core;
namespace Converter
{
    //  Счетчик для события по добавочным материалам в конвертер								

    //   PLC:	PLC x.2	(x=номер конвертера)							
    //   Событие									
    //   После подтверждения предварительного расчета

    [Serializable]
    [DataContract]
    [PLCGroup(Location = "test_PLC", Destination = "Additions")]
    [PLCGroup(Location = "PLC12", Destination = "Converter1")]
    [PLCGroup(Location = "PLC22", Destination = "Converter2")]
    [PLCGroup(Location = "PLC32", Destination = "Converter3")]
    [DBGroup(UnitNumber = 1)]
    [DBGroup(UnitNumber = 2)]
    [DBGroup(UnitNumber = 3)]
    public class cntAdditionsEvent : ConverterBaseEvent
    {
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT270")]
        public int Counter { set; get; }               // Счетчик                                        # SP_CX_CNT_MATNAMEN

        private static int _Counter = 1;

        public cntAdditionsEvent()
        {
            _Counter = (++_Counter <= 9999) ? _Counter : 1; 
            Counter = _Counter ;
        }
    }
}