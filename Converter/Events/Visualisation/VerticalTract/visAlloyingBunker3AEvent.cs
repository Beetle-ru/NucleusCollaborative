using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Reflection;
using Core;
namespace Converter
{

    // факт.данные от PLC x.2 (Визиуализация)
    // Von:	PLC x.2	(x=номер конвертера)
    // Данные по бункеру 3А легирующих вертикального тракта	(Этот бункер находится только на 2ом конвертере)
    [Serializable]
    [DataContract]
    [PLCGroup(Location = "test_PLC", Destination = "Additions")]
    [PLCGroup(Location = "PLC22", Destination = "Converter2")]
    [DBGroup(UnitNumber = 2)]
    public class visAlloyingBunker3AEvent : ConverterBaseEvent
    {
        [DataMember]
        [DBPoint(IsStored = true, MaxSize = 15)]
        [PLCPoint(Location = "DB12,STRING222,8", Encoding = "x-cp1251")]
        public string BunkerMaterialName { set; get; }                    // Наименование материала в расходном бункере 3A # AS32/AVARIYA.MATER_17

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,BYTE434", IsBoolean = true, BitNumber = 1)]
        public bool BunkerFeederVibrating { set; get; }                   // Индикация работы вибропитателя расходного бункера 3A # AS32/DATA_OS.Q3_VIBB3A
    }
}
