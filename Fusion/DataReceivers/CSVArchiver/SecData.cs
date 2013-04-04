using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Implements;

namespace CSVArchiver {
    internal class SecData {
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
        public double QOxygenCL { get; set; }
        public double POxygenCL { get; set; }
        public double TOxygenCL { get; set; }
        public double DPOxygenCL { get; set; }
        public int QNitrogenLanceWindow { get; set; }
        public int QNitrogenBoiler { get; set; }
        public double RB5 { get; set; }
        public double RB6 { get; set; }
        public double RB7 { get; set; }
        public double RB8 { get; set; }
        public double RB9 { get; set; }
        public double RB10 { get; set; }
        public double RB11 { get; set; }
        public double RB12 { get; set; }
        public double NeuralC { get; set; }
        public double UniversalC { get; set; }

        public char Separator;

        public SecData() {
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
            QOxygenCL = -1.0;
            POxygenCL = -1.0;
            TOxygenCL = -1.0;
            DPOxygenCL = -1.0;
            QNitrogenLanceWindow = -1;
            QNitrogenBoiler = -1;
            RB5 = -1.0;
            RB6 = -1.0;
            RB7 = -1.0;
            RB8 = -1.0;
            RB9 = -1.0;
            RB10 = -1.0;
            RB11 = -1.0;
            RB12 = -1.0;
            NeuralC = -1.0;
            UniversalC = -1.0;
        }

        public string GetHeader() {
            string str = "";
            str +=
                String.Format(
                    "{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}{0}{9}{0}{10}{0}{11}{0}{12}{0}{13}{0}{14}{0}{15}{0}{16}{0}{17}{0}{18}{0}{19}{0}{20}{0}{21}" +
                    "{0}{22}{0}{23}{0}{24}{0}{25}{0}{26}{0}{27}{0}{28}{0}{29}{0}{30}{0}{31}",
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
                    "DecompressionOffGasEvent",
                    "QOxygen",
                    "POxygen",
                    "TOxygen",
                    "DeltaPOxygen",
                    "QNitrogenLanceWindow",
                    "QNitrogenBoiler",
                    "RB5",
                    "RB6",
                    "RB7",
                    "RB8",
                    "RB9",
                    "RB10",
                    "RB11",
                    "RB12",
                    "NeuralC",
                    "Universal"
                    );
            return str;
        }

        public override string ToString() {
            string str = "";
            str +=
                String.Format(
                    "{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}{0}{9}{0}{10}{0}{11}{0}{12}{0}{13}{0}{14}{0}{15}{0}{16}{0}{17}{0}{18}{0}{19}{0}{20}{0}{21}" +
                    "{0}{22}{0}{23}{0}{24}{0}{25}{0}{26}{0}{27}{0}{28}{0}{29}{0}{30}{0}{31}",
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
                    Decompression,
                    QOxygenCL,
                    POxygenCL,
                    TOxygenCL,
                    DPOxygenCL,
                    QNitrogenLanceWindow,
                    QNitrogenBoiler,
                    RB5,
                    RB6,
                    RB7,
                    RB8,
                    RB9,
                    RB10,
                    RB11,
                    RB12,
                    NeuralC,
                    UniversalC
                    );
            return str;
        }
    }

    internal class SecDataSmooth {
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
        public RollingAverage QOxygenCL { get; set; }
        public RollingAverage POxygenCL { get; set; }
        public RollingAverage TOxygenCL { get; set; }
        public RollingAverage DPOxygenCL { get; set; }
        public RollingAverage QNitrogenLanceWindow { get; set; }
        public RollingAverage QNitrogenBoiler { get; set; }
        public double RB5 { get; set; }
        public double RB6 { get; set; }
        public double RB7 { get; set; }
        public double RB8 { get; set; }
        public double RB9 { get; set; }
        public double RB10 { get; set; }
        public double RB11 { get; set; }
        public double RB12 { get; set; }
        public RollingAverage NeuralC { get; set; }
        public double UniversalC { get; set; }

        public SecDataSmooth() {
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
            QOxygenCL = new RollingAverage();
            POxygenCL = new RollingAverage();
            TOxygenCL = new RollingAverage();
            DPOxygenCL = new RollingAverage();
            QNitrogenLanceWindow = new RollingAverage();
            QNitrogenBoiler = new RollingAverage();
            RB5 = -1.0;
            RB6 = -1.0;
            RB7 = -1.0;
            RB8 = -1.0;
            RB9 = -1.0;
            RB10 = -1.0;
            RB11 = -1.0;
            RB12 = -1.0;
            NeuralC = new RollingAverage();
            UniversalC = -1.0;
        }

        public SecData GetSecData(int sec) {
            var sd = new SecData();
            sd.LanceHeigth = (int) LanceHeigth.Average(sec);
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
            sd.Decompression = (int) Decompression.Average(sec);
            sd.QOxygenCL = QOxygenCL.Average(sec);
            sd.POxygenCL = POxygenCL.Average(sec);
            sd.TOxygenCL = TOxygenCL.Average(sec);
            sd.DPOxygenCL = DPOxygenCL.Average(sec);
            sd.QNitrogenLanceWindow = (int) QNitrogenLanceWindow.Average(sec);
            sd.QNitrogenBoiler = (int) QNitrogenBoiler.Average(sec);
            sd.RB5 = RB5;
            sd.RB6 = RB6;
            sd.RB7 = RB7;
            sd.RB8 = RB8;
            sd.RB9 = RB9;
            sd.RB10 = RB10;
            sd.RB11 = RB11;
            sd.RB12 = RB12;
            sd.NeuralC = NeuralC.Average(sec);
            sd.UniversalC = UniversalC;
            return sd;
        }
    }
}