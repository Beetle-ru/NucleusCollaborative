using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter.SteelMakingClasses
{
    [Serializable]
    public class Step : object
    {
        /// <summary>
        /// период
        /// </summary>
        public int Period { get; set; }              

        /// <summary>
        /// этап
        /// </summary>
        public int Phase { get; set; }                    

        /// <summary>
        /// расход кислорода на шаг
        /// </summary>
        public int O2Volume { get; set; }                 

        /// <summary>
        /// фурма
        /// </summary>
        public Lance lance { get; set; }                  


//        public Additions additions { get; set; }          //добавки

        /// <summary>
        /// очереди для весов - 5 шт
        /// 0 - весы 3;
        /// 1 - весы 4;
        /// 2 - весы 5;
        /// 3 - весы 6;
        /// 4 - весы 7;
        /// </summary>
        public List<WeigherLine> weigherLines { get; set; }        
 
        public Step()
        {
            Period = -1;
            Phase = -1;
            O2Volume = -1;
            lance = new Lance();
            //additions = new Additions();
            weigherLines = new List<WeigherLine>();
            const int WeigherLineCounter = 5;
            for (int i = 0; i < WeigherLineCounter; i++)
            {
                weigherLines.Add(new WeigherLine());
            }
        }
        public override string ToString()
        {
            string str = base.ToString() + "<";
            str += Period.ToString() + ";";
            str += Phase.ToString() + ";";
            str += O2Volume.ToString() + ";";
            str += lance.ToString() + ";";
            //str += additions.ToString() + ";";
            weigherLines.ForEach(delegate(WeigherLine item)
            {
                str += item.ToString() + ";";
            });
            return str + ">";
        }
     }

}
