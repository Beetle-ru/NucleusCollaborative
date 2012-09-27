using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Reflection;
using Core;

namespace Converter
{

    /// <summary>
    /// Событие по азоту
    /// </summary>
    [Serializable]
    [DataContract]
    //[PLCGroup(Location = "PLC13", Destination = "Converter1")] возможно для других ковертеров отличается
    [PLCGroup(Location = "PLC23", Destination = "Converter2")]
    //[PLCGroup(Location = "PLC33", Destination = "Converter3")] возможно для других ковертеров отличается
    //[DBGroup(UnitNumber = 1, DisplayName = "Обьем и температура отходящих газов", IsTrendGroup = true, BindingPropertyName = "OffGasHistory")]
    [DBGroup(UnitNumber = 2)]
    //[DBGroup(UnitNumber = 3, DisplayName = "Обьем и температура отходящих газов", IsTrendGroup = true, BindingPropertyName = "OffGasHistory")]
    public class N2Event : ConverterBaseEvent
    {
        /// <summary>
        /// Расход азота на уплотнение фурменного окна
        /// </summary>
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB19,INT14")]
        public int QNitrogenLanceWindow { get; set; }

        /// <summary>
        /// Расход азота на уплотнение котла
        /// </summary>
        [DataMember]
        [DBPoint(IsStored = true)]
        [PLCPoint(Location = "DB19,INT16")]
        public int QNitrogenBoiler { get; set; }

        ///// <summary>
        ///// Расход азота на уплотнение течек
        ///// </summary>
        //[DataMember]
        //[DBPoint(IsStored = true)]
        //[PLCPoint(Location = "DB19,INT16")]
        //public int QNitrogenEstrus { get; set; }

        ///// <summary>
        ///// Давление азота на уплотнении фурменного окна
        ///// </summary>
        //[DataMember]
        //[DBPoint(IsStored = true)]
        //[PLCPoint(Location = "DB19,INT14")]
        //public int PNitrogenLanceWindow { get; set; }

        ///// <summary>
        ///// Давление азота на уплотнении котла
        ///// </summary>
        //[DataMember]
        //[DBPoint(IsStored = true)]
        //[PLCPoint(Location = "DB19,INT16")]
        //public int PNitrogenBoiler { get; set; }

        ///// <summary>
        ///// Давление азота на уплотнении течек
        ///// </summary>
        //[DataMember]
        //[DBPoint(IsStored = true)]
        //[PLCPoint(Location = "DB19,INT16")]
        //public int PNitrogenEstrus { get; set; }

        ///// <summary>
        ///// Температура азота на уплотнении фурменного окна
        ///// </summary>
        //[DataMember]
        //[DBPoint(IsStored = true)]
        //[PLCPoint(Location = "DB19,INT14")]
        //public int TNitrogenLanceWindow { get; set; }

        ///// <summary>
        ///// Температура азота на уплотнении котла
        ///// </summary>
        //[DataMember]
        //[DBPoint(IsStored = true)]
        //[PLCPoint(Location = "DB19,INT16")]
        //public int TNitrogenBoiler { get; set; }

        ///// <summary>
        ///// Температура азота на уплотнении течек
        ///// </summary>
        //[DataMember]
        //[DBPoint(IsStored = true)]
        //[PLCPoint(Location = "DB19,INT16")]
        //public int TNitrogenEstrus { get; set; }

        ///// <summary>
        ///// Перепад давления азота на уплотнении фурменного окна
        ///// </summary>
        //[DataMember]
        //[DBPoint(IsStored = true)]
        //[PLCPoint(Location = "DB19,INT14")]
        //public int DPNitrogenLanceWindow { get; set; }

        ///// <summary>
        ///// Перепад давления азота на уплотнении котла
        ///// </summary>
        //[DataMember]
        //[DBPoint(IsStored = true)]
        //[PLCPoint(Location = "DB19,INT16")]
        //public int DPNitrogenBoiler { get; set; }

        ///// <summary>
        ///// Перепад давления азота на уплотнении течек
        ///// </summary>
        //[DataMember]
        //[DBPoint(IsStored = true)]
        //[PLCPoint(Location = "DB19,INT16")]
        //public int DPNitrogenEstrus { get; set; }
    }
}
