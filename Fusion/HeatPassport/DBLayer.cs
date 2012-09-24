using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
//using Converter;

namespace HeatPassport
{
    class DBLayer
    {
        OracleConnection m_Connection;


        public DBLayer()
        {
            m_Connection = new OracleConnection(System.Configuration.ConfigurationManager.OpenExeConfiguration("").AppSettings.Settings["ConnectionString"].Value);
            m_Connection.Open();
        }
        public void Close()
        {
            m_Connection.Close();
        }
        private string OracleDate(DateTime Date)
        {
            return string.Format("to_date('{0}','dd.mm.yyyy HH24:MI:SS')", Date.ToString());
        }

        private string CheckNubmerForNull(string value)
        {
            return !string.IsNullOrEmpty(value) ? value : "0";
        }

        private string CheckDateForNull(string value)
        {
            return !string.IsNullOrEmpty(value) ? value : "01.01.01";
        }

        private OracleDataReader Execute(string sql)
        {
            if (m_Connection.State != System.Data.ConnectionState.Open)
                m_Connection.Open();
            OracleCommand dbcmd = m_Connection.CreateCommand();
            dbcmd.CommandText = sql;
            return dbcmd.ExecuteReader();
        }

        private int ExecuteNonQuery(string sql)
        {
            if (m_Connection.State != System.Data.ConnectionState.Open)
                m_Connection.Open();
            OracleCommand dbcmd = m_Connection.CreateCommand();
            dbcmd.CommandText = sql;
            return dbcmd.ExecuteNonQuery();
        }

        private int ExecuteNonQuery(string sql, List<OracleParameter> parametres)
        {
            if (m_Connection.State != System.Data.ConnectionState.Open)
                m_Connection.Open();
            OracleCommand dbcmd = m_Connection.CreateCommand();
            dbcmd.CommandText = sql;
            foreach (OracleParameter par in parametres)
            {
                dbcmd.Parameters.Add(par);
            }
            return dbcmd.ExecuteNonQuery();
        }

        private string FormSQLString(params object[] Values)
        {
            string ret = "INSERT INTO BOF_TELEGRAMS (OPERATION_ID,EVENTTIME,CV_NO,HEAT_NO,STATUS";
            string retval = "";
            for (int i = 4; i < Values.Length; i++)
            {
                ret += ",PAR" + (i - 3).ToString();
                retval += ",:PAR" + (i - 3).ToString();
            }

            ret += ") VALUES (:OPERATION_ID,:EVENTTIME,:CV_NO,:HEAT_NO,:STATUS";
            ret += retval;
            ret += ")";
            return ret;
        }

        public void InsertOperation(params object[] Values)
        {
            string sql = FormSQLString(Values);
            List<OracleParameter> oracleParams = new List<OracleParameter>();
            oracleParams.Add(new OracleParameter() { ParameterName = "OPERATION_ID", OracleDbType = OracleDbType.Int32, Value = Values[0] });
            oracleParams.Add(new OracleParameter() { ParameterName = "EVENTTIME", OracleDbType = OracleDbType.Date, Value = Values[1] });
            oracleParams.Add(new OracleParameter() { ParameterName = "CV_NO", OracleDbType = OracleDbType.Int32, Value = Values[2] });
            oracleParams.Add(new OracleParameter() { ParameterName = "HEAT_NO", OracleDbType = OracleDbType.Varchar2, Value = Values[3], Size = 7 });
            oracleParams.Add(new OracleParameter() { ParameterName = "STATUS", OracleDbType = OracleDbType.Int32, Value = 0 });
            for (int i = 4; i < Values.Length; i++)
            {
                oracleParams.Add(new OracleParameter() { ParameterName = "PAR" + (i - 3).ToString(), OracleDbType = OracleDbType.Varchar2, Value = Values[i], Size = 20 });
            }

            ExecuteNonQuery(sql, oracleParams);
        }

        public int GetCurrentHeatNumber(int ConverterNumber)
        {
            //string sql = "SELECT HEAT_NO FROM HEATS WHERE CV_NO=" + ConverterNumber.ToString() + " AND HEAT_ID=(select max(heat_id) from heats)";
            //OracleDataReader reader = Execute(sql);
            //while (reader.Read())
            //{
            //    return int.Parse(CheckNubmerForNull(reader[0].ToString()));
            //}
            return 0;
        }
    }
}
