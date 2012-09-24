using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConverterHeatProcessorEngine
{
    public class StepWeigherQuant //: IComparable<StepWeigherQuant>
    {
        public List<WeigherQuant> weigherQuant { get; set; }        // очереди для весов - 5 шт
                                                                    // 0 - весы 3
                                                                    // 1 - весы 4
                                                                    // 2 - весы 5
                                                                    // 3 - весы 6
                                                                    // 4 - весы 7
        public StepWeigherQuant()
        {
            weigherQuant = new List<WeigherQuant>();
            const int WeigherQuantCounter = 5;
            for (int i = 0; i < WeigherQuantCounter; i++)
            {
                weigherQuant.Add(new WeigherQuant());
            }
            
        }

     /*   public int CompareTo(StepWeigherQuant other)
        {
            throw new NotImplementedException();
            //return weigherQuant.
        }
        */
        public override string ToString()
        {
            string str = base.ToString() + "<";
            weigherQuant.ForEach(delegate(WeigherQuant item)
            {
                str += item.ToString() + ";";
            });
            return str + ">";
        }
    }

    public class WeigherQuant : IComparable<WeigherQuant>
    {
        public double PortionWeight { get; set; }       // Заданный вес
        public int OxygenTreshold { get; set; }            // O2 расход при котором отдаем с весов
        public int BunkerId { get; set; }            // id бункера от 0 до 7
        /// <summary>
        /// Не отдавать в конвертер, ждать нажатия на кнопку( true - не отдавать)
        /// По умолчанию false - отдает в конвертер по достижению O2 на шаг.
        /// </summary>
        public bool NotToGive { get; set; }

        /// <summary>
        /// Позволить добавлять всмысле досыпать 
        /// (true - вместо отдачи по достижению кислорода на шаг будет 
        /// произведена догрузка до значения указанного в следующем шаге. 
        /// Если в следующем шаге после текущего будет на весах указан вес 
        /// меньше чем уже загружено, то догрузка не будет осуществлена).
        /// По умолчанию false - по достижению O2 на шаг отдает в конвертер.
        /// </summary>
        public bool AllowToAdd { get; set; }

        public WeigherQuant()
        {
            PortionWeight = -1.0;
            OxygenTreshold = -1;
            BunkerId = -1;
            NotToGive = false;
            AllowToAdd = false;
        }

        public int CompareTo(WeigherQuant other)
        {
            //throw new NotImplementedException();
            return OxygenTreshold.CompareTo(other.OxygenTreshold);
        }

        public override string ToString()
        {
            string str = base.ToString() + "<";
            str += PortionWeight.ToString() + ";";
            str += OxygenTreshold.ToString() + ";";
            str += BunkerId.ToString() + ";";
            str += NotToGive.ToString() + ";";
            str += AllowToAdd.ToString() + ";";
            return str + ">";
        }
    }
}
