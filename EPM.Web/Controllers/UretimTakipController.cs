using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using EPM.Core.FormModels.Uretim;
using EPM.Core.Helpers;
using EPM.Core.Managers;
using EPM.Core.Models;
using EPM.Core.Services;
using EPM.Fason.Dto.Fason;
using EPM.Track.Dto.Track;
using EPM.Web.ServiceHelper;
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
        public UretimTakipController()
        { 
        }

        public IActionResult Takip()
        {
            return View(new TrackList_Filter());
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
        public IActionResult _PartialUretimTakipFiltrele(TrackList_Filter liste) => PartialView(liste);

        public IActionResult _PartialUretimTakipDetay(int HEADER_ID, int RECIPE)
        {
            switch (RECIPE)
            {
                case 1:
                case 2:
                case 3:
                    return PartialView("_PartialUretimTakipSatinAlmaDetayi", HEADER_ID);
                case 4:
                case 5:
                    return PartialView("_PartialUretimTakipFason", FasonServiceHelper.FasonTakipListeAyarla( HEADER_ID));
                default:
                    return PartialView("_PartialUretimTakipSatinAlmaDetayi", HEADER_ID);
            }

        }

        public IActionResult FasonSiparisOlustur(PRODUCTION_HEADER header, List<PRODUCTION_PROCESS> plan,int firmaBilgi,DateTime terminTarihi)
        {
            object[] obj =  FasonServiceHelper.FasonSiparisOlustur( header, plan, firmaBilgi, terminTarihi);
            if ((bool)obj[0])
                return Ok();
            else return BadRequest(obj[1].ToString());
        } 
         
        public IActionResult _PartialEgemenOrmeList(string TAKIP_NO, int DETAIL_TAKIP_NO)
        {
            object[] values = { TAKIP_NO, DETAIL_TAKIP_NO };
            return PartialView(values);
        }

        public IActionResult _PartialKumasDepoList(int ITEM_ID, int PO_HEADER_ID)
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

        public IActionResult _PartialProcessInformations(int PO_HEADER_ID, int DETAIL_ID, int HEADER_ID)
        {
            int[] values = { PO_HEADER_ID, DETAIL_ID, HEADER_ID };
            return PartialView(values);
        }

        [HttpGet]
        public object TabPanelListLoad(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(UretimTakipServiceHelper.GetTabList(), loadOptions);
        }

        [HttpGet]
        public object EgemenOrmeListLoad(DataSourceLoadOptions loadOptions, [FromQuery(Name = "TAKIP_NO")] string TAKIP_NO, [FromQuery(Name = "DETAIL_TAKIP_NO")] int DETAIL_TAKIP_NO)
        {
            return DataSourceLoader.Load(UretimTakipServiceHelper.GetEgemenOrmeList(TAKIP_NO, DETAIL_TAKIP_NO), loadOptions);
        }
        [HttpGet]
        public object KumasDepoListLoad(DataSourceLoadOptions loadOptions, [FromQuery(Name = "ITEM_ID")] int ITEM_ID, [FromQuery(Name = "PO_HEADER_ID")] int PO_HEADER_ID)
        {
            return DataSourceLoader.Load(UretimTakipServiceHelper.GetKumasDepoList(ITEM_ID, PO_HEADER_ID), loadOptions);
        }
        [HttpGet]
        public object KesimFoyuListLoad(DataSourceLoadOptions loadOptions, [FromQuery(Name = "ITEM_ID")] int ITEM_ID, [FromQuery(Name = "PO_HEADER_ID")] int PO_HEADER_ID, [FromQuery(Name = "RENK_DETAY")] string RENK_DETAY)
        {
            return DataSourceLoader.Load(UretimTakipServiceHelper.GetKesimFoyuList(ITEM_ID, PO_HEADER_ID, RENK_DETAY), loadOptions);
        }

        [HttpGet]
        public object BantBitisListLoad(DataSourceLoadOptions loadOptions, [FromQuery(Name = "ITEM_ID")] int ITEM_ID, [FromQuery(Name = "PO_HEADER_ID")] int PO_HEADER_ID, [FromQuery(Name = "RENK_DETAY")] string RENK_DETAY)
        {
            return DataSourceLoader.Load(UretimTakipServiceHelper.GetBantList(ITEM_ID, PO_HEADER_ID, RENK_DETAY), loadOptions);
        }
        [HttpGet]
        public object KaliteBitisListLoad(DataSourceLoadOptions loadOptions, [FromQuery(Name = "ITEM_ID")] int ITEM_ID, [FromQuery(Name = "PO_HEADER_ID")] int PO_HEADER_ID, [FromQuery(Name = "RENK_DETAY")] string RENK_DETAY)
        {
            return DataSourceLoader.Load(UretimTakipServiceHelper.GetKaliteList(ITEM_ID, PO_HEADER_ID, RENK_DETAY), loadOptions);
        }

        [HttpGet]
        public object EgemenGerceklesenLoad(DataSourceLoadOptions loadOptions, KaliteGerceklesenFilter liste)
        {
            return DataSourceLoader.Load(UretimTakipServiceHelper.GetKaliteGerceklesenler(liste), loadOptions);
        }
        [HttpGet]
        public object UretimTakiplLoad(DataSourceLoadOptions loadOptions, TrackList_Filter liste)
        {
            return DataSourceLoader.Load(UretimTakipServiceHelper.GetUretimTakipListesi(new CookieHelper().GetUserFromCookie(Request.HttpContext).USER_CODE, liste), loadOptions);
        }

        [HttpGet]
        public object SatinAlmaDetayGetir(DataSourceLoadOptions loadOptions, [FromQuery(Name = "HEADER_ID")] int HEADER_ID)
        {
            return DataSourceLoader.Load(UretimTakipServiceHelper.GetSatinAlmaDetay(HEADER_ID), loadOptions);
        }

        [HttpGet]
        public object GetProcessInformations(DataSourceLoadOptions loadOptions, [FromQuery(Name = "PO_HEADER_ID")] int PO_HEADER_ID, [FromQuery(Name = "DETAIL_ID")] int DETAIL_ID, [FromQuery(Name = "HEADER_ID")] int HEADER_ID)
        {
            return DataSourceLoader.Load(UretimTakipServiceHelper.GetProcessInformations(PO_HEADER_ID, DETAIL_ID, HEADER_ID), loadOptions);
        }

        [HttpGet]
        public object ProductionOrderslLoad(DataSourceLoadOptions loadOptions, int HEADER_ID)
        {
            return DataSourceLoader.Load(UretimTakipServiceHelper.GetProductionOrderList(HEADER_ID), loadOptions);
        }

        [HttpPost]
        public IActionResult ProductionOrdersInsert(string values)
        {
            object[] islem = UretimTakipServiceHelper.ProductionOrdersInsert(values);
            if ((bool)islem[0])
                return Ok();
            else return BadRequest(islem[1]);
        }
        [HttpPut]
        public IActionResult ProductionOrdersUpdate(int key, string values)
        {
            object[] islem = UretimTakipServiceHelper.ProductionOrdersUpdate(key, values);
            if ((bool)islem[0])
                return Ok();
            else return BadRequest(islem[1]);
        }

        [HttpDelete]
        public IActionResult ProductionOrdersDelete(int key)
        {
            object[] islem = UretimTakipServiceHelper.ProductionOrdersDelete(key);
            if ((bool)islem[0])
                return Ok();
            else return BadRequest(islem[1]);
        } 

        [HttpGet]
        public object GetContractList(DataSourceLoadOptions loadOptions) => DataSourceLoader.Load(UretimTakipServiceHelper.GetContractList(), loadOptions);



    }
}
