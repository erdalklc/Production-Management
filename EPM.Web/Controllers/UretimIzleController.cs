using EPM.Service.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.Web.Controllers
{
    public class UretimIzleController : Controller
    {
        private readonly IUretimIzleService _uretimIzleService;

        public UretimIzleController(IUretimIzleService uretimIzleService)
        {
            _uretimIzleService = uretimIzleService;
        }
        public IActionResult UretimIzle()
        {
            return View();
        }

        public IActionResult _PartialSeasons()
        {
            return PartialView();
        }
        public IActionResult _PartialWeeks(int SEASON)
        {
            return PartialView(_uretimIzleService.GetWeeks(SEASON));
        }

        public IActionResult _PartialModels(int SEASON,int YEAR, int WEEK)
        {
            return PartialView(_uretimIzleService.GetModels(SEASON, YEAR, WEEK));
        }
    }
}
