using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Reflection;
using Core;

namespace Converter
{
    [Serializable]
    [DataContract]
    //[PLCGroup(Location = "PLC13", Destination = "Converter1")] возможно для других ковертеров отличается
    [PLCGroup(Location = "PLC21", Destination = "Converter2")]
    //[PLCGroup(Location = "PLC33", Destination = "Converter3")] возможно для других ковертеров отличается
    //[DBGroup(UnitNumber = 1, DisplayName = "Обьем и температура отходящих газов", IsTrendGroup = true, BindingPropertyName = "OffGasHistory")]
    [DBGroup(UnitNumber = 2)]
    //[DBGroup(UnitNumber = 3, DisplayName = "Обьем и температура отходящих газов", IsTrendGroup = true, BindingPropertyName = "OffGasHistory")]
    public class O2Event : ConverterBaseEvent
    {
        /// <summary>
        /// Расход кислорода правая фурма
        /// </summary>
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB4,REAL14")]
        public double QOxygenRight { get; set; }

        /// <summary>
        /// Давление кислорода правая фурма
        /// </summary>
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB4,REAL6")]
        public double POxygenRight { get; set; }

        /// <summary>
        /// Температура кислорода правая фурма
        /// </summary>
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB8,REAL6")]
        public double TOxygenRight { get; set; }

        /// <summary>
        /// Перепад давления кислорода правая фурма # ACT_CX_DIFFO2_R
        /// </summary>
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,REAL140")]
        public double DPOxygenRight { get; set; }

        /// <summary>
        /// Индикация выбора правой фурмы ("1" - выбрана) # AS31/SEN_S31.L3L2_right
        /// </summary>
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB6,BYTE28", IsBoolean = true, BitNumber = 1)]
        public bool RightLanceIsSelected { set; get; } 

        ////////////////////////////////////////////////
        
        /// <summary>
        /// Расход кислорода левая фурма
        /// </summary>
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB4,REAL10")]
        public double QOxygenLeft { get; set; }

        /// <summary>
        /// Давление кислорода левая фурма
        /// </summary>
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB4,REAL2")]
        public double POxygenLeft { get; set; }

        /// <summary>
        /// Температура кислорода левая фурма
        /// </summary>
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB8,REAL2")]
        public double TOxygenLeft { get; set; }

        /// <summary>
        /// Перепад давления кислорода левая фурма # ACT_CX_DIFFO2_L
        /// </summary>
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB2,REAL136")]
        public double DPOxygenLeft { get; set; }
        /// <summary>
        /// Индикация выбора левой фурмы ("1" - выбрана) # AS31/SEN_S31.L3L2_left
        /// </summary>
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB6,BYTE28", IsBoolean = true, BitNumber = 0)]
        public bool LeftLanceIsSelected { set; get; }
    }
}
