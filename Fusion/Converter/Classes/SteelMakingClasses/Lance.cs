using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter.SteelMakingClasses
{
    [Serializable]
    public class Lance : object
    {
        /// <summary>
        /// положение фурмы
        /// </summary>
        public int LancePositin { get; set; }            
 
        /// <summary>
        /// интенсивность кислорода
        /// </summary>
        public double O2Flow { get; set; }               

     //   public int O2Volume { get; set; }                       // расход кислорода
        
        public Lance()
        {
            LancePositin = -1;
            O2Flow = -1;
      //      O2Volume = -1;
        }
        public override string ToString()
        {
            string str = base.ToString() + "<";
            str += LancePositin.ToString() + ";";
            str += O2Flow.ToString() + ";";
          //  str += O2Volume.ToString() + ";";
            return str + ">";
        }
    }
}
