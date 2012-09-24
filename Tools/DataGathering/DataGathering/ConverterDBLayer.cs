using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Converter.Trends;
using Oracle.DataAccess.Client;

namespace DataGathering
{
    class ConverterDBLayer
    {
        OracleConnection m_Connection;


        public ConverterDBLayer()
        {
            m_Connection = new OracleConnection("DATA SOURCE=localhost;PERSIST SECURITY INFO=True;USER ID=SMK;PASSWORD=SMK");
            m_Connection.Open();
        }

        private string OracleDate(DateTime Date)
        {
            return string.Format("to_date('{0}','dd.mm.yyyy HH24:MI:SS')", Date.ToString());
        }


        public List<BathLevel> GetBathLevel(int fusionId)
        {
            List<BathLevel> result = new List<BathLevel>();
            string sql = "SELECT BATHLEVEL_ID,HEAT_ID, to_char(INSERTTIME,'dd.mm.yyyy HH24:MI:SS'),VALUE FROM HEAT_BATHLEVEL WHERE HEAT_ID="+ fusionId.ToString();
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
            reader.Close();
            return result;
        }

        public List<Addition> GetAdditions(int fusionId)
        {
            List<Addition> result = new List<Addition>();
            string sql = "SELECT ha.HTADDACT_ID, ha.HEAT_ID, ha.MAT_ID, ha.PHASE_NO, ha.DESTINATION_AGGNO, ha.DATA_SOURCE, ha.INSERTTIME, ha.PORTION_WGT,ha.TOTAL_WGT, ";
            sql += " ms.NAME_ENGLISH FROM HEAT_ADDITIONS_ACT ha, MATERIAL_SPEC ms WHERE ms.MAT_ID= ha.MAT_ID AND ha.HEAT_ID=" + fusionId.ToString()+" ORDER BY INSERTTIME "; 
            OracleDataReader reader = Execute(sql);
            while (reader.Read())
            {
                Addition addition = new Addition();
                addition.Id = int.Parse(CheckNubmerForNull(reader[0].ToString()));
                addition.FusionId = int.Parse(CheckNubmerForNull(reader[1].ToString()));
                addition.MaterialId = int.Parse(CheckNubmerForNull(reader[2].ToString()));
                addition.PhaseNo= reader[3].ToString();
                addition.Destination = reader[4].ToString();
                addition.DataSource = reader[5].ToString();
                addition.Date = DateTime.Parse(CheckDateForNull(reader[6].ToString()));
                addition.PortionWeight = int.Parse(CheckNubmerForNull(reader[7].ToString()));
                addition.TotalWeight = int.Parse(CheckNubmerForNull(reader[8].ToString()));
                addition.MaterialName = reader[9].ToString();
                result.Add(addition);
            }
            reader.Close();
            return result;
        }

        public List<ScrapBucket> GetScrapBuckets(int fusionId)
        {
            List<ScrapBucket> result = new List<ScrapBucket>();
            string sql = "SELECT sb.BUCKET_ID,sb.HEAT_ID,sbm.WEIGHT,ms.NAME_ENGLISH,sbm.MAT_ID FROM SCRAP_BUCKETMATS sbm, SCRAP_BUCKETS sb, MATERIAL_SPEC ms WHERE sb.HEAT_ID=" + fusionId.ToString()+" AND sbm.BUCKET_ID=sb.BUCKET_ID";
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
                result.Add(scrapBucket);
            }
            reader.Close();
            return result;
        }

