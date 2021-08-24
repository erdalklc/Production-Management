using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;

namespace EPM.Tools.Helpers
{
    public class CookieHelper
    {
        public void AddCookie(HttpContext context, object obj, string CookieName, string hash)
        {
            CookieOptions cookieOp = new CookieOptions();
            cookieOp.Expires = DateTime.Now.AddDays(200);
            context.Response.Cookies.Append(CookieName, CryptHelper.Encrypt(JsonConvert.SerializeObject(obj), hash), cookieOp);
        }

        public T GetObjectFromCookie<T>(HttpContext context,string objIdentify)
        {
            object cookie = context.Request.Cookies[objIdentify];
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
            object cookie = context.Request.Cookies[objIdentify];
            if (cookie != null)
            {
                CookieOptions cookieOp = new CookieOptions();
                cookieOp.Expires = DateTime.Now.AddDays(-1); 
                context.Response.Cookies.Append(objIdentify, "",cookieOp);
            }
        }
    }
}
