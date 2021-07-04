using EPM.Core.Managers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.Web.Controllers
{
    [ServiceFilter(typeof(AppFilterAttribute), Order = 1)]
    public class EtiketYazdirController : Controller
    {
        public IActionResult EtiketYazdir()
        {
            return View();
        }
    }
}
