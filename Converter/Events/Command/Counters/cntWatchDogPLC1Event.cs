using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Reflection;
using Core;
namespace Converter
{
    //   WatchDog контроллера PLCх.1							
    //   PLC:	PLC х.1								
    //   Событие									
    //   Событие для обновления счетчика контроллера 1

    [Serializable]
    [DataContract]
    [PLCGroup(Location = "test_PLC", Destination = "Blowing")]
    [PLCGroup(Location = "PLC11", Destination = "Converter1")]
    [PLCGroup(Location = "PLC21", Destination = "Converter2")]
    [PLCGroup(Location = "PLC31", Destination = "Converter3")]
    [DBGroup(UnitNumber = 1)]
    [DBGroup(UnitNumber = 2)]
    [DBGroup(UnitNumber = 3)]
    public class cntWatchDogPLC1Event : ConverterBaseEvent
    {
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB1,INT0")]
        public int Counter { set; get; }               // Watchdog для уровня 2                                   # SP_WATCHDOG_PLC

        private static int _Counter = 1;

        public cntWatchDogPLC1Event()
        {
            _Counter = (++_Counter <= 9999) ? _Counter : 1;
            Counter = _Counter;
        }
    }
}