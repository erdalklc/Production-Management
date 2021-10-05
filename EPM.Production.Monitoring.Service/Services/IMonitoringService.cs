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
    }
}
