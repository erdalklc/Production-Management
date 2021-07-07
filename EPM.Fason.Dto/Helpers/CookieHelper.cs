using EPM.Fason.Dto.Fason;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.Fason.Dto.Helpers
{

    public class CookieHelper
    {
        public void AddCookie(HttpContext context, object obj, string CookieName, string hash)
        {
            CookieOptions cookieOp = new CookieOptions();
            cookieOp.Expires = DateTime.Now.AddDays(200);
            context.Response.Cookies.Append(CookieName, CryptHelper.Encrypt(JsonConvert.SerializeObject(obj), hash), cookieOp);
        }

        public PRODUCTION_USER_LOGINS GetUserFromCookie(HttpContext context)
        {
            object cookie = context.Request.Cookies["USER_FASON"];
            PRODUCTION_USER_LOGINS user = new PRODUCTION_USER_LOGINS();
            if (cookie != null)
            {
                try
                {
                    user = JsonConvert.DeserializeObject<PRODUCTION_USER_LOGINS>(CryptHelper.Decrypt(cookie.ToString(), CryptHelper.hash));
                }
                catch (Exception) { user = null; }
            }
            else user = null;
            return user;
        }
        public string GetEmailFromCookie(HttpContext context)
        {
            object cookie = context.Request.Cookies["EMAIL_FASON"];
            string mail = "";
            if (cookie != null)
            {
                try
                {
                    mail = JsonConvert.DeserializeObject<string>(CryptHelper.Decrypt(cookie.ToString(), CryptHelper.hash2));
                }
                catch (Exception) { }
            }
            return mail;
        }
        public void DeleteUserCookie(HttpContext context)
        {
            object cookie = context.Request.Cookies["USER_FASON"];
            if (cookie != null)
            {
                CookieOptions cookieOp = new CookieOptions();
                cookieOp.Expires = DateTime.Now.AddDays(-1);
                context.Response.Cookies.Append("USER_FASON", "", cookieOp);
            }
        }
    }
}
