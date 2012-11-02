using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using Implements;
using CommonTypes;
using Converter;


namespace PipeCatcher
{
    public class DBReader
    {
        private OracleConnection OraConn; 
        private OracleCommand OraCmd; 
        private OracleDataReader OraReader;
        public String ConnStr, PipeName, ProcName, HeatNo;
        public Decimal RecId;
        public DBReader(String ConnStr_, String PipeName_)
        {
            using (Logger l = new Logger("PipeCatcher"))
            {
                ConnStr = ConnStr_;
                PipeName = PipeName_;
                OraConn = new OracleConnection(ConnStr);
                OraCmd = OraConn.CreateCommand();
                l.msg("A new DBReader created ConnStr={0} PipeName={1}", ConnStr, PipeName);
            }
        }
        public bool HaveNews()
        {
            OraCmd.CommandText = 
                  "BEGIN "
                + "  :RESULT := DBMS_PIPE.RECEIVE_MESSAGE('" + PipeName + "',0); "
                + "  IF :RESULT = 0 THEN "
                + "    DBMS_PIPE.UNPACK_MESSAGE(:SHEATNO); "
                + "    DBMS_PIPE.UNPACK_MESSAGE(:NRECID); "
                + "    DBMS_PIPE.UNPACK_MESSAGE(:SNAME_PROC); "
                + "  END IF; "
                + "END; ";
            OraCmd.CommandType = System.Data.CommandType.Text;
            OraCmd.Parameters.Clear();
            OraCmd.Parameters.Add(new OracleParameter("RESULT", OracleDbType.Int32, System.Data.ParameterDirection.Output));
            OraCmd.Parameters.Add(new OracleParameter("SHEATNO", OracleDbType.Varchar2, System.Data.ParameterDirection.Output));
            OraCmd.Parameters.Add(new OracleParameter("NRECID", OracleDbType.Decimal, System.Data.ParameterDirection.Output));
            OraCmd.Parameters.Add(new OracleParameter("SNAME_PROC", OracleDbType.Varchar2, System.Data.ParameterDirection.Output));
            OraCmd.Parameters["SHEATNO"].Size = 150;
            OraCmd.Parameters["SNAME_PROC"].Size = 30;
            if (OraCmd.Connection.State != System.Data.ConnectionState.Closed)
            {
                OraCmd.Connection.Close();
            }
            OraCmd.Connection.Open();
            OraCmd.ExecuteReader();
            return Convert.ToString(OraCmd.Parameters["RESULT"].Value) == "0";
        }
        public String GetNews()
        {
            using (Logger l = new Logger("PipeCatcher"))
            {
                HeatNo = Convert.ToString(OraCmd.Parameters["SHEATNO"].Value);
                ProcName = Convert.ToString(OraCmd.Parameters["SNAME_PROC"].Value);
                var str = Convert.ToString(OraCmd.Parameters["NRECID"].Value);
                RecId = Convert.ToDecimal(str);
                str = String.Format("\nSHEATNO = {0} SNAME_PROC = {1} NRECID = {2}", HeatNo, ProcName, RecId);
                l.msg(str);
                return str;
            }
        }
        public String ProcessNews()
        {
            using (Logger l = new Logger("PipeCatcher"))
            {
                OraCmd.CommandText = ProcName;
                OraCmd.CommandType = System.Data.CommandType.StoredProcedure;
                OraCmd.Parameters.Clear();
                //OraCmd.Parameters.Add(new OracleParameter("NRECID", OracleDbType.Decimal, System.Data.ParameterDirection.Input)).Value = RecId;
                OraCmd.Parameters.Add(new OracleParameter("REFCURSOR", OracleDbType.RefCursor, System.Data.ParameterDirection.Output));
                if (OraCmd.Connection.State != System.Data.ConnectionState.Closed)
                {
                    OraCmd.Connection.Close();
                }
                OraCmd.Connection.Open();
                OraReader = OraCmd.ExecuteReader();
                var str = String.Format("\nCALL {0}", ProcName);
//                var mainevt = new FlexEvent("PipeCatcher.Call." + ProcName);
//                mainevt.Arguments.Add("@ProcName", ProcName);
                if (OraReader.HasRows)
                {
                    var iRow = 0;
                    FlexEvent evt = new FlexEvent("PipeCatcher.Call." + ProcName + ".Row." + (++iRow));
                    str += "+++";
                    while (OraReader.Read())
                    {
                        for (int i = 0; i < OraReader.FieldCount; i++)
                        {
                            evt.Arguments.Add(OraReader.GetName(i), OraReader[i]);
                            str += "\n" + OraReader.GetName(i) + "\t: " + OraReader[i];
                        }
                        str += "\n********************";
//                        mainevt.Arguments.Add(String.Format("Row{0}", ++iRow), evt);
                        Program.CoreGate.PushEvent(evt);
                        evt.Arguments.Clear();
                    }
//                    Program.CoreGate.PushEvent(mainevt);
                }
                else
                {
                    str += "---";
                }
                OraReader.Close();
                l.msg(str);
                return str;
            }
        }
    }
}
