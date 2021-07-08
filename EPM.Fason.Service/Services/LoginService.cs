using EPM.Fason.Dto.Fason;
using EPM.Fason.Dto.Helpers;
using EPM.Fason.Repository.Repository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Fason.Service.Services
{
    public class LoginService : ILoginService
    {
        private readonly IFasonRepository _fasonRepository;
        public LoginService(IFasonRepository fasonRepository)
        {
            _fasonRepository = fasonRepository;
        }
        public PRODUCTION_USER_LOGINS Login(HttpContext context, Login model)
        {
            PRODUCTION_USER_LOGINS login = new PRODUCTION_USER_LOGINS();
            string sql = string.Format("SELECT * FROM  PRODUCTION_FASON_USERS WHERE EMAIL='{0}' AND PASSWORD='{1}'", model.EMail, model.Password);

            PRODUCTION_FASON_USERS user = _fasonRepository.Deserialize<PRODUCTION_FASON_USERS>(sql);

            if (user != null && user.NAME!=null)
            {
                login.EMAIL = user.EMAIL;
                login.ENTERY_DATE = DateTime.Now;
                login.USER_NAME = user.NAME;
                login.USER_ID = user.ID;

                CookieHelper cHelper = new CookieHelper();
                cHelper.AddCookie(context, login, "USER_FASON", CryptHelper.hash);

                if (model.BeniHatirla)
                {

                    cHelper = new CookieHelper();
                    cHelper.AddCookie(context, model.EMail, "EMAIL_FASON", CryptHelper.hash2);
                }
            }

            return login;
        }
    }
}