        public List<Fusion> GetFusionsData(DateTime dateBeg, DateTime dateEnd, int converterNumber,int GradeID)
        {
            List<Fusion> result = new List<Fusion>();
            string sql = "SELECT DISTINCT h.HEAT_ID,h.HEAT_NO,h.RPT_TSOLL,h.RPT_TIST,h.RPT_CSOLL,h.RPT_CIST,to_char(h.DT_BEGIN,'dd.mm.yy HH24:mi:ss'),to_char(h.DT_END,'dd.mm.yy HH24:mi:ss'),h.TEAM_NAME,";
            sql += "g.NAME_ENGLISH,h.CHGD_HMWEIGHT,h.HM_TEMP FROM HEATS h, GRADE_SPEC g, heat_offgas ho WHERE h.HEAT_ID = ho.HEAt_ID AND h.DT_BEGIN BETWEEN " + OracleDate(dateBeg) + " AND ";
            sql += OracleDate(dateEnd) + " AND h.CV_NO=" + converterNumber.ToString() + " AND h.GRADE_ID=g.GRADE_ID";
            if (GradeID != 0)
            {
                sql += " AND h.GRADE_ID=" + GradeID.ToString();
            }
            sql += " ORDER BY h.HEAT_NO";
            OracleDataReader reader = Execute(sql);
            while (reader.Read())
            {
                Fusion fusion = new Fusion();
                fusion.Id = int.Parse(reader[0].ToString());
                fusion.Number = int.Parse(reader[1].ToString());
                fusion.PlannedTempereture = int.Parse(!string.IsNullOrEmpty(reader[2].ToString()) ? reader[2].ToString() : "0");
                fusion.FactTemperature = int.Parse(!string.IsNullOrEmpty(reader[3].ToString()) ? reader[3].ToString() : "0");
                fusion.PlannedC = double.Parse(!string.IsNullOrEmpty(reader[4].ToString()) ? reader[4].ToString() : "0");
                fusion.FactC = double.Parse(!string.IsNullOrEmpty(reader[5].ToString()) ? reader[5].ToString() : "0");
                fusion.StartDateDB = DateTime.Parse(reader[6].ToString());
                fusion.EndDate = DateTime.Parse(!string.IsNullOrEmpty(reader[7].ToString()) ? reader[7].ToString() : "01.01.01");
                fusion.TeamNumber = int.Parse(!string.IsNullOrEmpty(reader[8].ToString()) ? reader[8].ToString() : "0");
                fusion.Grade = reader[9].ToString();
                fusion.CastIronWeight = int.Parse(CheckNubmerForNull(reader[10].ToString()));
                fusion.CastIronTemp = int.Parse(CheckNubmerForNull(reader[11].ToString()));
                fusion.ConverterNumber = converterNumber;
                result.Add(fusion);
            }
            // clean up 
            reader.Close();
            return result;

        }

        private string CheckNubmerForNull(string value)
        {
            return !string.IsNullOrEmpty(value) ? value : "0";
        }

        private string CheckDateForNull(string value)
        {
            return !string.IsNullOrEmpty(value) ? value : "01.01.01";
        }

        public HotMetal GetHotMetal(int fusionId)
        {
            string sql = "select HML.ANA_C, HML.ANA_MN, HML.ANA_P, HML.ANA_S, HML.ANA_SI, HML.ANA_TI, HML.HM_TEMP, HML.HM_WEIGHT from HEATS h, HEAT_HOTMETAL hhm, HOTMETAL_LADLES hml Where "
                        + "HHM.HEAT_ID = H.HEAT_ID AND HML.CHGL_ID = HHM.CHGL_ID AND h.HEAT_ID=" + fusionId;
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
            return hotMetal;
        }

        public List<MarkSteel> GetMarks(int ConverterNumber,DateTime DateBegin, DateTime DateEnd)
        {
            List<MarkSteel> marks = new List<MarkSteel>();
            string sql = "SELECT h.grade_id, gs.name_english FROM HEATS h, GRADE_SPEC gs WHERE h.CV_NO=" + ConverterNumber.ToString();
            sql += " AND h.grade_id=gs.grade_id and h.DT_BEGIN BETWEEN " + OracleDate(DateBegin)+" AND "+ OracleDate(DateEnd) + " GROUP BY h.grade_id,gs.name_english    ORDER BY gs.name_english ";
            OracleDataReader reader = Execute(sql);
            while (reader.Read())
            {
                MarkSteel mark = new MarkSteel();
                mark.ID = int.Parse(CheckNubmerForNull(reader[0].ToString()));
                mark.Name = reader[1].ToString();
                marks.Add(mark);
            }
            reader.Close();
            return marks;
        }

