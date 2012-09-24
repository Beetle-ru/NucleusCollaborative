using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Converter
{
    [Serializable]
    public class SteelMakingPatternEvent : ConverterBaseEvent
    {
        //[DataMember]
        /// <summary>
        /// шаги
        /// </summary>
        public List<Converter.SteelMakingClasses.Step> steps { set; get; }           

        /// <summary>
        /// Названия материалов 10 шт.
        /// </summary>
        public List<string> materialsName { set; get; }                          


        public SteelMakingPatternEvent()
        {
            steps = new List<Converter.SteelMakingClasses.Step>();
            materialsName = new List<string>();
            const int HeatAdditionsCounter = 10;
            for (int i = 0; i < HeatAdditionsCounter; i++)
            {
                materialsName.Add("");
            }

        }
        public override string ToString()
        {
            string str = base.ToString() + "<";
            steps.ForEach(delegate(Converter.SteelMakingClasses.Step item) 
            { 
                str += item.ToString() + ";"; 
            });
            materialsName.ForEach(delegate(string item)
            {
                str += item.ToString() + ";";
            });
            return str + ">";
        }

    }
}
