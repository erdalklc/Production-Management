using EPM.Core.FormModels; 
using EPM.Core.Services;
using EPM.Dto.Models;
using EPM.Tools.Helpers; 
using Microsoft.AspNetCore.Mvc; 

namespace EPM.Web.Controllers
{
    //[ServiceFilter(typeof(AppFilterAttribute), Order = 1)]

    public class LoginController : Controller
    {
        private readonly ILoginService _loginRepository;
        private readonly IADAccountService _aDARepository;
        private readonly ILogService _logRepository;
        public LoginController(ILoginService loginRepository, IADAccountService aDARepository, ILogService logRepository)
        {
            _loginRepository = loginRepository;
            _aDARepository = aDARepository;
            _logRepository = logRepository;
        }
        public IActionResult Login(Login model)
        { 
            if (ModelState.IsValid && model.EMail != null && model.Password != null)
            {
               
                WebLogin loginM = _loginRepository.LoginAD(Request.HttpContext, model, _aDARepository);
                if (loginM.USER_CODE != "")
                {
                    _loginRepository.LoginLog(loginM, _logRepository);
                    return RedirectToAction("Index", "Home");
                } 
                else
                {
                    model.Message= "E-Mail veya şifre bilgisi hatalı! Lütfen Kontrol Ediniz"; 
                    ModelState.AddModelError("", "EMail veya şifre hatalı!"); 
                }
            }
            else
            {
                CookieHelper cookieHelper = new CookieHelper();
                string mail = cookieHelper.GetObjectFromCookie<string>(Request.HttpContext, "EMAIL");
                if (mail != "")
                {
                    model.BeniHatirla = true;model.EMail = mail;
                }
            }
            return View(model);
        }


        public IActionResult LogOut()
        {
            new CookieHelper().DeleteCookie(Request.HttpContext, "USER");
            return RedirectToAction("Login");
        }
    }
}
