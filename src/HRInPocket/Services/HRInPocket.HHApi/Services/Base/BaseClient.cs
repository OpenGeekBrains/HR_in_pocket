using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using HRInPocket.HHApi.Models.Errors;

namespace HRInPocket.HHApi.Services.Base
{
    public class BaseClient
    {
        protected readonly string _ServiceAddress;
        protected readonly HttpClient _Client;
        public IEnumerable<HHError> Errors { get; private set; }

        public BaseClient()
        {
            _Client = new HttpClient();
            _Client.BaseAddress = new Uri("https://api.hh.ru");
            _Client.DefaultRequestHeaders.Add("User-Agent", "HRInPocket/0.1");
        }

        private void GetErrors(string content)
        {
            HHErrors hErrors = JsonSerializer.Deserialize<HHErrors>(content);
            Errors = hErrors.errors;
        }

        //internal async Task<T> GetJsonAsync<T>(string serviceUrl, CancellationToken cancellationToken = default) =>
        //    await _Client.GetFromJsonAsync<T>(serviceUrl, cancellationToken);

        internal async Task<T> GetJsonAsync<T>(string serviceUrl, CancellationToken cancellationToken = default)
        {
            var resp = await _Client.GetAsync(serviceUrl, cancellationToken);
            var content = await resp.Content.ReadAsStringAsync();
            if (resp.IsSuccessStatusCode)
                return JsonSerializer.Deserialize<T>(content);
            else
            {
                GetErrors(content);
                return default(T);
            }
        }

    }
}
