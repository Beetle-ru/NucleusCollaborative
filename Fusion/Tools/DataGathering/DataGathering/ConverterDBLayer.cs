using System;
using System.Collections.Generic;
using Converter;
using Converter.Trends;
using HeatInfo;
using NordSteel.Data;
using Oracle.DataAccess.Client;

namespace DataGathering
{
    class ConverterDBLayer : OracleDBLayer
    {

        public List<BathLevel> GetBathLevel(int fusionId)
        {
            List<BathLevel> result = new List<BathLevel>();
            string sql = "SELECT BATHLEVEL_ID,HEAT_ID, to_char(INSERTTIME,'dd.mm.yyyy HH24:MI:SS'),VALUE FROM HEAT_BATHLEVEL WHERE HEAT_ID=" + fusionId.ToString();
            OracleDataReader reader = Execute(sql);
            while (reader.Read())
            {
                BathLevel bathLevel = new BathLevel();
                bathLevel.Id = int.Parse(CheckNubmerForNull(reader[0].ToString()));
                bathLevel.FusionId = int.Parse(CheckNubmerForNull(reader[1].ToString()));
                bathLevel.Date = DateTime.Parse(CheckDateForNull(reader[2].ToString()));
                bathLevel.Value = int.Parse(CheckNubmerForNull(reader[3].ToString()));
                result.Add(bathLevel);
            }
            return result;
        }

        public List<Addition> GetAdditionsDozen(int heatID)
        {
            List<Addition> result = new List<Addition>();
            string sql = "SELECT ha.MAT_ID,ha.INSERTTIME,ha.PORTION_WGT, ";
            sql += " ms.NAME_ENGLISH FROM HEAT_ADDITIONS_DOZEN ha, MATERIAL_SPEC ms WHERE ms.MAT_ID= ha.MAT_ID AND ha.HEAT_ID=" + heatID.ToString() + " ORDER BY INSERTTIME ";
            OracleDataReader reader = Execute(sql);
            while (reader.Read())
            {
                Addition addition = new Addition();
                addition.MaterialId = int.Parse(CheckNubmerForNull(reader[0].ToString()));
                addition.Date = DateTime.Parse(CheckDateForNull(reader[1].ToString()));
                addition.PortionWeight = int.Parse(CheckNubmerForNull(reader[2].ToString()));
                addition.MaterialName = reader[3].ToString();
                result.Add(addition);
            }
            reader.Close();
            return result;
        }


        public List<Addition> GetAdditions(int fusionId)
        {
            List<Addition> result = new List<Addition>();
            string sql = "SELECT ha.HTADDACT_ID, ha.HEAT_ID, ha.MAT_ID, ha.PHASE_NO, ha.DESTINATION_AGGNO, ha.DATA_SOURCE, ha.INSERTTIME, ha.PORTION_WGT,ha.TOTAL_WGT, ";
            sql += " ms.NAME_ENGLISH,ha.Lanze_pos,ha.o2vol_total FROM HEAT_ADDITIONS_ACT ha, MATERIAL_SPEC ms WHERE ms.MAT_ID= ha.MAT_ID AND ha.HEAT_ID=" + fusionId.ToString() + " ORDER BY INSERTTIME ";
            OracleDataReader reader = Execute(sql);
            while (reader.Read())
            {
                Addition addition = new Addition();
                addition.Id = int.Parse(CheckNubmerForNull(reader[0].ToString()));
                addition.FusionId = int.Parse(CheckNubmerForNull(reader[1].ToString()));
                addition.MaterialId = int.Parse(CheckNubmerForNull(reader[2].ToString()));
                addition.Destination = reader[4].ToString();
                addition.DataSource = reader[5].ToString();
                addition.Date = DateTime.Parse(CheckDateForNull(reader[6].ToString()));
                addition.PortionWeight = int.Parse(CheckNubmerForNull(reader[7].ToString()));
                addition.TotalWeight = int.Parse(CheckNubmerForNull(reader[8].ToString()));
                addition.MaterialName = reader[9].ToString();
                addition.LancePosition = int.Parse(CheckNubmerForNull(reader[10].ToString()));
                addition.O2TotalVol = int.Parse(CheckNubmerForNull(reader[11].ToString()));
                result.Add(addition);
            }
            reader.Close();
            return result;
        }

