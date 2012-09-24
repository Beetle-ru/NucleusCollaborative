using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using CommonTypes;

namespace Converter
{
    
    /// <summary>
    /// Действие на шаге
    /// </summary>
    public class HeatStep
    {
        /// <summary>
        /// Номер шага
        /// </summary>
        public int StepNumber { get; set; }
        /// <summary>
        /// Вертикальный тракт на шаге. Коллекция расходных метериалов.
        /// </summary>
        public List<VerticalTractUnit> VerticalTract;
        /// <summary>
        /// Данные по фурме на шаге
        /// </summary>
        public Lance Lance { get; set; }

        //Далее необходимо описать дополнтельные условия шагов.

    }
}
