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
    /// Событие для тестирования качества связи с ядром
    /// </summary>
    [Serializable]
    public class TestEvent : ConverterBaseEvent
    {
        /// <summary>
        /// Массив для тестирования большими пакетами
        /// </summary>
        public double[] Dimm { set; get; }

        //public TestEvent(int size = 0, double initData = 1.0)
        public TestEvent()
        {
            //Dimm = new double[size];
            //for (int i = 0; i < size; i++)
            //{
            //    Dimm[i] = initData;
            //}
        }
        
    }
}