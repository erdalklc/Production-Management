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
        public List<ProductModel> GetProductList(Tuple<List<HaftaModel>, FilterModel> model);
        public Tuple<List<PlanModel>, EPM_TRACKING_PROCESS_VALUES, List<MarketReleasedModel>, List<ProductionModel>> GetProductionDetails(Tuple<List<HaftaModel>, List<ProductModel>, FilterModel> model);
        public EPM_TRACKING_PROCESS_VALUES GetProductionDetailsByDate(Tuple<List<HaftaModel>, List<ProductModel>, FilterModel, DateTime> model);
    }
}