        public List<Sublance> GetSublance(Int64 heatID)
        {
            var result = new List<Sublance>();
            var sql = "SELECT INSERTTIME, TEMP, OXYGEN, CARBON ";
            sql += "FROM SMK.HEAT_MEASUREMENTS ";
            sql += "WHERE TYPE = 'AUTO' AND HEAT_ID = :HeatID ";
            sql += "ORDER BY INSERTTIME";
            var reader = Execute(sql, new[] { 
                new OracleParameter { 
                    OracleDbType = OracleDbType.Int32, ParameterName = "HeatID", Direction= System.Data.ParameterDirection.Input, Value = heatID 
                } 
            });
            while (reader.Read())
            {
                result.Add(new Sublance
                {
                    StartDate = DateTime.Parse(CheckDateForNull(reader["INSERTTIME"].ToString())),
                    Temperature = int.Parse(CheckNubmerForNull(reader["TEMP"].ToString())),
                    Oxigen = int.Parse(CheckNubmerForNull(reader["OXYGEN"].ToString())),
                    C = double.Parse(CheckNubmerForNull(reader["CARBON"].ToString())),

                });

            }
            reader.Close();
            return result;
        }

        public HotMetal GetHotMetal(int heatID)
        {
            string sql = "select HML.ANA_C, HML.ANA_MN, HML.ANA_P, HML.ANA_S, HML.ANA_SI, HML.ANA_TI, HML.HM_TEMP, HML.HM_WEIGHT from HEATS h, HEAT_HOTMETAL hhm, HOTMETAL_LADLES hml Where "
                        + "HHM.HEAT_ID = H.HEAT_ID AND HML.CHGL_ID = HHM.CHGL_ID AND h.HEAT_ID=" + heatID;
            OracleDataReader reader = Execute(sql);
            HotMetal hotMetal = new HotMetal();
            while (reader.Read())
            {
                hotMetal.C = float.Parse(CheckNubmerForNull(reader[0].ToString()));
                hotMetal.Mn = float.Parse(CheckNubmerForNull(reader[1].ToString()));
                hotMetal.P = float.Parse(CheckNubmerForNull(reader[2].ToString()));
                hotMetal.S = float.Parse(CheckNubmerForNull(reader[3].ToString()));
                hotMetal.Si = float.Parse(CheckNubmerForNull(reader[4].ToString()));
                hotMetal.Ti = float.Parse(CheckNubmerForNull(reader[5].ToString()));
                hotMetal.Temperature = int.Parse(CheckNubmerForNull(reader[6].ToString()));
                hotMetal.Weight = int.Parse(CheckNubmerForNull(reader[7].ToString()));
            }
            reader.Close();
            return hotMetal;
        }

        public List<HotMetalAnalysys> GetHotMetalAnalysys(int heatNumber)
        {
            Reconnect("XIM", "xim");
            var result = new List<HotMetalAnalysys>();
            var sql = "SELECT XIM_DT, XIM_NUM, XIM_MIX, XIM_PFANNE, XIM_C, XIM_MN, XIM_P, XIM_S, XIM_SI, XIM_TI ";
            sql += "FROM XIM.XIM_ANAL ";
            sql += "WHERE XIM_VID = 3 AND XIM_PLAVKA = :HeatNumber ";
            var reader = Execute(sql, new[] { 
                new OracleParameter { 
                    OracleDbType = OracleDbType.Int32, ParameterName = "HeatNumber", Direction= System.Data.ParameterDirection.Input, Value = heatNumber 
                } 
            });
            while (reader.Read())
            {
                result.Add(new HotMetalAnalysys
                {
                    Time = DateTime.Parse((CheckDateForNull(reader[0].ToString()))),
                    ProbeNumber = int.Parse(CheckNubmerForNull(reader[1].ToString())),
                    TorpedoNumber = int.Parse(CheckNubmerForNull(reader[2].ToString())),
                    LadleNumber = int.Parse(CheckNubmerForNull(reader[3].ToString())),
                    C = float.Parse(CheckNubmerForNull(reader[4].ToString())),
                    Mn = float.Parse(CheckNubmerForNull(reader[5].ToString())),
                    P = float.Parse(CheckNubmerForNull(reader[6].ToString())),
                    S = float.Parse(CheckNubmerForNull(reader[7].ToString())),
                    Si = float.Parse(CheckNubmerForNull(reader[8].ToString())),
                    Ti = float.Parse(CheckNubmerForNull(reader[9].ToString()))
                });
            }
            reader.Close();
            Reconnect("SMK", "smk");
            return result;
        }


