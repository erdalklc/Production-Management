using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using EPM.Core.Managers;
using EPM.Core.Models;
using EPM.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.Web.Controllers
{
    [ServiceFilter(typeof(AppFilterAttribute), Order = 1)]
    public class SatinAlmaController : Controller
    {
        private readonly ISatinAlmaService _satinAlmaRepository;
        public SatinAlmaController(ISatinAlmaService satinAlmaRepository) => _satinAlmaRepository = satinAlmaRepository;
        public IActionResult SatinAlmaOlustur()
        {
            return View();
        }
        public IActionResult _PartialSatinAlmaForm()
        {
            return PartialView();
        }

        [HttpGet]
        public object GetInventories(DataSourceLoadOptions loadOptions) => DataSourceLoader.Load(_satinAlmaRepository.GetInventories(), loadOptions);

        [HttpGet]
        public object GetVendorList(DataSourceLoadOptions loadOptions) => DataSourceLoader.Load(_satinAlmaRepository.GetVendorList(), loadOptions);

        [HttpGet]
        public object GetParaBirimleriList(DataSourceLoadOptions loadOptions,[FromQuery(Name = "VENDOR_SITE_ID")] int VENDOR_SITE_ID) => DataSourceLoader.Load(_satinAlmaRepository.GetParaBirimleri(105, VENDOR_SITE_ID), loadOptions);

        public IActionResult SatinAlmaListesiAktarExcelYukle()
        {
            OperationResult result = _satinAlmaRepository.SatinAlmaListesiniAktarExcelYukle(Request, 105);
            if (result.ISOK)
                return Ok("Başarılı");
            else return BadRequest(result.SYSTEMMESSAGE);
        }
        [HttpPost]
        public IActionResult SatinAlmaListesiAktar([FromBody] JObject obj)
        {
            _satinAlmaRepository.SatinAlmaAktar(Request.HttpContext, obj);
            return Ok();
        }
    }
}
