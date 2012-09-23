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
    /// Событие происходит в момент сбора обучающих данных для многофакторной модели
    /// </summary>
    [Serializable]
    public class FixDataMfactorModelEvent : ConverterBaseEvent
    {
        public int FixData { set; get; } 
        
        public FixDataMfactorModelEvent()
        {
            FixData = 0;
        }
    }
} 