using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using ConnectionProvider;
using Converter;
using Oracle.DataAccess.Client;

namespace DBFlex
{
    internal delegate void CompleteCallback(FlexEvent flx, Requester.Result result);
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
                        oraCmd.Dispose();
                    }
                }
            }

            catch (Exception e) {
                res.ErrorCode = Result.Es.S_ERROR;
                res.ErrorStr += String.Format("{0}\n", e.Message);
                return res;
            }
            return res;
        }

        private void ThreadPoolCallback(Object data) {
            var tpData = (ThreadPoolData) data;

            var sqlRes = SQLRequest(tpData.SQLString);

            tpData.ResponceGenerator(tpData.Flx, sqlRes);
        }

        public void SQLRequestAsync(string SQLString, FlexEvent flx, CompleteCallback responceGenerator)
        {
            var tpData = new ThreadPoolData(SQLString, flx, responceGenerator);
            ThreadPool.QueueUserWorkItem(ThreadPoolCallback, tpData);
        }

        public class ThreadPoolData
        {
            public ThreadPoolData(string sqlStr, FlexEvent flx, CompleteCallback responceGenerator)
            {
                SQLString = sqlStr;
                Flx = flx;
                ResponceGenerator = responceGenerator;
            }
            public string SQLString;
            public FlexEvent Flx;
            public CompleteCallback ResponceGenerator;
        }

        public class Result
        {
            public Result()
            {
                ErrorStr = "";
                ResultData = new Dictionary<string, List<object>>();
                ErrorCode = Es.S_OK;
            }
            public enum Es
            {
                S_OK,
                S_WARN,
                S_ERROR
            }
            public string ErrorStr;
            public Dictionary<string, List<object>> ResultData; 
            public Es ErrorCode;
        }

    }
}
