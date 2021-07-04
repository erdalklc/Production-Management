using EPM.Core.Managers;
using EPM.Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Core.Helpers
{
    public class MailHelper
    {

        public void SendEMail(string email,string konu,string mailText)
        {
            string sql = string.Format("CALL XXER.XXER_URETIM_TAKIP_MAIL_AT('{0}','{1}','{2}')",email,konu,mailText);
            OracleServer.ExecSql(sql);
        }

        public void SendEMail( HttpContext context,string konu, string mailText)
        {
            WebLogin user = new CookieHelper().GetUserFromCookie(context);
            SendEMail(user.EMAIL, konu, mailText);
        }
    }
}
