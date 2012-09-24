using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataGathering
{
    public class Lance
    {
        public Lance()
        {
        }

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

        private DateTime m_Date;

        public DateTime Date
        {
            get { return m_Date; }
            set { m_Date = value; }
        }

        private string m_PhaseNo;

        public string PhaseNo
        {
            get { return m_PhaseNo; }
            set { m_PhaseNo = value; }
        }

        private int m_O2Vol;

        public int O2Vol
        {
            get { return m_O2Vol; }
            set { m_O2Vol = value; }
        }

        private int m_O2Flow;

        public int O2Flow
        {
            get { return m_O2Flow; }
            set { m_O2Flow = value; }
        }

        private double m_O2Pressure;

        public double O2Pressure
        {
            get { return m_O2Pressure; }
            set { m_O2Pressure = value; }
        }

        private int m_Height;

        public int Height
        {
            get { return m_Height; }
            set { m_Height = value; }
        }

    }
}
