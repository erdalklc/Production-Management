using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using EPM.Fason.Dto.Fason;
using EPM.Fason.Dto.Helpers;
using EPM.Fason.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

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
            return View(new INSPECTION_FILTER());
        }
        [HttpGet]
        public object SiparisListesiDetailLoad(DataSourceLoadOptions loadOptions, int ENTEGRATION_HEADER_ID)
        {
            return DataSourceLoader.Load(_inspectionService.GetSiparisDetailList(ENTEGRATION_HEADER_ID), loadOptions);

        }


        [HttpPut, HttpGet, HttpPost]
        public object UpdateAQL([FromBody] JObject elem) => _inspectionService.UpdateAQL(elem);
        [HttpPut, HttpGet, HttpPost]
        public object UpdateNumbers([FromBody] JObject elem) => _inspectionService.UpdateNumbers(elem);
        public IActionResult CreateAQL(int ENTEGRATION_HEADER_ID,int TYPE)
        {
            var user = new CookieHelper().GetUserFromCookie(Request.HttpContext);
            return Json(_inspectionService.GetAQL(user.USER_ID, ENTEGRATION_HEADER_ID,TYPE));
        }

        [HttpPost, HttpGet]
        public IActionResult _PartialSiparisIslemler(int ENTEGRATION_HEADER_ID)
        {
            return PartialView(_inspectionService.SiparisIslemlerKontrol(ENTEGRATION_HEADER_ID));
        }
        [HttpGet, HttpPost]
        public IActionResult _PartialPopupHtml() => PartialView();
        [HttpPost, HttpGet]
        public IActionResult _PartialInspectionInlineAQL(int ENTEGRATION_HEADER_ID) => PartialView(ENTEGRATION_HEADER_ID);

        [HttpPost, HttpGet]
        public IActionResult _PartialSiparisListesiDetail(int ENTEGRATION_HEADER_ID) => PartialView(ENTEGRATION_HEADER_ID);

        [HttpPost, HttpGet]
        public IActionResult _PartialSiparisListesi(INSPECTION_FILTER liste) => PartialView(_inspectionService.GetSiparisList(liste));


        [HttpGet]
        public object GetInspectionInlineAQL(DataSourceLoadOptions loadOptions,int ENTEGRATION_HEADER_ID)
        {
            var user = new CookieHelper().GetUserFromCookie(Request.HttpContext);
            return DataSourceLoader.Load(_inspectionService.GetInspectionInlineAQL(user.USER_ID, ENTEGRATION_HEADER_ID), loadOptions);
        }

        [HttpPost]
        public IActionResult InsertInspectionLine(string values)
        {
            var user = new CookieHelper().GetUserFromCookie(Request.HttpContext);
            var newOrder = _inspectionService.InsertInspection(user.USER_ID, values); 

            return Ok(newOrder);
        }
        [HttpPut]
        public IActionResult UpdateInspectionLine(int key, string values) => Ok(_inspectionService.UpdateInspection(key,values)); 

        [HttpDelete]
        public void DeleteInspectionLine(int key)=> _inspectionService.DeleteInspection(key);

        [HttpPost, HttpGet]
        public IActionResult _PartialInspectionList(INSPECTION_FILTER liste) => PartialView(_inspectionService.GetInspectionList(liste));

        [HttpGet]
        public object GetFasonUsers(DataSourceLoadOptions loadOptions, [FromQuery(Name = "all")] bool hepsi) => DataSourceLoader.Load(_inspectionService.GetFasonUsers(hepsi), loadOptions); 

        [HttpGet]
        public object GetInspectorList(DataSourceLoadOptions loadOptions, [FromQuery(Name = "all")] bool hepsi) => DataSourceLoader.Load(_inspectionService.GetInspectorList(hepsi), loadOptions);

        [HttpGet]
        public object GetSeasonList(DataSourceLoadOptions loadOptions, [FromQuery(Name = "all")] bool hepsi) => DataSourceLoader.Load(_inspectionService.GetSeasonList(hepsi), loadOptions);

        [HttpGet]
        public object GetModelRenk(DataSourceLoadOptions loadOptions, [FromQuery(Name = "all")] bool hepsi) => DataSourceLoader.Load(_inspectionService.GetModelRenk(hepsi), loadOptions);

        [HttpGet]
        public object GetProcessList(DataSourceLoadOptions loadOptions, [FromQuery(Name = "all")] bool hepsi) => DataSourceLoader.Load(_inspectionService.GetProcessList(hepsi), loadOptions);

        [HttpPost]
        public IActionResult SaveAQL([FromBody] JObject elem) => Json(_inspectionService.SaveAQL(elem));
    }
}
