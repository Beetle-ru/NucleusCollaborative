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
    public class comJobW6Event : ConverterBaseEvent
    {
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,REAL52")]
        public double RB9Weight { set; get; }                        // Заданный вес, бункер 9      # SP_CX_ADDMAINP1WGT5

        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(IsWritable = true, Location = "DB1,INT148")]
        public int RB9Oxygen { set; get; }                             // O2 расход бункер 9     # SP_CX_ADDSTEPP1MAT5
        
        public comJobW6Event()
        {
            RB9Weight = -1;
            RB9Oxygen = -1;
        }

    }
}