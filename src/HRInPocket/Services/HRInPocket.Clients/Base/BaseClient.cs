using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

namespace HRInPocket.Clients.Base
{
    public abstract class BaseClient
    {
     
        protected readonly string ServiceAddress;
        protected readonly HttpClient Client;

        protected BaseClient(IConfiguration configuration, string serviceAddress)
        {
            ServiceAddress = serviceAddress;

            Client = new HttpClient
            {
                BaseAddress = new Uri(configuration["WebAPI"]),
                DefaultRequestHeaders =
                {
                    Accept = { new MediaTypeWithQualityHeaderValue("application/json") }
                }
            };
        }


        protected HttpResponseMessage Post<T>(string url, T item) => PostAsync(url, item).Result;
        
        protected async Task<HttpResponseMessage> PostAsync<T>(string url, T item)
        {
            var response = await Client.PostAsJsonAsync(url, item);
            return response.EnsureSuccessStatusCode();
        }
        protected HttpResponseMessage Put<T>(string url, T item) => PutAsync(url, item).Result;

        protected async Task<HttpResponseMessage> PutAsync<T>(string url, T item)
        {
            var response = await Client.PutAsJsonAsync(url, item);
            return response.EnsureSuccessStatusCode();
        }
    }
}
