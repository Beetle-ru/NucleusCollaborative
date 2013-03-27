using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace DBFlex
{
    class Requester
    {
        static void FNC(string[] args)
        {
            using (var oraConn = new OracleConnection("DATA SOURCE=KPBOF;PASSWORD=bof7jfa4;USER ID=BOF"))
            {
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

    }
}
