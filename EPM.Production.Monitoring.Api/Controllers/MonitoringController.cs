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

        public List<ProductModel> GetProductList(Tuple<HaftaModel, FilterModel> model)
        {
            return _monitoringService.GetProductList(model);
        }

        public Tuple<EPM_MASTER_PRODUCTION_H, List<PlanModel>, EPM_TRACKING_PROCESS_VALUES> GetProductionDetails(Tuple<HaftaModel, ProductModel, FilterModel> model)
        {
            return _monitoringService.GetProductionDetails(model); 
        }
        public EPM_TRACKING_PROCESS_VALUES GetProductionDetailsByDate(Tuple<HaftaModel, ProductModel, FilterModel,DateTime> model)
        {
            return _monitoringService.GetProductionDetailsByDate(model);
        }
    }
}
