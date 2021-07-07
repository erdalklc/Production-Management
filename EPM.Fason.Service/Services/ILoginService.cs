using EPM.Fason.Dto.Fason;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Fason.Service.Services
{
    public interface ILoginService
    {
        public PRODUCTION_USER_LOGINS Login(HttpContext context, Login model);
    }
}
