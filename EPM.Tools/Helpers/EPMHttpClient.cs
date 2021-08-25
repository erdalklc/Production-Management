using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EPM.Tools.Helpers
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
            switch (serviceType)
            {
                case EPMServiceType.Fason:
                    apiUrl = config.GetSection("AppServices:Fason").Value;
                    break;
                case EPMServiceType.Track:
                    apiUrl = config.GetSection("AppServices:Track").Value;
                    break;
                case EPMServiceType.Plan:
                    apiUrl = config.GetSection("AppServices:Plan").Value;
                    break;
                case EPMServiceType.Admin:
                    apiUrl = config.GetSection("AppServices:Admin").Value;
                    break;
                case EPMServiceType.Production:
                    apiUrl = config.GetSection("AppServices:Production").Value;
                    break;
                case EPMServiceType.WMS:
                    apiUrl = config.GetSection("AppServices:WMS").Value;
                    break;
            }

            apiUrl = apiUrl == null ? string.Empty : apiUrl.Trim();
            if (apiUrl.Length > 0 && !apiUrl.EndsWith("/"))
            {
                apiUrl += "/";
            }
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public Task<HttpResponseMessage> GetAsync(string requestUri)
        {  
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
