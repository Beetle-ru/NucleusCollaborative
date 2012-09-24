using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Reflection;
using Core;
namespace Converter
{
    // 1003	Заданные добавки в главной продувке						

    // PLC:	PLC x.2	(x=номер конвертера)	
    // Событие			
    // После подтверждения предварительного расчета		

    [Serializable]
    [DataContract]
    [PLCGroup(Location = "test_PLC", Destination = "Additions")]
    [PLCGroup(Location = "PLC12", Destination = "Converter1")]
    [PLCGroup(Location = "PLC22", Destination = "Converter2")]
    [PLCGroup(Location = "PLC32", Destination = "Converter3")]
    [DBGroup(UnitNumber = 1)]
    [DBGroup(UnitNumber = 2)]
    [DBGroup(UnitNumber = 3)]
    public class comJobW3Event : ConverterBaseEvent
    {
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,REAL4")]
        public double RB5Weight { set; get; }                        // Заданный вес, бункер 5       # SP_CX_ADDMAINP1WGT1

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,REAL16")]
        public double RB6Weight { set; get; }                        // Заданный вес, бункер 6       # SP_CX_ADDMAINP1WGT2

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT124")]
        public int RB5Oxygen { set; get; }                             // O2 расход бункер 5       # SP_CX_ADDSTEPP1MAT1

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT130")]
        public int RB6Oxygen { set; get; }                             // O2 расход бункер 6      # SP_CX_ADDSTEPP1MAT2


        public comJobW3Event()
        {
            RB5Weight = -1;
            RB6Weight = -1;
            RB5Oxygen = -1;
            RB6Oxygen = -1;
        }

    }
}