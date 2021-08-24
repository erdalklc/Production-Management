 using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using EPM.Core.Helpers;
using EPM.Service.Base;
using EPM.Tools.Managers;
using EPM.Web.ServiceHelper;
using Microsoft.AspNetCore.Mvc; 

namespace EPM_Web.Controllers
{
    [ServiceFilter(typeof(AppFilterAttribute), Order = 1)]
    public class HomeController : Controller
    {
        private readonly IMenuService _menuService;
        public HomeController(IMenuService menuService)
        {
            _menuService = menuService;
        }
        public IActionResult Index()
        { 
            ViewBag.PageTitle = "Ana Sayfa";
            return View();
        }

        [HttpGet, HttpPost]
        public PartialViewResult _PartialLeftMenu(string ACTION,string CONTROLLER)
        {
            ViewData["CONTROLLER"] = CONTROLLER;
            ViewData["ACTION"] = ACTION;
            return PartialView("~/Views/Shared/_PartialLeftMenu.cshtml", _menuService.GetMenuList(Request.HttpContext));
        }

        [HttpGet,HttpPost] 
        public object GetMenuList()
        {
            Request.HttpContext.Session.
            return _menuService.GetMenuList(Request.HttpContext);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View();
        }

       
    }
}
