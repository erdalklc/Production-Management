using EPM.Dto.Base;
using EPM.Plan.Dto.Extensions;
using EPM.Plan.Dto.Plan;
using EPM.Plan.Repository.Repository;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace EPM.Plan.Service.Services
{
    public class PlanService:IPlanService
    {
        private readonly IPlanRepository _planRepository;
        public PlanService(IPlanRepository planRepository)
        {
            _planRepository = planRepository;
        }

        public object GetPlan(string USER_CODE, int BRAND, int SEASON, string MODEL, string COLOR, int ORDER_TYPE, int PRODUCTION_TYPE, int FABRIC_TYPE)
        { 
            DataTable dtColumnNames = new DataTable();
            dtColumnNames.TableName = "COLUMNNAMES";
            dtColumnNames.Columns.Add("NAME", typeof(string));
            DataSet set = new DataSet();
            string sql = @"SELECT ROWNUM||'_'||ID AS ROW_ID,A.* FROM (SELECT H.ID,
                             BR.ADI BRAND,
                             PS.ADI SEASON,
                             H.MODEL,
                             H.COLOR,
                             PG.ADI AS PRODUCT_GROUP,
                             FT.ADI AS FABRIC_TYPE,
                             OT.ADI AS ORDER_TYPE,
                             PT.ADI AS PRODUCTION_TYPE,
                             H.DEADLINE,
                             MR.ADI AS MARKET,
                             MR.ID AS MARKET_ID,
                             SUM (QUANTITY) AS URETIM_ADET,
                             0 AS PLANLANAN_ADET,
                             0 AS PLANSIZ_ADET
                        FROM FDEIT005.EPM_MASTER_PRODUCTION_H H
                        INNER JOIN FDEIT005.EPM_PRODUCTION_BRANDS BR ON BR.ID=H.BRAND
                        INNER JOIN FDEIT005.EPM_PRODUCTION_TYPES PT ON PT.ID=H.PRODUCTION_TYPE
                        INNER JOIN FDEIT005.EPM_PRODUCTION_FABRIC_TYPES FT ON FT.ID=H.FABRIC_TYPE
                        INNER JOIN FDEIT005.EPM_PRODUCTION_ORDER_TYPES OT ON OT.ID=H.ORDER_TYPE
                        INNER JOIN FDEIT005.EPM_PRODUCTION_SEASON PS ON PS.ID=H.SEASON
                        INNER JOIN FDEIT005.EPM_PRODUCT_GROUP PG ON PG.ID=H.PRODUCT_GROUP
                        INNER JOIN FDEIT005.EPM_MASTER_PRODUCTION_D D ON D.HEADER_ID = H.ID 
                        INNER JOIN FDEIT005.EPM_PRODUCTION_MARKET MR ON MR.ID=D.MARKET
                        WHERE 0=0 AND H.STATUS=0 AND H.APPROVAL_STATUS=1";

            if (BRAND != 0)
                sql += " AND H.BRAND=" + BRAND;
            if (ORDER_TYPE != 0)
                sql += " AND H.ORDER_TYPE=" + ORDER_TYPE;
            if (FABRIC_TYPE != 0)
                sql += " AND H.FABRIC_TYPE=" + FABRIC_TYPE;
            else sql += " AND H.FABRIC_TYPE IN (SELECT FABRIC_TYPE_ID FROM FDEIT005.EPM_USER_FABRIC_TYPES WHERE USER_CODE='" + USER_CODE + "')";
            if (PRODUCTION_TYPE != 0)
                sql += " AND H.PRODUCTION_TYPE=" + PRODUCTION_TYPE;
            else sql += " AND H.PRODUCTION_TYPE IN (SELECT PRODUCTION_TYPE_ID FROM FDEIT005.EPM_USER_PRODUCTION_TYPES WHERE USER_CODE='" + USER_CODE + "')";
            if (COLOR != null && COLOR != "")
                sql += " AND H.COLOR='" + COLOR + "'";
            if (MODEL != null && MODEL != "")
                sql += " AND H.MODEL='" + MODEL + "'";
            sql += " AND H.SEASON=" + SEASON;
            sql += @"GROUP BY H.ID,
                             BR.ADI,
                             PS.ADI,
                             H.MODEL,
                             H.COLOR,
                             OT.ADI,
                             PG.ADI,
                             FT.ADI,
                             PT.ADI,
                             H.DEADLINE,
                             MR.ADI, MR.ID
                    ORDER BY H.ID) A";
            DataTable dt = _planRepository.QueryFill(sql);
            string tSql = "SELECT ID FROM ( " + sql + " )B";
            EPM_PRODUCTION_SEASON_WEEKS seasonWeek = _planRepository.Deserialize<EPM_PRODUCTION_SEASON_WEEKS>("SELECT * FROM FDEIT005.EPM_PRODUCTION_SEASON_WEEKS WHERE SEASON_ID=" + SEASON + "");
            for (int i = seasonWeek.START_WEEK; i <= 52; i++)
            {
                string columnName = seasonWeek.START_YEAR.ToString() + "_" + i;
                dtColumnNames.Rows.Add(columnName);
                DataColumn column = dt.Columns.Add(columnName, typeof(int));
                column.Caption = i + ". HAFTA";
                column.DefaultValue = 0;
            }
            for (int i = 1; i <= seasonWeek.END_WEEK; i++)
            {
                string columnName = seasonWeek.END_YEAR.ToString() + "_" + i;
                dtColumnNames.Rows.Add(columnName);
                DataColumn column = dt.Columns.Add(columnName, typeof(int));
                column.Caption = i + ". HAFTA";
                column.DefaultValue = 0;
            }
            //List<EPM_PRODUCTION_PLAN> plan = OracleServer.DeserializeList<EPM_PRODUCTION_PLAN>("SELECT * FROM FDEIT005.EPM_PRODUCTION_PLAN WHERE HEADER_ID IN (" + tSql + ")");
            List<EPM_PRODUCTION_PLAN> plan = _planRepository.DeserializeList<EPM_PRODUCTION_PLAN>(@"SELECT PL.ID,PL.HEADER_ID,PL.MARKET_ID,PL.WEEK,PL.YEAR,SUM(PL.QUANTITY) QUANTITY FROM FDEIT005.EPM_PRODUCTION_PLAN PL
                    INNER JOIN FDEIT005.EPM_MASTER_PRODUCTION_H H ON H.ID=PL.HEADER_ID AND H.STATUS=0 AND H.APPROVAL_STATUS=1 AND H.SEASON=" + SEASON + "    GROUP BY PL.ID,PL.HEADER_ID,PL.MARKET_ID,PL.WEEK,PL.YEAR");
            foreach (var item in plan)
            {
                DataRow[] row = dt.Select("ID=" + item.HEADER_ID + " AND MARKET_ID=" + item.MARKET_ID + "");
                if (row.Length > 0)
                {
                    if(dt.Columns.Contains(item.YEAR + "_" + item.WEEK))
                    {
                        row[0][item.YEAR + "_" + item.WEEK] = item.QUANTITY;
                        row[0]["PLANLANAN_ADET"] = row[0]["PLANLANAN_ADET"].IntParse() + item.QUANTITY;
                        row[0]["PLANSIZ_ADET"] = row[0]["URETIM_ADET"].IntParse() + row[0]["PLANLANAN_ADET"].IntParse();

                    }
                }
            }
            dt.AcceptChanges();
            dt.TableName = "DATA";
            DataTable dtYear = new DataTable();
            dtYear.Columns.Add("YEAR", typeof(int));
            dtYear.Rows.Add(seasonWeek.START_YEAR);
            dtYear.Rows.Add(seasonWeek.END_YEAR);
            dtYear.TableName = "YEARS";
            set.Tables.Add(dt);
            set.Tables.Add(dtYear);
            set.Tables.Add(dtColumnNames);
            return JsonConvert.SerializeObject(set, Formatting.Indented);
        }

        public object UpdateInsertPlan(string USER_CODE, JObject obj)
        {
            UretimPlanResponse response = new UretimPlanResponse();
            response.isOK = true;
            response.errorText = ""; 
            dynamic value = obj;
            string newValue = Convert.ToString(value.newValue);
            string oldValues = Convert.ToString(value.oldValue);
            Dictionary<string, object> newItem = JsonConvert.DeserializeObject<Dictionary<string, object>>(newValue);
            Dictionary<string, object> oldItems = JsonConvert.DeserializeObject<Dictionary<string, object>>(oldValues);
            if (newItem.Count > 0)
            {
                object THEADER_ID = 0;
                int HEADER_ID = 0;
                int MARKET_ID = 0;
                int YEAR = 0;
                int WEEK = 0;
                int QUANTITY = 0;
                int ESKI_ADET = 0;
                int URETIMADET = 0;
                int PLANADET = 0;
                int PLANSIZADET = 0;
                object TESKI_ADET = 0;
                object TURETIMADET = 0;
                object TPLANADET = 0;
                object TPLANSIZADET = 0;
                object TMARKET_ID = 0;


                QUANTITY = newItem.ElementAt(0).Value.IntParse();
                string tempYearWeek = newItem.ElementAt(0).Key.ToString();
                YEAR = tempYearWeek.Split('_')[0].IntParse();
                WEEK = tempYearWeek.Split('_')[1].IntParse();
                oldItems.TryGetValue("ID", out THEADER_ID);
                oldItems.TryGetValue("URETIM_ADET", out TURETIMADET);
                oldItems.TryGetValue("PLANLANAN_ADET", out TPLANADET);
                oldItems.TryGetValue("PLANSIZ_ADET", out TPLANSIZADET);
                oldItems.TryGetValue("MARKET_ID", out TMARKET_ID);
                oldItems.TryGetValue(tempYearWeek, out TESKI_ADET);

                HEADER_ID = THEADER_ID.IntParse();
                URETIMADET = TURETIMADET.IntParse();
                PLANADET = TPLANADET.IntParse();
                PLANSIZADET = TPLANSIZADET.IntParse();
                MARKET_ID = TMARKET_ID.IntParse();
                ESKI_ADET = TESKI_ADET.IntParse();

                int tumPlanlananlar = _planRepository.ReadInteger(string.Format(@"
                    SELECT SUM(TOTAL) FROM (
                        SELECT SUM(QUANTITY) AS TOTAL FROM FDEIT005.EPM_PRODUCTION_PLAN WHERE MARKET_ID={0} AND HEADER_ID={1}
                        UNION ALL
                        SELECT -SUM(QUANTITY) AS TOTAL FROM FDEIT005.EPM_PRODUCTION_PLAN WHERE MARKET_ID={0} AND HEADER_ID={1} AND WEEK ={2} AND YEAR ={3}
                    )A", MARKET_ID, HEADER_ID, WEEK, YEAR));
                response.oldValue = ESKI_ADET;
                if (URETIMADET - tumPlanlananlar >= QUANTITY)
                {
                    string sql = string.Format("SELECT * FROM FDEIT005.EPM_PRODUCTION_PLAN WHERE MARKET_ID={0} AND HEADER_ID={1} AND WEEK={2} AND YEAR={3}", MARKET_ID, HEADER_ID, WEEK, YEAR);
                    EPM_PRODUCTION_PLAN plan = _planRepository.Deserialize<EPM_PRODUCTION_PLAN>(sql);
                    plan.MARKET_ID = MARKET_ID;
                    plan.HEADER_ID = HEADER_ID;
                    plan.WEEK = WEEK;
                    plan.YEAR = YEAR;
                    plan.QUANTITY = QUANTITY;
                    plan.CREATED_BY = USER_CODE;
                    _planRepository.Serialize(plan);
                    _planRepository.ExecSql("DELETE FDEIT005.EPM_PRODUCTION_PLAN WHERE QUANTITY=0");
                }
                else
                {
                    response.isOK = false;
                    response.errorText = "Üretim planından fazla adet girilemez";
                }
            }

            return response;
        }

        public List<EPM_PRODUCTION_BAND_CAPASITIES> GetKapasitePlanList()
        {
            List<EPM_PRODUCTION_BAND_CAPASITIES> list = _planRepository.DeserializeList<EPM_PRODUCTION_BAND_CAPASITIES>("SELECT * FROM FDEIT005.EPM_PRODUCTION_BAND_CAPASITIES");
            return list;
        }

        public List<KapasitePlanListChart> GetKapasitePlanListChart()
        {
            List<KapasitePlanListChart> list = _planRepository.DeserializeList<KapasitePlanListChart>(@"SELECT BG.ADI BAND_NAME,CAPACITY FROM FDEIT005.EPM_PRODUCTION_BAND_CAPASITIES CP
INNER JOIN FDEIT005.EPM_PRODUCTION_BAND_GROUP BG ON BG.ID = CP.BAND_ID");
            return list;
        }

        public object[] KapasitePlanListUpdate(int Key, string Values)
        {
            object[] ok = new object[] { true, "Başarılı" };
            try
            {
                EPM_PRODUCTION_BAND_CAPASITIES detail = _planRepository.Deserialize<EPM_PRODUCTION_BAND_CAPASITIES>(@"SELECT * FROM FDEIT005.EPM_PRODUCTION_BAND_CAPASITIES D  WHERE D.ID=" + Key);
                JsonConvert.PopulateObject(Values, detail);
                _planRepository.Serialize<EPM_PRODUCTION_BAND_CAPASITIES>(detail);
            }
            catch (Exception ex)
            {
                ok[0] = false;
                ok[1] = "İşlemler yapılırken hatayla karşılaşıldı\n" + ex.Message;
            }
            return ok;
        }

        public List<KapasitePlanUyum> GetKapasiteUyumList(int YEAR, int BAND_GROUP)
        {
            string sql = string.Format(@"SELECT * FROM (SELECT M.BAND_ID,
M.BAND_NAME,
M.CAPACITY,
M.WEEK,
NVL(N.QUANTITY,0) AS QUANTITY FROM (
SELECT CP.BAND_ID, BG.ADI AS BAND_NAME, CP.CAPACITY,A.WEEK
    FROM FDEIT005.EPM_PRODUCTION_BAND_CAPASITIES CP
         INNER JOIN FDEIT005.EPM_PRODUCTION_BAND_GROUP BG ON BG.ID = CP.BAND_ID
         CROSS JOIN (
         SELECT 1 WEEK FROM DUAL
         UNION ALL
         SELECT 2 WEEK FROM DUAL
         UNION ALL
         SELECT 3 WEEK FROM DUAL
         UNION ALL
         SELECT 4 WEEK FROM DUAL 
         UNION ALL
         SELECT 5 WEEK FROM DUAL
         UNION ALL
         SELECT 6 WEEK FROM DUAL 
         UNION ALL
         SELECT 7 WEEK FROM DUAL 
         UNION ALL
         SELECT 8 WEEK FROM DUAL 
         UNION ALL
         SELECT 9 WEEK FROM DUAL
         UNION ALL
         SELECT 10 WEEK FROM DUAL
         UNION ALL
         SELECT 11 WEEK FROM DUAL
         UNION ALL
         SELECT 12 WEEK FROM DUAL
         UNION ALL
         SELECT 13 WEEK FROM DUAL
         UNION ALL
         SELECT 14 WEEK FROM DUAL
         UNION ALL
         SELECT 15 WEEK FROM DUAL
         UNION ALL
         SELECT 16 WEEK FROM DUAL
         UNION ALL
         SELECT 17 WEEK FROM DUAL
         UNION ALL
         SELECT 18 WEEK FROM DUAL
         UNION ALL
         SELECT 19 WEEK FROM DUAL
         UNION ALL
         SELECT 20 WEEK FROM DUAL
         UNION ALL
         SELECT 21 WEEK FROM DUAL
         UNION ALL
         SELECT 22 WEEK FROM DUAL
         UNION ALL
         SELECT 23 WEEK FROM DUAL
         UNION ALL
         SELECT 24 WEEK FROM DUAL
         UNION ALL
         SELECT 25 WEEK FROM DUAL
         UNION ALL
         SELECT 26 WEEK FROM DUAL
         UNION ALL
         SELECT 27 WEEK FROM DUAL
         UNION ALL
         SELECT 28 WEEK FROM DUAL
         UNION ALL
         SELECT 29 WEEK FROM DUAL
         UNION ALL
         SELECT 30 WEEK FROM DUAL
         UNION ALL
         SELECT 31 WEEK FROM DUAL
         UNION ALL
         SELECT 32 WEEK FROM DUAL
         UNION ALL
         SELECT 33 WEEK FROM DUAL
         UNION ALL
         SELECT 34 WEEK FROM DUAL
         UNION ALL
         SELECT 35 WEEK FROM DUAL
         UNION ALL
         SELECT 36 WEEK FROM DUAL
         UNION ALL
         SELECT 37 WEEK FROM DUAL
         UNION ALL
         SELECT 38 WEEK FROM DUAL
         UNION ALL
         SELECT 39 WEEK FROM DUAL
         UNION ALL
         SELECT 40 WEEK FROM DUAL
         UNION ALL
         SELECT 41 WEEK FROM DUAL
         UNION ALL
         SELECT 42 WEEK FROM DUAL
         UNION ALL
         SELECT 43 WEEK FROM DUAL
         UNION ALL
         SELECT 44 WEEK FROM DUAL
         UNION ALL
         SELECT 45 WEEK FROM DUAL
         UNION ALL
         SELECT 46 WEEK FROM DUAL
         UNION ALL
         SELECT 47 WEEK FROM DUAL
         UNION ALL
         SELECT 48 WEEK FROM DUAL
         UNION ALL
         SELECT 49 WEEK FROM DUAL
         UNION ALL
         SELECT 50 WEEK FROM DUAL
         UNION ALL
         SELECT 51 WEEK FROM DUAL
         UNION ALL
         SELECT 52 WEEK FROM DUAL 
         ) A
ORDER BY BAND_NAME ) M
LEFT JOIN ( 
SELECT H.BAND_ID,
         SUM (PL.QUANTITY) QUANTITY,
         PL.WEEK 
    FROM FDEIT005.EPM_PRODUCTION_PLAN PL
         INNER JOIN FDEIT005.EPM_MASTER_PRODUCTION_H H ON H.ID = PL.HEADER_ID WHERE 0=0 AND PL.YEAR={0}
GROUP BY H.BAND_ID, PL.WEEK, PL.YEAR
ORDER BY H.BAND_ID, PL.WEEK, PL.YEAR ) N ON N.BAND_ID =M.BAND_ID AND N.WEEK=M.WEEK ) A
WHERE BAND_ID={1} ", YEAR, BAND_GROUP);
            List<KapasitePlanUyum> kapasite = _planRepository.DeserializeList<KapasitePlanUyum>(sql);

            return kapasite;
        }
    }
}
