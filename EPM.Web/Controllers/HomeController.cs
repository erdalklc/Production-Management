using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using EPM.Core.Helpers;
using EPM.Core.Managers;
using EPM.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EPM_Web.Controllers
{
    [ServiceFilter(typeof(AppFilterAttribute), Order = 1)]
    public class HomeController : Controller
    {
        private readonly IAyarlarService _ayarlarRepository;
        private readonly IADAccountService _aDARepository;
        public HomeController(IAyarlarService ayarlarRepository, IADAccountService aDARepository)
        {
            _ayarlarRepository = ayarlarRepository;
            _aDARepository = aDARepository;
        }
        public IActionResult Index()
        { 
            ViewBag.PageTitle = "Ana Sayfa";
            return View();
        }

        public IActionResult About() {
            return View();
        }
        public PartialViewResult _PartialLeftMenu()
        {
            return PartialView("~/Views/Shared/_PartialLeftMenu.cshtml");
        }

        [HttpGet,HttpPost] 
        public object GetMenuList()
        {
            return  new MenuHelper().GetMenuList(Request.HttpContext);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View();
        }

        public IActionResult _PartialAyarlar()
        {
            return PartialView();
        }
         
        public IActionResult _PartialKullaniciYetki()
        {
            return PartialView();
        }

        [HttpGet,HttpPost]
        public PartialViewResult _PartialKullaniciYetkiListesi(string USER_CODE)
        {
            return PartialView("_PartialKullaniciYetkiListesi", USER_CODE);
        }
        [HttpGet, HttpPost]
        public IActionResult _PartialKullaniciFabricTypes(string USER_CODE)
        {
            return PartialView("_PartialKullaniciFabricTypes", USER_CODE);
        }
        [HttpGet, HttpPost]
        public IActionResult _PartialKullaniciProductionTypes(string USER_CODE)
        {
            return PartialView("_PartialKullaniciProductionTypes", USER_CODE);
        }

        [HttpGet, HttpPost]
        public IActionResult _PartialKullaniciBrands(string USER_CODE)
        {
            return PartialView("_PartialKullaniciBrands", USER_CODE);
        }

        [HttpGet]
        public object GetUserList(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_aDARepository.GetAccounts(), loadOptions);
        }

        [HttpGet]
        public object GetUserResponsibiity(DataSourceLoadOptions loadOptions, [FromQuery(Name = "USER_CODE")] string USER_CODE)
        {
            return DataSourceLoader.Load(_ayarlarRepository.GetUserResponsibiity(USER_CODE), loadOptions);
        } 
        [HttpPut]
        public IActionResult UserResponsibilityUpdate(int key, string values, string USER_CODE)
        {
            object[] islem = _ayarlarRepository.UserResponsibilityUpdate(key, values, USER_CODE);
            if ((bool)islem[0])
                return Ok();
            else return BadRequest(islem[1]);
        }


        [HttpGet]
        public object GetUserFabricTypes(DataSourceLoadOptions loadOptions, [FromQuery(Name = "USER_CODE")] string USER_CODE)
        {
            return DataSourceLoader.Load(_ayarlarRepository.GetUserFabricTypes(USER_CODE), loadOptions);
        }


        [HttpGet]
        public object GetUserBrands(DataSourceLoadOptions loadOptions, [FromQuery(Name = "USER_CODE")] string USER_CODE)
        {
            return DataSourceLoader.Load(_ayarlarRepository.GetUserBrands(USER_CODE), loadOptions);
        }

        [HttpPut]
        public IActionResult UserBrandsUpdate(int key, string values, string USER_CODE)
        {
            object[] islem = _ayarlarRepository.UserBrandsUpdate(key, values, USER_CODE);
            if ((bool)islem[0])
                return Ok();
            else return BadRequest(islem[1]);
        }


        [HttpPut]
        public IActionResult UserFabricTypeUpdate(int key, string values, string USER_CODE)
        {
            object[] islem = _ayarlarRepository.UserFabricTypeUpdate(key, values, USER_CODE);
            if ((bool)islem[0])
                return Ok();
            else return BadRequest(islem[1]);
        }


        [HttpGet]
        public object GetUserProductionTypes(DataSourceLoadOptions loadOptions, [FromQuery(Name = "USER_CODE")] string USER_CODE)
        {
            return DataSourceLoader.Load(_ayarlarRepository.GetUserProductionTypes(USER_CODE), loadOptions);
        }

        [HttpPut]
        public IActionResult UserProductionTypeUpdate(int key, string values, string USER_CODE)
        {
            object[] islem = _ayarlarRepository.UserProductionTypeUpdate(key, values, USER_CODE);
            if ((bool)islem[0])
                return Ok();
            else return BadRequest(islem[1]);
        }
    }
}
