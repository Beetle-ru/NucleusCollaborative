using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace NordSteel.Data
{
    public class OracleDBLayer
    {
        public OracleConnection Connection { get; private set; }
        public Dictionary<Type, OracleDbType> OracleTypes { get; private set; }

        public bool Reconnect(string username, string password)
        {
            try
            {
                Connection.Close();
                Connection = new OracleConnection(string.Format("DATA SOURCE={0};PERSIST SECURITY INFO=True;USER ID={1};PASSWORD={2}", Connection.DataSource, username, password));
                Connection.Open();
            }
            catch { return false; }
            return true;
        }

        public OracleDBLayer(string datasource, string username, string password)
        {
            Initialize();
            Connection = new OracleConnection(string.Format("DATA SOURCE={0};PERSIST SECURITY INFO=True;USER ID={1};PASSWORD={2}", datasource, username, password));
            Connection.Open();
        }

        public OracleDBLayer()
        {
            Initialize();
            Connection = new OracleConnection(System.Configuration.ConfigurationManager.OpenExeConfiguration("").AppSettings.Settings["ConnectionString"].Value);
            Connection.Open();
        }

        private void Initialize()
        {
            OracleTypes = new Dictionary<Type, OracleDbType>
                               {
                                   {typeof (int), OracleDbType.Int32},
                                   {typeof (double), OracleDbType.Double},
                                   {typeof (bool), OracleDbType.Int32},
                                   {typeof (float), OracleDbType.Double},
                                   {typeof (string), OracleDbType.Varchar2}
                               };
        }

        public void Close()
        {
            Connection.Close();
        }

        protected string OracleDate(DateTime Date)
        {
            return string.Format("to_date('{0}','dd.mm.yyyy HH24:MI:SS')", Date.ToString());
        }

        protected string CheckNubmerForNull(string value)
        {
            return !string.IsNullOrEmpty(value) ? value : "0";
        }

        protected string CheckDateForNull(string value)
        {
            return !string.IsNullOrEmpty(value) ? value : DateTime.MinValue.ToString(); 
        }

        protected OracleDataReader Execute(string sql)
        {
            if (Connection.State != System.Data.ConnectionState.Open)
                Connection.Open();
            var dbcmd = Connection.CreateCommand();
            dbcmd.CommandText = sql;
            return dbcmd.ExecuteReader();
        }

        protected OracleDataReader Execute(string sql, OracleParameter[] parametres)
        {
            if (Connection.State != System.Data.ConnectionState.Open)
                Connection.Open();
            var dbcmd = Connection.CreateCommand();
            dbcmd.CommandText = sql;
            dbcmd.Parameters.AddRange(parametres);
            return dbcmd.ExecuteReader();
        }

        protected int ExecuteNonQuery(string sql)
        {
            if (Connection.State != System.Data.ConnectionState.Open)
                Connection.Open();
            var dbcmd = Connection.CreateCommand();
            dbcmd.CommandText = sql;
            return dbcmd.ExecuteNonQuery();
        }

        protected int ExecuteNonQuery(string sql, OracleParameter[] parametres)
        {
            if (Connection.State != System.Data.ConnectionState.Open)
                Connection.Open();
            var dbcmd = Connection.CreateCommand();
            dbcmd.CommandText = sql;
            dbcmd.Parameters.AddRange(parametres);
            return dbcmd.ExecuteNonQuery();
        }

        protected string ToOracleTypeString(Type type, int maxSize)
        {
            switch (type.Name)
            {
                case "Int16":
                case "Int64":
                case "Int32": return "INTEGER" + (maxSize > 0 ? string.Format("({0})", maxSize) : " ");
                case "String": return "VARCHAR2" + (maxSize > 0 ? string.Format("({0})", maxSize) : "(20)");
                case "Double": return "FLOAT" + (maxSize > 0 ? string.Format("({0})", maxSize) : " ");
                case "DateTime": return "DATE" + (maxSize > 0 ? string.Format("({0})", maxSize) : " ");
                case "Bool": return "INTEGER" + (maxSize > 0 ? string.Format("({0})", maxSize) : " ");
                default: return "VARCHAR2" + (maxSize > 0 ? string.Format("({0})", maxSize) : "(20)");
            }
        }
    }
}
