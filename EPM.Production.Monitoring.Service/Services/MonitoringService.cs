using EPM.Production.Monitoring.Dto.Models;
using EPM.Production.Monitoring.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Production.Monitoring.Service.Services
{
    public class MonitoringService : IMonitoringService
    {
        private readonly IMonitoringRepository _monitoringRepository;
        public MonitoringService(IMonitoringRepository monitoringRepository)
        {
            _monitoringRepository = monitoringRepository;
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

        public List<ProductModel> GetProductList(HaftaModel haftaModel)
        {
            string sql = string.Format(@"SELECT H.MODEL ,H.COLOR,M.ADI AS MARKET FROM FDEIT005.EPM_PRODUCTION_PLAN P 
INNER JOIN  FDEIT005.EPM_MASTER_PRODUCTION_H  H ON H.ID=P.HEADER_ID
INNER JOIN FDEIT005.EPM_PRODUCTION_MARKET M ON M.ID=P.MARKET_ID WHERE 0=0 AND P.WEEK={0} AND P.YEAR={1}", haftaModel.WEEK, haftaModel.YEAR);
            return _monitoringRepository.DeserializeList<ProductModel>(sql);
        }

    }
}
