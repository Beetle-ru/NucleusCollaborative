using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataGathering
{
    class OffGas
    {
        public OffGas()
        {
        }

        private DateTime m_Date;

        public DateTime Date
        {
            get { return m_Date; }
            set { m_Date = value; }
        }
        private double m_H2;

        public double H2
        {
            get { return m_H2; }
            set { m_H2 = value; }
        }

        private double m_O2;

        public double O2
        {
            get { return m_O2; }
            set { m_O2 = value; }
        }
        private double m_CO;

        public double CO
        {
            get { return m_CO; }
            set { m_CO = value; }
        }
        private double m_CO2;

        public double CO2
        {
            get { return m_CO2; }
            set { m_CO2 = value; }
        }
        private double m_N2;

        public double N2
        {
            get { return m_N2; }
            set { m_N2 = value; }
        }
        private double m_Ar;

        public double Ar
        {
            get { return m_Ar; }
            set { m_Ar = value; }
        }
        /*
        private double m_C;

        public double C
        {
            get { return m_C; }
            set { m_C = value; }
        }
         * /
        /*
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
        */
        private int m_Id;

        public int Id
        {
            get { return m_Id; }
            set { m_Id = value; }
        }

        private int m_FusionId;

        public int FusionId
        {
            get { return m_FusionId; }
            set { m_FusionId = value; }
        }

        private int m_Temp;

        public int Temp
        {
            get { return m_Temp; }
            set { m_Temp = value; }
        }

        private int m_Flow;

        public int Flow
        {
            get { return m_Flow; }
            set { m_Flow = value; }
        }

        private string m_PhaseNo;

        public string PhaseNo
        {
            get { return m_PhaseNo; }
            set { m_PhaseNo = value; }
        }

    }
}




