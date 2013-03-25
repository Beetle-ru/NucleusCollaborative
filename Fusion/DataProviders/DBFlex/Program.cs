using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace DBFlex
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var oraConn = new OracleConnection("DATA SOURCE=KPBOF;PASSWORD=bof7jfa4;USER ID=BOF"))
            {
                var oraCmd = oraConn.CreateCommand();
                oraCmd.CommandText = "SELECT " 
                    + "ELEMENT.NAME AS N, ELEMENT.\"VALUE\" AS V "
                    + "FROM ADDITION, ELEMENT "
                    + "WHERE ADDITION.ID = ELEMENT.SID AND (ADDITION.NAME = :A)";
                oraCmd.CommandType = System.Data.CommandType.Text;
                oraCmd.Parameters.Add(new OracleParameter("A",OracleDbType.Varchar2, ParameterDirection.Input));
                oraCmd.Parameters["A"].Value = "DOLMIT";
                oraCmd.Connection.Open();
                var oraReader = oraCmd.ExecuteReader();
                if (oraReader.HasRows) {
                    while (oraReader.Read()) {
                        foreach (var variable in oraReader) {
                            Console.WriteLine(variable);
                        }
                    }
                }
                else {
                    Console.WriteLine("!HasRows");
                }
                oraCmd.Connection.Close();
            }
            Console.ReadLine();
        }
        
    }
}
