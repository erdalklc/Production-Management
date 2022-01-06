using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using EPM.Dto.Base;
using EPM.Dto.Models;
using EPM.Service.Base;
using EPM.Tools.Helpers;
using EPM.Tools.Managers;
using EPM.Web.ServiceHelper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace EPM.Web.Controllers
{
    [ServiceFilter(typeof(AppFilterAttribute), Order = 1)]
    public class UretimController : Controller
    {
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly IUretimService _uretimRepository;
        private readonly IMenuService _menuService;
        private readonly ILogService _logRepository;
        private readonly ICacheService _cacheService;
        public UretimController(IUretimService uretimRepository, ILogService logRepository, IWebHostEnvironment appEnvironment, IMenuService menuService, ICacheService cacheService)
        {
            _uretimRepository = uretimRepository;
            _logRepository = logRepository;
            _appEnvironment = appEnvironment;
            _menuService = menuService;
            _cacheService = cacheService;
        }
         
        public IActionResult UretimListesiAktarExcelYukle()
        {
            object[] obj = _uretimRepository.UretimListesiAktarExcelYukle(Request);
            if ((bool)obj[0])
                return new EmptyResult();
            else return BadRequest(obj[1]);
        }

        public IActionResult _PartialLeftMenu() => PartialView("~/Views/Shared/_PartialLeftMenu.cshtml");

        public IActionResult UretimListesiAktarim()
        {
            ViewBag.Page = "ÜRETİM LİSTESİ AKTARIM";
            return View();
        }
 

        public IActionResult UretimListesi()
        {
            EPM.Production.Dto.Production.UretimOnayliListe liste =new EPM.Production.Dto.Production.UretimOnayliListe();
            liste.APPROVAL_STATUS = 1;
            return View(liste);
        }

        public IActionResult UretimListesiDikey()
        {
            ViewBag.Page = "ÜRETİM LİSTESİ DİKEY";
            return View(new EPM.Production.Dto.Production.UretimOnayliListe());
        }

        [HttpPost, HttpGet]
        public IActionResult _PartialUretimOnayliListeFiltrele(EPM.Production.Dto.Production.UretimOnayliListe liste)
        {
            ViewData["CanEditUretim"] = _menuService.CanUserEditUretim(Request.HttpContext);
            ViewData["OnayliKullanici"] = _menuService.OnayliKullanici(Request.HttpContext);
            return PartialView(new Tuple<List<EPM.Production.Dto.Production.MasterList>, EPM.Production.Dto.Production.UretimOnayliListe>(ProductionServiceHelper.OnayliUretimListesi(new CookieHelper().GetObjectFromCookie<WebLogin>(Request.HttpContext, "USER").USER_CODE, liste), liste));

        }

        [HttpPost, HttpGet]
        public IActionResult _PartialUretimliListeDikeyFiltrele(EPM.Production.Dto.Production.UretimOnayliListe liste)
        {
            ViewData["CanEditUretim"] = _menuService.CanUserEditUretim(Request.HttpContext);
            return PartialView(liste);
        }

        [HttpGet]
        public object UretimOnaylilLoad2( EPM.Production.Dto.Production.UretimOnayliListe liste)
        {
            return ProductionServiceHelper.OnayliUretimListesi(new CookieHelper().GetObjectFromCookie<WebLogin>(Request.HttpContext,"USER").USER_CODE, liste);
        }
        [HttpGet]
        public object UretimListeDikeyLoad(DataSourceLoadOptions loadOptions, EPM.Production.Dto.Production.UretimOnayliListe liste)
        {
            return DataSourceLoader.Load(ProductionServiceHelper.UretimListesiDikey(new CookieHelper().GetObjectFromCookie<WebLogin>(Request.HttpContext, "USER").USER_CODE, liste), loadOptions);
        }

        public IActionResult _PartialUretimOnayliListeFiltreleDetail(int ID)
        {
            ViewData["CanEditUretim"] = _menuService.CanUserEditUretim(Request.HttpContext); 
            return PartialView(ID);
        }

        [HttpGet]
        public object UretimOnayliDetailLoad(DataSourceLoadOptions loadOptions,[FromQuery(Name ="HEADER_ID")] int HEADER_ID)
        {   
            return DataSourceLoader.Load(ProductionServiceHelper.OnayliUretimListesiDetail(HEADER_ID),loadOptions);
        }

        [HttpPut]
        public IActionResult UretimOnayliDetailUpdate(int key, string values)
        {
            object[] islem= ProductionServiceHelper.UretimOnayliDetailUpdate(new CookieHelper().GetObjectFromCookie<WebLogin>(Request.HttpContext, "USER").USER_CODE, key, values);
            if ((bool)islem[0])
                return Ok();
            else return BadRequest(islem[1]);
        }

        [HttpDelete]
        public IActionResult UretimOnayliDetailDelete(int key)
        {
            object[] islem = ProductionServiceHelper.UretimOnayliDetailDelete(new CookieHelper().GetObjectFromCookie<WebLogin>(Request.HttpContext, "USER").USER_CODE, key);
            if ((bool)islem[0])
                return Ok();
            else return BadRequest(islem[1]); 
        } 

        [HttpPost]
        public IActionResult UretimOnayliDetailInsert( string values)
        { 
            return Ok(ProductionServiceHelper.UretimOnayliDetailInsert(values));
        }

        [HttpPut]
        public IActionResult UretimOnayliUpdate(int key, string values)
        {
            object[] islem = ProductionServiceHelper.UretimOnayliUpdate(new CookieHelper().GetObjectFromCookie<WebLogin>(Request.HttpContext, "USER").USER_CODE, key, values);
            if ((bool)islem[0])
                return Ok();
            else return BadRequest(islem[1]);
        }

        [HttpDelete]
        public IActionResult UretimOnayliDelete(int key)
        {
            object[] islem = ProductionServiceHelper.UretimOnayliDelete(new CookieHelper().GetObjectFromCookie<WebLogin>(Request.HttpContext, "USER").USER_CODE, key);
            if ((bool)islem[0])
                return Ok();
            else return BadRequest(islem[1]);
        }

        [HttpPost]
        public IActionResult UretimOnayliInsert(string values)
        {
            try
            {

                return Ok(ProductionServiceHelper.UretimOnayliInsert(values));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public object GetMarkets(DataSourceLoadOptions loadOptions, [FromQuery(Name = "all")] bool hepsi) => DataSourceLoader.Load(ProductionServiceHelper.GetMarketList(hepsi), loadOptions);
        [HttpGet]
        public object GetProductionTypes(DataSourceLoadOptions loadOptions, [FromQuery(Name = "all")] bool hepsi) => DataSourceLoader.Load(ProductionServiceHelper.GetProductionTypes(new CookieHelper().GetObjectFromCookie<WebLogin>(Request.HttpContext, "USER").USER_CODE, new CookieHelper().GetObjectFromCookie<List<EPM_USER_PRODUCTION_TYPES>>(Request.HttpContext, "EPM_USER_PRODUCTION_TYPES"), hepsi), loadOptions);
        [HttpGet]
        public object GetOrderList(DataSourceLoadOptions loadOptions, [FromQuery(Name = "all")] bool hepsi) => DataSourceLoader.Load(ProductionServiceHelper.GetOrderList(hepsi), loadOptions);
        [HttpGet]
        public object GetFabricList(DataSourceLoadOptions loadOptions, [FromQuery(Name = "all")] bool hepsi) => DataSourceLoader.Load(ProductionServiceHelper.GetFabricTypes(new CookieHelper().GetObjectFromCookie<WebLogin>(Request.HttpContext, "USER").USER_CODE, new CookieHelper().GetObjectFromCookie<List<EPM_USER_FABRIC_TYPES>>(Request.HttpContext, "EPM_USER_FABRIC_TYPES"), hepsi), loadOptions);
        [HttpGet]
        public object GetBrandList(DataSourceLoadOptions loadOptions, [FromQuery(Name = "all")] bool hepsi) => DataSourceLoader.Load(ProductionServiceHelper.GetBrandList(new CookieHelper().GetObjectFromCookie<WebLogin>(Request.HttpContext, "USER").USER_CODE, new CookieHelper().GetObjectFromCookie<List<EPM_USER_BRANDS>>(Request.HttpContext, "EPM_USER_BRANDS"), hepsi), loadOptions);
        [HttpGet]
        public object GetSubBrandList(DataSourceLoadOptions loadOptions, [FromQuery(Name = "all")] bool hepsi) => DataSourceLoader.Load(ProductionServiceHelper.GetSubBrandList(new CookieHelper().GetObjectFromCookie<WebLogin>(Request.HttpContext, "USER").USER_CODE, hepsi), loadOptions);
        [HttpGet]
        public object GetSeasonList(DataSourceLoadOptions loadOptions) => DataSourceLoader.Load(ProductionServiceHelper.GetSeasonList(), loadOptions);
        [HttpGet]
        public object GetProductGroupList(DataSourceLoadOptions loadOptions, [FromQuery(Name = "all")] bool hepsi) => DataSourceLoader.Load(ProductionServiceHelper.GetProductGroupList(hepsi), loadOptions);
        [HttpGet]
        public object GetRecipeList(DataSourceLoadOptions loadOptions, [FromQuery(Name = "all")] bool hepsi) => DataSourceLoader.Load(ProductionServiceHelper.GetRecipeList(hepsi), loadOptions);
        [HttpGet]
        public object GetRecipeListByType(DataSourceLoadOptions loadOptions, [FromQuery(Name = "all")] bool hepsi, [FromQuery(Name = "TYPE")] int TYPE) => DataSourceLoader.Load(ProductionServiceHelper.GetRecipeListByType(hepsi, TYPE), loadOptions);
        [HttpGet]
        public object GetCollectionTypes(DataSourceLoadOptions loadOptions, [FromQuery(Name = "all")] bool hepsi) => DataSourceLoader.Load(ProductionServiceHelper.GetCollectionTypes(hepsi), loadOptions);
        [HttpGet]
        public object GetBandList(DataSourceLoadOptions loadOptions, [FromQuery(Name = "all")] bool hepsi) => DataSourceLoader.Load(ProductionServiceHelper.GetBandList(hepsi), loadOptions);
        [HttpGet]
        public object GetApprovalStatusList(DataSourceLoadOptions loadOptions, [FromQuery(Name = "all")] bool hepsi) => DataSourceLoader.Load(ProductionServiceHelper.GetApprovalStatueList(hepsi), loadOptions);
        [HttpGet]
        public object GetStatusList(DataSourceLoadOptions loadOptions, [FromQuery(Name = "all")] bool hepsi) => DataSourceLoader.Load(ProductionServiceHelper.GetStatusList(hepsi), loadOptions);
        [HttpGet]
        public object GetFlagList(DataSourceLoadOptions loadOptions, [FromQuery(Name = "all")] bool hepsi) => DataSourceLoader.Load(ProductionServiceHelper.GetFlagList(hepsi), loadOptions);
        [HttpGet]
        public object GetCurrencyUnits(DataSourceLoadOptions loadOptions) => DataSourceLoader.Load(ProductionServiceHelper.GetCurrencyUnits(), loadOptions);

        public IActionResult _PartialUretimListesiAktarimExcel() => PartialView();

        public IActionResult _PartialUretimLog([FromQuery(Name = "HEADER_ID")] int HEADER_ID) => PartialView(HEADER_ID);

        public IActionResult _PartialUretimDetailLog([FromQuery(Name = "DETAIL_ID")] int DETAIL_ID) => PartialView(DETAIL_ID);

        [HttpGet]
        public object UretimOnayliLogLoad(DataSourceLoadOptions loadOptions, [FromQuery(Name = "HEADER_ID")] int HEADER_ID)
        {
            return DataSourceLoader.Load(ProductionServiceHelper.OnayliUretimListesiLog(HEADER_ID), loadOptions);
        }

        [HttpGet]
        public object UretimOnayliDetailLogLoad(DataSourceLoadOptions loadOptions, [FromQuery(Name = "DETAIL_ID")] int DETAIL_ID)
        {
            return DataSourceLoader.Load(ProductionServiceHelper.OnayliUretimListesiLogDetail(DETAIL_ID), loadOptions);
        } 

        [HttpGet]
        public IActionResult SablonDownload()
        { 
            var path = Path.Combine(_appEnvironment.WebRootPath, "AppData\\EPM AKTARIM SABLON.xlsx");
            var fs = new FileStream(path, FileMode.Open,FileAccess.Read); 
            return File(fs, "application/octet-stream", "EPM AKTARIM SABLON.xlsx");
        }

        [HttpGet]
        public IActionResult SablonBilgileriDownload()
        {
            var path = Path.Combine(_appEnvironment.WebRootPath, "AppData\\AKTARIM ŞABLON ALAN BİLGİLERİ.xlsx");
            var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            return File(fs, "application/octet-stream", "EPM AKTARIM SABLON.xlsx");
        }
    }
}
