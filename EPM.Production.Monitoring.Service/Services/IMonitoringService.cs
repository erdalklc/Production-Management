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
        public List<ProductModel> GetProductList(Tuple<HaftaModel, FilterModel> model);
        public Tuple<EPM_MASTER_PRODUCTION_H, List<PlanModel>, EPM_TRACKING_PROCESS_VALUES> GetProductionDetails(Tuple<HaftaModel, ProductModel, FilterModel> model);
        public EPM_TRACKING_PROCESS_VALUES GetProductionDetailsByDate(Tuple<HaftaModel, ProductModel, FilterModel, DateTime> model);
    }
}
