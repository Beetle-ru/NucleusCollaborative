using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter.SteelMakingClasses
{
    [Serializable]
    public class WeigherLine
    {
        /// <summary>
        /// Заданный вес
        /// </summary>
        public double PortionWeight { get; set; }       
        
        //public int OxygenTreshold { get; set; }            // O2 расход при котором отдаем с весов
 
        /// <summary>
        /// id бункера от 0 до 7
        /// </summary>
        public int BunkerId { get; set; }            
        
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
        
        public WeigherLine()
        {
            PortionWeight = -1.0;
            NotToGive = false;
            AllowToAdd = false;
            //OxygenTreshold = -1;
            BunkerId = -1;
        }
        public override string ToString()
        {
            string str = base.ToString() + "<";
            str += PortionWeight.ToString() + ";";
            //str += OxygenTreshold.ToString() + ";";
            str += BunkerId.ToString() + ";";
            str += NotToGive.ToString() +";";
            str += AllowToAdd.ToString() + ";";
            return str + ">";
        }
    }
}