        public List<ScrapBucket> GetScrapBuckets(int fusionId)
        {
            List<ScrapBucket> result = new List<ScrapBucket>();
            string sql = "SELECT sb.BUCKET_ID, sb.HEAT_ID,sbm.WEIGHT,ms.NAME_ENGLISH,sbm.MAT_ID,ms.mat_no, sb.bucket_no FROM SCRAP_BUCKETMATS sbm, SCRAP_BUCKETS sb, MATERIAL_SPEC ms WHERE sb.HEAT_ID=" + fusionId.ToString() + " AND sbm.BUCKET_ID=sb.BUCKET_ID";
            sql += " AND ms.MAT_ID=sbm.MAT_ID";
            OracleDataReader reader = Execute(sql);
            while (reader.Read())
            {
                ScrapBucket scrapBucket = new ScrapBucket();
                scrapBucket.Id = int.Parse(CheckNubmerForNull(reader[0].ToString()));
                scrapBucket.FusionId = int.Parse(CheckNubmerForNull(reader[1].ToString()));
                scrapBucket.Weight = int.Parse(CheckNubmerForNull(reader[2].ToString()));
                scrapBucket.MaterialName = reader[3].ToString();
                scrapBucket.MaterialId = int.Parse(CheckNubmerForNull(reader[4].ToString()));
                scrapBucket.MaterialNumber = int.Parse(reader[5].ToString().Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries)[2]);
                scrapBucket.Number = int.Parse(CheckNubmerForNull(reader[6].ToString()));
                result.Add(scrapBucket);
            }

            return result;
        }

