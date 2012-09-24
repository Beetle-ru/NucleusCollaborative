using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Reflection;
using Core;
namespace Converter
{
    //  Счетчик для события по добавочным материалам								

    //   PLC:	PLC x.2	(x=номер конвертера)							
    //   Событие									
    //   a) После загрузки в контроллер данных по добавачным материалам счетчик увеличивается

    [Serializable]
    [DataContract]
    [PLCGroup(Location = "test_PLC", Destination = "Additions")]
    [PLCGroup(Location = "PLC12", Destination = "Converter1")]
    [PLCGroup(Location = "PLC22", Destination = "Converter2")]
    [PLCGroup(Location = "PLC32", Destination = "Converter3")]
    [DBGroup(UnitNumber = 1)]
    [DBGroup(UnitNumber = 2)]
    [DBGroup(UnitNumber = 3)]
    public class cntAdditionsSchemaEvent : ConverterBaseEvent
    {
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT2")]
        public int Counter { set; get; }               // Счетчик                                        # SP_CX_CNT_ADDSCHEME

        private static int _Counter = 1;

        public cntAdditionsSchemaEvent()
        {
            _Counter = (++_Counter <= 9999) ? _Counter : 1; 
            Counter = _Counter ;
        }
    }
}