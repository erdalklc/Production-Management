using EPM.Production.Monitoring.Dto.Models;
using EPM.Service.Base;
using EPM.Web.ServiceHelper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.Web.Controllers
{
    public class UretimIzleController : Controller
    {
        private readonly IUretimIzleService _uretimIzleService;

        public UretimIzleController(IUretimIzleService uretimIzleService)
        {
            _uretimIzleService = uretimIzleService;
        }
        public IActionResult UretimIzle()
        {
            return View(new FilterModel());
        }
        [HttpPost, HttpGet]
        public IActionResult UretimIzleFilter(FilterModel tt)
        { 
            return Json(MonitoringServiceHelper.GetTerminList(tt));
        }

        [HttpPost, HttpGet]
        public IActionResult GetProductList(List<HaftaModel> model,FilterModel filterModel)
        {
            return Json(MonitoringServiceHelper.GetProductList(model,filterModel));
        }

        [HttpPost, HttpGet]
        public IActionResult GetProductionDetails([FromBody] JObject data)
        {
            dynamic value = data;
            //List<HaftaModel> haftaModel,List<ProductModel> productModel, FilterModel filterModel
            return Json(MonitoringServiceHelper.GetProductionDetails(JsonConvert.DeserializeObject < List<HaftaModel>>(value.haftaModel.ToString()), JsonConvert.DeserializeObject<List<ProductModel>>(value.productModel.ToString()), JsonConvert.DeserializeObject<FilterModel>(value.filterModel.ToString())));
             
        }
        [HttpPost, HttpGet]
        public IActionResult GetProductionDetailsByDate([FromBody] JObject data)
        {
            dynamic value = data;
            //List<HaftaModel> haftaModel, List<ProductModel> productModel, FilterModel filterModel,DateTime Tarih
            return Json(MonitoringServiceHelper.GetProductionDetailsByDate(
                JsonConvert.DeserializeObject<List<HaftaModel>>(value.haftaModel.ToString())
                , JsonConvert.DeserializeObject<List<ProductModel>>(value.productModel.ToString())
                , JsonConvert.DeserializeObject<FilterModel>(value.filterModel.ToString())
                , Convert.ToDateTime(value.Tarih.ToString("dd.MM.yyyy"))));
        }


    }
}
