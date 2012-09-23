using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Reflection;
using Core;
namespace Converter
{
    //   WatchDog контроллера PLC0.1							
    //   PLC:	PLC 0.1								
    //   Событие									
    //   Событие для обновления счетчика контроллера 0.1

    [Serializable]
    [DataContract]
    [PLCGroup(Location = "test_PLC", Destination = "Common")]
    [PLCGroup(Location = "PLC01", Destination = "Converter1")]
    [PLCGroup(Location = "PLC01", Destination = "Converter2")]
    [PLCGroup(Location = "PLC01", Destination = "Converter3")]
    [DBGroup(UnitNumber = 1)]
    [DBGroup(UnitNumber = 2)]
    [DBGroup(UnitNumber = 3)]
    public class cntWatchDogPLC01Event : ConverterBaseEvent
    {
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT0")]
        public int Counter { set; get; }               // Watchdog для уровня 2                                   # SP_WATCHDOG_PLC

        private static int _Counter = 1;

        public cntWatchDogPLC01Event()
        {
            _Counter = (++_Counter <= 9999) ? _Counter : 1; 
            Counter = _Counter ;
        }
    }
}