using EPM.WMS.Api.ServiceHelper;
using EPM.WMS.Dto.Login;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.WMS.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WMSController : ControllerBase
    {
        public ActionResult<LoginReturnModel> Login()
        {
            return WMSServiceHelper.Login();
        }
    }
}