        public List<Lance> GetLance(int FusionId)
        {
            List<Lance> result = new List<Lance>();
            string sql = "SELECT BLOWING_ID,HEAT_ID,to_char(INSERTTIME,'dd.mm.yy HH24:MI:SS'),PHASE_NO,O2VOL,O2FLOW,O2PRESSURE,LANCEHGT FROM HEAT_BLOWINGS WHERE HEAT_ID=" + FusionId.ToString();
            OracleDataReader reader = Execute(sql);
            while (reader.Read())
            {
                Lance lance = new Lance();
                lance.Id = int.Parse(CheckNubmerForNull(reader[0].ToString()));
                lance.FusionId = int.Parse(CheckNubmerForNull(reader[1].ToString()));
                lance.Date = DateTime.Parse(CheckDateForNull(reader[2].ToString()));
                lance.PhaseNo = reader[3].ToString();
                lance.O2Vol = int.Parse(CheckNubmerForNull(reader[4].ToString()));
                lance.O2Flow = int.Parse(CheckNubmerForNull(reader[5].ToString()));
                lance.O2Pressure = double.Parse(CheckNubmerForNull(reader[6].ToString()));
                lance.Height = int.Parse(CheckNubmerForNull(reader[7].ToString()));
                result.Add(lance);
            }
            reader.Close();
            return result;
        }

        public List<OffGas> GetOffGases(int FusionId)
        {
            List<OffGas> result = new List<OffGas>();
            string sql = "SELECT OFFGAS_ID,HEAT_ID,to_char(INSERTTIME,'dd.mm.yy HH24:MI:SS'),TEMP,FLOW,ANA_CO,ANA_CO2,ANA_O2,ANA_H2,ANA_AR,PHASE_NO FROM HEAT_OFFGAS WHERE HEAT_ID=" + FusionId.ToString();
            OracleDataReader reader = Execute(sql);
            while (reader.Read())
            {
                OffGas offgas = new OffGas();
                offgas.Id = int.Parse(CheckNubmerForNull(reader[0].ToString()));
                offgas.FusionId = int.Parse(CheckNubmerForNull(reader[1].ToString()));
                offgas.Date = DateTime.Parse(CheckDateForNull(reader[2].ToString()));
                offgas.Temp = int.Parse(CheckNubmerForNull(reader[3].ToString()));
                offgas.Flow = int.Parse(CheckNubmerForNull(reader[4].ToString()));
                offgas.CO = double.Parse(CheckNubmerForNull(reader[5].ToString()));
                offgas.CO2 = double.Parse(CheckNubmerForNull(reader[6].ToString()));
                offgas.O2 = double.Parse(CheckNubmerForNull(reader[7].ToString()));
                offgas.H2 = double.Parse(CheckNubmerForNull(reader[8].ToString()));
                offgas.Ar = double.Parse(CheckNubmerForNull(reader[9].ToString()));
                offgas.PhaseNo = reader[10].ToString();
                result.Add(offgas);

            }
            reader.Close();
            return result;
        }

