 using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using EPM.Core.Helpers;
using EPM.Core.Managers;
using EPM.Core.Services;
using EPM.Web.ServiceHelper;
using Microsoft.AspNetCore.Mvc; 

namespace EPM_Web.Controllers
{
    [ServiceFilter(typeof(AppFilterAttribute), Order = 1)]
    public class HomeController : Controller
    {
        //Erdal KILIÇ
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
