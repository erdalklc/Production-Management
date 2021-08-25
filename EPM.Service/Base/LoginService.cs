using EPM.Dto.Base;
using EPM.Dto.FormModels.Login;
using EPM.Dto.Loglar;
using EPM.Dto.Models;
using EPM.Repository.Base;
using EPM.Tools.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;

namespace EPM.Service.Base
{
    public class LoginService : ILoginService
    { 
        private readonly IEPMRepository _epmRepository;
        public LoginService(IEPMRepository epmRepository)
        {
            _epmRepository = epmRepository;
        }

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
                    login.responsibility = _epmRepository.DeserializeList<EPM_USER_RESPONSIBILITY>("SELECT * FROM FDEIT005.EPM_USER_RESPONSIBILITY WHERE USER_CODE='" + login.USER_CODE + "'");
                    CookieHelper cHelper = new CookieHelper();
                    cHelper.AddCookie(context, login, "USER");

                    if (model.BeniHatirla)  
                        new CookieHelper().AddCookie(context, model.EMail, "EMAIL"); 
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
