 using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
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
        [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Client)]
        public PartialViewResult _PartialLeftMenu(string ACTION,string CONTROLLER)
        {
            var menuList = _menuService.GetMenuList(Request.HttpContext);
            foreach (var item in menuList)
            {
                if (item.ACTION != null)
                {
                    if (item.ACTION == ACTION && item.CONTROLLER == CONTROLLER)
                        item.SELECTED = true;
                    if (item.SELECTED)
                    {
                        if (item.CATEGORY_ID != 0)
                            menuList.Find(ob => ob.ID == item.CATEGORY_ID).ISEXPANDED = true;
                    }

                }
                else item.SELECTED = false;
            } 
            return PartialView("~/Views/Shared/_PartialLeftMenu.cshtml", menuList);
        }

        [HttpGet,HttpPost] 
        public object GetMenuList()
        { 
            return _menuService.GetMenuList(Request.HttpContext);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View();
        }

       
    }
}
