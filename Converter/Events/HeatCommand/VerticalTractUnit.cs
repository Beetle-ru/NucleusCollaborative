using CommonTypes;

namespace Converter
{
    /// <summary>
    /// Вертикальный тракт. Шаг по конкретному материалу.
    /// </summary>
    public class VerticalTractUnit
    {
        /// <summary>
        /// Наименование материала
        /// </summary>
        public string MaterialName { get; set; }
        /// <summary>
        /// Вес материала
        /// </summary>
        public float? MaterialWeight { get; set; }
        /// <summary>
        /// Признак корректировки
        /// </summary>
        public bool MaterialIsEditable { get; set; }
        /// <summary>
        /// Номер расходного бункера
        /// </summary>
        public int FlowrateBuncerNumber { get; set; }
        /// <summary>
        /// Признак отдачи материалов на весов
        /// </summary>
        public bool FlowrateBuncerIsOpen { get; set; }
        /// <summary>
        /// Номер весов
        /// </summary>
        public int ScalesNumber { get; set; }
        /// <summary>
        /// Признак отдачи материалов с весов
        /// </summary>
        public bool ScalesIsOpen { get; set; }
        /// <summary>
        /// Номер промежуточного бункера
        /// </summary>
        public int? MediateBuncerNumber { get; set; }
    }
}
