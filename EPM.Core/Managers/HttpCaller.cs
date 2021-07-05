using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EPM.Core.Managers
{
    public class HttpCaller
    {
        public async Task<T> GetAsync<T>(string url)
        {
            T d = Activator.CreateInstance<T>();
            HttpClient client = new HttpClient(); 
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                d = await response.Content.ReadAsAsync<T>();
            }
            return d; 
        }

        public async Task<object[]> PostAsync<T>(string url,T t)
        {
            object[] obj = { true, "" }; 
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsJsonAsync(url,t);
            if (response.IsSuccessStatusCode)
            {
                obj[1] = await response.Content.ReadAsStringAsync();
            }
            else obj[0] = false;
            return obj;
        }
        public async Task<string> PostAsync(string url)
        {
            string answer = "";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsJsonAsync(url, "");
            if (response.IsSuccessStatusCode)
            {
                answer = await response.Content.ReadAsStringAsync();
            }
            return answer;
        }
        public async Task<object> PostAsyncObject(string url)
        {
            object m = null;
            //erdal
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsync(url, "");
            if (response.IsSuccessStatusCode)
            {
                m = await response.Content.ReadAsAsync<object>();
            }
            return m;
        }
    }
}
