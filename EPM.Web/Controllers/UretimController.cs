using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using EPM.Core.FormModels.Uretim;
using EPM.Core.Helpers;
using EPM.Core.Managers;
using EPM.Core.Models;
using EPM.Core.Services;
using ExcelDataReader;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.Web.Controllers
{
    [ServiceFilter(typeof(AppFilterAttribute), Order = 1)]
    public class UretimController : Controller
    {
        private readonly IUretimService _uretimRepository;
        private readonly ILogService _logRepository;
        public UretimController(IUretimService uretimRepository, ILogService logRepository)
        {
            _uretimRepository = uretimRepository;
            _logRepository = logRepository;
        }
         
        public IActionResult UretimListesiAktarExcelYukle()
        {
            object[] obj=_uretimRepository.UretimListesiAktarExcelYukle(Request);
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

        public IActionResult UretimOnayliListe()
        {
            ViewBag.Page = "ONAYLI ÜRETİM LİSTESİ";
            return View(new UretimOnayliListe());
        }


        public IActionResult UretimListesiDikey()
        {
            ViewBag.Page = "ÜRETİM LİSTESİ DİKEY";
            return View(new UretimOnayliListe());
        }

        [HttpPost, HttpGet]
        public IActionResult _PartialUretimOnayliListeFiltrele(UretimOnayliListe liste) => PartialView(liste);

        [HttpPost, HttpGet]
        public IActionResult _PartialUretimliListeDikeyFiltrele(UretimOnayliListe liste) => PartialView(liste);

        [HttpGet]
        public object UretimOnaylilLoad(DataSourceLoadOptions loadOptions, UretimOnayliListe liste)
        {
            return DataSourceLoader.Load(_uretimRepository.OnayliUretimListesi(Request.HttpContext, liste), loadOptions);
        }

        [HttpGet]
        public object UretimListeDikeyLoad(DataSourceLoadOptions loadOptions, UretimOnayliListe liste)
        {
            return DataSourceLoader.Load(_uretimRepository.UretimListesiDikey(Request.HttpContext, liste), loadOptions);
        }

        public IActionResult _PartialUretimOnayliListeFiltreleDetail(int ID)=> PartialView(ID);

        [HttpGet]
        public object UretimOnayliDetailLoad(DataSourceLoadOptions loadOptions,[FromQuery(Name ="HEADER_ID")] int HEADER_ID)
        {   
            return DataSourceLoader.Load( _uretimRepository.OnayliUretimListesiDetail(HEADER_ID),loadOptions);
        }

        [HttpPut]
        public IActionResult UretimOnayliDetailUpdate(int key, string values)
        {
            object[] islem=_uretimRepository.UretimOnayliDetailUpdate(Request.HttpContext, key, values, _logRepository);
            if ((bool)islem[0])
                return Ok();
            else return BadRequest(islem[1]);
        }

        [HttpDelete]
        public IActionResult UretimOnayliDetailDelete(int key)
        {
            object[] islem = _uretimRepository.UretimOnayliDetailDelete(key);
            if ((bool)islem[0])
                return Ok();
            else return BadRequest(islem[1]); 
        } 

        [HttpPost]
        public IActionResult UretimOnayliDetailInsert( string values)
        { 
            return Ok(_uretimRepository.UretimOnayliDetailInsert(values));
        }

        [HttpPut]
        public IActionResult UretimOnayliUpdate(int key, string values)
        {
            object[] islem = _uretimRepository.UretimOnayliUpdate(Request.HttpContext, key, values,_logRepository);
            if ((bool)islem[0])
                return Ok();
            else return BadRequest(islem[1]);
        }

        [HttpDelete]
        public IActionResult UretimOnayliDelete(int key)
        {
            object[] islem = _uretimRepository.UretimOnayliDelete(key);
            if ((bool)islem[0])
                return Ok();
            else return BadRequest(islem[1]);
        }

        [HttpPost]
        public IActionResult UretimOnayliInsert(string values)
        {
            return Ok(_uretimRepository.UretimOnayliInsert(values));
        }

        [HttpGet]
        public object GetMarkets(DataSourceLoadOptions loadOptions, [FromQuery(Name = "all")] bool hepsi) => DataSourceLoader.Load(_uretimRepository.GetMarketList(hepsi), loadOptions);
        [HttpGet]
        public object GetProductionTypes(DataSourceLoadOptions loadOptions, [FromQuery(Name = "all")] bool hepsi) => DataSourceLoader.Load(_uretimRepository.GetProductionTypes(Request.HttpContext, hepsi), loadOptions);
        [HttpGet]
        public object GetOrderList(DataSourceLoadOptions loadOptions, [FromQuery(Name = "all")] bool hepsi) => DataSourceLoader.Load(_uretimRepository.GetOrderList(hepsi), loadOptions);
        [HttpGet]
        public object GetFabricList(DataSourceLoadOptions loadOptions, [FromQuery(Name = "all")] bool hepsi) => DataSourceLoader.Load(_uretimRepository.GetFabricTypes(Request.HttpContext,hepsi), loadOptions);
        [HttpGet]
        public object GetBrandList(DataSourceLoadOptions loadOptions, [FromQuery(Name = "all")] bool hepsi) => DataSourceLoader.Load(_uretimRepository.GetBrandList(Request.HttpContext, hepsi), loadOptions);
        [HttpGet]
        public object GetSubBrandList(DataSourceLoadOptions loadOptions, [FromQuery(Name = "all")] bool hepsi) => DataSourceLoader.Load(_uretimRepository.GetSubBrandList(Request.HttpContext, hepsi), loadOptions);
        [HttpGet]
        public object GetSeasonList(DataSourceLoadOptions loadOptions) => DataSourceLoader.Load(_uretimRepository.GetSeasonList(), loadOptions);
        [HttpGet]
        public object GetProductGroupList(DataSourceLoadOptions loadOptions, [FromQuery(Name = "all")] bool hepsi) => DataSourceLoader.Load(_uretimRepository.GetProductGroupList(hepsi), loadOptions);
        [HttpGet]
        public object GetRecipeList(DataSourceLoadOptions loadOptions, [FromQuery(Name = "all")] bool hepsi) => DataSourceLoader.Load(_uretimRepository.GetRecipeList(hepsi), loadOptions);
        [HttpGet]
        public object GetRecipeListByType(DataSourceLoadOptions loadOptions, [FromQuery(Name = "all")] bool hepsi, [FromQuery(Name = "TYPE")] int TYPE) => DataSourceLoader.Load(_uretimRepository.GetRecipeListByType(hepsi, TYPE), loadOptions);
        [HttpGet]
        public object GetCollectionTypes(DataSourceLoadOptions loadOptions, [FromQuery(Name = "all")] bool hepsi) => DataSourceLoader.Load(_uretimRepository.GetCollectionTypes(hepsi), loadOptions);
        [HttpGet]
        public object GetBandList(DataSourceLoadOptions loadOptions, [FromQuery(Name = "all")] bool hepsi) => DataSourceLoader.Load(_uretimRepository.GetBandList(hepsi), loadOptions);
        [HttpGet]
        public object GetApprovalStatusList(DataSourceLoadOptions loadOptions, [FromQuery(Name = "all")] bool hepsi) => DataSourceLoader.Load(_uretimRepository.GetApprovalStatueList(), loadOptions);
        [HttpGet]
        public object GetCurrencyUnits(DataSourceLoadOptions loadOptions) => DataSourceLoader.Load(_uretimRepository.GetCurrencyUnits(), loadOptions);

        public IActionResult _PartialUretimListesiAktarimExcel() => PartialView();

        public IActionResult _PartialUretimLog([FromQuery(Name = "HEADER_ID")] int HEADER_ID) => PartialView(HEADER_ID);

        public IActionResult _PartialUretimDetailLog([FromQuery(Name = "DETAIL_ID")] int DETAIL_ID) => PartialView(DETAIL_ID);

        [HttpGet]
        public object UretimOnayliLogLoad(DataSourceLoadOptions loadOptions, [FromQuery(Name = "HEADER_ID")] int HEADER_ID)
        {
            return DataSourceLoader.Load(_uretimRepository.OnayliUretimListesiLog(HEADER_ID, _logRepository), loadOptions);
        }
        [HttpGet]
        public object UretimOnayliDetailLogLoad(DataSourceLoadOptions loadOptions, [FromQuery(Name = "DETAIL_ID")] int DETAIL_ID)
        {
            return DataSourceLoader.Load(_uretimRepository.OnayliUretimListesiLogDetail(DETAIL_ID, _logRepository), loadOptions);
        }


    }
}
