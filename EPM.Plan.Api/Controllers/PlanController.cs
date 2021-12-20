using EPM.Dto.Base;
using EPM.Plan.Dto.Extensions;
using EPM.Plan.Dto.Plan;
using EPM.Plan.Service.Services; 
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
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
        public ActionResult<object> GetPlan(object[] obj)=>_planService.GetPlan(obj[0].ToStringParse(),obj[1].IntParse(), obj[2].IntParse(), obj[3].ToStringParse(), obj[4].ToStringParse(), obj[5].IntParse(), obj[6].IntParse(), obj[7].IntParse(), obj[8].IntParse(), obj[9].ToString());
        public ActionResult<object> GetPlanBandGiris(object[] obj)=>_planService.GetPlanBandGiris(obj[0].ToStringParse(),obj[1].IntParse(), obj[2].IntParse(), obj[3].ToStringParse(), obj[4].ToStringParse(), obj[5].IntParse(), obj[6].IntParse(), obj[7].IntParse(), obj[8].IntParse(), obj[9].ToString());

        [HttpPost]
        public ActionResult<object> GetPlanByChart(KapasiyeUyumChart_Filter filter) => _planService.GetPlanByChart(filter);
        public ActionResult<object> GetPlanByChartBandGiris(KapasiyeUyumChart_Filter filter) => _planService.GetPlanByChartBandGiris(filter);
        [HttpPost]
        public ActionResult<object> GetUretimGerceklesenByChart(KapasiyeUyumChart_Filter filter) => _planService.GetUretimGerceklesenByChart(filter);
        [HttpPost]
        public ActionResult<object> GeKapasiteListByChart(KapasiyeUyumChart_Filter filter) => _planService.GeKapasiteListByChart(filter);
        [HttpPost]
        public ActionResult<object> GeKapasiteListByChartBandGiris(KapasiyeUyumChart_Filter filter) => _planService.GeKapasiteListByChartBandGiris(filter);

        [HttpPost]
        public object UpdateInsertPlan(object[] obj)=>_planService.UpdateInsertPlan(obj[0].ToString(),(JObject)obj[1]);
        public object UpdateInsertPlanBandGiris(object[] obj)=>_planService.UpdateInsertPlanBandGiris(obj[0].ToString(),(JObject)obj[1]);

        [HttpGet]
        public List<EPM_PRODUCTION_BAND_CAPASITIES> GetKapasitePlanList()=>_planService.GetKapasitePlanList();
        [HttpGet]
        public bool UretimSureleriYenile() => _planService.UretimSureleriYenile();

        [HttpGet]
        public List<ModelSureleriView> ProductionTimesLoad() => _planService.ProductionTimesLoad();

        [HttpGet]
        public List<KapasitePlanListChart> GetKapasitePlanListChart()=>_planService.GetKapasitePlanListChart();

        [HttpPost]
        public object[] KapasitePlanListUpdate(object[] obj)=>_planService.KapasitePlanListUpdate(obj[0].IntParse(),obj[1].ToString());

        [HttpPost]
        public object[] BandWorkersUpdate(object[] obj) => _planService.BandWorkersUpdate(obj[0].IntParse(), obj[1].ToString());


        [HttpPost]
        public object[] BandWorkMinutesUpdate(object[] obj) => _planService.BandWorkMinutesUpdate(obj[0].IntParse(), obj[1].ToString());

        [HttpPost]
        public List<KapasitePlanUyum> GetKapasiteUyumList(object[] obj) => _planService.GetKapasiteUyumList(obj[0].IntParse(), obj[1].IntParse());

        [HttpPost]
        public List<KapasitePlanUyum> GetYuklemeUyumList(object[] obj) => _planService.GetYuklemeUyumList(obj[0].IntParse(), obj[1].IntParse());

        [HttpPost]
        public List<KapasitePlanPerformans> GetKapasitePerformansList(object[] obj) => _planService.GetKapasitePerformansList(obj[0].IntParse(), obj[1].IntParse());
        [HttpPost]
        public List<KapasitePlanPerformans> GetYuklemePerformansList(object[] obj) => _planService.GetYuklemePerformansList(obj[0].IntParse(), obj[1].IntParse());


        [HttpPost]
        public List<EpmBandWorkModel> GetBandWorkers(object[] obj) => _planService.GetBandWorkers(obj[0].IntParse(), obj[1].IntParse(),obj[2].IntParse());


        [HttpPost]
        public List<EpmBandWorkMinuteModel> GetBandWorkMinutes(object[] obj) => _planService.GetBandWorkMinutes(obj[0].IntParse(), obj[1].IntParse());

        [HttpGet, HttpPost]
        public List<PlanStatus> GetPlanStatusList(object[] obj) => _planService.GetPlanStatusList(obj[0].BooleanParse());


        [HttpGet, HttpPost]
        public List<EPM_PRODUCT_GROUP> GetProductGroupList(object[] obj) => _planService.GetProductGroupList(obj[0].IntParse());
    }
}
