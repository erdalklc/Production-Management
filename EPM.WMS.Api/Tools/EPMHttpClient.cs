using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EPM.WMS.Api.Tools
{
    public class EPMHttpClient : IDisposable
    {
        HttpClient client = null;
        private IConfiguration config;


        public EPMHttpClient(EPMServiceType serviceType)
        {
            config = EPMServiceConfiguration.GetConfig();

            client = new HttpClient();
            string apiUrl = string.Empty;

            apiUrl = config.GetSection("ApiUrl").Value;

            apiUrl = apiUrl == null ? string.Empty : apiUrl.Trim();
            if (apiUrl.Length > 0 && !apiUrl.EndsWith("/"))
            {
                apiUrl += "/";
            }
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Add("LNG", "TR");
            client.DefaultRequestHeaders.Add("ShopUrl", "wms.erenperakende.com");
            client.DefaultRequestHeaders.Add("TemplateLNG", "TR");
            client.DefaultRequestHeaders.Add("CompanyCode", "EREN");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public Task<HttpResponseMessage> GetAsync(string requestUri)
        {  
            return client.GetAsync(requestUri);
        }

        public Task<HttpResponseMessage> GetLogin(string requestUri, string EmailAddress, string Pwd)
        {

            client.DefaultRequestHeaders.Add("EmailAddress", EmailAddress);
            client.DefaultRequestHeaders.Add("Pwd", Pwd);
            return client.GetAsync(requestUri);
        }

        public Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content)
        { 
            return client.PostAsync(requestUri, content);
        }
         
        public void Dispose()
        {
            try
            {
                client.Dispose();
            }
            catch (Exception)
            {

            }
        }
    }
}
