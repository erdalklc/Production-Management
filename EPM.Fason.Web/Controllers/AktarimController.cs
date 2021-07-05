using EPM.Fason.Core.Models;
using EPM.Fason.Core.Nesneler;
using EPM.Fason.Core.Services; 
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EPM.Fason.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AktarimController : ControllerBase
    {
        public readonly IAktarimService _aktarimService;
        public AktarimController(IAktarimService aktarimService)
        {
            _aktarimService = aktarimService;
        }

        [HttpPost]
        public ActionResult FasonUretimInsert(PRODUCTION_HEADER header)
        {
            object[] obj = _aktarimService.FasonUretimInsert(header);
            if ((bool)obj[0])
                return Ok(obj);
            else return BadRequest(obj[1].ToString());
        }

        [HttpPost]
        public ActionResult<PRODUCTION_STATUS> GetProductionStatus(int HEADER_ID) => _aktarimService.GetProductionStatus(HEADER_ID);

        [HttpGet]
        public ActionResult<List<PRODUCTION_PROCESS>> GetProcessList() => _aktarimService.GetProcessList();

        [HttpGet]
        public ActionResult<List<PRODUCTION_FASON_USERS>> GetFasonUsers() => _aktarimService.GetFasonUsers();
    }
}
