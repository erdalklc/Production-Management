using EPM.Dto.Base;
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
        public IActionResult GetProductList([FromBody] JObject data)
        {
            dynamic value = data;
            var haftaModel = JsonConvert.DeserializeObject<List<HaftaModel>>(value.haftaModel.ToString());
            var marketModel = JsonConvert.DeserializeObject<List<EPM_PRODUCTION_MARKET>>(value.marketModel.ToString());
            var productGroupModel = JsonConvert.DeserializeObject<List<EPM_PRODUCT_GROUP>>(value.productGroupModel.ToString());
            var filterModel = JsonConvert.DeserializeObject<FilterModel>(value.filterModel.ToString());
            return Json(MonitoringServiceHelper.GetProductList(haftaModel, productGroupModel,marketModel, filterModel));
        }
        [HttpPost, HttpGet]
        public IActionResult GetMarketList()
        {
            return Json(MonitoringServiceHelper.GetMarketList());
        }
        [HttpPost, HttpGet]
        public IActionResult GetProductGroup()
        {
            return Json(MonitoringServiceHelper.GetProductGroup());
        }
        [HttpPost, HttpGet]
        public IActionResult GetProductionDetails([FromBody] JObject data)
        {
            dynamic value = data;
            var haftaModel = JsonConvert.DeserializeObject<List<HaftaModel>>(value.haftaModel.ToString());
            var productModel = JsonConvert.DeserializeObject<List<ProductModel>>(value.productModel.ToString());
            var filterModel = JsonConvert.DeserializeObject<FilterModel>(value.filterModel.ToString());
            var marketModel = JsonConvert.DeserializeObject<List<EPM_PRODUCTION_MARKET>>(value.marketModel.ToString());
            var productGroupModel = JsonConvert.DeserializeObject<List<EPM_PRODUCT_GROUP>>(value.productGroupModel.ToString());

            //List<HaftaModel> haftaModel,List<ProductModel> productModel, FilterModel filterModel
            return Json(MonitoringServiceHelper.GetProductionDetails(haftaModel, productModel, filterModel,productGroupModel,marketModel));
             
        }
        [HttpPost, HttpGet]
        public IActionResult GetProductionDetailsList([FromBody] JObject data)
        {
            dynamic value = data;
            var haftaModel = JsonConvert.DeserializeObject<List<HaftaModel>>(value.haftaModel.ToString());
            var productModel = JsonConvert.DeserializeObject<List<ProductModel>>(value.productModel.ToString());
            var filterModel = JsonConvert.DeserializeObject<FilterModel>(value.filterModel.ToString());
            var marketModel = JsonConvert.DeserializeObject<List<EPM_PRODUCTION_MARKET>>(value.marketModel.ToString());
            var productGroupModel = JsonConvert.DeserializeObject<List<EPM_PRODUCT_GROUP>>(value.productGroupModel.ToString());
            var operation = JsonConvert.DeserializeObject<ProductionDetail>(value.operation.ToString());

            //List<HaftaModel> haftaModel,List<ProductModel> productModel, FilterModel filterModel
            return Json(MonitoringServiceHelper.GetProductionDetailsList(haftaModel, productModel, filterModel, productGroupModel, marketModel,operation));

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

        public IActionResult _GetProductionDetailsListGrid()
        {
            return PartialView();
        }



    }
}
