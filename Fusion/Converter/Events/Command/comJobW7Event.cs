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
    public class comJobW7Event : ConverterBaseEvent
    {
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,REAL64")]
        public double RB10Weight { set; get; }                        // Заданный вес, бункер 10      # SP_CX_ADDMAINP1WGT6

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,REAL76")]
        public double RB11Weight { set; get; }                        // Заданный вес, бункер 11       # SP_CX_ADDMAINP1WGT7

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,REAL88")]
        public double RB12Weight { set; get; }                        // Заданный вес, бункер 12       # SP_CX_ADDMAINP1WGT8

        
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT154")]
        public int RB10Oxygen { set; get; }                             // O2 расход бункер 10      # SP_CX_ADDSTEPP1MAT6

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT160")]
        public int RB11Oxygen { set; get; }                             // O2 расход бункер 11      # SP_CX_ADDSTEPP1MAT7

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT166")]
        public int RB12Oxygen { set; get; }                             // O2 расход бункер 12      # SP_CX_ADDSTEPP1MAT8

        public comJobW7Event()
        {
            RB10Weight = -1;
            RB11Weight = -1;
            RB12Weight = -1;


            RB10Oxygen = -1;
            RB11Oxygen = -1;
            RB12Oxygen = -1;
        }
    }
}