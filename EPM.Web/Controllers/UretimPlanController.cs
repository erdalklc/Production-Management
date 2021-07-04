using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using EPM.Core.FormModels.Uretim;
using EPM.Core.Managers;
using EPM.Core.Services;
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
        private readonly IUretimPlanService _uretimPlanRepository;
        public UretimPlanController(IUretimPlanService uretimPlanRepository) => _uretimPlanRepository = uretimPlanRepository;
         
        public IActionResult PlanOlustur()
        {
            UretimOnayliListe model = new UretimOnayliListe();
            return View(model);
        }

        public IActionResult KapasitePlan()
        { 
            return View();
        }
        public IActionResult KapasiteUyum()
        {
            return View();
        }

        public  IActionResult _PartialKapasitePlanList()
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
            return DataSourceLoader.Load(_uretimPlanRepository.GetKapasiteUyumList(YEAR, BAND_GROUP), loadOptions);
        }
        [HttpGet]
        public object KapasitePlanListLoad(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_uretimPlanRepository.GetKapasitePlanList(), loadOptions);
        }
        [HttpPut]
        public IActionResult KapasitePlanListUpdate(int key, string values)
        {
            object[] islem = _uretimPlanRepository.KapasitePlanListUpdate(key, values);
            if ((bool)islem[0])
                return Ok();
            else return BadRequest(islem[1]);
        }
        [HttpGet]
        public object KapasitePlanListChartLoad(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_uretimPlanRepository.GetKapasitePlanListChart(), loadOptions);
        }

        [HttpGet,HttpPost]
        public object _PartialGetPlanList(int BRAND, int SEASON, string MODEL, string COLOR, int ORDER_TYPE, int PRODUCTION_TYPE, int FABRIC_TYPE)
        {
            return _uretimPlanRepository.GetPlan(Request.HttpContext, BRAND, SEASON, MODEL, COLOR, ORDER_TYPE, PRODUCTION_TYPE, FABRIC_TYPE);
        }
        [HttpPut,HttpGet,HttpPost]
        public object UpdateTask( [FromBody] JObject elem)
        {
            return _uretimPlanRepository.UpdateInsertPlan(Request.HttpContext, elem);
        }
        [HttpGet, HttpPost]
        public object _Test([FromBody]  JObject a)
        {
            return a;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
