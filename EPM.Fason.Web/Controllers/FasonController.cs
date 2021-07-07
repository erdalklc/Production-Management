using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.Fason.Web.Controllers
{
    public class FasonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
