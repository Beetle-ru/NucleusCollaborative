using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Threading;

namespace Tools.Emulator
{
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

        private int m_PlannedTempereture;

        public int PlannedTempereture
        {
            get { return m_PlannedTempereture; }
            set { m_PlannedTempereture = value; }
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
    }
}
