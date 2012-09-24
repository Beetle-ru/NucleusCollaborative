using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Converter
{
    /// <summary>
    /// Событие по освобождению весов, нужно посылать событие и указывать id ввесов которые будем освобождать
    /// </summary>
    [Serializable]
    public class ReleaseWeigherEvent : ConverterBaseEvent
    {
        //[DataMember]
        /// <summary>
        /// Id весов  - 5 шт
        /// 0 - весы 3;
        /// 1 - весы 4;
        /// 2 - весы 5;
        /// 3 - весы 6;
        /// 4 - весы 7;
        /// </summary>
        public int WeigherId { set; get; }

        public ReleaseWeigherEvent()
        {
            WeigherId = -1;
        }
        public override string ToString()
        {
            string str = base.ToString() + "<";
            str += WeigherId.ToString() + ";";
            return str + ">";
        }

    }
}
