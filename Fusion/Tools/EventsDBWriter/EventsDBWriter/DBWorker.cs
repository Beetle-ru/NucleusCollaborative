using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using CommonTypes;
namespace Tools.DB
{
    class DBWorker
    {
        private DBLayer m_DB; 

        private DBWorker()
        {
           m_DB = new DBLayer();
        }

        private static DBWorker _Instance = new DBWorker();

        public static DBWorker Instance
        {
            get
            {
                return _Instance;
            }
        }

        public bool Insert(BaseEvent _event, int unitNumber)
        {
            return m_DB.Insert(_event, unitNumber);
        }

        public void CheckTables(Type[] types, int unitNumber)
        {
            m_DB.CheckTables(types, unitNumber);
        }
    }
}
