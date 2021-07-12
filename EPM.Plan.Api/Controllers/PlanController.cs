using EPM.Dto.Base;
using EPM.Plan.Dto.Extensions;
using EPM.Plan.Dto.Plan;
using EPM.Plan.Service.Services; 
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq; 
using System.Collections.Generic; 

namespace EPM.Plan.Api.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class PlanController : ControllerBase
    {
        private readonly IPlanService _planService;
        public PlanController(IPlanService planService)
        {
            _planService = planService;
        }
        [ HttpPost]
        public ActionResult<object> GetPlan(object[] obj)=>_planService.GetPlan(obj[0].ToStringParse(),obj[1].IntParse(), obj[2].IntParse(), obj[3].ToStringParse(), obj[4].ToStringParse(), obj[5].IntParse(), obj[6].IntParse(), obj[7].IntParse());

        [HttpPost]
        public object UpdateInsertPlan(object[] obj)=>_planService.UpdateInsertPlan(obj[0].ToString(),(JObject)obj[1]);

        [HttpGet]
        public List<EPM_PRODUCTION_BAND_CAPASITIES> GetKapasitePlanList()=>_planService.GetKapasitePlanList();

        [HttpGet]
        public List<KapasitePlanListChart> GetKapasitePlanListChart()=>_planService.GetKapasitePlanListChart();

        [HttpPost]
        public object[] KapasitePlanListUpdate(object[] obj)=>_planService.KapasitePlanListUpdate(obj[0].IntParse(),obj[1].ToString());

        [HttpPost]
        public List<KapasitePlanUyum> GetKapasiteUyumList(object[] obj) => _planService.GetKapasiteUyumList(obj[0].IntParse(), obj[1].IntParse());
    }
}
