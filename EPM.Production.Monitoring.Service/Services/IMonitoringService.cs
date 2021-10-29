using EPM.Dto.Base;
using EPM.Production.Monitoring.Dto.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Production.Monitoring.Service.Services
{
    public interface IMonitoringService
    {
        public List<HaftaModel> GetHaftaModelList();
        public List<HaftaModel> GetTerminList(FilterModel model);
        public List<ProductModel> GetProductList(Tuple<List<HaftaModel>, List<EPM_PRODUCT_GROUP>, List<EPM_PRODUCTION_MARKET>, FilterModel> model);
        public Tuple<List<PlanModel>, EPM_TRACKING_PROCESS_VALUES, List<MarketReleasedModel>, List<ProductionModel>> GetProductionDetails(Tuple<List<HaftaModel>, List<ProductModel>, FilterModel, List<EPM_PRODUCT_GROUP>, List<EPM_PRODUCTION_MARKET>> model);
        public List<ProductionDetailModel> GetProductionDetailsList(Tuple<List<HaftaModel>, List<ProductModel>, FilterModel, List<EPM_PRODUCT_GROUP>, List<EPM_PRODUCTION_MARKET>,ProductionDetail> model);
        public EPM_TRACKING_PROCESS_VALUES GetProductionDetailsByDate(Tuple<List<HaftaModel>, List<ProductModel>, FilterModel, DateTime> model);
        public List<EPM_PRODUCTION_MARKET> GetMarketList();
        public List<EPM_PRODUCT_GROUP> GetProductGroup();
    }
}
