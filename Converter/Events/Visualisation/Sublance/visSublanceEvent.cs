using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Reflection;
using Core;
namespace Converter
{

    // факт.данные от PLC x.3 (Визиуализация)
    // Von:	PLC x.3	(x=номер конвертера)
    // Данные по измерительной фурме	
    [Serializable]
    [DataContract]
    [PLCGroup(Location = "test_PLC", Destination = "Blockings")]
    [PLCGroup(Location = "PLC13", Destination = "Converter1")]
    [PLCGroup(Location = "PLC23", Destination = "Converter2")]
    [PLCGroup(Location = "PLC33", Destination = "Converter3")]
    [DBGroup(UnitNumber = 1)]
    [DBGroup(UnitNumber = 2)]
    [DBGroup(UnitNumber = 3)]
    public class visSublanceEvent : ConverterBaseEvent
    {
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,W4")]
        public int MetalLevel { set; get; }                      // Уровень металла от ИФ # DB10_S3_UR_MET

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,W2")]
        public int Height { set; get; }                          // Фактическая координата ИФ # DB10_S3_FAKT_POLG

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,REAL52")]
        public double Al { set; get; }                           // Содержание "Al" от ИФ # DB10_kAL

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB22,REAL138")]
        public double TotalO2Vol { set; get; }                   // Суммарный расход кислорода при котором произведен замер ИФ # DB22_QO2_tot_mess

        // !new
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB10,REAL32")]
        public double PPM { set; get; }                          // ppm от ИФ # DB10_ppm
    }
}
