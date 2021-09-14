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
    }
}
