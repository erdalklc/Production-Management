using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc; 
using EPM.Dto.Models;
using EPM.Plan.Dto.Plan;
using EPM.Service.Base;
using EPM.Tools.Helpers;
using EPM.Tools.Managers;
using EPM.Web.ServiceHelper; 
using Microsoft.AspNetCore.Mvc;  
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace EPM.Web.Controllers
{
    [ServiceFilter(typeof(AppFilterAttribute), Order = 1)]
    public class UretimPlanController : Controller
    {

        private readonly IMenuService _menuService;
        public UretimPlanController(IMenuService menuService)
        {
            _menuService = menuService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PlanOlustur()
        { 
            ViewData["CanEditPlan"] = _menuService.CanEditPlan(Request.HttpContext);
            ViewBag.Page = "PLAN GİRİŞİ (YÜKLEME TERMİN)"; 
            return View(new Plan_Filter());
        }
        public IActionResult PlanOlusturBandGiris()
        {
            ViewData["CanEditPlan"] = _menuService.CanEditPlan(Request.HttpContext);
            ViewBag.Page = "PLAN GİRİŞİ (BANT GİRİŞ)";
            return View(new Plan_Filter());
        }
        public IActionResult KapasitePlan()
        {
            ViewBag.Page = "KAPASİTE PLANI GİRİŞİ";
            return View();
        }

        public IActionResult KapasiteUyum()
        {
            ViewData["CanEditPlan"] = _menuService.CanEditPlan(Request.HttpContext);
            ViewBag.Page = "KAPASİTE UYUM";
            return View();
        }
        public IActionResult YuklemeUyum()
        {
            ViewData["CanEditPlan"] = _menuService.CanEditPlan(Request.HttpContext);
            ViewBag.Page = "YÜKLEME UYUM";
            return View();
        }

        public IActionResult _PartialKapasitePlanList()
        {
            return PartialView();
        }

        public IActionResult _PartialKapasitePlanUyum()
        {
            return PartialView();
        }

        public IActionResult _PartialYuklemePlanUyum()
        {
            return PartialView();
        }
        [HttpGet, HttpPost]
        public IActionResult _PartialBandWork()
        {
            ViewData["CanEditPlan"] = _menuService.CanEditPlan(Request.HttpContext);
            return PartialView();
        }
        [HttpGet,HttpPost]
        public IActionResult _PartialBandWorkMinutes()
        {
            ViewData["CanEditPlan"] = _menuService.CanEditPlan(Request.HttpContext);
            return PartialView();
        }
        [HttpGet, HttpPost]
        public IActionResult _PartialProductionTimes()
        {
            return PartialView();
        }
        [HttpGet]
        public object KapasitePlanUyumLoad(DataSourceLoadOptions loadOptions, [FromQuery(Name = "YEAR")] int YEAR, [FromQuery(Name = "BAND_GROUP")] int BAND_GROUP)
        {
            return DataSourceLoader.Load(PlanServiceHelper.GetKapasiteUyumList(YEAR, BAND_GROUP), loadOptions);
        }
        [HttpGet]
        public object YuklemePlanUyumLoad(DataSourceLoadOptions loadOptions, [FromQuery(Name = "YEAR")] int YEAR, [FromQuery(Name = "BAND_GROUP")] int BAND_GROUP)
        {
            return DataSourceLoader.Load(PlanServiceHelper.GetYuklemeUyumList(YEAR, BAND_GROUP), loadOptions);
        }

        [HttpGet]
        public object GetProductGroupList(DataSourceLoadOptions loadOptions ,[FromQuery(Name = "BAND_GROUP")] int BAND_GROUP)
        {
            return DataSourceLoader.Load(PlanServiceHelper.GetProductGroupList( BAND_GROUP), loadOptions);
        }
        [HttpGet]
        public object KapasitePlanPerformansLoad(DataSourceLoadOptions loadOptions, [FromQuery(Name = "YEAR")] int YEAR, [FromQuery(Name = "BAND_GROUP")] int BAND_GROUP)
        {
            return DataSourceLoader.Load(PlanServiceHelper.GetKapasitePerformansList(YEAR, BAND_GROUP), loadOptions);
        }
        [HttpGet]
        public object YuklemePlanPerformansLoad(DataSourceLoadOptions loadOptions, [FromQuery(Name = "YEAR")] int YEAR, [FromQuery(Name = "BAND_GROUP")] int BAND_GROUP)
        {
            return DataSourceLoader.Load(PlanServiceHelper.GetYuklemePerformansList(YEAR, BAND_GROUP), loadOptions);
        }
        [HttpGet]
        public object BandWorkersLoad(DataSourceLoadOptions loadOptions, [FromQuery(Name = "YEAR")] int YEAR, [FromQuery(Name = "BAND_GROUP")] int BAND_GROUP, [FromQuery(Name = "PRODUCT_GROUP")] int PRODUCT_GROUP)
        {
            return DataSourceLoader.Load(PlanServiceHelper.GetBandWorkers(YEAR, BAND_GROUP, PRODUCT_GROUP), loadOptions);
        }
        [HttpGet]
        public object BandWorkMinutesLoad(DataSourceLoadOptions loadOptions, [FromQuery(Name = "YEAR")] int YEAR, [FromQuery(Name = "BAND_GROUP")] int BAND_GROUP)
        {
            return DataSourceLoader.Load(PlanServiceHelper.GetBandWorkMinutes(YEAR, BAND_GROUP), loadOptions);
        }
        [HttpGet]
        public object ProductionTimesLoad(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(PlanServiceHelper.ProductionTimesLoad(), loadOptions);
        }


        [HttpGet]
        public object KapasitePlanListLoad(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(PlanServiceHelper.GetKapasitePlanList(), loadOptions);
        }

        [HttpPut]
        public IActionResult KapasitePlanListUpdate(int key, string values)
        {
            object[] islem = PlanServiceHelper.KapasitePlanListUpdate(key, values);
            if ((bool)islem[0])
                return Ok();
            else return BadRequest(islem[1]);
        }
        [HttpPut]
        public IActionResult BandWorkersUpdate(int key, string values)
        {
            object[] islem = PlanServiceHelper.BandWorkersUpdate(key, values);
            if ((bool)islem[0])
                return Ok();
            else return BadRequest(islem[1]);
        }
        [HttpPut]
        public IActionResult BandWorkMinutesUpdate(int key, string values)
        {
            object[] islem = PlanServiceHelper.BandWorkMinutesUpdate(key, values);
            if ((bool)islem[0])
                return Ok();
            else return BadRequest(islem[1]);
        }
        [HttpGet]
        public object KapasitePlanListChartLoad(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(PlanServiceHelper.GetKapasitePlanListChart(), loadOptions);
        }

        [HttpGet,HttpPost]
        public object _PartialGetPlanList(int BRAND, int SEASON, string MODEL, string COLOR, int ORDER_TYPE, int PRODUCTION_TYPE, int FABRIC_TYPE,int PLAN_DURUM,List<string> YEAR)
        {
            return PlanServiceHelper.GetPlan(new CookieHelper().GetObjectFromCookie<WebLogin>(Request.HttpContext, "USER").USER_CODE, BRAND, SEASON, MODEL, COLOR, ORDER_TYPE, PRODUCTION_TYPE, FABRIC_TYPE, PLAN_DURUM,YEAR);
        }
        [HttpGet, HttpPost]
        public object _PartialGetPlanListBandGiris(int BRAND, int SEASON, string MODEL, string COLOR, int ORDER_TYPE, int PRODUCTION_TYPE, int FABRIC_TYPE, int PLAN_DURUM, List<string> YEAR)
        {
            return PlanServiceHelper.GetPlanBandGiris(new CookieHelper().GetObjectFromCookie<WebLogin>(Request.HttpContext, "USER").USER_CODE, BRAND, SEASON, MODEL, COLOR, ORDER_TYPE, PRODUCTION_TYPE, FABRIC_TYPE, PLAN_DURUM, YEAR);
        }
        [HttpGet, HttpPost]
        public object _PartialGetPlanListByChart(KapasiyeUyumChart_Filter filter)
        {
            return PlanServiceHelper.GetPlanByChart(filter);
        }

        [HttpGet, HttpPost]
        public object _PartialGetPlanListByChartBandGiris(KapasiyeUyumChart_Filter filter)
        {
            return PlanServiceHelper.GetPlanByChartBandGiris(filter);
        }
        [HttpGet, HttpPost]
        public object _PartialGetUretimGerceklesenListByChart(KapasiyeUyumChart_Filter filter)
        {
            return PlanServiceHelper.GetUretimGerceklesenByChart(filter);
        }
        [HttpGet, HttpPost]
        public object _PartialGeKapasiteListByChart(KapasiyeUyumChart_Filter filter)
        {
            return PlanServiceHelper.GeKapasiteListByChart(filter);
        }
        [HttpGet, HttpPost]
        public object _PartialGeKapasiteListByChartBandGiris(KapasiyeUyumChart_Filter filter)
        {
            return PlanServiceHelper.GeKapasiteListByChartBandGiris(filter);
        }

        [HttpGet,HttpPost]
        public bool UretimSureleriYenile()
        {
            return PlanServiceHelper.UretimSureleriYenile();
        }
        [HttpPut,HttpGet,HttpPost]
        public object UpdateTask( [FromBody] JObject elem)
        {
            return PlanServiceHelper.UpdateInsertPlan(new CookieHelper().GetObjectFromCookie<WebLogin>(Request.HttpContext, "USER").USER_CODE, elem);
        }
        public object UpdateTaskBandGiris([FromBody] JObject elem)
        {
            return PlanServiceHelper.UpdateInsertPlanBandGiris(new CookieHelper().GetObjectFromCookie<WebLogin>(Request.HttpContext, "USER").USER_CODE, elem);
        }

        [HttpGet]
        public object GetPlanStatusList(DataSourceLoadOptions loadOptions, [FromQuery(Name = "all")] bool hepsi) => DataSourceLoader.Load(PlanServiceHelper.GetPlanStatusList(hepsi), loadOptions);
    }
}
