using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter.SteelMakingClasses
{
    [Serializable]
    public class Additions : object
    {
        public List<Addition> addition { get; set; }        // добавки (10 шт)
        
        public Additions()
        {
            addition = new List<Addition>();
            addition.Add(new Addition());
            addition.Add(new Addition());
            addition.Add(new Addition());
            addition.Add(new Addition());
            addition.Add(new Addition());
            addition.Add(new Addition());
            addition.Add(new Addition());
            addition.Add(new Addition());
            addition.Add(new Addition());
            addition.Add(new Addition());
        }
       
        public override string ToString()
        {
            string str = base.ToString() + "<";
            addition.ForEach(delegate(Addition item)
            {
                str += item.ToString() + ";";
            });
            return str + ">";
        }
    }
}
