using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HunterEvents
{
    public class CEDataFormat
    {
        /// <summary>
        /// 0
        /// </summary>
        public string Operation;
        /// <summary>
        /// 1
        /// </summary>
        public long CountTotal;
        /// <summary>
        /// 2
        /// </summary>
        public long CountDayAverage;
        /// <summary>
        /// 3
        /// </summary>
        public DateTime CaptureTime;
        /// <summary>
        /// 7
        /// </summary>
        public string Comment;
        public List<AttDataFormat> AttList;

        public CEDataFormat()
        {
            CaptureTime = new DateTime();
            AttList = new List<AttDataFormat>();
        }
    }

    public class AttDataFormat
    {
        /// <summary>
        /// 4
        /// </summary>
        public string Key;
        /// <summary>
        /// 5
        /// </summary>
        public string Type;
        /// <summary>
        /// 6
        /// </summary>
        public string ExampleValue;
        /// <summary>
        /// 7
        /// </summary>
        public string Comment;
    }
}
