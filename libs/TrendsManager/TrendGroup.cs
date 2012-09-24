using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonTypes;

namespace NordSteel.Data
{
    public class TrendGroup
    {
        public string Name { get; set; }
        public Type EventType { get; set; }
        public string PropertyName { get; set; }
        public List<TrendPoint> Points { get; set; }
        public int UnitNumber { get; set; }
        public object DataSource { get; set; }
        public TrendGroup()
        {
            Points = new List<TrendPoint>();
        }
    }
}
