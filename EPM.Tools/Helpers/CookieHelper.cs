using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;

namespace EPM.Tools.Helpers
{
    public class CookieHelper
    {
        public void AddCookie(HttpContext context, object obj, string CookieName)
        {
            CookieOptions cookieOp = new CookieOptions();
            cookieOp.Expires = DateTime.Now.AddDays(200);
            context.Response.Cookies.Append(CryptHelper.Encrypt(CookieName,CryptHelper.hash2), CryptHelper.Encrypt(JsonConvert.SerializeObject(obj), CryptHelper.hash), cookieOp);
        }

        public T GetObjectFromCookie<T>(HttpContext context,string objIdentify)
        {
            object cookie = context.Request.Cookies[CryptHelper.Encrypt(objIdentify, CryptHelper.hash2)];
            var rval = default(T);
            if (cookie != null)
            {
                try
                {
                    rval = JsonConvert.DeserializeObject<T>(CryptHelper.Decrypt(cookie.ToString(), CryptHelper.hash));
                }
                catch (Exception) { }
            } 
            return rval;
        }
      
        public void DeleteCookie(HttpContext context, string objIdentify)
        {
            object cookie = context.Request.Cookies[CryptHelper.Encrypt(objIdentify, CryptHelper.hash2)];
            if (cookie != null)
            {
                CookieOptions cookieOp = new CookieOptions();
                cookieOp.Expires = DateTime.Now.AddDays(-1); 
                context.Response.Cookies.Append(objIdentify, "",cookieOp);
            }
        }
    }
}
