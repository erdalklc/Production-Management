using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using EPM.Core.FormModels.Uretim;
using EPM.Core.Helpers;
using EPM.Core.Managers;
using EPM.Core.Services;
using EPM.Plan.Dto.Plan;
using EPM.Web.ServiceHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc; 
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace EPM.Web.Controllers
{
    [ServiceFilter(typeof(AppFilterAttribute), Order = 1)]
    public class UretimPlanController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PlanOlustur()
        {
            ViewBag.Page = "ÜRETİM PLAN GİRİŞİ"; 
            return View(new Plan_Filter());
        }

        public IActionResult KapasitePlan()
        {
            ViewBag.Page = "KAPASİTE PLANI GİRİŞİ";
            return View();
        }

        public IActionResult KapasiteUyum()
        {
            ViewBag.Page = "KAPASİTE UYUM";
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

        [HttpGet]
        public object KapasitePlanUyumLoad(DataSourceLoadOptions loadOptions, [FromQuery(Name = "YEAR")] int YEAR, [FromQuery(Name = "BAND_GROUP")] int BAND_GROUP)
        {
            return DataSourceLoader.Load(PlanServiceHelper.GetKapasiteUyumList(YEAR, BAND_GROUP), loadOptions);
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

        [HttpGet]
        public object KapasitePlanListChartLoad(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(PlanServiceHelper.GetKapasitePlanListChart(), loadOptions);
        }

        [HttpGet,HttpPost]
        public object _PartialGetPlanList(int BRAND, int SEASON, string MODEL, string COLOR, int ORDER_TYPE, int PRODUCTION_TYPE, int FABRIC_TYPE)
        {
            return PlanServiceHelper.GetPlan(new CookieHelper().GetUserFromCookie(Request.HttpContext).USER_CODE, BRAND, SEASON, MODEL, COLOR, ORDER_TYPE, PRODUCTION_TYPE, FABRIC_TYPE);
        }

        [HttpPut,HttpGet,HttpPost]
        public object UpdateTask( [FromBody] JObject elem)
        {
            return PlanServiceHelper.UpdateInsertPlan(new CookieHelper().GetUserFromCookie(Request.HttpContext).USER_CODE, elem);
        }
    }
}
