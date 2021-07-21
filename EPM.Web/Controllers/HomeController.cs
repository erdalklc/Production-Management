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
        public IActionResult Index()
        { 
            ViewBag.PageTitle = "Ana Sayfa";
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

       
    }
}
