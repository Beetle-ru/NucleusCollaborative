using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using System.Runtime.Serialization;

namespace Converter
{
    // Данные с PLC 0.1	
    // Хим состав чугуна в миксерах
    // Имя группы SP_TOTORPLI
    [Serializable]
    [DataContract]
    public class MixerAnalysisEvent : ConverterBaseEvent
    {
        [DataMember]
        public int MixerNumber { set; get; }                // № миксера                 # SP_1TORPNR / SP_2TORPNR / SP_3TORPNR  

        [DataMember]
        public double C { set; get; }                       // Анализ миксера  [C]       # SP_1TORPANALC / SP_2TORPANALC / SP_3TORPANALC

        [DataMember]
        public double Si { set; get; }                      // Анализ миксера  [Si]      # SP_1TORPANALSI / SP_2TORPANALSI / SP_3TORPANALSI

        [DataMember]
        public double Mn { set; get; }                      // Анализ миксера  [Mn]      # SP_1TORPANALMN / SP_2TORPANALMN / SP_3TORPANALMN

        [DataMember]
        public double P { set; get; }                       // Анализ миксера  [P]       # SP_1TORPANALP / SP_2TORPANALP / SP_3TORPANALP

        [DataMember]
        public double S { set; get; }                       // Анализ миксера  [S]       # SP_1TORPANALS / SP_2TORPANALS / SP_3TORPANALS

    }
}
