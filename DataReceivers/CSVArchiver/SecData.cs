using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Implements;

namespace CSVArchiver
{
    class SecData
    {
        public string CurrentTime { get; set; }
        public int LanceHeigth { get; set; }
        public double OxygenRate { get; set; }
        public double H2Perc { get; set; }
        public double O2Perc { get; set; }
        public double COPerc { get; set; }
        public double CO2Perc { get; set; }
        public double N2Perc { get; set; }
        public double ArPerc { get; set; }
        public double VGas { get; set; }
        public double TGas { get; set; }
        public double CCalc { get; set; }
        public double CSubLance { get; set; }
        public int Ignition { get; set; }
        public int Decompression { get; set; }

        public char Separator;

        public SecData()
        {
            Separator = ';';

            CurrentTime = DateTime.Now.ToString();

            LanceHeigth = -1;
            OxygenRate = -1.0;
            H2Perc = -1.0;
            O2Perc = -1.0;
            COPerc = -1.0;
            CO2Perc = -1.0;
            N2Perc = -1.0;
            ArPerc = -1.0;
            VGas = -1.0;
            TGas = -1.0;
            CCalc = -1.0;
            CSubLance = -1.0;
            Ignition = -1;
            Decompression = -1;

        }
        public string GetHeader()
        {
            string str = "";
            str += String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}{0}{9}{0}{10}{0}{11}{0}{12}{0}{13}{0}{14}{0}{15}",
                Separator,
                "Time",
                "Heigth lance",
                "Rate O2",
                "H2",
                "O2",
                "CO",
                "CO2",
                "N2",
                "Ar",
                "V gas",
                "T gas",
                "C calculated",
                "C sublance",
                "Ignition",
                "DecompressionOffGasEvent"
                );
            return str;
        }

        public override string ToString()
        {
            string str = "";
            str += String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}{0}{9}{0}{10}{0}{11}{0}{12}{0}{13}{0}{14}{0}{15}",
                Separator,
                CurrentTime,
                LanceHeigth,
                OxygenRate,
                H2Perc,
                O2Perc,
                COPerc,
                CO2Perc,
                N2Perc,
                ArPerc,
                VGas,
                TGas,
                CCalc,
                CSubLance,
                Ignition,
                Decompression
                );
            return str;
        }
    }

    internal class SecDataSmooth
    {
        public RollingAverage LanceHeigth { get; set; }
        public RollingAverage OxygenRate { get; set; }
        public RollingAverage H2Perc { get; set; }
        public RollingAverage O2Perc { get; set; }
        public RollingAverage COPerc { get; set; }
        public RollingAverage CO2Perc { get; set; }
        public RollingAverage N2Perc { get; set; }
        public RollingAverage ArPerc { get; set; }
        public RollingAverage VGas { get; set; }
        public RollingAverage TGas { get; set; }
        public RollingAverage CCalc { get; set; }
        public double CSubLance { get; set; }
        public int Ignition { get; set; }
        public RollingAverage Decompression { get; set; }
        
        public SecDataSmooth()
        {
            LanceHeigth = new RollingAverage();
            OxygenRate = new RollingAverage();
            H2Perc = new RollingAverage();
            O2Perc = new RollingAverage();
            COPerc = new RollingAverage();
            CO2Perc = new RollingAverage();
            N2Perc = new RollingAverage();
            ArPerc = new RollingAverage();
            VGas = new RollingAverage();
            TGas = new RollingAverage();
            CCalc = new RollingAverage();
            CSubLance = 0.0;
            Ignition = 0;
            Decompression = new RollingAverage();
        }
        public SecData GetSecData(int sec)
        {
            var sd = new SecData();
            sd.LanceHeigth = (int)LanceHeigth.Average(sec);
            sd.OxygenRate = OxygenRate.Average(sec);
            sd.H2Perc = H2Perc.Average(sec);
            sd.O2Perc = O2Perc.Average(sec);
            sd.COPerc = COPerc.Average(sec);
            sd.CO2Perc = CO2Perc.Average(sec);
            sd.N2Perc = N2Perc.Average(sec);
            sd.ArPerc = ArPerc.Average(sec);
            sd.VGas = VGas.Average(sec);
            sd.TGas = TGas.Average(sec);
            sd.CCalc = CCalc.Average(sec);
            sd.CSubLance = CSubLance;
            sd.Ignition = Ignition;
            sd.Decompression = (int)Decompression.Average(sec);
            return sd;
        }
    }
}
