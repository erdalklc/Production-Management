using EPM.WMS.Api.Tools;
using EPM.WMS.Dto.Login;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EPM.WMS.Api.ServiceHelper
{
    public class WMSServiceHelper : EPMHttpCaller
    {
        private IConfiguration config;

        public static LoginReturnModel Login()
        {
            string apiUrl = "/user/auth/login";
            LoginReturnModel model = GetLogin(EPMServiceType.WMS, apiUrl, EPMServiceConfiguration.GetConfig().GetSection("USER_MAIL").Value, EPMServiceConfiguration.GetConfig().GetSection("USER_PASS").Value);
            return model;
        }
    }
}
