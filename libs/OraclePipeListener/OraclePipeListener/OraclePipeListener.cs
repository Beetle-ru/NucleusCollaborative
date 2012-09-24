using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace Tools.Db
{
    public delegate void OnChanged();
  
    public class OraclePipeListener
    {
        private OracleConnection m_Connection;
        private OracleCommand m_Command;
        private OracleDependency m_Dependency;

        private string m_DateBase;

        public string DateBase
        {
            get { return m_DateBase; }
        }

        private string m_TableName;

        public string TableName { get { return m_TableName; } }
        
        public OraclePipeListener(string connectionString, string dataBase, string tableName)
        {
            m_DateBase = dataBase;
            m_TableName = tableName;

            m_Connection = new OracleConnection(connectionString);
            m_Connection.Open();

            m_Command = m_Connection.CreateCommand();
            m_Command.CommandText = string.Format("SELECT * FROM {0}.{1} WHERE rownum = 1", DateBase,TableName);

            m_Dependency = new OracleDependency(m_Command);
            m_Dependency.OnChange += new OnChangeEventHandler(DependencyChange);
           
            m_Command.Notification.IsNotifiedOnce = false;
            m_Command.AddRowid = true;
            
            m_Command.ExecuteNonQuery();
            m_Connection.Close();
            m_Connection.Dispose();
        }

        public event OnChanged OnChanged; 

        void DependencyChange(object sender, OracleNotificationEventArgs eventArgs)
        {
                if (OnChanged != null) OnChanged();
        }

               
    }
}
