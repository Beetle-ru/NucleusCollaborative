﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Reflection;
using Core;
namespace Converter
{
    //  Счетчик для события по заданной схеме продувки								

    //   PLC:	PLC x.1	(x=номер конвертера)							
    //   Событие									
    //   a) После загрузки данных в контроллер 								
    //   b) шлем событие увелечения счетчика

    [Serializable]
    [DataContract]
    [PLCGroup(Location = "test_PLC", Destination = "Blowing")]
    [PLCGroup(Location = "PLC11", Destination = "Converter1")]
    [PLCGroup(Location = "PLC21", Destination = "Converter2")]
    [PLCGroup(Location = "PLC31", Destination = "Converter3")]
    [DBGroup(UnitNumber = 1)]
    [DBGroup(UnitNumber = 2)]
    [DBGroup(UnitNumber = 3)]
    public class cntBlowingSchemaEvent : ConverterBaseEvent
    {
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT2")]
        public int Counter { set; get; }               // Счетчик                                        # SP_CX_CNT_BLOWSCHEME


        private static int _Counter = 1;
       
        public cntBlowingSchemaEvent()
        {
            _Counter = (++_Counter <= 9999) ? _Counter : 1; 
            Counter = _Counter ;
        }
    }
}