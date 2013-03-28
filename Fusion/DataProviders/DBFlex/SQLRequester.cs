using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ConnectionProvider;
using Oracle.DataAccess.Client;

namespace DBFlex
{
    class Requester {
        private readonly string m_connectionString;
        public Requester(string connectionString) {
            m_connectionString = connectionString;
        }

        public Result SQLRequest(string SQLString) {
            var res = new Result();
            try {
                using (var oraConn = new OracleConnection(m_connectionString)) {

                    var oraCmd = oraConn.CreateCommand();
                    oraCmd.CommandText = SQLString;
                    oraCmd.CommandType = System.Data.CommandType.Text;

                    if (oraCmd.Connection.State != System.Data.ConnectionState.Closed) oraCmd.Connection.Close();
                    try {

                        oraCmd.Connection.Open();
                        var oraReader = oraCmd.ExecuteReader();
                        if (oraReader.HasRows) {
                            while (oraReader.Read()) {
                                for (int i = 0; i < oraReader.FieldCount; i++) {
                                    var k = oraReader.GetName(i);
                                    var v = oraReader[i];
                                    if (!res.ResultData.ContainsKey(k)) {
                                        res.ResultData.Add(k,new List<object>());
                                    }
                                    res.ResultData[k].Add(v);
                                }
                            }
                        }
                    }

                    finally {
                        oraCmd.Connection.Close();
                    }
                }
            }

            catch (Exception e) {
                res.ErrorCode = Result.Es.S_Error;
                res.ErrorStr += String.Format("{0}\n", e.Message);
                return res;
            }
            return res;
        }
        public void FNC()
        {
            using (var oraConn = new OracleConnection("DATA SOURCE=KPBOF;PASSWORD=bof7jfa4;USER ID=BOF")) {
                var oraCmd = oraConn.CreateCommand();
                //oraCmd.CommandText = "SELECT "
                //    + "ELEMENT.NAME AS N, ELEMENT.\"VALUE\" AS V "
                //    + "FROM ADDITION, ELEMENT "
                //    + "WHERE ADDITION.ID = ELEMENT.SID AND (ADDITION.NAME = :A)";
                //oraCmd.CommandText = "SELECT NAME FROM ADDITION, ELEMENT";
                //oraCmd.CommandText = "SELECT ADDITION.NAME, ELEMENT.NAME, ELEMENT.VALUE FROM ELEMENT, ADDITION";
                //oraCmd.CommandText = "SELECT ADDITION.NAME, ADDITION.ID FROM ADDITION WHERE ADDITION.NAME = 'DOLMIT'";
                oraCmd.CommandText = "SELECT ELEMENT.NAME, ELEMENT.VALUE FROM ADDITION, ELEMENT WHERE ADDITION.ID = ELEMENT.SID AND ADDITION.NAME = 'DOLMIT'";

                oraCmd.CommandType = System.Data.CommandType.Text;
                //oraCmd.Parameters.Add(new OracleParameter("A", OracleDbType.Varchar2, ParameterDirection.Input));
                //oraCmd.Parameters["A"].Value = "DOLMIT";
                oraCmd.Connection.Open();
                var oraReader = oraCmd.ExecuteReader();
                if (oraReader.HasRows)
                {
                    while (oraReader.Read())
                    {
                        for (int i = 0; i < oraReader.FieldCount; i++)
                        {
                            Console.Write(oraReader[i] + ";  ");
                        }
                        Console.WriteLine();
                        //Console.WriteLine(oraReader[0]);
                    }
                }
                else
                {
                    Console.WriteLine("!HasRows");
                }
                oraCmd.Connection.Close();
            }
            Console.ReadLine();
        }

        public class Result
        {
            public Result()
            {
                ErrorStr = "";
                ResultData = new Dictionary<string, List<object>>();
                ErrorCode = Es.S_Ok;
            }
            public enum Es
            {
                S_Ok,
                S_Warn,
                S_Error
            }
            public string ErrorStr;
            public Dictionary<string, List<object>> ResultData; 
            public Es ErrorCode;
        }

    }
}
