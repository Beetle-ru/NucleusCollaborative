using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Threading;

namespace Converter.Trends
{
    public class TrendsFusion
    {
        private bool m_NewType;
        private List<Fusion> m_Fusions;
        public TrendsFusion(string FileName)
        {
            CultureInfo curCulture = Thread.CurrentThread.CurrentCulture;
            CultureInfo newCulture = new CultureInfo(curCulture.Name);
            newCulture.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = newCulture;
            m_Fusions = GetTrendPoints(FileName);
        }

        public void Save()
        {
            if (m_Fusions.Count > 0)
            {
                string path = GetFileNameNew();
                System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(path);
                streamWriter.WriteLine("New .pldx files V1. H2 O2 CO CO2 N2 Ar O2Pressure LanceHeight GasFlow");
                foreach (Fusion fusion in m_Fusions)
                {
                    streamWriter.WriteLine(string.Format(">>{0} {1} {2} {3} {4} {5} {6} {7} {8} {9}",
                                                            fusion.Id, fusion.Number,
                                                            fusion.StartDate, fusion.StartDateDB,
                                                            fusion.TeamNumber,fusion.Grade,
                                                            fusion.PlannedTempereture,fusion.FactTemperature,
                                                            fusion.PlannedC,fusion.FactC));
                    foreach (TrendPoint trendPoint in fusion.Points)
                    {
                        streamWriter.WriteLine(string.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9}",
                                                              trendPoint.Time, trendPoint.H2,
                                                              trendPoint.O2,trendPoint.CO,
                                                              trendPoint.CO2,trendPoint.N2,
                                                              trendPoint.Ar,trendPoint.O2Pressure,
                                                              trendPoint.LanceHeight,trendPoint.GasFlow));
                    }
                }
                streamWriter.Close();
            }
        }

        public string GetFileNameNew()
        {
            if (m_Fusions.Count > 0)
                return string.Format("C{0}_{1}.pldx", m_Fusions[0].ConverterNumber, m_Fusions[0].StartDate.ToShortDateString());
            else
                return "error";
        }
        public List<Fusion> Fusions { get { return m_Fusions; } }

        public List<Fusion> GetTrendPoints(string fileName)
        {
            List<Fusion> result = new List<Fusion>();
            List<TrendPoint> points = new List<TrendPoint>();
            System.IO.StreamReader streamReader = new System.IO.StreamReader(fileName);
            System.IO.FileInfo file = new System.IO.FileInfo(fileName);
            m_NewType = file.Extension == ".pldx";

            int countFusion = 0;
            string startDate = "";
            Fusion fusion = null;
            while (!streamReader.EndOfStream)
            {
                string temp = streamReader.ReadLine();
                if (temp.StartsWith("V7") || temp.StartsWith("New"))
                {
                    continue;
                }
                if (temp.StartsWith(">"))
                {

                    
                    countFusion++;
                    if (countFusion > 1)
                    {
                        
                        if (!m_NewType)
                        {
                            string startDate1 = ParseDateFromFileName(fileName);
                            startDate1 = string.Format("{0} {1}:00", startDate1, temp.Substring(1, 5));
                            if (Convert.ToDateTime(startDate) != Convert.ToDateTime(startDate1))
                            {
                                fusion = new Fusion(countFusion, Convert.ToDateTime(startDate), points);
                                fusion.ConverterNumber = int.Parse(fileName.Substring(fileName.Length - 16, 1));
                            }
                            else
                            {
                                countFusion--;
                            }
                        }
                        else
                        {
                            
                            string[] split = temp.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            fusion = new Fusion(int.Parse(split[1]), DateTime.Parse(split[2]+" "+split[3]), points);
                            fusion.ConverterNumber = int.Parse(fileName.Substring(fileName.Length - 17, 1));
                            fusion.Id = int.Parse(split[0].Substring(2));
                            fusion.StartDateDB = DateTime.Parse(split[4]+" "+ split[5]);
                            fusion.TeamNumber = int.Parse(split[6]);
                            int offset = 0;
                            if (split.Length < 12) continue;
                            if (split.Length > 12)
                            {
                                offset = split.Length - 12;
                                for (int i = 7; i <= 7 + offset; i++)
                                {
                                    fusion.Grade += split[i];
                                }
                            }
                            else
                            {
                                fusion.Grade = split[7];
                            }

                            fusion.PlannedTempereture =int.Parse( split[8+offset]);
                            fusion.FactTemperature = int.Parse(split[9 + offset]);
                            fusion.PlannedC = double.Parse(split[10 + offset]);
                            fusion.FactC = double.Parse(split[11 + offset]);
                            
                        }
                        if (fusion != null)
                        {
                            result.Add(fusion);
                            points = new List<TrendPoint>();
                        }
                    }
                    if (!m_NewType)
                    {
                        startDate = ParseDateFromFileName(fileName);
                        startDate = string.Format("{0} {1}:00", startDate, temp.Substring(1, 5));
                    }
                    else
                    {
                        startDate = temp;
                    }
                    continue;
                }
                if (m_NewType)
                {
                    points.Add(ParseLineNew(temp.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries) ));
                }
                else
                {
                    points.Add(ParseLine(temp.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries), startDate));
                }
            }


            return result;
        }

        private TrendPoint ParseLineNew(string[] lines)
        {
            TrendPoint trendPoint;
            if (lines.Length < 10) return null;
            trendPoint =  new TrendPoint(TimeSpan.Parse(lines[0]),
                       double.Parse(lines[1]),
                       double.Parse(lines[2]),
                       double.Parse(lines[3]),
                       double.Parse(lines[4]),
                       double.Parse(lines[5]),
                       double.Parse(lines[6])
                       );
            trendPoint.O2Pressure = double.Parse(lines[7]);
            trendPoint.LanceHeight = int.Parse(lines[8]);
            trendPoint.GasFlow = int.Parse(lines[9]);

            return trendPoint;
        }


        private TrendPoint ParseLine(string[] lines, string startDate)
        {
            DateTime date = DateTime.Parse(startDate);

            return new TrendPoint(date.AddSeconds(double.Parse(lines[0])) - date,
                       double.Parse(lines[2]),
                       double.Parse(lines[3]),
                       double.Parse(lines[4]),
                       double.Parse(lines[5]),
                       double.Parse(lines[6]),
                       double.Parse(lines[7])
                       );
        }

        private string ParseDateFromFileName(string fileName)
        {
            return fileName.Substring(fileName.Length - 14, 10);
        }
    }

    public class Fusion
    {
        private int m_Id;

        public int Id
        {
            get { return m_Id; }
            set { m_Id = value; }
        }
        private int m_Number;
        private DateTime m_EndDate;

        public DateTime EndDate
        {
            get { return m_EndDate; }
            set { m_EndDate = value; }
        }

        private int m_PlannedTemperature;

        public int PlannedTempereture
        {
            get { return m_PlannedTemperature; }
            set { m_PlannedTemperature = value; }
        }

        private int m_FactTemperature;

        public int FactTemperature
        {
            get { return m_FactTemperature; }
            set { m_FactTemperature = value; }
        }

        private double m_PlannedC;

        public double PlannedC
        {
            get { return m_PlannedC; }
            set { m_PlannedC = value; }
        }

        private double m_FactC;

        public double FactC
        {
            get { return m_FactC; }
            set { m_FactC = value; }
        }

        private int m_ConverterNumber;

        public int ConverterNumber
        {
            get { return m_ConverterNumber; }
            set { m_ConverterNumber = value; }
        }

        private int m_TeamNumber;

        public int TeamNumber
        {
            get { return m_TeamNumber; }
            set { m_TeamNumber = value; }
        }

        private string m_Grade;

        public string Grade
        {
            get { return m_Grade; }
            set { m_Grade = value; }
        }

        private List<TrendPoint> m_Points;
        private DateTime m_StartDate;
        private DateTime m_StartDateDB;

        public DateTime StartDateDB
        {
            get { return m_StartDateDB; }
            set { m_StartDateDB = value; }
        }
        public int Number { get { return m_Number; } set { m_Number = value; } }
        public List<TrendPoint> Points { get { return m_Points; } }
        public DateTime StartDate { get { return m_StartDate; } }

        public Fusion()
        {
            m_Points = new List<TrendPoint>();
        }

        public Fusion(int number, DateTime startDate, List<TrendPoint> points)
        {
            m_Number = number;
            m_StartDate = startDate;
            m_Points = points;
        }

        private int m_CastIronWeight;

        public int CastIronWeight
        {
            get { return m_CastIronWeight; }
            set { m_CastIronWeight = value; }
        }

        private int m_CastIronTemp;

        public int CastIronTemp
        {
            get { return m_CastIronTemp; }
            set { m_CastIronTemp = value; }
        }

    }
}
