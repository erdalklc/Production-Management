using EPM.Dto.Base;
using EPM.Plan.Dto.Extensions;
using EPM.Plan.Dto.Plan;
using EPM.Plan.Repository.Repository;
using EPM.Tools.Helpers;
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
        private readonly IEgemenRepository _egemenRepository;
        private readonly ICacheService _cacheService;
        public PlanService(IPlanRepository planRepository, IEgemenRepository egemenRepository, ICacheService cacheService)
        {
            _planRepository = planRepository;
            _egemenRepository = egemenRepository;
            _cacheService = cacheService;
        }
        public object GetPlanByChart(KapasiyeUyumChart_Filter filter)
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
                             H.DEADLINE,H.SHIPMENT_DATE,
                             MR.ADI AS MARKET,
                             MR.ID AS MARKET_ID,
                             0.00 AS CREATE_MINUTE,
                             SUM (D.QUANTITY) AS URETIM_ADET,
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
                        INNER JOIN FDEIT005.EPM_PRODUCTION_PLAN PL ON PL.HEADER_ID=H.ID AND PL.MARKET_ID=MR.ID
                        WHERE 0=0 AND H.STATUS=0 AND H.APPROVAL_STATUS=1";
            sql += " AND H.BAND_ID=" + filter.BAND;
            sql += " AND PL.WEEK=" + filter.WEEK;
            sql += " AND PL.YEAR=" + filter.YEAR; 
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
                             H.SHIPMENT_DATE,
                             MR.ADI, MR.ID
                    ORDER BY H.ID) A WHERE URETIM_ADET>0";
            DataTable dt = _planRepository.QueryFill(sql);

            List<ModelSureleri> sureler = GetSureler();

            foreach (DataRow item in dt.Rows)
            {
                var Model = item["MODEL"].ToString();
                var sureL = sureler.Find(ob => ob.MODEL == Model);
                if (sureL != null)
                    item["CREATE_MINUTE"] = sureL.SURE;
            }
            string tSql = "SELECT ID FROM ( " + sql + " )B";
            EPM_PRODUCTION_SEASON_WEEKS seasonWeek = new EPM_PRODUCTION_SEASON_WEEKS();
            seasonWeek.START_WEEK = filter.WEEK-10;
            seasonWeek.START_YEAR=filter.YEAR;
            seasonWeek.END_WEEK = filter.WEEK + 10;
            seasonWeek.END_YEAR = filter.YEAR;
            if (seasonWeek.START_WEEK <= 5)
            {
                seasonWeek.START_WEEK = 45;
                seasonWeek.START_YEAR = seasonWeek.START_YEAR - 1;
            }
            else
            {
                if (seasonWeek.START_WEEK >= 42)
                {
                    seasonWeek.START_WEEK = 45;
                    seasonWeek.START_YEAR = seasonWeek.START_YEAR;
                    seasonWeek.END_YEAR = filter.YEAR + 1;
                    seasonWeek.END_WEEK = 10;
                }
            }
            if(seasonWeek.END_WEEK > 52)
            {

                seasonWeek.END_YEAR = filter.YEAR + 1;
                seasonWeek.END_WEEK = seasonWeek.END_WEEK-52;
            }
            var lastValue = 52;
            if (seasonWeek.START_YEAR == seasonWeek.END_YEAR)
                lastValue = seasonWeek.END_WEEK;
            for (int i = seasonWeek.START_WEEK; i <= lastValue; i++)
            {
                string columnName = seasonWeek.START_YEAR.ToString() + "_" + i;
                dtColumnNames.Rows.Add(columnName);
                DataColumn column = dt.Columns.Add(columnName, typeof(int));
                column.Caption = i + ". HAFTA";
                column.DefaultValue = 0;
            }
            if (seasonWeek.START_YEAR != seasonWeek.END_YEAR)
            {
                for (int i = 1; i <= seasonWeek.END_WEEK; i++)
                {

                    string columnName = seasonWeek.END_YEAR.ToString() + "_" + i;
                    if (!dt.Columns.Contains(columnName))
                    {
                        dtColumnNames.Rows.Add(columnName);
                        DataColumn column = dt.Columns.Add(columnName, typeof(int));
                        column.Caption = i + ". HAFTA";
                        column.DefaultValue = 0;
                    }
                }
            }
            dtColumnNames.DefaultView.Sort = "NAME ASC"; 
            List<EPM_PRODUCTION_PLAN> plan = _planRepository.DeserializeList<EPM_PRODUCTION_PLAN>(@"SELECT PL.ID,PL.HEADER_ID,PL.MARKET_ID,PL.WEEK,PL.YEAR,SUM(PL.QUANTITY) QUANTITY FROM FDEIT005.EPM_PRODUCTION_PLAN PL
                    INNER JOIN FDEIT005.EPM_MASTER_PRODUCTION_H H ON H.ID=PL.HEADER_ID AND H.STATUS=0 AND H.APPROVAL_STATUS=1 AND H.BAND_ID=" + filter.BAND + " AND   PL.WEEK=" + filter.WEEK + " AND PL.YEAR=" + filter.YEAR + " GROUP BY PL.ID,PL.HEADER_ID,PL.MARKET_ID,PL.WEEK,PL.YEAR");
            foreach (var item in plan)
            {
                DataRow[] row = dt.Select("ID=" + item.HEADER_ID + " AND MARKET_ID=" + item.MARKET_ID + "");
                if (row.Length > 0)
                {
                    if (dt.Columns.Contains(item.YEAR + "_" + item.WEEK))
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
            if (seasonWeek.START_YEAR != seasonWeek.END_YEAR)
                dtYear.Rows.Add(seasonWeek.END_YEAR);
            dtYear.TableName = "YEARS";
            set.Tables.Add(dt);
            set.Tables.Add(dtYear);
            set.Tables.Add(dtColumnNames);
            return JsonConvert.SerializeObject(set, Formatting.Indented);
        }
        public object GetPlan(string USER_CODE, int BRAND, int SEASON, string MODEL, string COLOR, int ORDER_TYPE, int PRODUCTION_TYPE, int FABRIC_TYPE, int PLAN_DURUM)
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
                             0.00 AS CREATE_MINUTE,
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
                    ORDER BY H.ID) A WHERE URETIM_ADET>0";
            DataTable dt = _planRepository.QueryFill(sql); 

            List<ModelSureleri> sureler = GetSureler();

            foreach (DataRow item in dt.Rows)
            {
                var Model = item["MODEL"].ToString(); 
                var sureL = sureler.Find(ob => ob.MODEL == Model);
                if (sureL != null) 
                    item["CREATE_MINUTE"] = sureL.SURE; 
            }
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
                        row[0]["PLANSIZ_ADET"] = row[0]["URETIM_ADET"].IntParse() - row[0]["PLANLANAN_ADET"].IntParse();

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

        public object[] BandWorkersUpdate(int Key, string Values)
        {
            object[] ok = new object[] { true, "Başarılı" };
            try
            {
                EPM_BAND_WORKERS detail = _planRepository.Deserialize<EPM_BAND_WORKERS>(@"SELECT * FROM FDEIT005.EPM_BAND_WORKERS D  WHERE D.ID=" + Key);
                JsonConvert.PopulateObject(Values, detail);
                _planRepository.Serialize<EPM_BAND_WORKERS>(detail);
            }
            catch (Exception ex)
            {
                ok[0] = false;
                ok[1] = "İşlemler yapılırken hatayla karşılaşıldı\n" + ex.Message;
            }
            return ok;
        }

        public object[] BandWorkMinutesUpdate(int Key, string Values)
        {
            object[] ok = new object[] { true, "Başarılı" };
            try
            {
                EPM_BAND_WORK_MINUTES detail = _planRepository.Deserialize<EPM_BAND_WORK_MINUTES>(@"SELECT * FROM FDEIT005.EPM_BAND_WORK_MINUTES D  WHERE D.ID=" + Key);
                JsonConvert.PopulateObject(Values, detail);
                _planRepository.Serialize<EPM_BAND_WORK_MINUTES>(detail);
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

            if (BAND_GROUP == 0 || YEAR == 0)
                return new List<KapasitePlanUyum>();
            List<EPM_BAND_WORKERS> workers = _planRepository.DeserializeList<EPM_BAND_WORKERS>("SELECT * FROM  FDEIT005.EPM_BAND_WORKERS WHERE YEAR=" + YEAR + "");
            List<EPM_BAND_WORK_MINUTES> workerMinutes = _planRepository.DeserializeList<EPM_BAND_WORK_MINUTES>("SELECT * FROM  FDEIT005.EPM_BAND_WORK_MINUTES WHERE YEAR=" + YEAR + "");
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

            List<ModelSureleri> sureler = GetSureler();


            List<KapasitePlanPerformans> performans = new List<KapasitePlanPerformans>();
            for (int i = 1; i < 53; i++)
                performans.Add(new KapasitePlanPerformans() { WEEK = i, PERFORMANS = 0 });

            sql = String.Format(@"SELECT H.MODEL,P.YEAR,P.WEEK,SUM(P.QUANTITY) QUANTITY FROM FDEIT005.EPM_MASTER_PRODUCTION_H H
INNER JOIN FDEIT005.EPM_PRODUCTION_PLAN P ON P.HEADER_ID=H.ID
WHERE H.BAND_ID={0} AND P.YEAR={1}
GROUP BY H.MODEL,P.YEAR,P.WEEK
ORDER BY P.YEAR,P.WEEK,H.MODEL", BAND_GROUP, YEAR);
            List<ModelPlanToplamlari> modelPlanToplamlari = _planRepository.DeserializeList<ModelPlanToplamlari>(sql);

            foreach (var item in performans)
            {
                var modelToplam = modelPlanToplamlari.FindAll(ob => ob.WEEK == item.WEEK);
                if (modelToplam != null)
                { 
                    decimal tQuantity = 0;
                    foreach (var itm in modelToplam)
                    {
                        var sure = sureler.Find(ob => ob.MODEL == itm.MODEL);
                        if (sure != null)
                        {
                            var kisiSayisi = 0;
                            int PRODUCT_GROUP_ID = _planRepository.ReadInteger("SELECT PRODUCT_GROUP FROM (SELECT PRODUCT_GROUP FROM FDEIT005.EPM_MASTER_PRODUCTION_H WHERE MODEL ='" + itm.MODEL + "'  ORDER BY ID DESC) A WHERE ROWNUM=1 ");
                            var worker = workers.Find(ob => ob.BAND_ID == BAND_GROUP && ob.WEEK == item.WEEK && ob.PRODUCT_GROUP == PRODUCT_GROUP_ID);
                            if (worker != null)
                                kisiSayisi = worker.WORKER;
                            else
                            {
                                if (BAND_GROUP == 1)
                                {
                                    switch (PRODUCT_GROUP_ID)
                                    {
                                        case 8://ELBISE
                                            kisiSayisi = 23;
                                            break;
                                        case 16://POLO
                                        case 20://TRIKO
                                            kisiSayisi = 17;
                                            break;
                                        case 21://SSHIRT
                                            kisiSayisi = 11;
                                            break;
                                        case 24://SACBANDI
                                            kisiSayisi = 3;
                                            break;
                                        case 19://SSHIRT
                                            kisiSayisi = 17;
                                            break;
                                        default:
                                            break;
                                    }
                                    kisiSayisi = 70;
                                }
                                else if (BAND_GROUP == 2)
                                    kisiSayisi = 46;
                                else if (BAND_GROUP == 3)
                                    kisiSayisi = 50;
                            }
                            if(kisiSayisi!=0)
                            tQuantity += (sure.SURE * itm.QUANTITY)/kisiSayisi;
                        }
                    }


                    var workTime = 0;
                    var work = workerMinutes.Find(ob => ob.BAND_ID == BAND_GROUP && ob.WEEK == item.WEEK);
                    if (work != null)
                        workTime = work.WORK_MINUTE;
                    else workTime = 2700;
                    decimal tOrt = ((tQuantity) / workTime);
                    item.PERFORMANS = tOrt;
                }
                else
                {
                    item.PERFORMANS = 0;
                }
            }
            foreach (var item in kapasite)
            {
                var a =performans.Find(ob=>ob.WEEK== item.WEEK);
                if (a != null)
                {
                    if (item.QUANTITY > 0)
                    {
                        item.CAPACITY_RELEASED = Convert.ToInt32((item.QUANTITY / a.PERFORMANS));
                    }
                    else item.CAPACITY_RELEASED = 0;
                }

                if (BAND_GROUP == 1)
                    item.PRODUCTION_RELEASED = _planRepository.ReadInteger("SELECT SUM(MIKTAR) FROM  FDEIT005.EPM_OPERATION_QUANTITYS WHERE OPERASYON_ID IN (5,600,845,80) AND START_YEAR=" + YEAR + " AND START_WEEK=" + item.WEEK + "");
                else if (BAND_GROUP == 2)
                    item.PRODUCTION_RELEASED = _planRepository.ReadInteger("SELECT SUM(MIKTAR) FROM  FDEIT005.EPM_OPERATION_QUANTITYS WHERE OPERASYON_ID IN (670) AND START_YEAR=" + YEAR + " AND START_WEEK=" + item.WEEK + "");
                else if (BAND_GROUP == 3)
                    item.PRODUCTION_RELEASED = _planRepository.ReadInteger("SELECT SUM(MIKTAR) FROM  FDEIT005.EPM_OPERATION_QUANTITYS WHERE OPERASYON_ID IN (9595) AND START_YEAR=" + YEAR + " AND START_WEEK=" + item.WEEK + "");

            }
           
            
            return kapasite;
        }

        public List<KapasitePlanPerformans> GetKapasitePerformansList(int YEAR, int BAND_GROUP)
        {
            if (BAND_GROUP == 0 || YEAR == 0)
                return new List<KapasitePlanPerformans>();
            List<ModelSureleri> sureler = GetSureler();
            List<EPM_BAND_WORKERS> workers = _planRepository.DeserializeList<EPM_BAND_WORKERS>("SELECT * FROM  FDEIT005.EPM_BAND_WORKERS WHERE YEAR="+YEAR+"");
            List<EPM_BAND_WORK_MINUTES> workerMinutes = _planRepository.DeserializeList<EPM_BAND_WORK_MINUTES>("SELECT * FROM  FDEIT005.EPM_BAND_WORK_MINUTES WHERE YEAR=" + YEAR + "");
             
           
            List<KapasitePlanPerformans> performans = new List<KapasitePlanPerformans>();
            for (int i = 1; i < 53; i++)
                performans.Add(new KapasitePlanPerformans() { WEEK = i, PERFORMANS = 0 });

            string sql = String.Format(@"SELECT H.MODEL,P.YEAR,P.WEEK,SUM(P.QUANTITY) QUANTITY FROM FDEIT005.EPM_MASTER_PRODUCTION_H H
INNER JOIN FDEIT005.EPM_PRODUCTION_PLAN P ON P.HEADER_ID=H.ID
WHERE H.BAND_ID={0} AND P.YEAR={1}
GROUP BY H.MODEL,P.YEAR,P.WEEK
ORDER BY P.YEAR,P.WEEK,H.MODEL", BAND_GROUP, YEAR);
            List<ModelPlanToplamlari> modelPlanToplamlari = _planRepository.DeserializeList<ModelPlanToplamlari>(sql);


            foreach (var item in performans)
            {
                var modelToplam = modelPlanToplamlari.FindAll(ob => ob.WEEK == item.WEEK);
                if (modelToplam != null)
                {
                    decimal tQuantity = 0;
                    foreach (var itm in modelToplam)
                    {
                        var sure = sureler.Find(ob => ob.MODEL == itm.MODEL);
                        if (sure != null)
                        {
                            var kisiSayisi = 0;
                            int PRODUCT_GROUP_ID = _planRepository.ReadInteger("SELECT PRODUCT_GROUP FROM (SELECT PRODUCT_GROUP FROM FDEIT005.EPM_MASTER_PRODUCTION_H WHERE MODEL ='" + itm.MODEL + "'  ORDER BY ID DESC) A WHERE ROWNUM=1 ");
                            var worker = workers.Find(ob => ob.BAND_ID == BAND_GROUP && ob.WEEK == item.WEEK && ob.PRODUCT_GROUP == PRODUCT_GROUP_ID);
                            if (worker != null)
                                kisiSayisi = worker.WORKER;
                            else
                            {
                                if (BAND_GROUP == 1)
                                {
                                    switch (PRODUCT_GROUP_ID)
                                    {
                                        case 8://ELBISE
                                            kisiSayisi = 23;
                                            break;
                                        case 16://POLO
                                        case 20://TRIKO
                                            kisiSayisi = 17;
                                            break;
                                        case 21://SSHIRT
                                            kisiSayisi = 11;
                                            break;
                                        case 24://SACBANDI
                                            kisiSayisi = 3;
                                            break;
                                        case 19://SSHIRT
                                            kisiSayisi = 17;
                                            break;
                                        default:
                                            break;
                                    }
                                    kisiSayisi = 70;
                                }
                                else if (BAND_GROUP == 2)
                                    kisiSayisi = 46;
                                else if (BAND_GROUP == 3)
                                    kisiSayisi = 50;
                            }
                            if (kisiSayisi != 0)
                                tQuantity += (sure.SURE * itm.QUANTITY) / kisiSayisi;
                        }
                    }


                    var workTime = 0;
                    var work = workerMinutes.Find(ob => ob.BAND_ID == BAND_GROUP && ob.WEEK == item.WEEK);
                    if (work != null)
                        workTime = work.WORK_MINUTE;
                    else workTime = 2700;
                    decimal tOrt = ((tQuantity) / workTime);
                    item.PERFORMANS = tOrt;
                }
                else
                {
                    item.PERFORMANS = 0;
                }
            }

            return performans;
        }

        public List<EpmBandWorkModel> GetBandWorkers(int YEAR, int BAND_GROUP,int PRODUCT_GROUP)
        {
            if (YEAR == 0 || BAND_GROUP == 0|| PRODUCT_GROUP==0)
                return new List<EpmBandWorkModel>();
            string sql = string.Format(@"
SELECT WK.ID,
       A.WEEK, 
       {0} AS BAND_ID,
       {1} AS YEAR,
       {2} AS PRODUCT_GROUP,
       (SELECT ADI FROM FDEIT005.EPM_PRODUCT_GROUP WHERE ID={2}) AS PRODUCT_GROUP_NAME,
       NVL(WK.WORKER,(CASE WHEN {0} =1 THEN 
(CASE WHEN {2} = 8 THEN 23 --ELBISE
WHEN {2}=16 THEN 17 --POLO
WHEN {2}=21 THEN 11 --TSHIRT
WHEN {2}=24 THEN 3 --SACBANDI
WHEN {2}=19 THEN 17 --SSHIRT
ELSE 0 END
)
WHEN {0}=2 THEN 46 WHEN {0}=3 THEN 50 ELSE 0 END)) AS WORKER
  FROM (SELECT 1 WEEK FROM DUAL
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
        SELECT 52 WEEK FROM DUAL) A
       LEFT JOIN FDEIT005.EPM_BAND_WORKERS WK ON WK.WEEK = A.WEEK AND WK.BAND_ID={0} AND WK.YEAR={1} AND WK.PRODUCT_GROUP = {2}
       LEFT JOIN FDEIT005.EPM_PRODUCTION_BAND_GROUP BG ON BG.ID = WK.BAND_ID
       ORDER BY WEEK
       ", BAND_GROUP, YEAR, PRODUCT_GROUP);
            return _planRepository.DeserializeList<EpmBandWorkModel>(sql);
        }

        public List<EpmBandWorkMinuteModel> GetBandWorkMinutes(int YEAR, int BAND_GROUP)
        {
            if (YEAR == 0 || BAND_GROUP == 0)
                return new List<EpmBandWorkMinuteModel>();
            string sql = string.Format(@"
SELECT WK.ID,
       A.WEEK, 
       {0} AS BAND_ID,
       {1} AS YEAR,
       NVL(WK.WORK_MINUTE,2700) AS WORK_MINUTE
  FROM (SELECT 1 WEEK FROM DUAL
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
        SELECT 52 WEEK FROM DUAL) A
       LEFT JOIN FDEIT005.EPM_BAND_WORK_MINUTES WK ON WK.WEEK = A.WEEK AND WK.BAND_ID={0} AND WK.YEAR={1}
       LEFT JOIN FDEIT005.EPM_PRODUCTION_BAND_GROUP BG ON BG.ID = WK.BAND_ID
       ORDER BY WEEK
       ", BAND_GROUP, YEAR);
            var list= _planRepository.DeserializeList<EpmBandWorkMinuteModel>(sql);
            return list;
        }

        List<ModelSureleri> GetSureler()
        {
            List<ModelSureleri> sureler = _cacheService.Get<List<ModelSureleri>>(0, "ModelSureleri");
            if (sureler == null)
            {
                string sqlSureler = @"Select RotaOperasyonlari.RotaId MODEL,sum(RotaOperasyonlari.StandartSure) SURE  from RotaOperasyonlari
  Left Join Operasyon on Operasyon.OperasyonId = RotaOperasyonlari.OperasyonId
  Left Join Kullanici IK on IK.KullaniciId = RotaOperasyonlari.InsertKullaniciId
  Left Join Kullanici SK on SK.KullaniciId = RotaOperasyonlari.KullaniciId
  Left Join KaliteLimitiStrGetir on KaliteLimitiStrGetir.KaliteLimitiId = RotaOperasyonlari.KaliteLimiti
  WHERE  (Operasyon.Adi LIKE 'G %' OR Operasyon.Adi LIKE 'XG %'  OR Operasyon.Adi LIKE 'D %'  OR Operasyon.Adi  LIKE 'Ö %' OR Operasyon.Adi  LIKE 'XÖ %') AND Operasyon.Adi NOT LIKE 'XÖ K.K.%' AND Operasyon.Adi NOT LIKE 'Ö K.K.%'
  AND Operasyon.Adi NOT LIKE 'XG K.K.%'  AND Operasyon.Adi NOT LIKE 'G K.K.%' AND Operasyon.Adi NOT LIKE 'D MONT%'
   GROUP BY RotaOperasyonlari.RotaId
   
   ";
                sureler = _egemenRepository.DeserializeList<ModelSureleri>(sqlSureler);
                _cacheService.Add(0, "ModelSureleri", sureler);
            }

            return sureler;
        }

        public List<ModelSureleriView> ProductionTimesLoad()
        {
            return JsonConvert.DeserializeObject<List<ModelSureleriView>>(JsonConvert.SerializeObject(GetSureler()));
        }

        public List<PlanStatus> GetPlanStatusList(bool hepsi)
        {
            List<PlanStatus> status = new List<PlanStatus>();
            status.Add(new PlanStatus { ID = 0, ADI = "HEPSİ" });
            status.Add(new PlanStatus { ID = 1, ADI = "PLANLANMIŞ" });
            status.Add(new PlanStatus { ID = 2, ADI = "PLANLAMA YAPILMAMIŞ" });

            return status;
        }

        public List<EPM_PRODUCT_GROUP> GetProductGroupList(int BAND_GROUP)
        {
            List<EPM_PRODUCT_GROUP> productGroups = new List<EPM_PRODUCT_GROUP>();
            if (BAND_GROUP == 1)
            {
                productGroups = _planRepository.DeserializeList<EPM_PRODUCT_GROUP>(@"SELECT DISTINCT PH.* FROM FDEIT005.EPM_MASTER_PRODUCTION_H H 
INNER JOIN FDEIT005.EPM_PRODUCT_GROUP PH ON H.PRODUCT_GROUP =PH.ID
WHERE  BRAND=1 AND H.PRODUCTION_TYPE=1 AND PH.ID NOT IN(11,12,14,20)
ORDER BY 1
");
            }
            else if (BAND_GROUP == 2)
            {
                productGroups = _planRepository.DeserializeList<EPM_PRODUCT_GROUP>(@"SELECT DISTINCT PH.* FROM FDEIT005.EPM_MASTER_PRODUCTION_H H 
INNER JOIN FDEIT005.EPM_PRODUCT_GROUP PH ON H.PRODUCT_GROUP =PH.ID
WHERE  BRAND=1 AND H.PRODUCTION_TYPE=1 AND PH.ID IN(14)
ORDER BY 1
");
            }
            else if (BAND_GROUP == 3)
            {
                productGroups = _planRepository.DeserializeList<EPM_PRODUCT_GROUP>(@"SELECT DISTINCT PH.* FROM FDEIT005.EPM_MASTER_PRODUCTION_H H 
INNER JOIN FDEIT005.EPM_PRODUCT_GROUP PH ON H.PRODUCT_GROUP =PH.ID
WHERE  BRAND=1 AND H.PRODUCTION_TYPE=1 AND PH.ID IN(11)
ORDER BY 1
");
            }

            return productGroups;
        }

        public object GetUretimGerceklesenByChart(KapasiyeUyumChart_Filter filter)
        {
            DataTable dt = new DataTable();
            if (filter.BAND == 1)
                dt = _planRepository.QueryFill("SELECT MODEL_ADI,RENK_ADI,SEZON_ADI,PAZAR_ADI,SIPARIS_TIPI,KESIM_FOYU_NO,BEDEN_ADI,START_WEEK,START_YEAR,SUM(MIKTAR) MIKTAR  FROM  FDEIT005.EPM_OPERATION_QUANTITYS WHERE OPERASYON_ID IN (5,600,845,80) AND START_YEAR=" + filter.YEAR + " AND START_WEEK=" + filter.WEEK + " GROUP BY MODEL_ADI,RENK_ADI,SEZON_ADI,PAZAR_ADI,SIPARIS_TIPI,KESIM_FOYU_NO,BEDEN_ADI,START_WEEK,START_YEAR");
            else if (filter.BAND == 2)
                dt = _planRepository.QueryFill("SELECT MODEL_ADI,RENK_ADI,SEZON_ADI,PAZAR_ADI,SIPARIS_TIPI,KESIM_FOYU_NO,BEDEN_ADI,START_WEEK,START_YEAR,SUM(MIKTAR) MIKTAR  FROM  FDEIT005.EPM_OPERATION_QUANTITYS WHERE OPERASYON_ID IN (670) AND START_YEAR=" + filter.YEAR + " AND START_WEEK=" + filter.WEEK + " GROUP BY MODEL_ADI,RENK_ADI,SEZON_ADI,PAZAR_ADI,SIPARIS_TIPI,KESIM_FOYU_NO,BEDEN_ADI,START_WEEK,START_YEAR");
            else if (filter.BAND == 3)
                dt = _planRepository.QueryFill("SELECT MODEL_ADI,RENK_ADI,SEZON_ADI,PAZAR_ADI,SIPARIS_TIPI,KESIM_FOYU_NO,BEDEN_ADI,START_WEEK,START_YEAR,SUM(MIKTAR) MIKTAR  FROM  FDEIT005.EPM_OPERATION_QUANTITYS WHERE OPERASYON_ID IN (9595) AND START_YEAR=" + filter.YEAR + " AND START_WEEK=" + filter.WEEK + " GROUP BY MODEL_ADI,RENK_ADI,SEZON_ADI,PAZAR_ADI,SIPARIS_TIPI,KESIM_FOYU_NO,BEDEN_ADI,START_WEEK,START_YEAR");

            dt.Columns.Add("ROW_ID",typeof(string));
            foreach (DataRow row in dt.Rows) 
                row["ROW_ID"] = Guid.NewGuid().ToString(); 
            return JsonConvert.SerializeObject(dt, Formatting.Indented);
        }

        public object GeKapasiteListByChart(KapasiyeUyumChart_Filter filter)
        {
            List<EPM_BAND_WORKERS> workers = _planRepository.DeserializeList<EPM_BAND_WORKERS>("SELECT * FROM  FDEIT005.EPM_BAND_WORKERS WHERE YEAR=" + filter.YEAR + "");
            List<EPM_BAND_WORK_MINUTES> workerMinutes = _planRepository.DeserializeList<EPM_BAND_WORK_MINUTES>("SELECT * FROM  FDEIT005.EPM_BAND_WORK_MINUTES WHERE YEAR=" + filter.YEAR + "");
            List<ModelSureleri> sureler = GetSureler();

            string sql = string.Format(@"SELECT H.MODEL,P.YEAR,P.WEEK,SUM(P.QUANTITY) QUANTITY,0 AS KISI,0 AS WORK_TIME,0 AS EXPECTED_QUANTITY,0.00 SURE  FROM FDEIT005.EPM_MASTER_PRODUCTION_H H
INNER JOIN FDEIT005.EPM_PRODUCTION_PLAN P ON P.HEADER_ID=H.ID
WHERE H.BAND_ID={0} AND P.YEAR={1} and P.WEEK={2}
GROUP BY H.MODEL,P.YEAR,P.WEEK
ORDER BY P.YEAR,P.WEEK,H.MODEL", filter.BAND,filter.YEAR,filter.WEEK);
            DataTable dt=_planRepository.QueryFill(sql);
            foreach (DataRow row in dt.Rows)
            {
                var sure = sureler.Find(ob => ob.MODEL == row["MODEL"].ToString());
                if (sure != null)
                {
                    var kisiSayisi = 0;
                    int PRODUCT_GROUP_ID = _planRepository.ReadInteger("SELECT PRODUCT_GROUP FROM (SELECT PRODUCT_GROUP FROM FDEIT005.EPM_MASTER_PRODUCTION_H WHERE MODEL ='" + row["MODEL"].ToString() + "'  ORDER BY ID DESC) A WHERE ROWNUM=1 ");
                    var worker = workers.Find(ob => ob.BAND_ID == filter.BAND && ob.WEEK == filter.WEEK && ob.PRODUCT_GROUP == PRODUCT_GROUP_ID);
                    if (worker != null)
                        kisiSayisi = worker.WORKER;
                    else
                    {
                        if (filter.BAND == 1)
                        {
                            switch (PRODUCT_GROUP_ID)
                            {
                                case 8://ELBISE
                                    kisiSayisi = 23;
                                    break;
                                case 16://POLO
                                case 20://TRIKO
                                    kisiSayisi = 17;
                                    break;
                                case 21://SSHIRT
                                    kisiSayisi = 11;
                                    break;
                                case 24://SACBANDI
                                    kisiSayisi = 3;
                                    break;
                                case 19://SSHIRT
                                    kisiSayisi = 17;
                                    break;
                                default:
                                    break;
                            }
                            kisiSayisi = 70;
                        }
                        else if (filter.BAND == 2)
                            kisiSayisi = 46;
                        else if (filter.BAND == 3)
                            kisiSayisi = 50;
                    }
                    if (kisiSayisi != 0)
                    {

                        var tQuantity = (sure.SURE * row["QUANTITY"].IntParse()) / kisiSayisi;
                        var workTime = 0;
                        var work = workerMinutes.Find(ob => ob.BAND_ID == filter.BAND && ob.WEEK == filter.WEEK);
                        if (work != null)
                            workTime = work.WORK_MINUTE;
                        else workTime = 2700;
                        decimal tOrt = ((tQuantity) / workTime);
                        row["SURE"] = sure.SURE;
                        row["WORK_TIME"] = workTime;
                        row["KISI"] = kisiSayisi;
                        row["EXPECTED_QUANTITY"] = Convert.ToInt32((row["QUANTITY"].IntParse() / tOrt));
                    }
                }
            }
            dt.Columns.Add("ROW_ID", typeof(string));
            foreach (DataRow row in dt.Rows)
                row["ROW_ID"] = Guid.NewGuid().ToString();
            return JsonConvert.SerializeObject(dt, Formatting.Indented);
        }
    }
}
