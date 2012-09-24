using System;
using System.Collections.Generic;
using Esms;
using NordSteel.Data;


namespace EsmsFusionProtocol
{
    class DBLayer : OracleDBLayer
    {
        public List<HeatCommon> GetHeatList(DateTime startDate, DateTime endDate, int unitNumber)
        {
            var sql = string.Format("SELECT MIN(Time) AS MinTime,  MAX(Time) AS MaxTime,  HeatNumber, MAX(PrecedingHeatEndTime) AS PrecedingHeatEndTime " +
                "FROM HeatPassportEvent{0} WHERE TIME BETWEEN {1} AND {2} GROUP BY HeatNumber ORDER BY HeatNumber", unitNumber, OracleDate(startDate), OracleDate(endDate));
            var reader = Execute(sql);
            var result = new List<HeatCommon>();
            while (reader.Read())
            {
                result.Add(new HeatCommon()
                {
                    HeatStart =  DateTime.Parse(CheckDateForNull(reader[0].ToString())),
                    HeatEnd = DateTime.Parse(CheckDateForNull(reader[1].ToString())),
                    HeatNumber = int.Parse(CheckNubmerForNull(reader[2].ToString())),
                    PrecedingHeatEnd = DateTime.Parse(CheckDateForNull(reader[3].ToString())),
                });
            }
            reader.Close();
            return result;
        }

        public HeatCommon GetHeatInfo(HeatCommon heat)
        {
            var sql = string.Format("SELECT HEAT_ID, HEAT_NO, SHP_NO, HEAT_DTB, HEAT_DTE FROM SHP.SHP_HEATS WHERE HEAT_NO =  {0} ", heat.HeatNumber);
            var reader = Execute(sql);
            if (reader.Read())
            {
                heat.HeatId = int.Parse(CheckNubmerForNull(reader[0].ToString()));
                heat.ShpNumber = int.Parse(CheckNubmerForNull(reader[2].ToString()));
                heat.HeatStartDB = DateTime.Parse(CheckDateForNull(reader[3].ToString()));
                heat.HeatEndtDB = DateTime.Parse(CheckDateForNull(reader[4].ToString()));
            }
            reader.Close();
            return GetPreviousHeatInfo(heat);
        }

        private HeatCommon GetPreviousHeatInfo(HeatCommon heat)
        {
            var sql = string.Format("SELECT MIN(Time) AS MinTime,  MAX(Time) AS MaxTime,  HeatNumber " +
                "FROM EVENTS.HEATPASSPORTEVENT2 WHERE HeatNumber  = " +
                "(SELECT HeatNumber FROM EVENTS.HEATPASSPORTEVENT2 WHERE TIME = {0}) " +
                "GROUP BY HeatNumber ", OracleDate(heat.PrecedingHeatEnd));
            var reader = Execute(sql);
            if (reader.Read())
            {
                heat.PreviousHeatStart = DateTime.Parse(CheckDateForNull(reader[0].ToString()));
                heat.PreviousHeatEnd = DateTime.Parse(CheckDateForNull(reader[1].ToString()));
                heat.PreviousHeatNumber = int.Parse(CheckNubmerForNull(reader[2].ToString()));
            }
            reader.Close();
            return heat;
        }

        public List<HotMetal> GetHotMetal(int heatId)
        {
            var sql = string.Format("SELECT SHM.INPUT_DT, SHM.LADLE_NO, SHM.WEIGHT, IL.TEMP FROM SHP.SHP_HOTMETAL SHM, SHP.IRON_LADLES IL " +
                                    "WHERE SHM.INPUT_DT BETWEEN IL.DATA_BEG AND IL.DATA_END AND IL.NOMER = SHM.LADLE_NO AND HEAT_ID =  {0}", heatId);
            var reader = Execute(sql);
            var result = new List<HotMetal>();
            while (reader.Read())
            {
                result.Add(new HotMetal
                {
                    Time = DateTime.Parse(CheckDateForNull(reader[0].ToString())),
                    LadleNUmber = int.Parse(CheckNubmerForNull(reader[1].ToString())),
                    Weight = float.Parse(CheckNubmerForNull(reader[2].ToString())),
                    Temperature = float.Parse(CheckNubmerForNull(reader[3].ToString()))
                });
            }
            reader.Close();
            return result;
        }
          
        public List<Additions> GetAdditions(int heatId)
        {
            var sql = string.Format("SELECT SA.DOSIGN_DT,  SA.WEIGHT, NM.NAME_LONG, CASE WHEN UPPER(NM.NAME_LONG) LIKE '%КОКС%' THEN 1 " +
                "WHEN UPPER(NM.NAME_LONG) LIKE '%ИЗВЕСТ%' THEN 2 WHEN UPPER(NM.NAME_LONG) LIKE '%ДОЛОМ%' THEN 3 END AS MAT " +
                "FROM SHP.SHP_ADDITIONS SA, NSI.NSI_MATERIAL NM " +
                "WHERE (UPPER(NM.NAME_LONG) LIKE '%КОКС%' OR UPPER(NM.NAME_LONG) LIKE '%ИЗВЕСТ%' OR UPPER(NM.NAME_LONG) LIKE '%ДОЛОМ%') " +
                "AND SA.MAT_ID=NM.MAT_ID AND SA.HEAT_ID  =  {0}", heatId);
            var reader = Execute(sql);
            var result = new List<Additions>();
            while (reader.Read())
            {
                result.Add(new Additions
                {
                    Time = DateTime.Parse(CheckDateForNull(reader[0].ToString())),
                    Weight = float.Parse(CheckNubmerForNull(reader[1].ToString())),
                    Name = reader[2].ToString(),
                    Index = int.Parse(CheckNubmerForNull(reader[3].ToString())),
                });
            }
            reader.Close();
            return result;
        }

        public List<ScrapLoadEvent> GetScrapLoad(int heatNumber)
        {
            var sql = string.Format("SELECT TIME, WEIGHT, TASKNUMBER, TANKNUMBER, SCRAPNAME, CHARGENUMBER, ID  FROM SCRAPLOADEVENT2 WHERE CHARGENUMBER = {0}", heatNumber);
            var reader = Execute(sql);
            var result = new List<ScrapLoadEvent>();
            while (reader.Read())
            {
                result.Add(new ScrapLoadEvent
                {
                    Time = DateTime.Parse(CheckDateForNull(reader[0].ToString())),
                    Weight = float.Parse(CheckNubmerForNull(reader[1].ToString())),
                    TaskNumber = int.Parse(CheckNubmerForNull(reader[2].ToString())),
                    TankNumber = int.Parse(CheckNubmerForNull(reader[3].ToString())),
                    ScrapName = reader[4].ToString(),
                    ChargeNumber = int.Parse(CheckNubmerForNull(reader[5].ToString())),
                    Id = int.Parse(CheckNubmerForNull(reader[6].ToString())),
                });
            }
            reader.Close();
            return result;
        }

         
    }
}
