using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using EPM.Fason.Dto.Fason;
using EPM.Fason.Dto.Helpers;
using EPM.Fason.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace EPM.Fason.Web.Controllers
{
    public class InspectionController : Controller
    {
        private readonly IInspectionService _inspectionService;
        public InspectionController(IInspectionService inspectionService)
        {
            _inspectionService = inspectionService;
        }

        public PartialViewResult _PartialLeftMenuInspection()
        {
            return PartialView("~/Views/Shared/_PartialLeftMenuInspection.cshtml");
        }
        [HttpGet]
        public object MenuList(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_inspectionService.GetMenuList(), loadOptions);
        }
        public IActionResult SiparisListesi()
        {
            return View(new INSPECTION_FILTER());
        }
        public IActionResult InspectionListesi()
        {
            return View();
        }
        [HttpGet]
        public object SiparisListesiDetailLoad(DataSourceLoadOptions loadOptions, int ENTEGRATION_HEADER_ID)
        {
            return DataSourceLoader.Load(_inspectionService.GetSiparisDetailList(ENTEGRATION_HEADER_ID), loadOptions);

        }

        [HttpPost, HttpGet]
        public IActionResult _PartialAQL(int ENTEGRATION_HEADER_ID)
        {
            var user = new CookieHelper().GetUserFromCookie(Request.HttpContext);
            return PartialView(_inspectionService.GetAQL(user.USER_ID,ENTEGRATION_HEADER_ID));
        }

        [HttpPost, HttpGet]
        public IActionResult _PartialSiparisIslemler(int ENTEGRATION_HEADER_ID) => PartialView(ENTEGRATION_HEADER_ID);

        [HttpPost, HttpGet]
        public IActionResult _PartialSiparisListesiDetail(int ENTEGRATION_HEADER_ID) => PartialView(ENTEGRATION_HEADER_ID);

        [HttpPost, HttpGet]
        public IActionResult _PartialSiparisListesi(INSPECTION_FILTER liste) => PartialView(_inspectionService.GetSiparisList(liste));

        [HttpGet]
        public object GetFasonUsers(DataSourceLoadOptions loadOptions, [FromQuery(Name = "all")] bool hepsi) => DataSourceLoader.Load(_inspectionService.GetFasonUsers(hepsi), loadOptions);

        [HttpGet]
        public object GetSeasonList(DataSourceLoadOptions loadOptions, [FromQuery(Name = "all")] bool hepsi) => DataSourceLoader.Load(_inspectionService.GetSeasonList(hepsi), loadOptions);
    }
}
