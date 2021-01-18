using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using HRInPocket.Interfaces;
using Microsoft.Extensions.Configuration;

namespace HRInPocket.Clients.Base
{
    public abstract class BaseClient
    {
        protected HttpClient Client { get; }

        protected BaseClient(HttpClient client) => Client = client;

        protected void Post<T>(string url, T item) => PostAsync(url, item).Wait();
        
        protected async Task<HttpResponseMessage> PostAsync<T>(string url, T item)
        {
            var response = await Client.PostAsJsonAsync(url, item);
            return response.EnsureSuccessStatusCode();
        }

        protected bool  Put<T>(string url, T item) => PutAsync(url, item).IsCompletedSuccessfully;

        protected async Task<bool> PutAsync<T>(string url, T item)
        {
            var response = await Client.PutAsJsonAsync(url, item);
            return await response.Content.ReadAsAsync<bool>();
        }
        protected bool Delete<T>(string url, T item) => DeleteAsync(url, item).IsCompletedSuccessfully;
        protected async Task<bool> DeleteAsync<T>(string url,T id)
        {
            var response = await Client.DeleteAsync($"{url}/{id}");
            return await response.Content.ReadAsAsync<bool>();
        }




    }
}
