using EPM.Core.FormModels;
using EPM.Core.Helpers;
using EPM.Core.Loglar;
using EPM.Core.Managers;
using EPM.Core.Models;
using EPM.Core.Nesneler;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;

namespace EPM.Core.Services
{
    public class LoginService : ILoginService
    {
        

        public WebLogin LoginAD(HttpContext context, Login model,IADAccountService _aDAccountRepository)
        {
            WebLogin login = new WebLogin();
            List<ADAccount> accounts = _aDAccountRepository.GetAccounts();
            ADAccount acc = accounts.Find(ob => ob.USER_EMAIL == model.EMail);
            using (var principalContext = new PrincipalContext(ContextType.Domain, "eren.holding"))
            {
                bool ok = principalContext.ValidateCredentials(acc.USER_CODE, model.Password);
                if (ok)
                {
                    login.EMAIL = model.EMail; 
                    login.USER_CODE = acc.USER_CODE;
                    login.USER_NAME = acc.USER_NAME;
                    login.responsibility = OracleServer.DeserializeList<EPM_USER_RESPONSIBILITY>("SELECT * FROM FDEIT005.EPM_USER_RESPONSIBILITY WHERE USER_CODE='" + login.USER_CODE + "'");
                    CookieHelper cHelper = new CookieHelper();
                    cHelper.AddCookie(context, login, "USER", CryptHelper.hash);

                    if (model.BeniHatirla)
                    {

                        cHelper = new CookieHelper();
                        cHelper.AddCookie(context, model.EMail, "EMAIL", CryptHelper.hash2);
                    }
                }
            } 


            return login;
        }

        public void LoginLog(WebLogin login,  ILogService logRepository)
        {
            LOG_Login log = new LOG_Login();
            log.EMAIL = login.EMAIL;
            log.USER_CODE = login.USER_CODE;
            log.USER_NAME = login.USER_NAME;
            logRepository.SaveLogin(log);
        } 
    }
}