        public List<SteelAnalysys> GetSteelAnalysys(Int64 heatNumber)
        {
            Reconnect("XIM", "xim");
            var result = new List<SteelAnalysys>();
            var sql = "SELECT XIM_DT, XIM_NUM, XIM_C, XIM_SI, XIM_MN, XIM_P, XIM_S, XIM_CR, XIM_NI, XIM_CU, XIM_AL, XIM_N, XIM_V, XIM_TI, ";
            sql += "XIM_AS, XIM_NB, XIM_MO, XIM_ZR, XIM_B, XIM_SN, XIM_AL_KR, XIM_W, XIM_CO, XIM_CA ";
            sql += "FROM XIM.XIM_ANAL WHERE XIM_VID = 1 AND XIM_PLAVKA = :HeatNumber AND (XIM_PLACE >= 20 AND XIM_PLACE < 30) ";
            var reader = Execute(sql,new[] {  
                new OracleParameter { OracleDbType = OracleDbType.Int32, ParameterName = "HeatNumber", Direction= System.Data.ParameterDirection.Input, Value = heatNumber }});
            if (reader.Read())
            {
                result.Add(new SteelAnalysys
                {
                    Time = DateTime.Parse(CheckDateForNull(reader[0].ToString())),
                    ProbeNumber = int.Parse(CheckNubmerForNull(reader[1].ToString())),
                    C = double.Parse(CheckNubmerForNull(reader[2].ToString())),
                    Si = double.Parse(CheckNubmerForNull(reader[3].ToString())),
                    Mn = double.Parse(CheckNubmerForNull(reader[4].ToString())),
                    P = double.Parse(CheckNubmerForNull(reader[5].ToString())),
                    S = double.Parse(CheckNubmerForNull(reader[6].ToString())),
                    Cr = double.Parse(CheckNubmerForNull(reader[7].ToString())),
                    Ni = double.Parse(CheckNubmerForNull(reader[8].ToString())),
                    Cu = double.Parse(CheckNubmerForNull(reader[9].ToString())),
                    Al = double.Parse(CheckNubmerForNull(reader[10].ToString())),
                    N = double.Parse(CheckNubmerForNull(reader[11].ToString())),
                    V = double.Parse(CheckNubmerForNull(reader[12].ToString())),
                    Ti = double.Parse(CheckNubmerForNull(reader[13].ToString())),
                    As = double.Parse(CheckNubmerForNull(reader[14].ToString())),
                    Nb = double.Parse(CheckNubmerForNull(reader[15].ToString())),
                    Mo = double.Parse(CheckNubmerForNull(reader[16].ToString())),
                    Zr = double.Parse(CheckNubmerForNull(reader[17].ToString())),
                    B = double.Parse(CheckNubmerForNull(reader[18].ToString())),
                    Sn = double.Parse(CheckNubmerForNull(reader[19].ToString())),
                    Al_kp = double.Parse(CheckNubmerForNull(reader[20].ToString())),
                    W = double.Parse(CheckNubmerForNull(reader[21].ToString())),
                    Co = double.Parse(CheckNubmerForNull(reader[22].ToString())),
                    Ca = double.Parse(CheckNubmerForNull(reader[23].ToString()))
                });
            }
            reader.Close();
            Reconnect("SMK", "smk");
            return result;
        }

        public List<SlagAnalysys> GetSlagAnalysys(Int64 heatNumber)
        {
            Reconnect("XIM", "xim");
            var result = new List<SlagAnalysys>();
            var sql = "SELECT XIM_DT, XIM_NUM, XIM_CAO, XIM_SIO2, XIM_FEO, XIM_MGO, XIM_MNO, XIM_S, XIM_AL2O3, XIM_P2O5 ";
            sql += "FROM XIM.XIM_ANAL WHERE XIM_VID = 2 AND XIM_PLAVKA = :HeatNumber";
            var reader = Execute(sql, new[] {  
                new OracleParameter { OracleDbType = OracleDbType.Int32, ParameterName = "HeatNumber", Direction= System.Data.ParameterDirection.Input, Value = heatNumber }
            });
            while (reader.Read())
            {
                result.Add(new SlagAnalysys
                {
                    Time = DateTime.Parse(CheckDateForNull(reader[0].ToString())),
                    ProbeNumber = int.Parse(CheckNubmerForNull(reader[1].ToString())),
                    CaO = double.Parse(CheckNubmerForNull(reader[2].ToString())),
                    SiO2 = double.Parse(CheckNubmerForNull(reader[3].ToString())),
                    FeO = double.Parse(CheckNubmerForNull(reader[4].ToString())),
                    MgO = double.Parse(CheckNubmerForNull(reader[5].ToString())),
                    MnO = double.Parse(CheckNubmerForNull(reader[6].ToString())),
                    S = double.Parse(CheckNubmerForNull(reader[7].ToString())),
                    Al2O3 = double.Parse(CheckNubmerForNull(reader[8].ToString())),
                    P2O5 = double.Parse(CheckNubmerForNull(reader[9].ToString()))
                });
            }
            reader.Close();
            Reconnect("SMK", "smk");
            return result;
        }

