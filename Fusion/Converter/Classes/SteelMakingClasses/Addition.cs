using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter.SteelMakingClasses
{
    [Serializable]
    public class Addition : object
    {
        public double MaterialPortionWeight { get; set; }       // Заданный вес материала
     //   public int O2VolPortionMateria { get; set; }            // O2 расход при котором засыпаем
        public Addition()
        {
            MaterialPortionWeight = -1.0;
     //       O2VolPortionMateria = -1;
        }
        public override string ToString()
        {
            string str = base.ToString() + "<";
            str += MaterialPortionWeight.ToString() + ";";
      //      str += O2VolPortionMateria.ToString() + ";";
            return str + ">";
        }
    }
}
