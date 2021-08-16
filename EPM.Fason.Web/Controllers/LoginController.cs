using EPM.Fason.Dto.Fason;
using EPM.Fason.Dto.Helpers;
using EPM.Fason.Service.Services; 
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.Fason.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        public IActionResult Login(Login model)
        {
            if (ModelState.IsValid && model.EMail != null && model.Password != null)
            {
                if (model.YetkiliGirisi)
                {

                }
                else
                {
                    PRODUCTION_USER_LOGINS loginM = _loginService.Login(Request.HttpContext, model);
                    if (loginM.USER_NAME != "")
                    {
                        loginM.ISAUTHORIZED = false;
                        return RedirectToAction("SiparisListesi", "Fason");
                    }
                    else
                    {
                        model.Message = "E-Mail veya şifre bilgisi hatalı! Lütfen Kontrol Ediniz";
                        ModelState.AddModelError("", "EMail veya şifre hatalı!");
                    }
                }
                
            }
            else
            {
                CookieHelper cookieHelper = new CookieHelper();
                string mail = cookieHelper.GetEmailFromCookie(Request.HttpContext);
                if (mail != "")
                {
                    model.BeniHatirla = true; 
                    model.EMail = mail;
                }
            }
            return View(model);
        }

        public IActionResult LogOut()
        {
            new CookieHelper().DeleteUserCookie(Request.HttpContext);
            return RedirectToAction("Login");
        }
    }
}
