using EPM.Dto.Base;
using EPM.Production.Monitoring.Dto.Models;
using EPM.Production.Monitoring.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.Production.Monitoring.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MonitoringController : ControllerBase
    {
        private readonly IMonitoringService _monitoringService;
        public MonitoringController(IMonitoringService monitoringService)
        {
            _monitoringService = monitoringService;
        }
        public List<HaftaModel> GetTerminList(FilterModel model)
        {
            return _monitoringService.GetTerminList(model);
        }

        public List<ProductModel> GetProductList(Tuple<List<HaftaModel>, List<EPM_PRODUCT_GROUP>, List<EPM_PRODUCTION_MARKET>, FilterModel> model)
        {
            return _monitoringService.GetProductList(model);
        }

        public Tuple<List<PlanModel>, EPM_TRACKING_PROCESS_VALUES, List<MarketReleasedModel>, List<ProductionModel>, List<PlanModel>, List<ProductionDetailPivotModel>> GetProductionDetails(Tuple<List<HaftaModel>, List<ProductModel>, FilterModel, List<EPM_PRODUCT_GROUP>, List<EPM_PRODUCTION_MARKET>> model)
        {
            return _monitoringService.GetProductionDetails(model); 
        }
        public List<ProductionDetailModel> GetProductionDetailsList(Tuple<List<HaftaModel>, List<ProductModel>, FilterModel, List<EPM_PRODUCT_GROUP>, List<EPM_PRODUCTION_MARKET>,ProductionDetail> model)
        {
            return _monitoringService.GetProductionDetailsList(model);
        }
        public EPM_TRACKING_PROCESS_VALUES GetProductionDetailsByDate(Tuple<List<HaftaModel>, List<ProductModel>, FilterModel,DateTime> model)
        {
            return _monitoringService.GetProductionDetailsByDate(model);
        }

        public List<EPM_PRODUCTION_MARKET> GetMarketList()
        {
            return _monitoringService.GetMarketList();
        }
        public List<EPM_PRODUCT_GROUP> GetProductGroup()
        {
            return _monitoringService.GetProductGroup();
        }
    }
}
