using EPM.Dto.Base;
using EPM.Production.Monitoring.Dto.Models;
using EPM.Production.Monitoring.Repository.Repository;
using EPM.Production.Monitoring.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

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

        public List<ProductModel> GetProductList(Tuple<HaftaModel, FilterModel> model)
        {
            string sql = string.Format(@"SELECT * FROM (SELECT DISTINCT H.MODEL ,H.COLOR, H.ID AS HEADER_ID FROM FDEIT005.EPM_PRODUCTION_PLAN P 
INNER JOIN  FDEIT005.EPM_MASTER_PRODUCTION_H  H ON H.ID=P.HEADER_ID
INNER JOIN FDEIT005.EPM_PRODUCTION_MARKET M ON M.ID=P.MARKET_ID WHERE 0=0 AND P.WEEK={0} AND P.YEAR={1} _SQLFILTER_", model.Item1.WEEK, model.Item1.YEAR);
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

        public Tuple<EPM_MASTER_PRODUCTION_H, List<PlanModel>, EPM_TRACKING_PROCESS_VALUES> GetProductionDetails(Tuple<HaftaModel, ProductModel, FilterModel> model)
        {
            EPM_MASTER_PRODUCTION_H master = _monitoringRepository.Deserialize<EPM_MASTER_PRODUCTION_H>(model.Item2.HEADER_ID);
            List<PlanModel> plan = _monitoringRepository.DeserializeList<PlanModel>(string.Format("SELECT P.*,M.ADI AS MARKET_NAME FROM FDEIT005.EPM_PRODUCTION_PLAN P INNER JOIN FDEIT005.EPM_PRODUCTION_MARKET M ON M.ID=P.MARKET_ID WHERE P.YEAR={0} AND P.WEEK={1} AND P.HEADER_ID={2}", model.Item1.YEAR, model.Item1.WEEK, model.Item2.HEADER_ID));
            foreach (var item in plan) 
                item.ORDER_QUANTITY = _monitoringRepository.ReadInteger(string.Format("SELECT SUM(QUANTITY) FROM FDEIT005.EPM_MASTER_PRODUCTION_D WHERE MARKET={0} AND HEADER_ID={1}",item.MARKET_ID,item.HEADER_ID)); 

            EPM_TRACKING_PROCESS_VALUES values = _monitoringRepository.Deserialize<EPM_TRACKING_PROCESS_VALUES>("SELECT * FROM FDEIT005.EPM_TRACKING_PROCESS_VALUES WHERE HEADER_ID=" + model.Item2.HEADER_ID + "");
            return new Tuple<EPM_MASTER_PRODUCTION_H, List<PlanModel>, EPM_TRACKING_PROCESS_VALUES>(master, plan, values); 

        }
        public EPM_TRACKING_PROCESS_VALUES GetProductionDetailsByDate(Tuple<HaftaModel, ProductModel, FilterModel, DateTime> model)
        {
            EPM_MASTER_PRODUCTION_H master = _monitoringRepository.Deserialize<EPM_MASTER_PRODUCTION_H>(model.Item2.HEADER_ID);
            List<PlanModel> plan = _monitoringRepository.DeserializeList<PlanModel>(string.Format("SELECT P.*,M.ADI AS MARKET_NAME FROM FDEIT005.EPM_PRODUCTION_PLAN P INNER JOIN FDEIT005.EPM_PRODUCTION_MARKET M ON M.ID=P.MARKET_ID WHERE P.YEAR={0} AND P.WEEK={1} AND P.HEADER_ID={2}", model.Item1.YEAR, model.Item1.WEEK, model.Item2.HEADER_ID));
            foreach (var item in plan)
                item.ORDER_QUANTITY = _monitoringRepository.ReadInteger(string.Format("SELECT SUM(QUANTITY) FROM FDEIT005.EPM_MASTER_PRODUCTION_D WHERE MARKET={0} AND HEADER_ID={1}", item.MARKET_ID, item.HEADER_ID));


            EPM_PRODUCTION_SEASON season = _monitoringRepository.Deserialize<EPM_PRODUCTION_SEASON>(master.SEASON);

            var kesimAdet = _monitoringRepository.ReadInteger("SELECT SUM(FIILI_KESIM) FROM XXXT.XXXT_IS_EMIRLERI WHERE SEZON_BILGISI='"+season.EGEMEN_ADI+"' AND MODEL='"+master.MODEL+"' AND MAMUL_RENGI='"+master.COLOR+"'");
            var tasnifAdet = _monitoringRepository.ReadInteger("SELECT SUM(TASNIF_MIKTARI) FROM XXXT.XXXT_IS_EMIRLERI WHERE SEZON_BILGISI='"+season.EGEMEN_ADI+"' AND MODEL='"+master.MODEL+"' AND MAMUL_RENGI='"+master.COLOR+"'");
            var bantAdet = _egemenRepository.ReadInteger(new EgemenDevanlayHelper().BantBitisleriByDate(model.Item4.Date, model.Item4.Date.AddDays(1).AddSeconds(-1), season.EGEMEN_ADI, master.MODEL, master.COLOR));
            var kaliteAdet = _egemenRepository.ReadInteger(new EgemenDevanlayHelper().KaliteBitisleriByDate(model.Item4.Date, model.Item4.Date.AddDays(1).AddSeconds(-1), season.EGEMEN_ADI, master.MODEL, master.COLOR));
            EPM_TRACKING_PROCESS_VALUES values = new EPM_TRACKING_PROCESS_VALUES();
            values.BANT = bantAdet;
            values.KALITE = kaliteAdet;
            values.TASNIF = tasnifAdet;
            values.KESIM = kesimAdet;
            return values;

        }
    }
}