        public void FillFusionData(Fusion fusion)
        {
            string sql = "SELECT h.HEAT_ID,h.HEAT_NO,h.RPT_TSOLL,h.RPT_TIST,h.RPT_CSOLL,h.RPT_CIST,h.DT_BEGIN,h.DT_END,h.TEAM_NAME,";
            sql += "g.NAME_ENGLISH FROM HEATS h, GRADE_SPEC g WHERE h.DT_BEGIN BETWEEN " + OracleDate(fusion.StartDate.AddMinutes(-6)) + " AND ";
            sql += OracleDate(fusion.StartDate.AddMinutes(6)) + " AND h.CV_NO=" + fusion.ConverterNumber.ToString() + " AND h.GRADE_ID=g.GRADE_ID";

            OracleDataReader reader = Execute(sql);
            while (reader.Read())
            {
                fusion.Id = int.Parse(reader[0].ToString());
                fusion.Number = int.Parse(reader[1].ToString());
                fusion.PlannedTempereture = int.Parse(!string.IsNullOrEmpty(reader[2].ToString()) ? reader[2].ToString() : "0");
                fusion.FactTemperature = int.Parse(!string.IsNullOrEmpty(reader[3].ToString()) ? reader[3].ToString() : "0");
                fusion.PlannedC = double.Parse(!string.IsNullOrEmpty(reader[4].ToString()) ? reader[4].ToString() : "0.0");
                fusion.FactC = double.Parse(!string.IsNullOrEmpty(reader[5].ToString()) ? reader[5].ToString() : "0");
                fusion.StartDateDB = DateTime.Parse(reader[6].ToString());
                fusion.EndDate = DateTime.Parse(reader[7].ToString());
                fusion.TeamNumber = int.Parse(!string.IsNullOrEmpty(reader[8].ToString()) ? reader[8].ToString() : "0");
                fusion.Grade = reader[9].ToString();

            }
            // clean up 
            reader.Close();
            FillDataByLance(fusion);
            FillDataByGasFlow(fusion);


        }

        private void FillDataByLance(Fusion fusion)
        {
            string sql = "SELECT INSERTTIME, O2PRESSURE, LANCEHGT FROM HEAT_BLOWINGS where HEAT_ID=" + fusion.Id.ToString();
            OracleDataReader reader = Execute(sql);
            while (reader.Read())
            {

                TrendPoint trendPoint = fusion.Points.Find(p => p.Time == DateTime.Parse(reader[0].ToString()) - fusion.StartDateDB);
                if (trendPoint != null)
                {
                    trendPoint.O2Pressure = double.Parse(!string.IsNullOrEmpty(reader[1].ToString()) ? reader[1].ToString() : "0.0");
                    trendPoint.LanceHeight = int.Parse(!string.IsNullOrEmpty(reader[2].ToString()) ? reader[2].ToString() : "0");
                }
            }
            reader.Close();
        }

        private void FillDataByGasFlow(Fusion fusion)
        {
            string sql = "SELECT INSERTTIME, FLOW FROM HEAT_OFFGAS where HEAT_ID=" + fusion.Id.ToString();
            OracleDataReader reader = Execute(sql);
            while (reader.Read())
            {
                TrendPoint trendPoint = fusion.Points.Find(p => p.Time == DateTime.Parse(reader[0].ToString()) - fusion.StartDateDB);
                if (trendPoint != null)
                    trendPoint.GasFlow = int.Parse(!string.IsNullOrEmpty(reader[1].ToString()) ? reader[1].ToString() : "0");
            }
            reader.Close();
        }

        private OracleDataReader Execute(string sql)
        {
            OracleCommand dbcmd = m_Connection.CreateCommand();
            dbcmd.CommandText = sql;
            return dbcmd.ExecuteReader();
        }

        public List<string> GetListFusions(DateTime dateBegin, DateTime dateEnd, int converterNumber)
        {
            List<string> result = new List<string>();
            //string sql = " SELECT DISTINCT h.HEAT_NO, h.HEAT_ID FROM HEATS h, HEAT_BLOWINGS hb, HEAT_OFFGAS ho WHERE h.HEAT_ID=ho.HEAT_ID AND h.DT_BEGIN BETWEEN " + OracleDate(dateBegin) + " AND " + OracleDate(dateEnd)+" AND h.cv_no=" + converterNumber.ToString();
            string sql = " SELECT DISTINCT h.HEAT_NO, h.HEAT_ID FROM HEATS h WHERE h.DT_BEGIN BETWEEN " + OracleDate(dateBegin) + " AND " + OracleDate(dateEnd) + " AND h.cv_no=" + converterNumber.ToString();
            OracleDataReader reader = Execute(sql);
            while (reader.Read())
            {
                result.Add(reader[0].ToString());
            }
            return result;
        }



    }
}
