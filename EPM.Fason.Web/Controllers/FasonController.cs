using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using EPM.Fason.Dto.Fason;
using EPM.Fason.Service.Services;
using EPM.Fason.Web.Attributes;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.Fason.Web.Controllers
{
    [ServiceFilter(typeof(AppFilterAttribute), Order = 1)]
    public class FasonController : Controller
    {
        private readonly IFasonService _fasonService;
        public FasonController(IFasonService fasonService)
        {
            _fasonService = fasonService;
        }
        public IActionResult SiparisListesi(SIPARIS_FILTER filter)
        {
            return View(filter);
        }
        [HttpGet]
        public object MenuList(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_fasonService.GetMenuList(), loadOptions);
        }
        public PartialViewResult _PartialLeftMenu()
        {
            return PartialView("~/Views/Shared/_PartialLeftMenu.cshtml");
        }

        [HttpGet]
        public object SiparisListesiLoad(DataSourceLoadOptions loadOptions, SIPARIS_FILTER liste)
        { 
            return DataSourceLoader.Load(_fasonService.GetSiparisList(liste), loadOptions);

        }

        [HttpGet]
        public object SiparisListesiDetailLoad(DataSourceLoadOptions loadOptions, int ENTEGRATION_HEADER_ID)
        {
            return DataSourceLoader.Load(_fasonService.GetSiparisDetailList(ENTEGRATION_HEADER_ID), loadOptions);

        }

        [HttpPost, HttpGet]
        public IActionResult _PartialSiparisListesi(SIPARIS_FILTER liste) => PartialView(liste);

        [HttpPost, HttpGet]
        public IActionResult _PartialSiparisListesiDetail(int ENTEGRATION_HEADER_ID) => PartialView(ENTEGRATION_HEADER_ID);
    }
}
