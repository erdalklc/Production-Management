using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using EPM.Service.Base;
using EPM.Web.ServiceHelper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IMenuService _menuService;
        public AdminController(IMenuService menuService)
        {
            _menuService = menuService;
        }
        public IActionResult _PartialAyarlar()
        {
            return PartialView();
        }

        public IActionResult _PartialKullaniciYetki()
        {
            if (!_menuService.CanUserEnterTools(Request.HttpContext))
                return BadRequest("Yetki Aşımı");
            return PartialView();
        }

        [HttpGet, HttpPost]
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
            return DataSourceLoader.Load(AdminServiceHelper.GetUserList(), loadOptions);
        }

        [HttpGet]
        public object GetUserResponsibiity(DataSourceLoadOptions loadOptions, [FromQuery(Name = "USER_CODE")] string USER_CODE)
        {
            return DataSourceLoader.Load(AdminServiceHelper.GetUserResponsibiity(USER_CODE), loadOptions);
        }
        [HttpPut]
        public IActionResult UserResponsibilityUpdate(int key, string values, string USER_CODE)
        {
            object[] islem = AdminServiceHelper.UserResponsibilityUpdate(key, values, USER_CODE);
            if ((bool)islem[0])
                return Ok();
            else return BadRequest(islem[1]);
        }


        [HttpGet]
        public object GetUserFabricTypes(DataSourceLoadOptions loadOptions, [FromQuery(Name = "USER_CODE")] string USER_CODE)
        {
            return DataSourceLoader.Load(AdminServiceHelper.GetUserFabricTypes(USER_CODE), loadOptions);
        }


        [HttpGet]
        public object GetUserBrands(DataSourceLoadOptions loadOptions, [FromQuery(Name = "USER_CODE")] string USER_CODE)
        {
            return DataSourceLoader.Load(AdminServiceHelper.GetUserBrands(USER_CODE), loadOptions);
        }

        [HttpPut]
        public IActionResult UserBrandsUpdate(int key, string values, string USER_CODE)
        {
            object[] islem = AdminServiceHelper.UserBrandsUpdate(key, values, USER_CODE);
            if ((bool)islem[0])
                return Ok();
            else return BadRequest(islem[1]);
        }


        [HttpPut]
        public IActionResult UserFabricTypeUpdate(int key, string values, string USER_CODE)
        {
            object[] islem = AdminServiceHelper.UserFabricTypeUpdate(key, values, USER_CODE);
            if ((bool)islem[0])
                return Ok();
            else return BadRequest(islem[1]);
        }


        [HttpGet]
        public object GetUserProductionTypes(DataSourceLoadOptions loadOptions, [FromQuery(Name = "USER_CODE")] string USER_CODE)
        {
            return DataSourceLoader.Load(AdminServiceHelper.GetUserProductionTypes(USER_CODE), loadOptions);
        }

        [HttpPut]
        public IActionResult UserProductionTypeUpdate(int key, string values, string USER_CODE)
        {
            object[] islem = AdminServiceHelper.UserProductionTypeUpdate(key, values, USER_CODE);
            if ((bool)islem[0])
                return Ok();
            else return BadRequest(islem[1]);
        }
    }
}
