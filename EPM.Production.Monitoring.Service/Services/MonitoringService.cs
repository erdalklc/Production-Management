using EPM.Dto.Base;
using EPM.Production.Monitoring.Dto.Models;
using EPM.Production.Monitoring.Repository.Repository;
using EPM.Production.Monitoring.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using EPM.Tools.Helpers;

namespace EPM.Production.Monitoring.Service.Services
{
    public class MonitoringService : IMonitoringService
    {
        private readonly IMonitoringRepository _monitoringRepository;
        private readonly ICacheService _cacheService;
        private readonly IEgemenRepository _egemenRepository;

        public MonitoringService(IMonitoringRepository monitoringRepository, IEgemenRepository egemenRepository, ICacheService cacheService)
        {
            _monitoringRepository = monitoringRepository;
            _egemenRepository = egemenRepository;
            _cacheService = cacheService;
        }
        public List<HaftaModel> GetHaftaModelList()
        {
            string sql = @"SELECT TO_CHAR (TRUNC (dt, 'IW') + 6, 'IW') AS WEEK,
       TO_CHAR (TRUNC (dt, 'yyy') + 6, 'yyyy') AS YEAR,
       TRUNC (dt, 'IW') AS START_DATE,
       TRUNC (dt, 'IW') + 6 AS END_DATE
  FROM (    SELECT DATE '2020-01-01' + ( (LEVEL - 1) * 7) dt
              FROM DUAL
        CONNECT BY LEVEL <= 200)";
            return _monitoringRepository.DeserializeList<HaftaModel>(sql);
        }

        public List<HaftaModel> GetTerminList(FilterModel model)
        {
            string sql = @"SELECT DISTINCT WK.* FROM (SELECT TO_CHAR (TRUNC (dt, 'IW') + 6, 'IW') AS WEEK,
       TO_CHAR (TRUNC (dt, 'yyy') + 6, 'yyyy') AS YEAR,
       TRUNC (dt, 'IW') AS START_DATE,
       TRUNC (dt, 'IW') + 6 AS END_DATE
  FROM (    SELECT DATE '2020-01-01' + ( (LEVEL - 1) * 7) dt
              FROM DUAL
        CONNECT BY LEVEL <= 200) ) WK 
        INNER JOIN (
                SELECT P.WEEK,P.YEAR FROM FDEIT005.EPM_MASTER_PRODUCTION_H H
INNER JOIN FDEIT005.EPM_PRODUCTION_PLAN P ON P.HEADER_ID=H.ID 
WHERE 0=0 AND H.STATUS=0 _SQLFILTER_
            ) PL ON PL.WEEK=WK.WEEK AND PL.YEAR=WK.YEAR
    ORDER BY WK.YEAR,WK.WEEK
         ";
            string sqlFilter = "";
            sqlFilter += " AND H.SEASON IN(" + string.Join(',', model.SEASON.Select(ob => ob.ID).ToArray()) + ")";
            if (model.ORDER_TYPE != 0)
                sqlFilter += " AND H.ORDER_TYPE=" + model.ORDER_TYPE;
            if (model.BRAND != 0)
                sqlFilter += " AND H.BRAND=" + model.BRAND;
            if (model.PRODUCT_GROUP != 0)
                sqlFilter += " AND H.PRODUCT_GROUP=" + model.PRODUCT_GROUP;
            if (model.ORDER_TYPE != 0)
                sqlFilter += " AND H.ORDER_TYPE=" + model.ORDER_TYPE;
            if (model.BAND != 0)
                sqlFilter += " AND H.BAND_ID=" + model.BAND;
            if (model.MODEL!=null && model.MODEL != "")
                sqlFilter += " AND H.MODEL='" + model.MODEL + "'";
            if (model.COLOR != null && model.COLOR != "")
                sqlFilter += " AND H.COLOR='" + model.COLOR + "'";
            sql = sql.Replace("_SQLFILTER_", sqlFilter);
            return _monitoringRepository.DeserializeList<HaftaModel>(sql);
        }

        public List<ProductModel> GetProductList(Tuple<List<HaftaModel>, List<EPM_PRODUCT_GROUP>, List<EPM_PRODUCTION_MARKET>, FilterModel> model)
        {
            string sql = string.Format(@"SELECT * FROM (SELECT DISTINCT H.MODEL ,H.COLOR, H.ID AS HEADER_ID FROM FDEIT005.EPM_PRODUCTION_PLAN P 
INNER JOIN  FDEIT005.EPM_MASTER_PRODUCTION_H  H ON H.ID=P.HEADER_ID
INNER JOIN FDEIT005.EPM_PRODUCTION_MARKET M ON M.ID=P.MARKET_ID WHERE 0=0 AND M.ID IN ({3}) AND H.PRODUCT_GROUP IN ({2}) AND H.STATUS=0 AND P.WEEK IN ({0}) AND P.YEAR IN ({1}) _SQLFILTER_"
, string.Join(',', model.Item1.Select(ob=>ob.WEEK).Distinct().ToList()), string.Join(',', model.Item1.Select(ob => ob.YEAR).Distinct().ToList()), string.Join(',', model.Item2.Select(ob => ob.ID).Distinct().ToList()), string.Join(',', model.Item3.Select(ob => ob.ID).Distinct().ToList()));
            string sqlFilter = "";
                sqlFilter += " AND H.SEASON IN(" + string.Join(',', model.Item4.SEASON.Select(ob => ob.ID).ToArray()) + ")";
            if (model.Item4.BRAND != 0)
                sqlFilter += " AND H.BRAND=" + model.Item4.BRAND;
            if (model.Item4.ORDER_TYPE != 0)
                sqlFilter += " AND H.ORDER_TYPE=" + model.Item4.ORDER_TYPE;
            if (model.Item4.ORDER_TYPE != 0)
                sqlFilter += " AND H.ORDER_TYPE=" + model.Item4.ORDER_TYPE;
            if (model.Item4.BAND != 0)
                sqlFilter += " AND H.BAND_ID=" + model.Item4.BAND;
            if (model.Item4.MODEL != null && model.Item4.MODEL != "")
                sqlFilter += " AND H.MODEL='" + model.Item4.MODEL + "'";
            if (model.Item4.COLOR != null && model.Item4.COLOR != "")
                sqlFilter += " AND H.COLOR='" + model.Item4.COLOR + "'";
            sql = sql.Replace("_SQLFILTER_", sqlFilter);
            sql += ") A ORDER BY MODEL,COLOR";
            return _monitoringRepository.DeserializeList<ProductModel>(sql);
        }