        public List<EventDuration> GetBlowings(int heatID)
        {
            string sql = "select h.eventtime,h.value1,h.type from heat_events h where heat_id =" + heatID.ToString() + " and type in ('BLOS','BLOE') order by eventtime";
            OracleDataReader reader = Execute(sql);

            List<EventDuration> durations = new List<EventDuration>();
            EventDuration dur = new EventDuration();
            while (reader.Read())
            {
                if (reader[2].ToString() == "BLOS")
                {
                    dur.StartDate = DateTime.Parse(CheckDateForNull(reader[0].ToString()));
                }
                else
                {
                    dur.EndDate = DateTime.Parse(CheckDateForNull(reader[0].ToString()));
                    dur.Tag = int.Parse(CheckNubmerForNull(reader[1].ToString()));
                    durations.Add(dur);
                    dur = new EventDuration();
                }


            }
            reader.Close();
            return durations;
        }

        public EventDuration GetDeslagingDuration(int heatID)
        {
            EventDuration duration = new EventDuration();
            string sql = "select eventtime from heat_events where heat_id =" + heatID.ToString() + " and type in ('DSLS','DSLE') order by eventtime";
            OracleDataReader reader = Execute(sql);

            if (reader.Read())
            {
                duration.StartDate = DateTime.Parse(CheckDateForNull(reader[0].ToString()));
            }

            if (reader.Read())
            {
                duration.EndDate = DateTime.Parse(CheckDateForNull(reader[0].ToString()));
            }
            reader.Close();
            return duration;
        }

        public EventDuration GetTapsDuration(int heatID)
        {
            string sql = "select eventtime from heat_events where heat_id =" + heatID.ToString() + " and type in ('TAPS','TAPE') order by eventtime";
            OracleDataReader reader = Execute(sql);
            EventDuration duration = new EventDuration();
            if (reader.Read())
            {
                duration.StartDate = DateTime.Parse(CheckDateForNull(reader[0].ToString()));
            }

            if (reader.Read())
            {
                duration.EndDate = DateTime.Parse(CheckDateForNull(reader[0].ToString()));
            }
            reader.Close();
            return duration;
        }

        public EventDuration GetHotMetalChargingDuration(int heatID)
        {
            string sql = "select eventtime from heat_events where heat_id =" + heatID.ToString() + " and type in ('HMCS','HMCE') order by eventtime";
            OracleDataReader reader = Execute(sql);
            EventDuration duration = new EventDuration();

            if (reader.Read())
            {
                duration.StartDate = DateTime.Parse(CheckDateForNull(reader[0].ToString()));
            }

            if (reader.Read())
            {
                duration.EndDate = DateTime.Parse(CheckDateForNull(reader[0].ToString()));
            }
            reader.Close();
            return duration;
        }

        public EventDuration GetScrapHeatingDuration(int heatID)
        {
            string sql = "select eventtime from heat_events where heat_id =" + heatID.ToString() + " and type in ('SCHS','SCHE') order by eventtime";
            OracleDataReader reader = Execute(sql);
            EventDuration duration = new EventDuration();

            if (reader.Read())
            {
                duration.StartDate = DateTime.Parse(CheckDateForNull(reader[0].ToString()));
            }
            if (reader.Read())
            {
                duration.EndDate = DateTime.Parse(CheckDateForNull(reader[0].ToString()));
            }
            reader.Close();
            return duration;
        }

        public EventDuration GetScrapChargingDuration(int heatID)
        {
            string sql = "select eventtime from heat_events where heat_id =" + heatID.ToString() + " and type in ('SCCS','SCCE') order by eventtime";
            OracleDataReader reader = Execute(sql);
            EventDuration duration = new EventDuration();

            if (reader.Read())
            {
                duration.StartDate = DateTime.Parse(CheckDateForNull(reader[0].ToString()));
            }

            if (reader.Read())
            {
                duration.EndDate = DateTime.Parse(CheckDateForNull(reader[0].ToString()));
            }
            reader.Close();
            return duration;
        }

