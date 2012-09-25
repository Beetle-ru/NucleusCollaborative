using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataGathering
{
    class BathLevel
    {
        private int m_Id;


        public int Id
        {
            get { return m_Id; }
            set { m_Id = value; }
        }

        private DateTime m_Date;

        public DateTime Date
        {
            get { return m_Date; }
            set { m_Date = value; }
        }

        private int m_FusionId;

        public int FusionId
        {
            get { return m_FusionId; }
            set { m_FusionId = value; }
        }

        private int m_Value;

        public int Value
        {
            get { return m_Value; }
            set { m_Value = value; }
        }


    }
}
