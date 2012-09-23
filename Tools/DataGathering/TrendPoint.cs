using System;
using System.Collections.Generic;
using System.Text;


namespace Converter.Trends
{
    public class TrendPoint
    {
        public TimeSpan Time { get; private set; }
        public double H2 { get; private set; }
        public double O2 { get; private set; }
        public double CO { get; private set; }
        public double CO2 { get; private set; }
        public double N2 { get; private set; }
        public double Ar { get; private set; }
        public double C { get; private set; }
        public double O2Pressure { get; set; }
        public int LanceHeight { get; set; }
        public int GasFlow { get; set; }
        public TrendPoint()
        {
        }

        public TrendPoint(TimeSpan time, double H2, double O2, double CO, double CO2, double N2, double Ar)
        {
            Time = time;
            this.H2 = H2;
            this.O2 = O2;
            this.CO = CO;
            this.CO2 = CO2;
            this.N2 = N2;
            this.Ar = Ar;
        }
    }
}