        public Fusion GetFusion(string heatNumber)
        {
            Fusion fusion = new Fusion();
            string sql = "SELECT h.HEAT_ID,h.HEAT_NO,h.RPT_TSOLL,h.RPT_TIST,h.RPT_CSOLL,h.RPT_CIST,to_char(h.DT_BEGIN,'dd.mm.yy HH24:mi:ss'),to_char(h.DT_END,'dd.mm.yy HH24:mi:ss'),h.TEAM_NAME,";
            sql += "g.NAME_ENGLISH,h.CHGD_HMWEIGHT,h.HM_TEMP,h.CONVERTER_LIFE FROM HEATS h, GRADE_SPEC g WHERE h.HEAT_NO='" + heatNumber + "' AND h.GRADE_ID=g.GRADE_ID";

            OracleDataReader reader = Execute(sql);
            while (reader.Read())
            {
                fusion.ID = int.Parse(reader[0].ToString());
                fusion.Number = int.Parse(reader[1].ToString());
                fusion.PlannedTemperature = int.Parse(!string.IsNullOrEmpty(reader[2].ToString()) ? reader[2].ToString() : "0");
                fusion.FactTemperature = int.Parse(!string.IsNullOrEmpty(reader[3].ToString()) ? reader[3].ToString() : "0");
                fusion.PlannedCarbon = double.Parse(!string.IsNullOrEmpty(reader[4].ToString()) ? reader[4].ToString() : "0");
                fusion.FactCarbon = double.Parse(!string.IsNullOrEmpty(reader[5].ToString()) ? reader[5].ToString() : "0");
                fusion.StartDate = DateTime.Parse(reader[6].ToString());
                fusion.EndDate = DateTime.Parse(!string.IsNullOrEmpty(reader[7].ToString()) ? reader[7].ToString() : "01.01.01");
                fusion.TeamNumber = int.Parse(!string.IsNullOrEmpty(reader[8].ToString()) ? reader[8].ToString() : "0");
                fusion.Grade = reader[9].ToString();
                fusion.CastIronWeight = int.Parse(CheckNubmerForNull(reader[10].ToString()));
                fusion.CastIronTemp = int.Parse(CheckNubmerForNull(reader[11].ToString()));
                fusion.AggregateLifeTime = int.Parse(CheckNubmerForNull(reader[12].ToString()));
                fusion.AggregateNumber = int.Parse(heatNumber.Substring(0, 1));
            }
            // clean up 
            reader.Close();
            return fusion;

        }


        public HotMetalLadle GetHotMetalLadle(Int64 heatID)
        {
            string sql = @"select hl.chgl_no, hl.CHGTIME, tor.TLC_NO, tor.ANA_SI, tor.ana_c, tor.ana_mn, tor.ana_p, tor.ana_s, tor.weight, tor.pouringtime
                          from heats_torpedos tor, heats_ladles hl
                          where  tor.CHGL_ID = hl.chgl_id and hl.heat_no =" + heatID.ToString();
            OracleDataReader reader = Execute(sql);
            HotMetalLadle hotMetalLadle = new HotMetalLadle();
            while (reader.Read())
            {
                hotMetalLadle.Number = int.Parse(CheckNubmerForNull(reader[0].ToString()));
                hotMetalLadle.ChargeTime = DateTime.Parse(CheckDateForNull(reader[1].ToString()));
                hotMetalLadle.Torpedes.Add(new HotMetalTorpedo()
                {
                    Number = int.Parse(CheckNubmerForNull(reader[2].ToString())),
                    Analysys = new HotMetalAnalysys()
                    {
                        Si = double.Parse(CheckNubmerForNull(reader[3].ToString())),
                        C = double.Parse(CheckNubmerForNull(reader[4].ToString())),
                        Mn = double.Parse(CheckNubmerForNull(reader[5].ToString())),
                        P = double.Parse(CheckNubmerForNull(reader[6].ToString())),
                        S = double.Parse(CheckNubmerForNull(reader[7].ToString())),
                    },
                    Weight = int.Parse(CheckNubmerForNull(reader[8].ToString())),
                    ChargeTime = DateTime.Parse(CheckDateForNull(reader[9].ToString()))
                });
            }
            reader.Close();
            return hotMetalLadle;
        }

    }
}
