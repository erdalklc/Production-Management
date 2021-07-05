using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using EPM.Core.FormModels.Uretim;
using EPM.Core.FormModels.UretimTakip;
using EPM.Core.Managers;
using EPM.Core.Models;
using EPM.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.Web.Controllers
{
    [ServiceFilter(typeof(AppFilterAttribute), Order = 1)]
    public class UretimTakipController : Controller
    {
        private readonly IOptions<AppServices> _config;
        private readonly IUretimTakipService _uretimTakipRepository;
        public UretimTakipController(IUretimTakipService uretimTakipRepository, IOptions<AppServices> config)
        {
            _uretimTakipRepository = uretimTakipRepository;
            _config = config;
        }
         
        public IActionResult Takip()
        {
            return View(new UretimOnayliListe());
        }

        public IActionResult EgemenGerceklesen()
        {
            return View(new KaliteGerceklesenFilter());
        }
        [HttpGet]
        public IActionResult _PartialProductionOrderList(int HEADER_ID) => PartialView(HEADER_ID);

        [HttpPost, HttpGet]
        public IActionResult _PartiaEgemenGerceklesenFiltrele(KaliteGerceklesenFilter liste) => PartialView(liste);
        [HttpPost, HttpGet]
        public IActionResult _PartialUretimTakipFiltrele(UretimOnayliListe liste) => PartialView(liste);

        public async Task<IActionResult> _PartialUretimTakipDetayAsync(int HEADER_ID,int RECIPE)
        {
            switch (RECIPE)
            {
                case 1:
                case 2:
                case 3:
                    return PartialView("_PartialUretimTakipSatinAlmaDetayi", HEADER_ID);
                case 4:
                case 5:
                    var a=await _uretimTakipRepository.FasonTakipListeAyarlaAsync(_config.Value.FasonTakip,HEADER_ID);
                    return PartialView("_PartialUretimTakipFason", a);
                default:
                    return PartialView("_PartialUretimTakipSatinAlmaDetayi", HEADER_ID);
            }
            
        }

        public IActionResult _PartialEgemenOrmeList(string TAKIP_NO,int DETAIL_TAKIP_NO)
        {
            object[] values = { TAKIP_NO, DETAIL_TAKIP_NO };
            return PartialView(values);
        }

        public IActionResult _PartialKumasDepoList(int ITEM_ID,int PO_HEADER_ID)
        {
            int[] values = { ITEM_ID, PO_HEADER_ID };
            return PartialView(values);
        }

        public IActionResult _PartialKesimFoyuList(int ITEM_ID, int PO_HEADER_ID, string RENK_DETAY)
        {
            object[] values = { ITEM_ID, PO_HEADER_ID, RENK_DETAY };
            return PartialView(values);
        }

        public IActionResult _PartialBantList(int ITEM_ID, int PO_HEADER_ID, string RENK_DETAY)
        {
            object[] values = { ITEM_ID, PO_HEADER_ID, RENK_DETAY };
            return PartialView(values);
        }
        public IActionResult _PartialKaliteList(int ITEM_ID, int PO_HEADER_ID, string RENK_DETAY)
        {
            object[] values = { ITEM_ID, PO_HEADER_ID, RENK_DETAY };
            return PartialView(values);
        }
        public IActionResult _PartialProcessInformations(int PO_HEADER_ID, int DETAIL_ID,int HEADER_ID)
        {
            int[] values = { PO_HEADER_ID, DETAIL_ID , HEADER_ID };
            return PartialView(values);
        }
        [HttpGet]
        public object TabPanelListLoad(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_uretimTakipRepository.GetTabList(), loadOptions);
        }

        [HttpGet]
        public object EgemenOrmeListLoad(DataSourceLoadOptions loadOptions, [FromQuery(Name = "TAKIP_NO")] string TAKIP_NO, [FromQuery(Name = "DETAIL_TAKIP_NO")] int DETAIL_TAKIP_NO)
        {
            return DataSourceLoader.Load(_uretimTakipRepository.GetEgemenOrmeList(TAKIP_NO,DETAIL_TAKIP_NO), loadOptions);
        }
        [HttpGet]
        public object KumasDepoListLoad(DataSourceLoadOptions loadOptions, [FromQuery(Name = "ITEM_ID")] int ITEM_ID, [FromQuery(Name = "PO_HEADER_ID")] int PO_HEADER_ID)
        {
            return DataSourceLoader.Load(_uretimTakipRepository.GetKumasDepoList(ITEM_ID, PO_HEADER_ID), loadOptions);
        }
        [HttpGet]
        public object KesimFoyuListLoad(DataSourceLoadOptions loadOptions, [FromQuery(Name = "ITEM_ID")] int ITEM_ID, [FromQuery(Name = "PO_HEADER_ID")] int PO_HEADER_ID, [FromQuery(Name = "RENK_DETAY")] string RENK_DETAY)
        {
            return DataSourceLoader.Load(_uretimTakipRepository.GetKesimFoyuList(ITEM_ID, PO_HEADER_ID,RENK_DETAY), loadOptions);
        }

        [HttpGet]
        public object BantBitisListLoad(DataSourceLoadOptions loadOptions, [FromQuery(Name = "ITEM_ID")] int ITEM_ID, [FromQuery(Name = "PO_HEADER_ID")] int PO_HEADER_ID, [FromQuery(Name = "RENK_DETAY")] string RENK_DETAY)
        {
            return DataSourceLoader.Load(_uretimTakipRepository.GetBantList(ITEM_ID, PO_HEADER_ID, RENK_DETAY), loadOptions);
        }
        [HttpGet]
        public object KaliteBitisListLoad(DataSourceLoadOptions loadOptions, [FromQuery(Name = "ITEM_ID")] int ITEM_ID, [FromQuery(Name = "PO_HEADER_ID")] int PO_HEADER_ID, [FromQuery(Name = "RENK_DETAY")] string RENK_DETAY)
        {
            return DataSourceLoader.Load(_uretimTakipRepository.GetKaliteList(ITEM_ID, PO_HEADER_ID, RENK_DETAY), loadOptions);
        }

        [HttpGet]
        public object EgemenGerceklesenLoad(DataSourceLoadOptions loadOptions, KaliteGerceklesenFilter liste)
        {
            return DataSourceLoader.Load(_uretimTakipRepository.GetKaliteGerceklesenler(liste), loadOptions);
        }
        [HttpGet]
        public object UretimTakiplLoad(DataSourceLoadOptions loadOptions, UretimOnayliListe liste)
        {
            return DataSourceLoader.Load(_uretimTakipRepository.GetUretimTakipListesi(Request.HttpContext,liste), loadOptions);
        }

        [HttpGet]
        public object SatinAlmaDetayGetir(DataSourceLoadOptions loadOptions, [FromQuery(Name = "HEADER_ID")] int HEADER_ID)
        {
            return DataSourceLoader.Load(_uretimTakipRepository.SatinAlmaDetay(HEADER_ID), loadOptions);
        }

        [HttpGet] 
        public object GetProcessInformations(DataSourceLoadOptions loadOptions, [FromQuery(Name = "PO_HEADER_ID")] int PO_HEADER_ID, [FromQuery(Name = "DETAIL_ID")] int DETAIL_ID, [FromQuery(Name = "HEADER_ID")] int HEADER_ID)
        {
            return DataSourceLoader.Load(_uretimTakipRepository.GetProcessInformations(PO_HEADER_ID, DETAIL_ID, HEADER_ID), loadOptions);
        }

        [HttpGet]
        public object ProductionOrderslLoad(DataSourceLoadOptions loadOptions, int HEADER_ID)
        {
            return DataSourceLoader.Load(_uretimTakipRepository.ProductionOrderList(HEADER_ID), loadOptions);
        }

        [HttpPost]
        public IActionResult ProductionOrdersInsert(string values)
        {
            object[] islem = _uretimTakipRepository.ProductionOrdersInsert(values);
             if ((bool)islem[0])
                return Ok();
            else return BadRequest(islem[1]);
        }
        [HttpPut]
        public IActionResult ProductionOrdersUpdate(int key, string values)
        {
            object[] islem = _uretimTakipRepository.ProductionOrdersUpdate(key, values);
            if ((bool)islem[0])
                return Ok();
            else return BadRequest(islem[1]);
        }

        [HttpDelete]
        public IActionResult ProductionOrdersDelete(int key)
        {
            object[] islem = _uretimTakipRepository.ProductionOrdersDelete(key);
            if ((bool)islem[0])
                return Ok();
            else return BadRequest(islem[1]);
        }


        [HttpGet]
        public object GetContractList(DataSourceLoadOptions loadOptions) => DataSourceLoader.Load(_uretimTakipRepository.GetContractList(), loadOptions);

        

    }
}
