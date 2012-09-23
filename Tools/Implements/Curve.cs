using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Implements
{
    public class Curve
    {
        private readonly DataCurve data;
        public int Length { get; set; }
        public Color ColorCurve { get; set; }

        public Curve()
        {
            data = new DataCurve();
            Length = 0;
            ColorCurve = Color.FromArgb(255, 255, 255);
        }

        public void AddPoint(float xPercent, float yPercent)
        {
            data.XPercent.Add(xPercent);
            data.YPercent.Add(yPercent);
            Length++;
        }
        public DataCurve GetData()
        {
            return data;
        }
    }

    public class DataCurve
    {
        public List<float> XPercent;
        public List<float> YPercent;
        
        public DataCurve()
        {
            XPercent = new List<float>();
            YPercent = new List<float>();
        }
    }
}
