using EPM.Fason.Core.Nesneler;
using EPM.Fason.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        [HttpGet]
        public ActionResult<string> Test()
        {
            return "Erdal";
        }

        [HttpPost]
        public ActionResult FasonUretimInsert(PRODUCTION_HEADER header)
        {
            object[] obj = _aktarimService.FasonUretimInsert(header);
            if ((bool)obj[0])
                return Ok();
            else return BadRequest(obj[1].ToString()); 
        }
    }
}
