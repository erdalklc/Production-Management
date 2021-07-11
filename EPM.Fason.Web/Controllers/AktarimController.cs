using EPM.Fason.Dto.Fason;
using EPM.Fason.Service.Services;
using Microsoft.AspNetCore.Mvc;
using System;
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
         

        [HttpGet]
        public ActionResult<IEnumerable<PRODUCTION_PROCESS>> GetProcessList() =>  _aktarimService.GetProcessList();

        [HttpGet]
        public ActionResult<IEnumerable<PRODUCTION_FASON_USERS>> GetFasonUsers() => _aktarimService.GetFasonUsers();
        


        [HttpPost]
        public ActionResult<object[]> CreateOrder(CREATEORDER order)
        { 
            object[] obj= _aktarimService.SiparisOlustur(order.header, order.plan, order.firma, order.termin);
            if ((bool)obj[0])
                return Ok(obj);
            else return BadRequest(obj);
        }

        [HttpPost]
        public ActionResult<IEnumerable<PRODUCTION_STATUS>> SendProductionsStatues(int[] ids)  => _aktarimService.GetProductionStatus(ids); 
    }
}
