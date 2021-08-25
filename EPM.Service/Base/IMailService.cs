using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Service.Base
{
    public interface IMailService
    {
        public void SendEMail(string email, string konu, string mailText);

        public void SendEMail(HttpContext context, string konu, string mailText);
    }
}
