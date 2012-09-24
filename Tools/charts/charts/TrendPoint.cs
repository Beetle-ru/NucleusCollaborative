using System;
using System.Collections.Generic;
using System.Text;


namespace Converter.Trends
{
    public class TrendPoint
    {
        private TimeSpan m_Time;
        private double m_H2;        
        private double m_O2;
        private double m_CO;
        private double m_CO2;
        private double m_N2;
        private double m_Ar;

        public TimeSpan Time { get { return m_Time; } }
        public double H2 { get { return m_H2; } }
        public double O2 { get { return m_O2; } }
        public double CO { get { return m_CO; } }
        public double CO2 { get { return m_CO2; } }
        public double N2 { get { return m_N2; } }
        public double Ar { get { return m_Ar; } }

        private double m_O2Pressure;

        public double O2Pressure
        {
            get { return m_O2Pressure; }
            set { m_O2Pressure = value; }
        }

        private int m_LanceHeight;

        public int LanceHeight
        {
            get { return m_LanceHeight; }
            set { m_LanceHeight = value; }
        }

        private int m_GasFlow;

        public int GasFlow
        {
            get { return m_GasFlow; }
            set { m_GasFlow = value; }
        }

        public TrendPoint()
        {
        }

        public TrendPoint(TimeSpan time, double H2, double O2, double CO, double CO2, double N2, double Ar)
        {
            m_Time = time;
            m_H2 = H2;
            m_O2 = O2;
            m_CO = CO;
            m_CO2 = CO2;
            m_N2 = N2;
            m_Ar = Ar;
        }
    }
}
