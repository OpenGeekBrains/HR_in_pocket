using System.Net.Http;
using System.Threading.Tasks;

namespace HRInPocket.Clients.Service
{
    internal static class HttpClientEx
    {
        public static async Task<T> ResultAs<T>(this Task<HttpResponseMessage> Message) => await (await Message).EnsureSuccessStatusCode().Content.ReadAsAsync<T>();
    }
}
