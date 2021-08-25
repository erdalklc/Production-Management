using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace EPM.Tools.Helpers
{
    public abstract class EPMHttpCaller
    {
        private static void LoadFailureEvent(HttpResponseMessage response, bool throwException)
        {
            string msg = string.Empty;
            try
            {
                var data = response.Content.ReadAsStringAsync().Result;
                var errData = JsonConvert.DeserializeObject<dynamic>(data);
                msg = errData.ExceptionMessage;
                
            }
            catch (Exception ex)
            {
            }
            if (throwException)
            {
                if (msg == null)
                {
                    msg = string.Empty;
                }
                throw new Exception(msg);
            }
        }

        protected static string GetRequestContent(EPMServiceType serviceType, string apiUrl)
        {
            var data = string.Empty;
            using (var client = new EPMHttpClient(serviceType))
            {

                var response = client.GetAsync(apiUrl).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    data = response.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    LoadFailureEvent(response, true);

                }
            }
            return data;
        }

        protected static T GetRequest<T>(EPMServiceType serviceType, string apiUrl)
        {
            var data = GetRequestContent(serviceType, apiUrl);
            var rval = JsonConvert.DeserializeObject<T>(data);
            return rval;
        }

        protected static TReturn PostRequest<T, TReturn>(EPMServiceType serviceType, string apiUrl, T model)
        { 
            var rval = default(TReturn);
            using (EPMHttpClient client = new EPMHttpClient(serviceType))
            {
                var response = client.PostAsync(apiUrl,
                        new StringContent(JsonConvert.SerializeObject(model).ToString(),
                            Encoding.UTF8, "application/json"))
                            .Result;

                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    try
                    {

                        rval = JsonConvert.DeserializeObject<TReturn>(data);
                    }
                    catch (Exception ex)
                    {
                         
                    }

                }
                else
                {
                    LoadFailureEvent(response, true);
                }

            }
            return rval;
        }
      

        protected static bool PostRequestByRef<T, TReturn>(EPMServiceType serviceType, string apiUrl, T model, ref TReturn rval)
        {
            var returnVal = false;
            using (EPMHttpClient client = new EPMHttpClient(serviceType))
            {
                var response = client.PostAsync(apiUrl,
                        new StringContent(JsonConvert.SerializeObject(model).ToString(),
                            Encoding.UTF8, "application/json"))
                            .Result;

                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    try
                    {

                        rval = JsonConvert.DeserializeObject<TReturn>(data);
                    }
                    catch (Exception ex)
                    {
                         
                    }
                    returnVal = true;
                }
                else
                {
                    LoadFailureEvent(response, false);
                }

            }
            return returnVal;
        }
    }
}
