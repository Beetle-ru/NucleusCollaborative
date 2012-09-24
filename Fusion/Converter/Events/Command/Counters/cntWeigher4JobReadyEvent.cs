using System;
using System.Runtime.Serialization;
using Core;
namespace Converter
{
    //  Счетчик подтверждения задания весы 4
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
    public class cntWeigher4JobReadyEvent : ConverterBaseEvent
    {
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT480")]
        public int Counter { set; get; }               // Счетчик                                        # SP_CX_CNT_TASK_ACKNOW4

        private static int m_counter = 1;

        public cntWeigher4JobReadyEvent()
        {
            m_counter = (++m_counter <= 9999) ? m_counter : 1; 
            Counter = m_counter ;
        }
    }
}