        public Tuple<List<PlanModel>, EPM_TRACKING_PROCESS_VALUES, List<MarketReleasedModel>, List<ProductionModel>, List<PlanModel>, List<ProductionDetailPivotModel>> GetProductionDetails(Tuple<List<HaftaModel>, List<ProductModel>, FilterModel, List<EPM_PRODUCT_GROUP>, List<EPM_PRODUCTION_MARKET>> model)
        {
            string filterHeader = "";
            for (int i = 0; i < model.Item2.Count; i++)
            {
                var item = model.Item2[i];
                if (i != 0)
                    filterHeader += " UNION ALL";

                filterHeader += " SELECT '" + item.MODEL + "." + item.COLOR + "' M FROM DUAL";
            }
            string sqlPivot = string.Format(@"WITH FILTER_MODEL AS({2}) 
  SELECT YEAR,
         WEEK,
         MARKET_ID,
         MARKET_NAME,
         MODEL,
         COLOR,
         SEASON_NAME,
         ORDER_NAME,
         ORDER_TYPE,
         (SELECT SUM (QUANTITY)
            FROM FDEIT005.EPM_MASTER_PRODUCTION_D
           WHERE MARKET = A.MARKET_ID AND HEADER_ID = A.HEADER_ID)
            ORDER_QUANTITY,
         SUM (QUANTITY) AS QUANTITY,
             /*(SELECT SUM (MIKTAR)
                FROM FDEIT005.EPM_OPERATION_QUANTITYS QT
               WHERE     QT.SEZON_ADI = A.SEASON_NAME
                     AND QT.MODEL_ADI = A.MODEL
                     AND QT.RENK_ADI = A.COLOR
                     AND QT.SIPARIS_TIPI = A.ORDER_NAME 
                     AND OPERASYON_ID IN (5515, 5516, 5517))
                AS KESIM,
             (SELECT SUM (MIKTAR)
                FROM FDEIT005.EPM_OPERATION_QUANTITYS QT
               WHERE     QT.SEZON_ADI = A.SEASON_NAME
                     AND QT.MODEL_ADI = A.MODEL
                     AND QT.RENK_ADI = A.COLOR
                     AND QT.SIPARIS_TIPI = A.ORDER_NAME
                     AND QT.PAZAR_ADI = A.MARKET_NAME
                     AND OPERASYON_ID IN (8631,
                                          8632,
                                          8633,
                                          9576))
                AS TASNIF,
             (SELECT SUM (MIKTAR)
                FROM FDEIT005.EPM_OPERATION_QUANTITYS QT
               WHERE     QT.SEZON_ADI = A.SEASON_NAME
                     AND QT.MODEL_ADI = A.MODEL
                     AND QT.RENK_ADI = A.COLOR
                     AND QT.SIPARIS_TIPI = A.ORDER_NAME
                     AND QT.PAZAR_ADI = A.MARKET_NAME
                     AND OPERASYON_ID IN (5,
                                          600,
                                          9595,
                                          845,
                                          670,
                                          80))
                AS BANT,
             (SELECT SUM (MIKTAR)
                FROM FDEIT005.EPM_OPERATION_QUANTITYS QT
               WHERE     QT.SEZON_ADI = A.SEASON_NAME
                     AND QT.MODEL_ADI = A.MODEL
                     AND QT.RENK_ADI = A.COLOR
                     AND QT.SIPARIS_TIPI = A.ORDER_NAME
                     AND QT.PAZAR_ADI = A.MARKET_NAME
                     AND OPERASYON_ID IN (181,
                                          1160,
                                          745,
                                          743,
                                          744))
                AS KALITE,*/
         (SELECT SUM (BIRINCI_KALITE)
            FROM FDEIT005.EPM_PRODUCTION_EGEMEN KG
           WHERE     KG.MODEL = A.MODEL
                 AND KG.COLOR = A.COLOR
                 AND KG.MARKET = A.MARKET_ID
                 AND KG.SIPARIS_TIPI = A.ORDER_TYPE
                 AND KG.SEASON=A.SEASON)
            KALITE_GERCEKLESEN
    FROM (SELECT P.*,
                 M.EGEMEN_ADI AS MARKET_NAME,
                 H.MODEL,
                 H.COLOR,
                 (SELECT SUM (P.QUANTITY)
                    FROM FDEIT005.EPM_MASTER_PRODUCTION_D
                   WHERE MARKET = M.ID AND HEADER_ID = H.ID)
                    ORDER_QUANTITY,
                 S.EGEMEN_ADI SEASON_NAME,
                 TT.ADI AS ORDER_NAME,
                 TT.ID AS ORDER_TYPE,
                 H.SEASON
            FROM FDEIT005.EPM_PRODUCTION_PLAN P
                 INNER JOIN FDEIT005.EPM_PRODUCTION_MARKET M
                    ON M.ID = P.MARKET_ID
                 INNER JOIN FDEIT005.EPM_MASTER_PRODUCTION_H H
                    ON H.ID = P.HEADER_ID
                 INNER JOIN FDEIT005.EPM_PRODUCTION_ORDER_TYPES TT
                    ON TT.ID = H.ORDER_TYPE
                 INNER JOIN FDEIT005.EPM_PRODUCTION_SEASON S ON S.ID = H.SEASON
        WHERE H.STATUS=0 AND M.ID IN ({3}) AND H.PRODUCT_GROUP IN ({4}) AND P.YEAR IN ({0}) AND P.WEEK IN ({1}) AND H.MODEL||'.'||H.COLOR IN (SELECT M FROM FILTER_MODEL) "
, string.Join(',', model.Item1.Select(ob => ob.YEAR).Distinct().ToList())
, string.Join(',', model.Item1.Select(ob => ob.WEEK).Distinct().ToList())
, filterHeader
, string.Join(',', model.Item5.Select(ob => ob.ID).Distinct().ToList())
, string.Join(',', model.Item4.Select(ob => ob.ID).Distinct().ToList()));

            if (model.Item3.ORDER_TYPE != 0)
                sqlPivot += " AND H.ORDER_TYPE=" + model.Item3.ORDER_TYPE;
            sqlPivot += " ) A GROUP BY MARKET_ID,MARKET_NAME,MODEL,COLOR,SEASON_NAME,HEADER_ID,YEAR,WEEK,ORDER_NAME,ORDER_TYPE,SEASON";
            List<ProductionDetailPivotModel> pivotModel = _monitoringRepository.DeserializeList<ProductionDetailPivotModel>(sqlPivot);
            string sql = string.Format(@"WITH FILTER_MODEL AS({2}) 
SELECT SUM(QUANTITY) AS QUANTITY,MARKET_ID,MARKET_NAME,MODEL,COLOR,SEASON_NAME,
(SELECT SUM (QUANTITY)
          FROM FDEIT005.EPM_MASTER_PRODUCTION_D
         WHERE MARKET = A.MARKET_ID AND HEADER_ID = A.HEADER_ID)
          ORDER_QUANTITY FROM (
SELECT P.*
,M.EGEMEN_ADI AS MARKET_NAME
,H.MODEL
,H.COLOR
,(SELECT SUM(QUANTITY) FROM FDEIT005.EPM_MASTER_PRODUCTION_D WHERE MARKET=M.ID AND HEADER_ID=H.ID)  ORDER_QUANTITY
,S.EGEMEN_ADI SEASON_NAME
FROM FDEIT005.EPM_PRODUCTION_PLAN P 
INNER JOIN FDEIT005.EPM_PRODUCTION_MARKET M ON M.ID=P.MARKET_ID 
INNER JOIN FDEIT005.EPM_MASTER_PRODUCTION_H H ON H.ID=P.HEADER_ID
INNER JOIN FDEIT005.EPM_PRODUCTION_SEASON S ON S.ID=H.SEASON
WHERE H.STATUS=0 AND M.ID IN ({3}) AND H.PRODUCT_GROUP IN ({4}) AND P.YEAR IN ({0}) AND P.WEEK IN ({1}) AND H.MODEL||'.'||H.COLOR IN (SELECT M FROM FILTER_MODEL) "
, string.Join(',', model.Item1.Select(ob => ob.YEAR).Distinct().ToList())
, string.Join(',', model.Item1.Select(ob => ob.WEEK).Distinct().ToList())
, filterHeader
, string.Join(',', model.Item5.Select(ob => ob.ID).Distinct().ToList())
, string.Join(',', model.Item4.Select(ob => ob.ID).Distinct().ToList())) ;

            if (model.Item3.ORDER_TYPE != 0)
                sql += " AND H.ORDER_TYPE=" + model.Item3.ORDER_TYPE;
            sql += " ) A GROUP BY MARKET_ID,MARKET_NAME,MODEL,COLOR,SEASON_NAME,HEADER_ID";
            List<PlanModel> plan = _monitoringRepository.DeserializeList<PlanModel>(sql);
            //foreach (var item in plan)
            //    item.ORDER_QUANTITY = _monitoringRepository.ReadInteger(string.Format("SELECT SUM(QUANTITY) FROM FDEIT005.EPM_MASTER_PRODUCTION_D WHERE MARKET={0} AND HEADER_ID={1}", item.MARKET_ID, item.HEADER_ID));

            var tList = plan.Select(ob => new { ob.MODEL, ob.COLOR }).Distinct().ToList();
            string filter = "";
            string filterPazar = "";
            for (int i = 0; i < tList.Count; i++)
            {
                var item = tList[i];
                if (i != 0) 
                    filter += " UNION ALL";  
                 
                filter += " SELECT '" + item.MODEL + "." + item.COLOR + "' M FROM DUAL";
            }
            for (int i = 0; i < model.Item5.Count; i++)
            {
                var item = model.Item5[i];
                if (i != 0)
                    filterPazar += " UNION ALL";

                filterPazar += " SELECT '" + item.EGEMEN_ADI + "' M FROM DUAL";
            }
            List<PlanModel> planReturnSeasonal = new List<PlanModel>();
            List<PlanModel> planReturn = new List<PlanModel>();
            var distinctMarkets = plan.Select(ob => ob.MARKET_NAME).Distinct().ToList();
            var distinctSeason = plan.Select(ob => ob.SEASON_NAME).Distinct().ToList();
            foreach (var item in distinctMarkets)
                planReturn.Add(new PlanModel() { MARKET_NAME = item, QUANTITY = plan.FindAll(ob => ob.MARKET_NAME == item).Sum(ob => ob.QUANTITY), ORDER_QUANTITY = plan.FindAll(ob => ob.MARKET_NAME == item).Sum(ob => ob.ORDER_QUANTITY) });
            foreach (var item in distinctSeason)
                planReturnSeasonal.Add(new PlanModel() { SEASON_NAME = item, QUANTITY = plan.FindAll(ob => ob.SEASON_NAME == item).Sum(ob => ob.QUANTITY), ORDER_QUANTITY = plan.FindAll(ob => ob.SEASON_NAME == item).Sum(ob => ob.ORDER_QUANTITY),KALITE_GERCEKLESEN=0 });

            //EPM_PRODUCTION_SEASON season = _monitoringRepository.Deserialize<EPM_PRODUCTION_SEASON>(model.Item3.SEASON);

            string egemenSezon = "";
            foreach (var item in model.Item3.SEASON) 
                egemenSezon += "'" + item.EGEMEN_ADI + "',";
            egemenSezon = egemenSezon.TrimEnd(',');
            EPM_TRACKING_PROCESS_VALUES values = new EPM_TRACKING_PROCESS_VALUES();
            string pSiparisTipi = "";
            string pSiparisTipiINT = "";
            string pSiparisTipiORDER = "";
            if (model.Item3.ORDER_TYPE != 0)
            {
                pSiparisTipiINT = " AND PG.SIPARIS_TIPI=" + model.Item3.ORDER_TYPE + "";
                pSiparisTipiORDER = " AND H.ORDER_TYPE=" + model.Item3.ORDER_TYPE + "";
                if (model.Item3.ORDER_TYPE == 1) 
                    pSiparisTipi += " AND SIPARIS_TIPI='FIST'"; 
                else 
                    pSiparisTipi += " AND SIPARIS_TIPI='RPT'"; 
            }
        
            

            List<MarketReleasedModel> valuesReleased = new List<MarketReleasedModel>();
            List<MarketReleasedModel> kesimGM = _monitoringRepository.DeserializeList<MarketReleasedModel>("WITH FILTER_MODEL AS("+ filter + ")  SELECT 'KESİM' TYPE,PAZAR_ADI,SUM(MIKTAR) MIKTAR,SEZON_ADI FROM FDEIT005.EPM_OPERATION_QUANTITYS WHERE SEZON_ADI IN (" + egemenSezon + ") "+ pSiparisTipi + " AND OPERASYON_ID IN (5515,5516,5517) AND MODEL_ADI||'.'||RENK_ADI IN (SELECT M FROM FILTER_MODEL) GROUP BY PAZAR_ADI,SEZON_ADI");
            List<MarketReleasedModel> tasnifGM = _monitoringRepository.DeserializeList<MarketReleasedModel>("WITH FILTER_MODEL AS(" + filter + ") ,FILTER_PAZAR AS(" + filterPazar + ") SELECT 'TASNİF' TYPE,PAZAR_ADI,SUM(MIKTAR) MIKTAR,SEZON_ADI FROM FDEIT005.EPM_OPERATION_QUANTITYS WHERE PAZAR_ADI IN (SELECT M FROM FILTER_PAZAR)  " + pSiparisTipi + "  AND SEZON_ADI IN (" + egemenSezon + ") AND OPERASYON_ID IN (8631,8632,8633,9576) AND MODEL_ADI||'.'||RENK_ADI IN (SELECT M FROM FILTER_MODEL) GROUP BY PAZAR_ADI,SEZON_ADI");
            List<MarketReleasedModel> bantGM = _monitoringRepository.DeserializeList<MarketReleasedModel>("WITH FILTER_MODEL AS(" + filter + ")  ,FILTER_PAZAR AS(" + filterPazar + ") SELECT 'BANT' TYPE,PAZAR_ADI,SUM(MIKTAR) MIKTAR,SEZON_ADI FROM FDEIT005.EPM_OPERATION_QUANTITYS WHERE PAZAR_ADI IN (SELECT M FROM FILTER_PAZAR) " + pSiparisTipi + "  AND SEZON_ADI IN (" + egemenSezon + ") AND OPERASYON_ID IN (5,600,9595,845,670,80) AND MODEL_ADI||'.'||RENK_ADI IN (SELECT M FROM FILTER_MODEL) GROUP BY PAZAR_ADI,SEZON_ADI");
            List<MarketReleasedModel> kaliteGM = _monitoringRepository.DeserializeList<MarketReleasedModel>("WITH FILTER_MODEL AS(" + filter + ") ,FILTER_PAZAR AS(" + filterPazar + ") SELECT 'KALİTE' TYPE,PAZAR_ADI,SUM(MIKTAR) MIKTAR,SEZON_ADI FROM FDEIT005.EPM_OPERATION_QUANTITYS WHERE PAZAR_ADI IN (SELECT M FROM FILTER_PAZAR) " + pSiparisTipi + "  AND SEZON_ADI IN (" + egemenSezon + ") AND OPERASYON_ID IN (181,1160,745,743,744) AND MODEL_ADI||'.'||RENK_ADI IN (SELECT M FROM FILTER_MODEL)  GROUP BY PAZAR_ADI,SEZON_ADI");
       
            List<MarketReleasedModel> kaliteGerceklesenGM = _monitoringRepository.DeserializeList<MarketReleasedModel>(string.Format(@"
WITH FILTER_MODEL AS({0}) 
,FILTER_PAZAR AS({3})
SELECT 'KALİTEG' TYPE
,M.EGEMEN_ADI PAZAR_ADI 
,S.EGEMEN_ADI SEZON_ADI
,SUM(BIRINCI_KALITE) AS MIKTAR FROM FDEIT005.EPM_PRODUCTION_EGEMEN PG
INNER JOIN FDEIT005.EPM_PRODUCTION_MARKET M ON PG.MARKET=M.ID
INNER JOIN FDEIT005.EPM_PRODUCTION_SEASON S ON S.ID=PG.SEASON 
WHERE S.EGEMEN_ADI IN({1}) AND M.ID IN ({2}) {4}  AND PG.MODEL||'.'||PG.COLOR IN (SELECT M FROM FILTER_MODEL)  AND M.EGEMEN_ADI IN (SELECT M FROM FILTER_PAZAR)
GROUP BY M.EGEMEN_ADI,S.EGEMEN_ADI"
, filter
,egemenSezon
, string.Join(',', model.Item5.Select(ob => ob.ID).Distinct().ToList()), filterPazar
, pSiparisTipiINT));
            values.KESIM = kesimGM.Sum(ob => ob.MIKTAR);
            values.TASNIF = tasnifGM.Sum(ob => ob.MIKTAR);
            values.BANT = bantGM.Sum(ob => ob.MIKTAR);
            values.KALITE = kaliteGM.Sum(ob => ob.MIKTAR);
           

            foreach (var item in planReturnSeasonal)
            {
                item.KALITE_GERCEKLESEN = kaliteGerceklesenGM.FindAll(ob => ob.SEZON_ADI == item.SEASON_NAME).ToList().Sum(ob => ob.MIKTAR);
                item.KESIM = kesimGM.FindAll(ob => ob.SEZON_ADI == item.SEASON_NAME).ToList().Sum(ob => ob.MIKTAR);
                item.TASNIF = tasnifGM.FindAll(ob => ob.SEZON_ADI == item.SEASON_NAME).ToList().Sum(ob => ob.MIKTAR);
                item.BANT = bantGM.FindAll(ob => ob.SEZON_ADI == item.SEASON_NAME).ToList().Sum(ob => ob.MIKTAR);
                item.KALITE = kaliteGM.FindAll(ob => ob.SEZON_ADI == item.SEASON_NAME).ToList().Sum(ob => ob.MIKTAR);
            }
            List<MarketReleasedModel> kesimGML = new List<MarketReleasedModel>();
            List<MarketReleasedModel> tasnifGML = new List<MarketReleasedModel>();
            List<MarketReleasedModel> bantGML = new List<MarketReleasedModel>();
            List<MarketReleasedModel> kaliteGML = new List<MarketReleasedModel>();
            List<MarketReleasedModel> kaliteGerceklesenGML = new List<MarketReleasedModel>();

            var distinctMarketKesimM = kesimGM.Select(ob => ob.PAZAR_ADI).Distinct().ToList();
            foreach (var item in distinctMarketKesimM) 
                kesimGML.Add(new MarketReleasedModel { TYPE = "KESİM", PAZAR_ADI = item, MIKTAR = kesimGM.FindAll(ob=>ob.PAZAR_ADI==item).ToList().Sum(ob => ob.MIKTAR) });

            var distinctMarketTasnifM = tasnifGM.Select(ob => ob.PAZAR_ADI).Distinct().ToList();
            foreach (var item in distinctMarketTasnifM)
                tasnifGML.Add(new MarketReleasedModel { TYPE = "TASNİF", PAZAR_ADI = item, MIKTAR = tasnifGM.FindAll(ob => ob.PAZAR_ADI == item).ToList().Sum(ob => ob.MIKTAR) });

            var distinctMarketBantM = bantGM.Select(ob => ob.PAZAR_ADI).Distinct().ToList();
            foreach (var item in distinctMarketBantM)
                bantGML.Add(new MarketReleasedModel { TYPE = "BANT", PAZAR_ADI = item, MIKTAR = bantGM.FindAll(ob => ob.PAZAR_ADI == item).ToList().Sum(ob => ob.MIKTAR) });

            var distinctMarketKaliteM = kaliteGM.Select(ob => ob.PAZAR_ADI).Distinct().ToList();
            foreach (var item in distinctMarketKaliteM)
                kaliteGML.Add(new MarketReleasedModel { TYPE = "KALİTE", PAZAR_ADI = item, MIKTAR = kaliteGM.FindAll(ob => ob.PAZAR_ADI == item).ToList().Sum(ob => ob.MIKTAR) });

            var distinctMarketKaliteGM = kaliteGerceklesenGM.Select(ob => ob.PAZAR_ADI).Distinct().ToList();
            foreach (var item in distinctMarketKaliteGM)
                kaliteGerceklesenGML.Add(new MarketReleasedModel { TYPE = "KALİTEG", PAZAR_ADI = item, MIKTAR = kaliteGerceklesenGM.FindAll(ob => ob.PAZAR_ADI == item).ToList().Sum(ob => ob.MIKTAR) });

            valuesReleased.AddRange(kesimGML);
            valuesReleased.AddRange(tasnifGML);
            valuesReleased.AddRange(bantGML);
            valuesReleased.AddRange(kaliteGML);
            valuesReleased.AddRange(kaliteGerceklesenGML);

            sql = string.Format(@" 
WITH FILTER_MODEL AS({1}) 
SELECT A.*,CASE
          WHEN SATIN_ALMA_BAGLANTI > 0
          THEN
             CASE
                WHEN TANIMLANAN = 0
                THEN
                   'TAKIP BASLATILMADI'
                ELSE
                   CASE
                      WHEN TAMAMLANAN = TANIMLANAN
                      THEN
                         'TAMAMLANDI'
                      ELSE
                         CASE
                            WHEN LAST_STATE IS NULL THEN 'YOK'
                            ELSE LAST_STATE
                         END
                   END
             END
          ELSE
             'SATIN ALMA EŞLEŞTİRİLMEDİ'
       END
          AS PROCESS_INFO FROM (
SELECT H.*,
CASE WHEN H.PRODUCTION_TYPE =1 THEN
CASE WHEN RECIPE NOT IN (1,2,3)  THEN (SELECT PROCESS_NAME
                  FROM (  SELECT PI.PROCESS_NAME,
                                 PL.END_DATE,
                                 PL.PO_HEADER_ID,
                                 PL.HEADER_ID,
                                 RD.HEADER_ID AS RECETE_ID,
                                 (SELECT COUNT (*)
                                    FROM FDEIT005.EPM_PRODUCTION_TRACKING_LIST PL2
                                   WHERE     STATUS = 2
                                         AND PL2.PO_HEADER_ID = PL.PO_HEADER_ID)
                                    AS TAMAMLANAN,
                                 (SELECT COUNT (*)
                                    FROM FDEIT005.EPM_PRODUCTION_TRACKING_LIST PL2
                                   WHERE PL2.PO_HEADER_ID = PL.PO_HEADER_ID)
                                    AS TANIMLANAN
                            FROM FDEIT005.EPM_PRODUCTION_TRACKING_LIST PL
                                 INNER JOIN
                                 FDEIT005.EPM_PRODUCTION_PROCESS_INFO PI
                                    ON PI.ID = PL.PROCESS_ID
                                 INNER JOIN
                                 FDEIT005.EPM_PRODUCTION_RECIPE_DETAIL RD
                                    ON RD.PROCESS_ID = PL.PROCESS_ID
                           WHERE PL.STATUS = 1
                        ORDER BY PL.PO_HEADER_ID, RD.QUEUE) DE
                 WHERE     DE.HEADER_ID = H.ID
                       AND DE.RECETE_ID = H.RECIPE
                       AND ROWNUM = 1) ELSE '' END
ELSE '' END  LAST_STATE,
CASE
                  WHEN RECIPE IN (1, 2, 3)
                  THEN
                     (SELECT COUNT (*)
                        FROM FDEIT005.EPM_MASTER_PRODUCTION_ORDERS O
                       WHERE O.HEADER_ID = H.ID)
                  ELSE
                     3
               END
                  AS SATIN_ALMA_BAGLANTI
                  ,   CASE WHEN RECIPE IN (1,2,3)  THEN (SELECT COUNT (*)
                  FROM FDEIT005.EPM_PRODUCTION_TRACKING_LIST PL2
                 WHERE STATUS IN (2, 4) AND PL2.HEADER_ID = H.ID)
                  ELSE 0 END AS TAMAMLANAN,
              CASE WHEN RECIPE IN (1,2,3)  THEN (SELECT COUNT (*)
                  FROM FDEIT005.EPM_PRODUCTION_TRACKING_LIST PL2
                 WHERE PL2.HEADER_ID = H.ID)
                  ELSE 0 END AS TANIMLANAN
,(SELECT SUM(QUANTITY) FROM FDEIT005.EPM_MASTER_PRODUCTION_D D WHERE D.HEADER_ID=H.ID AND D.STATUS=0 AND D.MARKET IN ({2})) QUANTITY
FROM FDEIT005.EPM_MASTER_PRODUCTION_H H WHERE   H.PRODUCT_GROUP IN ({3}) {4} AND H.SEASON IN({0}) AND H.STATUS=0 AND H.MODEL||'.'||H.COLOR IN (SELECT M FROM FILTER_MODEL) ) A 
 ", string.Join(',',model.Item3.SEASON.Select(ob=>ob.ID).ToArray())
 , filterHeader
, string.Join(',', model.Item5.Select(ob => ob.ID).Distinct().ToList())
, string.Join(',', model.Item4.Select(ob => ob.ID).Distinct().ToList())
, pSiparisTipiORDER);

            foreach (var item in planReturnSeasonal)
                item.SEASON_NAME = _monitoringRepository.ReadString("SELECT ADI FROM FDEIT005.EPM_PRODUCTION_SEASON WHERE EGEMEN_ADI='" + item.SEASON_NAME + "'");
            List<ProductionModel> productionModel = _monitoringRepository.DeserializeList<ProductionModel>(sql);
            return new Tuple<List<PlanModel>, EPM_TRACKING_PROCESS_VALUES, List<MarketReleasedModel>, List<ProductionModel>, List<PlanModel>, List<ProductionDetailPivotModel>>(planReturn, values, valuesReleased, productionModel, planReturnSeasonal,pivotModel);

        }

        public EPM_TRACKING_PROCESS_VALUES GetProductionDetailsByDate(Tuple<List<HaftaModel>, List<ProductModel>, FilterModel, DateTime> model)
        {
            string egemenSezon = "";
            foreach (var item in model.Item3.SEASON)
                egemenSezon += "'" + item.EGEMEN_ADI + "',";
            egemenSezon = egemenSezon.TrimEnd(',');
            string filterHeader = "";
            for (int i = 0; i < model.Item2.Count; i++)
            {
                var item = model.Item2[i];

                filterHeader += " (H.MODEL='" + item.MODEL + "' AND H.COLOR='" + item.COLOR + "')";
                if (i != model.Item2.Count - 1)
                    filterHeader += " OR";
            }

            List<PlanModel> plan = _monitoringRepository.DeserializeList<PlanModel>(string.Format(@"SELECT P.*,M.EGEMEN_ADI AS MARKET_NAME,H.MODEL,H.COLOR FROM FDEIT005.EPM_PRODUCTION_PLAN P 
INNER JOIN FDEIT005.EPM_PRODUCTION_MARKET M ON M.ID=P.MARKET_ID 
INNER JOIN FDEIT005.EPM_MASTER_PRODUCTION_H H ON H.ID=P.HEADER_ID
WHERE P.YEAR IN ({0}) AND P.WEEK IN ({1})  AND ({2})"
, string.Join(',', model.Item1.Select(ob => ob.YEAR).Distinct().ToList()), string.Join(',', model.Item1.Select(ob => ob.WEEK).Distinct().ToList()), filterHeader));
            foreach (var item in plan)
                item.ORDER_QUANTITY = _monitoringRepository.ReadInteger(string.Format("SELECT SUM(QUANTITY) FROM FDEIT005.EPM_MASTER_PRODUCTION_D WHERE MARKET={0} AND HEADER_ID={1}", item.MARKET_ID, item.HEADER_ID));

            var tList = plan.Select(ob => new { ob.MODEL, ob.COLOR }).Distinct().ToList();
            string filter = "";
            string filterPazarsiz = "";
            for (int i = 0; i < tList.Count; i++)
            {
                var item = tList[i];
                var ttList = plan.FindAll(ob => ob.COLOR == item.COLOR && ob.MODEL == item.MODEL).Distinct().ToList().Select(ob => ob.MARKET_NAME).ToList();
                string pazar = "";
                foreach (var p in ttList)
                    pazar += "'" + p + "',";
                pazar = pazar.TrimEnd(',');
                filter += " ( Model.kisaadi='" + item.MODEL + "' AND Renk.TransferKodu='" + item.COLOR + "' AND Pazar.Adi IN (" + pazar + "))";
                filterPazarsiz += " ( Model.kisaadi='" + item.MODEL + "' AND Renk.TransferKodu='" + item.COLOR + "')";
                if (i != tList.Count - 1)
                {
                    filter += " OR";
                    filterPazarsiz += " OR";
                }
            }  
            EPM_TRACKING_PROCESS_VALUES values = new EPM_TRACKING_PROCESS_VALUES();
            values.KESIM = _egemenRepository.ReadInteger(new EgemenDevanlayHelper().KesimAdediniGetirByDateIN(egemenSezon, model.Item4.Date, model.Item4.Date.AddDays(1).AddSeconds(-1), filterPazarsiz));
            values.TASNIF = _egemenRepository.ReadInteger(new EgemenDevanlayHelper().TasnifAdediniGetirByDateIN(egemenSezon, model.Item4.Date, model.Item4.Date.AddDays(1).AddSeconds(-1),filter));
            values.KALITE = _egemenRepository.ReadInteger(new EgemenDevanlayHelper().KaliteAdediniGetirByDateIN(egemenSezon, model.Item4.Date, model.Item4.Date.AddDays(1).AddSeconds(-1),filter));
            values.BANT = _egemenRepository.ReadInteger(new EgemenDevanlayHelper().BantAdediniGetirByDateIN(egemenSezon, model.Item4.Date, model.Item4.Date.AddDays(1).AddSeconds(-1),filter));
            return values;

        }

        public List<EPM_PRODUCTION_MARKET> GetMarketList()
        {
            List<EPM_PRODUCTION_MARKET> market;
            market = _cacheService.Get<List<EPM_PRODUCTION_MARKET>>(6, "EPM_PRODUCTION_MARKET");
            if (market == null)
            {
                market = _monitoringRepository.DeserializeList<EPM_PRODUCTION_MARKET>("SELECT * FROM FDEIT005.EPM_PRODUCTION_MARKET"); 
                _cacheService.Add(6, "EPM_PRODUCTION_MARKET", market);
            }
            return market;
        }

        public List<EPM_PRODUCT_GROUP> GetProductGroup()
        {
            List<EPM_PRODUCT_GROUP> groups;

            groups = _cacheService.Get<List<EPM_PRODUCT_GROUP>>(6, "EPM_PRODUCT_GROUP");
            if (groups == null)
            {
                groups = _monitoringRepository.DeserializeList<EPM_PRODUCT_GROUP>("SELECT * FROM FDEIT005.EPM_PRODUCT_GROUP");
                _cacheService.Add(6, "EPM_PRODUCT_GROUP", groups);
            }
            return groups; 
        }

        public List<ProductionDetailModel> GetProductionDetailsList(Tuple<List<HaftaModel>, List<ProductModel>, FilterModel, List<EPM_PRODUCT_GROUP>, List<EPM_PRODUCTION_MARKET>, ProductionDetail> model)
        {
            string egemenSezon = "";
            foreach (var item in model.Item3.SEASON)
                egemenSezon += "'" + item.EGEMEN_ADI + "',";
            egemenSezon = egemenSezon.TrimEnd(',');
            List<ProductionDetailModel> list = new List<ProductionDetailModel>();
            string filterHeader = "";
            for (int i = 0; i < model.Item2.Count; i++)
            {
                var item = model.Item2[i];
                if (i != 0)
                    filterHeader += " UNION ALL";

                filterHeader += " SELECT '" + item.MODEL + "." + item.COLOR + "' M FROM DUAL";
            }

            string sql = string.Format(@"WITH FILTER_MODEL AS({2}) SELECT P.*
,M.EGEMEN_ADI AS MARKET_NAME
,H.MODEL
,H.COLOR
,(SELECT SUM(QUANTITY) FROM FDEIT005.EPM_MASTER_PRODUCTION_D WHERE MARKET=M.ID AND HEADER_ID=H.ID)  ORDER_QUANTITY
FROM FDEIT005.EPM_PRODUCTION_PLAN P 
INNER JOIN FDEIT005.EPM_PRODUCTION_MARKET M ON M.ID=P.MARKET_ID 
INNER JOIN FDEIT005.EPM_MASTER_PRODUCTION_H H ON H.ID=P.HEADER_ID
WHERE H.STATUS=0 AND M.ID IN ({3}) AND H.PRODUCT_GROUP IN ({4}) AND P.YEAR IN ({0}) AND P.WEEK IN ({1}) AND H.MODEL||'.'||H.COLOR IN (SELECT M FROM FILTER_MODEL) "
, string.Join(',', model.Item1.Select(ob => ob.YEAR).Distinct().ToList())
, string.Join(',', model.Item1.Select(ob => ob.WEEK).Distinct().ToList())
, filterHeader
, string.Join(',', model.Item5.Select(ob => ob.ID).Distinct().ToList())
, string.Join(',', model.Item4.Select(ob => ob.ID).Distinct().ToList()));
            List<PlanModel> plan = _monitoringRepository.DeserializeList<PlanModel>(sql);
           

            var tList = plan.Select(ob => new { ob.MODEL, ob.COLOR }).Distinct().ToList();
            string filter = "";
            string filterPazar = "";
            for (int i = 0; i < tList.Count; i++)
            {
                var item = tList[i];
                if (i != 0)
                    filter += " UNION ALL";

                filter += " SELECT '" + item.MODEL + "." + item.COLOR + "' M FROM DUAL";
            }
            if (model.Item6.MARKET != "")
            { 
                filterPazar += " SELECT '" + model.Item6.MARKET + "' M FROM DUAL";
            }
            else
            {
                for (int i = 0; i < model.Item5.Count; i++)
                {
                    var item = model.Item5[i];
                    if (i != 0)
                        filterPazar += " UNION ALL";

                    filterPazar += " SELECT '" + item.EGEMEN_ADI + "' M FROM DUAL";
                }
            } 


            List<MarketReleasedModel> valuesReleased = new List<MarketReleasedModel>();
            switch (model.Item6.OPERATION)
            {
                case "KESİM":
                    list = _monitoringRepository.DeserializeList<ProductionDetailModel>(@"WITH FILTER_MODEL AS(" + filter + ")  SELECT MODEL_ADI MODEL,RENK_ADI RENK,BEDEN_ADI BEDEN, SEZON_ADI SEZON,PAZAR_ADI PAZAR,SIPARIS_TIPI,KESIM_FOYU_NO,KESIM_FOYU_NOT,MIKTAR,START_WEEK,START_YEAR FROM FDEIT005.EPM_OPERATION_QUANTITYS WHERE SEZON_ADI IN("+egemenSezon+") AND OPERASYON_ID IN (5515,5516,5517) AND MODEL_ADI||'.'||RENK_ADI IN (SELECT M FROM FILTER_MODEL)");
                    break;
                case "TASNİF":
                    list = _monitoringRepository.DeserializeList<ProductionDetailModel>("WITH FILTER_MODEL AS(" + filter + ") ,FILTER_PAZAR AS(" + filterPazar + ") SELECT MODEL_ADI MODEL,RENK_ADI RENK,BEDEN_ADI BEDEN, SEZON_ADI SEZON,PAZAR_ADI PAZAR,SIPARIS_TIPI,KESIM_FOYU_NO,KESIM_FOYU_NOT,MIKTAR,START_WEEK,START_YEAR FROM FDEIT005.EPM_OPERATION_QUANTITYS WHERE PAZAR_ADI IN (SELECT M FROM FILTER_PAZAR) AND SEZON_ADI IN(" + egemenSezon + ")  AND OPERASYON_ID IN (8631,8632,8633,9576) AND MODEL_ADI||'.'||RENK_ADI IN (SELECT M FROM FILTER_MODEL)");
                    break;
                case "BANT":
                    list = _monitoringRepository.DeserializeList<ProductionDetailModel>("WITH FILTER_MODEL AS(" + filter + ") ,FILTER_PAZAR AS(" + filterPazar + ") SELECT MODEL_ADI MODEL,RENK_ADI RENK,BEDEN_ADI BEDEN, SEZON_ADI SEZON,PAZAR_ADI PAZAR,SIPARIS_TIPI,KESIM_FOYU_NO,KESIM_FOYU_NOT,MIKTAR,START_WEEK,START_YEAR FROM FDEIT005.EPM_OPERATION_QUANTITYS WHERE PAZAR_ADI IN (SELECT M FROM FILTER_PAZAR) AND SEZON_ADI IN(" + egemenSezon + ")  AND OPERASYON_ID IN (5,600,9595,845,670,80) AND MODEL_ADI||'.'||RENK_ADI IN (SELECT M FROM FILTER_MODEL)");
                    break;
                case "KALİTE":
                    list = _monitoringRepository.DeserializeList<ProductionDetailModel>("WITH FILTER_MODEL AS(" + filter + ") ,FILTER_PAZAR AS(" + filterPazar + ") SELECT MODEL_ADI MODEL,RENK_ADI RENK,BEDEN_ADI BEDEN, SEZON_ADI SEZON,PAZAR_ADI PAZAR,SIPARIS_TIPI,KESIM_FOYU_NO,KESIM_FOYU_NOT,MIKTAR,START_WEEK,START_YEAR FROM FDEIT005.EPM_OPERATION_QUANTITYS WHERE PAZAR_ADI IN (SELECT M FROM FILTER_PAZAR) AND SEZON_ADI IN(" + egemenSezon + ")  AND OPERASYON_ID IN (181,1160,745,743,744) AND MODEL_ADI||'.'||RENK_ADI IN (SELECT M FROM FILTER_MODEL)");
                    break;
                case "KALİTE GERÇEKLEŞEN":
                    list = _monitoringRepository.DeserializeList<ProductionDetailModel>(string.Format(@"
WITH FILTER_MODEL AS({0}) 
,FILTER_PAZAR AS({3})
SELECT 
PG.BIRINCI_KALITE AS MIKTAR,
PG.IKINCI_KALITE AS IKINCI_KALITE,
PG.KESIM_FOYU_NO,
PG.PRODUCT_SIZE AS BEDEN,
PG.MODEL AS MODEL,
PG.COLOR AS RENK,
S.ADI AS SEZON,
M.EGEMEN_ADI PAZAR,
O.ADI AS SIPARIS_TIPI,
START_WEEK,
START_YEAR
FROM FDEIT005.EPM_PRODUCTION_EGEMEN PG
INNER JOIN FDEIT005.EPM_PRODUCTION_MARKET M ON PG.MARKET=M.ID
INNER JOIN FDEIT005.EPM_PRODUCTION_SEASON S ON S.ID=PG.SEASON 
INNER JOIN FDEIT005.EPM_PRODUCTION_ORDER_TYPES O ON O.ID=PG.SIPARIS_TIPI
WHERE S.EGEMEN_ADI IN ({1}) AND M.ID IN ({2})   AND PG.MODEL||'.'||PG.COLOR IN (SELECT M FROM FILTER_MODEL)  AND M.EGEMEN_ADI IN (SELECT M FROM FILTER_PAZAR)
"
, filter
, egemenSezon
, string.Join(',', model.Item5.Select(ob => ob.ID).Distinct().ToList()), filterPazar
));
                    break;
                default:
                    break;
            }
             
            return list;
        }
    }
}
