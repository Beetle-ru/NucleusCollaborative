using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Timers;
using ConnectionProvider;
using Esms;

namespace SQLServerSyncProvider
{
    public class SqlProvider
    {
        private static ScrapLoadEvent _previousLoadEvent;
        private static ChemanalFusionEvent _previousChemanalEvent;
        private static Client _mainGate;
        private static readonly Timer CheckTimer = new Timer(1000);

        public static void Main()
        {
            _previousLoadEvent = new ScrapLoadEvent() { Id = 91854, LoadDateTime = Convert.ToDateTime("2012-05-07 23:13:19.000") };
            using (CheckTimer)
            {
                _mainGate = new Client();
                _mainGate.Subscribe();
                CheckTimer.Elapsed += CheckTimerElapsed;
                CheckTimer.Start();
                Console.WriteLine("Sync started. Press any key to stop.");
                Console.ReadKey();
                CheckTimer.Stop();
            }
        }

        private static void CheckTimerElapsed(object sender, ElapsedEventArgs e)
        {
            CheckTimer.Stop();
            var unitNumber = Convert.ToInt32(ConfigurationManager.AppSettings["UnitNumber"]);
            CheckScrapLoadForUpdates(unitNumber);
            CheckChemanalForUpdates(unitNumber);
            CheckTimer.Start();
            
         }

        private static void CheckScrapLoadForUpdates(int unitNumber)
        {
            var connection =
                new OleDbConnection(ConfigurationManager.ConnectionStrings["ScrapConnection"].ToString());
            var eafConnection = new OleDbConnection(ConfigurationManager.ConnectionStrings["EafConnection" + unitNumber].ToString());
            var dset = new DataSet();
            var zavalksTable = ConfigurationManager.AppSettings["ZavalksTableName"];
            var vesWeightTable = ConfigurationManager.AppSettings["VesweightTableName"];
            var command = new OleDbCommand(string.Format("SELECT TOP 1 {0}.Zavalk_Nr," +
                                                         " {0}.Tank_Nr, {0}.Task_Nr, {0}.Charge_Nr, " + "{0}.Time_Start, " + "{0}.Time_End, " +
                                                         " SkrapCode.Name, {1}.Date_t, {1}.Weight" + 
                                                         " FROM {0} INNER JOIN {1} ON " +
                                                         "({1}.Date_t BETWEEN {0}.Time_Start" + " AND {0}.Time_End) and ({0}.Ves_Nr = {1}.Ves_Nr)" + 
                                                         " INNER JOIN SkrapCode" + " ON {1}.Skrap_Code = SkrapCode.Code" +
                                                         " ORDER BY {1}.Date_t" + " DESC", zavalksTable, vesWeightTable),
                                           connection);
            try
            {
                connection.Open();

                var dadapter = new OleDbDataAdapter {SelectCommand = command};

                dadapter.Fill(dset);
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                
                connection.Close();
                connection.Dispose();
                command.Dispose();
            }

            if (dset.Tables.Count > 0)
            {
                var newEvent = new ScrapLoadEvent();
                newEvent.Id = (int) dset.Tables[0].Rows[0]["Zavalk_Nr"];
                newEvent.LoadDateTime = (DateTime) dset.Tables[0].Rows[0]["Date_t"];
                newEvent.ScrapName = dset.Tables[0].Rows[0]["Name"].ToString();
                newEvent.ChargeNumber = ((dset.Tables[0].Rows[0]["Charge_Nr"] is int))
                                            ? (int) dset.Tables[0].Rows[0]["Charge_Nr"]
                                            : 0;
                newEvent.TankNumber = (Int16) dset.Tables[0].Rows[0]["Tank_Nr"];
                newEvent.TaskNumber = String.IsNullOrEmpty(dset.Tables[0].Rows[0]["Task_Nr"].ToString())
                                          ? 0
                                          : (Int16) dset.Tables[0].Rows[0]["Task_Nr"];
                newEvent.Weight = (int) dset.Tables[0].Rows[0]["Weight"]/100;

                if (_previousLoadEvent == null ||
                    (newEvent.Id != _previousLoadEvent.Id && newEvent.LoadDateTime != _previousLoadEvent.LoadDateTime))
                {
                    // проверяем вхождение в базе EAF
                    command = new OleDbCommand("SELECT HeatNo, StrtTm_Time, TapTm_Time FROM EAF2_pasp" +
                                           " WHERE (StrtTm_Time >= '" + dset.Tables[0].Rows[0]["Time_Start"] + "')" +
                                           " AND (TapTm_Time <= '" + dset.Tables[0].Rows[0]["Time_End"] + "')" +
                                               " AND (STR(HeatNo, 10) LIKE '%' + '" + dset.Tables[0].Rows[0]["Charge_Nr"] + "' )", eafConnection);
                    try
                    {
                        dset = new DataSet();
                        eafConnection.Open();

                        var dadapter = new OleDbDataAdapter { SelectCommand = command };

                        dadapter.Fill(dset);
                    }
                    catch (Exception e)
                    {
                        throw;
                    }
                    finally
                    {

                        eafConnection.Close();
                        eafConnection.Dispose();
                        command.Dispose();
                    }
                    if (dset.Tables[0].Rows.Count > 0)
                    {
                        Console.WriteLine("Event pushed");
                        _mainGate.PushEvent(newEvent);
                    }
                    _previousLoadEvent = newEvent;
                }
            }
        }

