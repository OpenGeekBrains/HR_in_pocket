using System.Threading.Tasks;
using IdentityServer4.Extensions;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Http;

namespace HRInPocket.Extensions
{
    public static class IdentityServer4ModelExtensions
    {
        /// <summary>
        /// Determines whether the client is configured to use PKCE.
        /// </summary>
        /// <param name="store">The store.</param>
        /// <param name="client_id">The client identifier.</param>
        /// <returns></returns>
        public static async Task<bool> IsPkceClientAsync(this IClientStore store, string client_id)
        {
            if (!string.IsNullOrWhiteSpace(client_id))
            {
                var client = await store.FindEnabledClientByIdAsync(client_id);
                return client?.RequirePkce == true;
            }

            return false;
        }

        public static string GetEmailOrName(this HttpContext context)
        {
            var result = string.Empty;
            var user = context.User;
            if (user.IsAuthenticated())
            {
                result = user.FindFirst(c => c.Type.Equals("email", System.StringComparison.Ordinal))?.Value;
                if (string.IsNullOrEmpty(result) || string.IsNullOrWhiteSpace(result))
                {
                    result = user.FindFirst(c => c.Type.Equals("name", System.StringComparison.Ordinal))?.Value;
                }
            }

            result = (string.IsNullOrEmpty(result) || string.IsNullOrWhiteSpace(result)) ? "Соискатель" : result;
            return result;
        }
    }
}
