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
                //oraCmd.CommandText = "SELECT "
                //    + "ELEMENT.NAME AS N, ELEMENT.\"VALUE\" AS V "
                //    + "FROM ADDITION, ELEMENT "
                //    + "WHERE ADDITION.ID = ELEMENT.SID AND (ADDITION.NAME = :A)";
                //oraCmd.CommandText = "SELECT NAME FROM ADDITION, ELEMENT";
                //oraCmd.CommandText = "SELECT ADDITION.NAME, ELEMENT.NAME, ELEMENT.VALUE FROM ELEMENT, ADDITION";
                //oraCmd.CommandText = "SELECT ADDITION.NAME, ADDITION.ID FROM ADDITION WHERE ADDITION.NAME = 'DOLMIT'";
                //oraCmd.CommandText = "SELECT ELEMENT.NAME, ELEMENT.VALUE FROM ADDITION, ELEMENT WHERE ADDITION.ID = ELEMENT.SID AND ADDITION.NAME = 'DOLMIT'";
                oraCmd.CommandText = "SELECT "
                                     + "PAR.TECHN_NORDOC "
                                     + ",PAR.REFMARGOS_MARK "
                                     + ",PAR.REFMARGOST_FULLMARK "
                                     + ",PAR.PLANOPER_DATE "
                                     + ",PAR.PLANOPER_UNRS "
                                     + "FROM (SELECT TCH.TECHN_NORDOC "
                                     + ",RMG.REFMARGOS_MARK "
                                     + ",RMG.REFMARGOST_FULLMARK "
                                     + ",T.PLANOPER_DATE "
                                     + ",T.PLANOPER_UNRS "
                                     + ",V.PLSVPLAW_PRM "
                                     + "FROM CPU01_PLANOPER T "
                                     + "LEFT OUTER JOIN CPU01_REFOPER RO "
                                     + "ON RO.REFOPER_PRM = T.REFOPER_PRM "
                                     + "LEFT OUTER JOIN CPU01_PLANRAZL PR "
                                     + "ON PR.PLANOPER_PRM = T.PLANOPER_PRM "
                                     + "LEFT OUTER JOIN CPU01_TECHNA TCH "
                                     + "ON TCH.TECHN_PRM = PR.TECHN_PRM "
                                     + "LEFT OUTER JOIN CPU01_REFMARGOS RMG "
                                     + "ON RMG.REFMARGOS_PRM = TCH.REFMARGOS_PRM "
                                     + "LEFT OUTER JOIN CPU01_PLSVPLAW V "
                                     + "ON V.PLSVPLAW_PRM = PR.PLSVPLAW_PRM "
                                     + "LEFT OUTER JOIN CPU01_SVPLAW S "
                                     + "ON S.SVPLAW_PRM = V.SVPLAW_PRM "
                                     + ") PAR";
                                     //+ "WHERE T.REFCEH_PRM = 13 AND "
                                     //+ "T.PLANOPER_DATE >= :PAR_DTB  AND T.PLANOPER_DATE <= :PAR_DTE "
                                     //+ "AND T.PLANOPER_UNRS =:UNRS_NOM "
                                     //+ ") PAR";
                oraCmd.CommandType = System.Data.CommandType.Text;
                //oraCmd.Parameters.Add(new OracleParameter("A", OracleDbType.Varchar2, ParameterDirection.Input));
                //oraCmd.Parameters["A"].Value = "DOLMIT";
                oraCmd.Connection.Open();
                var oraReader = oraCmd.ExecuteReader();
                if (oraReader.HasRows) {
                    while (oraReader.Read()) {
                        for (int i = 0; i < oraReader.FieldCount; i++) {
                            Console.Write(oraReader[i] + ";  ");
                        }
                        Console.WriteLine();
                        //Console.WriteLine(oraReader[0]);
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
