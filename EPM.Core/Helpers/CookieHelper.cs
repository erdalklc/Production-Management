using EPM.Core.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;

namespace EPM.Core.Helpers
{
    public class CookieHelper
    { 
        public void AddCookie(HttpContext context,object obj, string CookieName,string hash)
        {
            CookieOptions cookieOp = new CookieOptions();
            cookieOp.Expires = DateTime.Now.AddDays(200);
            context.Response.Cookies.Append(CookieName, CryptHelper.Encrypt(JsonConvert.SerializeObject(obj),hash), cookieOp); 
        }

        public WebLogin GetUserFromCookie(HttpContext context)
        {
            object cookie = context.Request.Cookies["USER"];
            WebLogin user = new WebLogin();
            if (cookie != null)
            {
                try
                {
                    user = JsonConvert.DeserializeObject<WebLogin>(CryptHelper.Decrypt(cookie.ToString(), CryptHelper.hash));
                }
                catch (Exception) { user = null; }
            }
            else user = null;
            return user;
        }
        public string GetEmailFromCookie(HttpContext context)
        {
            object cookie = context.Request.Cookies["EMAIL"];
            string mail = "";
            if (cookie != null)
            {
                try
                {
                    mail = JsonConvert.DeserializeObject<string>(CryptHelper.Decrypt(cookie.ToString(), CryptHelper.hash2));
                }
                catch (Exception) {  }
            } 
            return mail;
        }
        public void DeleteUserCookie(HttpContext context)
        {
            object cookie = context.Request.Cookies["USER"];
            if (cookie != null)
            {
                CookieOptions cookieOp = new CookieOptions();
                cookieOp.Expires = DateTime.Now.AddDays(-1); 
                context.Response.Cookies.Append("USER","",cookieOp);
            }
        }
    }
}
