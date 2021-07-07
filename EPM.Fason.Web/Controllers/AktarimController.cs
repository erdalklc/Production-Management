﻿using EPM.Fason.Dto.Fason;
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
        public ActionResult<PRODUCTION_STATUS> GetProductionStatus(int HEADER_ID) => _aktarimService.GetProductionStatus(HEADER_ID);

        [HttpGet]
        public ActionResult<IEnumerable<PRODUCTION_PROCESS>> GetProcessList() =>  _aktarimService.GetProcessList();

        [HttpGet]
        public ActionResult<IEnumerable<PRODUCTION_FASON_USERS>> GetFasonUsers() => _aktarimService.GetFasonUsers();

        [HttpPost]
        public ActionResult CreateOrder(Tuple<PRODUCTION_HEADER, List<PRODUCTION_PROCESS>, int, DateTime> t)
        { 
            object[] obj= _aktarimService.SiparisOlustur(t.Item1, t.Item2, t.Item3, t.Item4);
            if ((bool)obj[0])
                return Ok();
            else return BadRequest(obj[1].ToString());
        }
    }
}
