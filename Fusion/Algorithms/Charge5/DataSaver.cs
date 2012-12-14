using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Charge5Classes;

namespace Charge5
{
    class DataSaver : OutData
    {
        /// <summary>
        /// разделитель данных в файле
        /// </summary>
        public char Separator = ';';

        public string ArchDir = "Arch";

        /// <summary>
        /// путь к файлу архиву
        /// </summary>
        public string ArchPath;

        /// <summary>
        /// номер плавки
        /// </summary>
        public Int64 HeatNumber;

        /// <summary>
        /// Температура чугуна
        /// </summary>
        public double THi;

        /// <summary>
        /// Кремний в чугуне
        /// </summary>
        public double SiHi;

        public DataSaver()
        {
            Directory.CreateDirectory(ArchDir);
            ArchPath = String.Format("{1}\\{0}", ArchNameGenerate("c5a"), ArchDir);
        }

        public void SaveArch()
        {
            var line = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}{0}{9}{0}{10}{0}{11}",
                                        Separator,
                                        DateTime.Now.ToString(),
                                        HeatNumber,
                                        SiHi,
                                        THi,
                                        MHi,
                                        MSc,
                                        MLi,
                                        MDlm,
                                        MFom,
                                        MDlms,
                                        IsFound
                                    );

            using (var outfile = new StreamWriter(ArchPath, true))
            {
                outfile.WriteLine(line);
            }
        }

        public void GetData(OutData od)
        {
            MHi = od.MHi;
            MSc = od.MSc;
            MLi = od.MLi;
            MDlm = od.MDlm;
            MFom = od.MFom;
            MDlms = od.MDlms;
            IsFound = od.IsFound;
        }

        public string ArchNameGenerate(string subname)
        {
            string timeLine = DateTime.Now.ToString();
            timeLine = timeLine.Replace(':', '_');
            timeLine = timeLine.Replace('.', '_');
            timeLine = timeLine + subname + ".csv";
            return timeLine;
        }

        public void Reset()
        {
            THi = 0.0;
            SiHi = 0.0;
            MSc = 0;
            MLi = 0;
            MFom = 0;
            MDlms = 0;
            MDlm = 0;
            HeatNumber = 0;
        }
    }
}
