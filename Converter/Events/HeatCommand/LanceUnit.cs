using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    /// <summary>
    /// Скорость движения фурмы: повышенная (0,86), пониженная (0,20)
    /// </summary>
    public enum Speed
    {
        None, Increase, Decrease
    }

    /// <summary>
    /// Фурма
    /// </summary>
    public class LanceUnit
    {
        /// <summary>
        /// Позиция фурмы начальна (Фн)
        /// </summary>
        public float? LancePositionStart { get; set; }
        /// <summary>
        /// Позиция фурмы конечная (Фк)
        /// </summary>
        public float? LancePositionEnd { get; set; }
        /// <summary>
        ///Зона нечуствительности		
        /// </summary>
        public float? Deadband { get; set; }
        /// <summary>
        /// Скорость движения фурмы
        /// </summary>
        public Speed LanceSpeed { get; set; }
        /// <summary>
        /// Интенсивность вдувания кислорода
        /// </summary>
        public float? IntensityOxigen { get; set; }
        /// <summary>
        /// Расход кислорода на шаге (Q О2 шаг) 		
        /// </summary>
        public float? StepFlowrate { get; set; }
        /// <summary>
        /// Суммарный расход кислорода на плавку к окончанию текущего шага(ƩО2 на плавку с нарастанием)
        /// </summary>
        public float? FlowrateInc { get; set; }
        /// <summary>
        /// Клапан регулирующий (КР)			
        /// </summary>
        public bool RegulatoryValveState { get; set; }
        /// <summary>
        /// Клапан отсечной (КО)				
        /// </summary>
        public bool ShutOffValveState { get; set; }
        /// <summary>
        /// Юбка
        /// </summary>
        public bool Skirt { get; set; }
    }
}
