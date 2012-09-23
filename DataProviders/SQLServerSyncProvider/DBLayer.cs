using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.Text;
using Esms;

namespace SQLServerSyncProvider
{
    internal class DBLayer
    {
        private int UnitNumber { get; set; }

        public DBLayer(int unitNumber)
        {
            UnitNumber = unitNumber;
        }

        private static DataSet Execute(string connectionString, string  sqlText)
        {
            var dSet = new DataSet();            
            var connection = new OleDbConnection(connectionString);
            var command = new OleDbCommand(sqlText, connection);
            try
            {
                connection.Open();
                var dadapter = new OleDbDataAdapter {SelectCommand = command};
                dadapter.Fill(dSet);
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                command.Dispose();
            }
            return dSet;
        }

        public DataSet ScrapLoad()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ScrapConnection"].ToString();
            var tableName = ConfigurationManager.AppSettings["ZavalksTableName"];
            var sqlText = string.Format(
                "SELECT TOP 1 Zavalk_Nr, Tank_Nr, Task_Nr, Charge_Nr, Time_Start, Time_End,  Ves_Nr " +
                "FROM {0} ORDER BY Time_Start DESC, Time_End DESC", tableName);
            return Execute(connectionString, sqlText);
        }

        public DataSet ScrapWeight(DateTime start, DateTime end, int ves)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ScrapConnection"].ToString();
            var tableName = ConfigurationManager.AppSettings["VesweightTableName"];
            var sqlText = string.Format(
                "SELECT Date_t, Ves_Nr, Skrap_Code, Weight " +
                "FROM {0} WHERE Ves_Nr = {1} AND Date_t BETWEEN CONVERT(DATETIME, '{2}',104) AND CONVERT(DATETIME, '{3}', 104) " +
                "ORDER BY Date_t DESC", tableName, ves, start, end);
            return Execute(connectionString, sqlText);
        }

        public ScrapLoadEvent ScrapName(ScrapLoadEvent scrapLoad, int code)
        {
             var connectionString = ConfigurationManager.ConnectionStrings["ScrapConnection"].ToString();
             var sqlText = string.Format("SELECT TOP 1 Name FROM SkrapCode WHERE Code = {0}", code);
             var dset = Execute(connectionString, sqlText);
             if (dset.Tables.Count != 0 && dset.Tables[0].Rows.Count != 0)
             {
                 scrapLoad.ScrapName = dset.Tables[0].Rows[0]["Name"].ToString();
             }
            return scrapLoad;
        }

        //фэйк для скарапа
        public DataSet ScrapLoad2()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ScrapConnection"].ToString();
            var tableName = ConfigurationManager.AppSettings["ZavalksTableName"];
            var sqlText = string.Format(
                "SELECT Zavalk_Nr, Tank_Nr, Task_Nr, Charge_Nr, Time_Start, Time_End,  Ves_Nr " +
                "FROM {0} " +
                "WHERE Time_Start >= CONVERT(DATETIME, '01.05.2012 00:00:00', 104) " +
                "ORDER BY Time_Start DESC, Time_End DESC", tableName);
            return Execute(connectionString, sqlText);
        }
         

    }
}
