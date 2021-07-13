using EPM.Admin.Dto.Admin;
using EPM.Admin.Dto.Extensions;
using EPM.Admin.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.Admin.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        } 
        [HttpGet]
        public ActionResult<List<KullaniciListe>> GetUserList() => _adminService.GetUserList();

        [HttpPost]
        public ActionResult<List<KullaniciYetki>> GetUserResponsibiity(object[] obj) => _adminService.GetUserResponsibiity(obj[0].ToStringParse());

        [HttpPost]
        public ActionResult<object[]> UserResponsibilityUpdate(object[] obj) => _adminService.UserResponsibilityUpdate(obj[0].IntParse(), obj[1].ToStringParse(), obj[2].ToStringParse());

        [HttpPost]
        public ActionResult<object[]> UserFabricTypeUpdate(object[] obj) => _adminService.UserFabricTypeUpdate(obj[0].IntParse(), obj[1].ToStringParse(), obj[2].ToStringParse());

        [HttpPost]
        public ActionResult<List<KullaniciFabricTypes>> GetUserFabricTypes(object[] obj) => _adminService.GetUserFabricTypes(obj[0].ToStringParse());

        [HttpPost]
        public ActionResult<List<KullaniciProductionTypes>> GetUserProductionTypes(object[] obj)=> _adminService.GetUserProductionTypes(obj[0].ToStringParse());

        [HttpPost]
        public ActionResult<object[]> UserProductionTypeUpdate(object[] obj) => _adminService.UserProductionTypeUpdate(obj[0].IntParse(), obj[1].ToStringParse(), obj[2].ToStringParse());

        [HttpPost]
        public ActionResult<object[]> UserBrandsUpdate(object[] obj) => _adminService.UserBrandsUpdate(obj[0].IntParse(), obj[1].ToStringParse(), obj[2].ToStringParse());

        [HttpPost]
        public ActionResult<List<KullaniciMarka>> GetUserBrands(object[] obj) => _adminService.GetUserBrands(obj[0].ToStringParse());
    }
}
