using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Implements;

namespace OGDecarbonaterFine
{
    class HeatDataReceiver
    {
        public int SmoothPeriod;

        public RollingAverage CO;
        public RollingAverage CO2;
        public RollingAverage H2;
        public RollingAverage O2;
        public RollingAverage N2;
        public RollingAverage Ar;
        public RollingAverage OffGasT;
        public RollingAverage OffGasV;
        public RollingAverage OffGasDecompression;

        public bool HeatIsStarted;
        public int LanceHeight;

        #region GET REGION

        private double m_offGasDecompression;
        public double GetOffGasDecompression()
        {
            var res = OffGasDecompression.Average(SmoothPeriod);
            m_offGasDecompression = Double.IsNaN(res) ? m_offGasDecompression : res;
            return m_offGasDecompression; 
        }

        private double m_offGasV;
        public double GetOffGasV()
        {
            var res = OffGasV.Average(SmoothPeriod);
            m_offGasV = Double.IsNaN(res) ? m_offGasV : res;
            return m_offGasV;
        }

        private double m_offGasT;
        public double GetOffGasT()
        {
            var res = OffGasT.Average(SmoothPeriod);
            m_offGasT = Double.IsNaN(res) ? m_offGasT : res;
            return m_offGasT;
        }

        private double m_CO;
        public double GetCO()
        {
            var res = CO.Average(SmoothPeriod);
            m_CO = Double.IsNaN(res) ? m_CO : res;
            return m_CO;
        }

        private double m_CO2;
        public double GetCO2()
        {
            var res = CO2.Average(SmoothPeriod);
            m_CO2 = Double.IsNaN(res) ? m_CO2 : res;
            return m_CO2;
        }

        private double m_H2;
        public double GetH2()
        {
            var res = H2.Average(SmoothPeriod);
            m_H2 = Double.IsNaN(res) ? m_H2 : res;
            return m_H2;
        }

        private double m_O2;
        public double GetO2()
        {
            var res = O2.Average(SmoothPeriod);
            m_O2 = Double.IsNaN(res) ? m_O2 : res;
            return m_O2;
        }

        private double m_N2;
        public double GetN2()
        {
            var res = N2.Average(SmoothPeriod);
            m_N2 = Double.IsNaN(res) ? m_N2 : res;
            return m_N2;
        }

        private double m_Ar;
        public double GetAr()
        {
            var res = Ar.Average(SmoothPeriod);
            m_Ar = Double.IsNaN(res) ? m_Ar : res;
            return m_Ar;
        }

        private int m_LanceHeight;
        public int GetLanceHeight()
        {
            var res = LanceHeight;
            m_LanceHeight = Int32.MinValue == res ? m_LanceHeight : res;
            return m_LanceHeight;
        }
        #endregion

        public HeatDataReceiver( int smoothPeriod, int lengthBuff = 50)
        {
            SmoothPeriod = smoothPeriod;

            CO = new RollingAverage(lengthBuff);
            CO2 = new RollingAverage(lengthBuff);
            H2 = new RollingAverage(lengthBuff);
            O2 = new RollingAverage(lengthBuff);
            N2 = new RollingAverage(lengthBuff);
            Ar = new RollingAverage(lengthBuff);
            OffGasT = new RollingAverage(lengthBuff);
            OffGasV = new RollingAverage(lengthBuff);
            OffGasDecompression = new RollingAverage(lengthBuff);

            HeatIsStarted = false;
            LanceHeight = 0;
        }
    }
}