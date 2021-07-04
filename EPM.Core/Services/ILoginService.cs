using EPM.Core.FormModels;
using EPM.Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Text;

namespace EPM.Core.Services
{
    public interface ILoginService
    {
        public void LoginLog(WebLogin login,ILogService logRepository);

        public WebLogin LoginAD(HttpContext context, Login model, IADAccountService _aDAccountRepository);
    }
}
