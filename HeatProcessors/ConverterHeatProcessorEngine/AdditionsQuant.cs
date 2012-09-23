using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConverterHeatProcessorEngine
{
    public class AdditionsQuant
    {
        public List<AdditionQuant> Addition { get; set; }        // добавки (10 шт)

        public AdditionsQuant()
        {
            const int HeatAdditionsCounter = 10; 
            Addition = new List<AdditionQuant>();
            for (int i = 0; i < HeatAdditionsCounter; i++)
            {
                Addition.Add(new AdditionQuant());
            }
        }
       
        public override string ToString()
        {
            string str = base.ToString() + "<";
            Addition.ForEach(delegate(AdditionQuant item)
            {
                str += item.ToString() + ";";
            });
            return str + ">";
        }
    }
}
