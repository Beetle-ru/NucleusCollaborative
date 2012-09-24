using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataGathering
{
    class Addition
    {
        public Addition()
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

        private int m_MaterialId;

        public int MaterialId
        {
            get { return m_MaterialId; }
            set { m_MaterialId = value; }
        }

        private string m_MaterialName;

        public string MaterialName
        {
            get { return m_MaterialName; }
            set { m_MaterialName = value; }
        }

        private string m_PhaseNo;

        public string PhaseNo
        {
            get { return m_PhaseNo; }
            set { m_PhaseNo = value; }
        }

        private string m_Destination;

        public string Destination
        {
            get { return m_Destination; }
            set { m_Destination = value; }
        }

        private string m_DataSource;

        public string DataSource
        {
            get { return m_DataSource; }
            set { m_DataSource = value; }
        }

        private DateTime m_Date;

        public DateTime Date
        {
            get { return m_Date; }
            set { m_Date = value; }
        }

        private int m_PortionWeight;

        public int PortionWeight
        {
            get { return m_PortionWeight; }
            set { m_PortionWeight = value; }
        }

        private int m_TotalWeight;

        public int TotalWeight
        {
            get { return m_TotalWeight; }
            set { m_TotalWeight = value; }
        }

        // HEAT_ADDITIONS_ACT
    }
}
