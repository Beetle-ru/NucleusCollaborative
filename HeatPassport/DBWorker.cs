using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeatPassport
{
    // singletone
    public class DBWorker
    {
        private DBLayer m_DB;

        private DBWorker()
        {
            m_DB = new DBLayer();
        }

        private static DBWorker _Instance = new DBWorker();

        public static DBWorker Instance
        {
            get { return _Instance; }
        }

        public void Insert(params object[] Values)
        {
            m_DB.InsertOperation(Values);
        }

        public int GetCurrentHeatNumber(int ConverterNumber)
        {
            return m_DB.GetCurrentHeatNumber(ConverterNumber);
        }

    }
}
