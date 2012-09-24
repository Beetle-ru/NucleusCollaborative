using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataGathering
{
    class ScrapBucket
    {
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

        public int MaterialNumber { get; set; }
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

        private int m_Weight;

        public int Weight
        {
            get { return m_Weight; }
            set { m_Weight = value; }
        }

        public int Number { get; set; }

        public ScrapBucket()
        {
        }
    }
}
