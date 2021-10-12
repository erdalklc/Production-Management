using EPM.Dto.Base;
using EPM.Production.Monitoring.Dto.Models;
using EPM.Production.Monitoring.Repository.Repository;
using EPM.Production.Monitoring.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace EPM.Production.Monitoring.Service.Services
{
    public class MonitoringService : IMonitoringService
    {
        private readonly IMonitoringRepository _monitoringRepository;
        private readonly IEgemenRepository _egemenRepository;
        public MonitoringService(IMonitoringRepository monitoringRepository, IEgemenRepository egemenRepository)
        {
            _monitoringRepository = monitoringRepository;
            _egemenRepository = egemenRepository;
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
WHERE 0=0 _SQLFILTER_
            ) PL ON PL.WEEK=WK.WEEK AND PL.YEAR=WK.YEAR
    ORDER BY WK.WEEK,WK.YEAR
         ";
            string sqlFilter = "";
            if (model.SEASON != 0)
                sqlFilter += " AND H.SEASON=" + model.SEASON;
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

        public List<ProductModel> GetProductList(Tuple<List<HaftaModel>, FilterModel> model)
        {
            string sql = string.Format(@"SELECT * FROM (SELECT DISTINCT H.MODEL ,H.COLOR, H.ID AS HEADER_ID FROM FDEIT005.EPM_PRODUCTION_PLAN P 
INNER JOIN  FDEIT005.EPM_MASTER_PRODUCTION_H  H ON H.ID=P.HEADER_ID
INNER JOIN FDEIT005.EPM_PRODUCTION_MARKET M ON M.ID=P.MARKET_ID WHERE 0=0 AND P.WEEK IN ({0}) AND P.YEAR IN ({1}) _SQLFILTER_", string.Join(',', model.Item1.Select(ob=>ob.WEEK).Distinct().ToList()), string.Join(',', model.Item1.Select(ob => ob.YEAR).Distinct().ToList()));
            string sqlFilter = "";
            if (model.Item2.SEASON != 0)
                sqlFilter += " AND H.SEASON=" + model.Item2.SEASON;
            if (model.Item2.BRAND != 0)
                sqlFilter += " AND H.BRAND=" + model.Item2.BRAND;
            if (model.Item2.PRODUCT_GROUP != 0)
                sqlFilter += " AND H.PRODUCT_GROUP=" + model.Item2.PRODUCT_GROUP;
            if (model.Item2.ORDER_TYPE != 0)
                sqlFilter += " AND H.ORDER_TYPE=" + model.Item2.ORDER_TYPE;
            if (model.Item2.BAND != 0)
                sqlFilter += " AND H.BAND_ID=" + model.Item2.BAND;
            if (model.Item2.MODEL != null && model.Item2.MODEL != "")
                sqlFilter += " AND H.MODEL='" + model.Item2.MODEL + "'";
            if (model.Item2.COLOR != null && model.Item2.COLOR != "")
                sqlFilter += " AND H.COLOR='" + model.Item2.COLOR + "'";
            sql = sql.Replace("_SQLFILTER_", sqlFilter);
            sql += ") A ORDER BY MODEL,COLOR";
            return _monitoringRepository.DeserializeList<ProductModel>(sql);
        }

        public Tuple<List<PlanModel>, EPM_TRACKING_PROCESS_VALUES, List<MarketReleasedModel>> GetProductionDetails(Tuple<List<HaftaModel>, List<ProductModel>, FilterModel> model)
        {
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
                filter += " (MODEL_ADI='" + item.MODEL + "' AND RENK_ADI='" + item.COLOR + "' AND PAZAR_ADI IN (" + pazar + "))";
                filterPazarsiz += " (MODEL_ADI='" + item.MODEL + "' AND RENK_ADI='" + item.COLOR + "')";
                if (i != tList.Count - 1)
                {
                    filter += " OR";
                    filterPazarsiz += " OR";
                }
            }
            List<PlanModel> planReturn = new List<PlanModel>();
            var distinctMarkets = plan.Select(ob => ob.MARKET_NAME).Distinct().ToList();
            foreach (var item in distinctMarkets)
                planReturn.Add(new PlanModel() { MARKET_NAME = item, QUANTITY = plan.FindAll(ob => ob.MARKET_NAME == item).Sum(ob => ob.QUANTITY), ORDER_QUANTITY = plan.FindAll(ob => ob.MARKET_NAME == item).Sum(ob => ob.ORDER_QUANTITY) });
            EPM_PRODUCTION_SEASON season = _monitoringRepository.Deserialize<EPM_PRODUCTION_SEASON>(model.Item3.SEASON);

            EPM_TRACKING_PROCESS_VALUES values = new EPM_TRACKING_PROCESS_VALUES();

            List<MarketReleasedModel> valuesReleased = new List<MarketReleasedModel>();
            List<MarketReleasedModel> kesimGM = _monitoringRepository.DeserializeList<MarketReleasedModel>("SELECT 'KESİM' TYPE,PAZAR_ADI,SUM(MIKTAR) MIKTAR FROM FDEIT005.EPM_OPERATION_QUANTITYS WHERE SEZON_ADI='" + season.EGEMEN_ADI + "' AND OPERASYON_ID IN (5515,5516,5517) AND (" + filterPazarsiz + ")  GROUP BY PAZAR_ADI");
            List<MarketReleasedModel> tasnifGM = _monitoringRepository.DeserializeList<MarketReleasedModel>("SELECT 'TASNİF' TYPE,PAZAR_ADI,SUM(MIKTAR) MIKTAR FROM FDEIT005.EPM_OPERATION_QUANTITYS WHERE SEZON_ADI='" + season.EGEMEN_ADI + "' AND OPERASYON_ID IN (8631,8632,8633,9576) AND (" + filter + ")  GROUP BY PAZAR_ADI");
            List<MarketReleasedModel> bantGM = _monitoringRepository.DeserializeList<MarketReleasedModel>("SELECT 'BANT' TYPE,PAZAR_ADI,SUM(MIKTAR) MIKTAR FROM FDEIT005.EPM_OPERATION_QUANTITYS WHERE SEZON_ADI='" + season.EGEMEN_ADI + "' AND OPERASYON_ID IN (5,600,9595,845,670,80) AND (" + filter + ")  GROUP BY PAZAR_ADI");
            List<MarketReleasedModel> kaliteGM = _monitoringRepository.DeserializeList<MarketReleasedModel>("SELECT 'KALİTE' TYPE,PAZAR_ADI,SUM(MIKTAR) MIKTAR FROM FDEIT005.EPM_OPERATION_QUANTITYS WHERE SEZON_ADI='" + season.EGEMEN_ADI + "' AND OPERASYON_ID IN (181,1160,745,743,744) AND (" + filter + ")  GROUP BY PAZAR_ADI");
            values.KESIM = kesimGM.Sum(ob => ob.MIKTAR);
            values.TASNIF = tasnifGM.Sum(ob => ob.MIKTAR);
            values.BANT = bantGM.Sum(ob => ob.MIKTAR);
            values.KALITE = kaliteGM.Sum(ob => ob.MIKTAR);
            valuesReleased.AddRange(kesimGM);
            valuesReleased.AddRange(tasnifGM);
            valuesReleased.AddRange(bantGM);
            valuesReleased.AddRange(kaliteGM);
            return new Tuple<List<PlanModel>, EPM_TRACKING_PROCESS_VALUES, List<MarketReleasedModel>>(planReturn, values, valuesReleased);

        }
        public EPM_TRACKING_PROCESS_VALUES GetProductionDetailsByDate(Tuple<List<HaftaModel>, List<ProductModel>, FilterModel, DateTime> model)
        {
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
            EPM_PRODUCTION_SEASON season = _monitoringRepository.Deserialize<EPM_PRODUCTION_SEASON>(model.Item3.SEASON);
            EPM_TRACKING_PROCESS_VALUES values = new EPM_TRACKING_PROCESS_VALUES();
            values.KESIM = _egemenRepository.ReadInteger(new EgemenDevanlayHelper().KesimAdediniGetirByDate(season.EGEMEN_ADI, model.Item4.Date, model.Item4.Date.AddDays(1).AddSeconds(-1), filterPazarsiz));
            values.TASNIF = _egemenRepository.ReadInteger(new EgemenDevanlayHelper().TasnifAdediniGetirByDate(season.EGEMEN_ADI, model.Item4.Date, model.Item4.Date.AddDays(1).AddSeconds(-1),filter));
            values.KALITE = _egemenRepository.ReadInteger(new EgemenDevanlayHelper().KaliteAdediniGetirByDate(season.EGEMEN_ADI, model.Item4.Date, model.Item4.Date.AddDays(1).AddSeconds(-1),filter));
            values.BANT = _egemenRepository.ReadInteger(new EgemenDevanlayHelper().BantAdediniGetirByDate(season.EGEMEN_ADI, model.Item4.Date, model.Item4.Date.AddDays(1).AddSeconds(-1),filter));
            return values;

        }
    }
}