        private static void CheckChemanalForUpdates(int unitNumber)
        {
            var connection =
                new OleDbConnection(ConfigurationManager.ConnectionStrings["ChemanalConnection"].ToString());
            string table = ConfigurationManager.AppSettings["chemanalTableName"];
            var dset = new DataSet();
            int placeNumber = unitNumber == 1 ? 4 : 2;
            var command =
                new OleDbCommand(
                    string.Format(
                        "SELECT TOP 1 {0}.id," + " {0}.chemanal_plav," + " places.name," + " {0}.chemanal_date," +
                        " {0}.chemanal_c," + " {0}.chemanal_si," + " {0}.chemanal_mn," + " {0}.chemanal_p," +
                        " {0}.chemanal_s," + " {0}.chemanal_cao," + " {0}.chemanal_sio2," + " {0}.chemanal_feo," +
                        " {0}.chemanal_mgo, {0}.chemanal_mno, {0}.chemanal_al2o3," +
                        " {0}.chemanal_cr, {0}.chemanal_ni, {0}.chemanal_cu," +
                        " {0}.chemanal_al, {0}.chemanal_n, {0}.chemanal_v," +
                        " {0}.chemanal_ti, {0}.chemanal_as, {0}.chemanal_nb," +
                        " {0}.chemanal_zr, {0}.chemanal_mo, {0}.chemanal_b," +
                        " {0}.chemanal_sn, {0}.chemanal_alkr, {0}.chemanal_w," +
                        " {0}.chemanal_ca, {0}.chemanal_co, {0}.chemanal_H2," +
                        " {0}.chemanal_Pb, {0}.chemanal_Sb, {0}.chemanal_Fi," +
                        " {0}.chemanal_Zn, {0}.chemanal_P2O5, {0}.chemanal_OSN," + " {0}.Carbon_Eqv " +
                        " FROM {0} INNER JOIN places ON {0}.chemanal_place = places.number " + "AND places.number = " + placeNumber +
                        " ORDER BY {0}.chemanal_date DESC", table), connection);
            try
            {
                connection.Open();

                var dadapter = new OleDbDataAdapter {SelectCommand = command};

                dadapter.Fill(dset);
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                command.Dispose();
            }

            if (dset.Tables.Count > 0)
            {
                var newEvent = new ChemanalFusionEvent();

                newEvent.Id = (int) dset.Tables[0].Rows[0]["Id"];
                newEvent.ChemanalDateTime = (dset.Tables[0].Rows[0]["chemanal_date"] is DateTime)
                                                ? DateTime.MinValue
                                                : (DateTime) dset.Tables[0].Rows[0]["chemanal_date"];
                newEvent.FusionNumber = dset.Tables[0].Rows[0]["chemanal_plav"].ToString();
                newEvent.PlaceName = dset.Tables[0].Rows[0]["name"].ToString();
                newEvent.ChemC = (dset.Tables[0].Rows[0]["chemanal_c"] is Decimal)
                                 ? (decimal) dset.Tables[0].Rows[0]["chemanal_c"]
                                 : 0;
                newEvent.ChemSi = ((dset.Tables[0].Rows[0]["chemanal_si"] is Decimal))
                                  ? (decimal) dset.Tables[0].Rows[0]["chemanal_si"]
                                  : 0;
                newEvent.ChemMn = ((dset.Tables[0].Rows[0]["chemanal_mn"] is Decimal))
                                  ? (decimal) dset.Tables[0].Rows[0]["chemanal_mn"]
                                  : 0;
                newEvent.ChemP = ((dset.Tables[0].Rows[0]["chemanal_p"] is Decimal))
                                 ? (decimal) dset.Tables[0].Rows[0]["chemanal_p"]
                                 : 0;
                newEvent.ChemS = ((dset.Tables[0].Rows[0]["chemanal_S"] is Decimal))
                                 ? (decimal) dset.Tables[0].Rows[0]["chemanal_S"]
                                 : 0;
                newEvent.ChemCao = ((dset.Tables[0].Rows[0]["chemanal_cao"] is Decimal))
                                   ? (decimal) dset.Tables[0].Rows[0]["chemanal_cao"]
                                   : 0;
                newEvent.ChemSio2 = ((dset.Tables[0].Rows[0]["chemanal_sio2"] is Decimal))
                                    ? (decimal) dset.Tables[0].Rows[0]["chemanal_sio2"]
                                    : 0;
                newEvent.ChemFeo = ((dset.Tables[0].Rows[0]["chemanal_feo"] is Decimal))
                                   ? (decimal) dset.Tables[0].Rows[0]["chemanal_feo"]
                                   : 0;
                newEvent.ChemMgo = ((dset.Tables[0].Rows[0]["chemanal_mgo"] is Decimal))
                                   ? (decimal) dset.Tables[0].Rows[0]["chemanal_mgo"]
                                   : 0;
                newEvent.ChemMno = ((dset.Tables[0].Rows[0]["chemanal_mno"] is Decimal))
                                   ? (decimal) dset.Tables[0].Rows[0]["chemanal_mno"]
                                   : 0;
                newEvent.ChemAl2O3 = ((dset.Tables[0].Rows[0]["chemanal_al2o3"] is Decimal))
                                     ? (decimal) dset.Tables[0].Rows[0]["chemanal_al2o3"]
                                     : 0;
                newEvent.ChemCr = ((dset.Tables[0].Rows[0]["chemanal_cr"] is Decimal))
                                  ? (decimal) dset.Tables[0].Rows[0]["chemanal_cr"]
                                  : 0;
                newEvent.ChemNi = ((dset.Tables[0].Rows[0]["chemanal_ni"] is Decimal))
                                  ? (decimal) dset.Tables[0].Rows[0]["chemanal_ni"]
                                  : 0;
                newEvent.ChemCu = ((dset.Tables[0].Rows[0]["chemanal_cu"] is Decimal))
                                  ? (decimal) dset.Tables[0].Rows[0]["chemanal_cu"]
                                  : 0;
                newEvent.ChemAl = ((dset.Tables[0].Rows[0]["chemanal_al"] is Decimal))
                                  ? (decimal) dset.Tables[0].Rows[0]["chemanal_al"]
                                  : 0;
                newEvent.ChemN = ((dset.Tables[0].Rows[0]["chemanal_n"] is Decimal))
                                 ? (decimal) dset.Tables[0].Rows[0]["chemanal_n"]
                                 : 0;
                newEvent.ChemV = ((dset.Tables[0].Rows[0]["chemanal_v"] is Decimal))
                                 ? (decimal) dset.Tables[0].Rows[0]["chemanal_v"]
                                 : 0;
                newEvent.ChemTi = ((dset.Tables[0].Rows[0]["chemanal_ti"] is Decimal))
                                  ? (decimal) dset.Tables[0].Rows[0]["chemanal_ti"]
                                  : 0;
                newEvent.ChemAs = ((dset.Tables[0].Rows[0]["chemanal_as"] is Decimal))
                                  ? (decimal) dset.Tables[0].Rows[0]["chemanal_as"]
                                  : 0;
                newEvent.ChemNb = ((dset.Tables[0].Rows[0]["chemanal_nb"] is Decimal))
                                  ? (decimal) dset.Tables[0].Rows[0]["chemanal_nb"]
                                  : 0;
                newEvent.ChemZr = ((dset.Tables[0].Rows[0]["chemanal_zr"] is Decimal))
                                  ? (decimal) dset.Tables[0].Rows[0]["chemanal_zr"]
                                  : 0;
                newEvent.ChemMo = ((dset.Tables[0].Rows[0]["chemanal_mo"] is Decimal))
                                  ? (decimal) dset.Tables[0].Rows[0]["chemanal_mo"]
                                  : 0;
                newEvent.ChemB = ((dset.Tables[0].Rows[0]["chemanal_b"] is Decimal))
                                 ? (decimal) dset.Tables[0].Rows[0]["chemanal_b"]
                                 : 0;
                newEvent.ChemSn = ((dset.Tables[0].Rows[0]["chemanal_sn"] is Decimal))
                                  ? (decimal) dset.Tables[0].Rows[0]["chemanal_sn"]
                                  : 0;
                newEvent.ChemAlkr = ((dset.Tables[0].Rows[0]["chemanal_alkr"] is Decimal))
                                    ? (decimal) dset.Tables[0].Rows[0]["chemanal_alkr"]
                                    : 0;
                newEvent.ChemW = ((dset.Tables[0].Rows[0]["chemanal_w"] is Decimal))
                                 ? (decimal) dset.Tables[0].Rows[0]["chemanal_w"]
                                 : 0;
                newEvent.ChemCa = ((dset.Tables[0].Rows[0]["chemanal_ca"] is Decimal))
                                  ? (decimal) dset.Tables[0].Rows[0]["chemanal_ca"]
                                  : 0;
                newEvent.ChemCo = ((dset.Tables[0].Rows[0]["chemanal_co"] is Decimal))
                                  ? (decimal) dset.Tables[0].Rows[0]["chemanal_co"]
                                  : 0;
                newEvent.ChemH2 = ((dset.Tables[0].Rows[0]["chemanal_H2"] is Decimal))
                                  ? (decimal) dset.Tables[0].Rows[0]["chemanal_H2"]
                                  : 0;
                newEvent.ChemPb = ((dset.Tables[0].Rows[0]["chemanal_Pb"] is Decimal))
                                  ? (decimal) dset.Tables[0].Rows[0]["chemanal_Pb"]
                                  : 0;
                newEvent.ChemSb = ((dset.Tables[0].Rows[0]["chemanal_Sb"] is Decimal))
                                  ? (decimal) dset.Tables[0].Rows[0]["chemanal_Sb"]
                                  : 0;
                newEvent.ChemFi = ((dset.Tables[0].Rows[0]["chemanal_Fi"] is Decimal))
                                  ? (decimal) dset.Tables[0].Rows[0]["chemanal_Fi"]
                                  : 0;
                newEvent.ChemZn = ((dset.Tables[0].Rows[0]["chemanal_Zn"] is Decimal))
                                  ? (decimal) dset.Tables[0].Rows[0]["chemanal_Zn"]
                                  : 0;
                newEvent.ChemP2O5 = ((dset.Tables[0].Rows[0]["chemanal_P2O5"] is Decimal))
                                    ? (decimal) dset.Tables[0].Rows[0]["chemanal_P2O5"]
                                    : 0;
                newEvent.ChemOsn = ((dset.Tables[0].Rows[0]["chemanal_OSN"] is Decimal))
                                   ? (decimal) dset.Tables[0].Rows[0]["chemanal_OSN"]
                                   : 0;
                newEvent.ChemCarbonEqv = ((dset.Tables[0].Rows[0]["Carbon_Eqv"] is Decimal))
                                         ? (decimal) dset.Tables[0].Rows[0]["Carbon_Eqv"]
                                         : 0;

                if (_previousChemanalEvent == null ||
                    (newEvent.Id != _previousChemanalEvent.Id &&
                     newEvent.ChemanalDateTime != _previousChemanalEvent.ChemanalDateTime))
                {
                   
                    Console.WriteLine("Event pushed");
                    _mainGate.PushEvent(newEvent);
                    _previousChemanalEvent = newEvent;
                }
            }
        }
    }
}