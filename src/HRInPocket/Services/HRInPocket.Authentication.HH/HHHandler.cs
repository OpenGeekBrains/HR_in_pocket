
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

using HRInPocket.Authentication.HH;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HRInPocket.Authentication.HH
{
    public class HHHandler : OAuthHandler<HHOptions>
    {
        public HHHandler(
            IOptionsMonitor<HHOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock) : base(options, logger, encoder, clock) { }

        protected override async Task<AuthenticationTicket> CreateTicketAsync(ClaimsIdentity identity, AuthenticationProperties properties, OAuthTokenResponse tokens)
        {
            var httpResponseMessage = await Backchannel.SendAsync(
                new HttpRequestMessage(HttpMethod.Get, Options.UserInformationEndpoint)
                {
                    Headers = { Authorization = new AuthenticationHeaderValue("Bearer", tokens.AccessToken) }
                }, Context.RequestAborted);
            httpResponseMessage.EnsureSuccessStatusCode();

            AuthenticationTicket authenticationTicket;

            using (var payload = JsonDocument.Parse(await httpResponseMessage.Content.ReadAsStringAsync()))
            {
                var context = new OAuthCreatingTicketContext(new ClaimsPrincipal(identity), properties, Context, Scheme, Options, Backchannel, tokens, payload.RootElement);
                context.RunClaimActions();
                await Events.CreatingTicket(context);
                authenticationTicket = new AuthenticationTicket(context.Principal, context.Properties, Scheme.Name);
            }

            return authenticationTicket;
        }

        protected override string BuildChallengeUrl(AuthenticationProperties properties, string redirectUri)
        {
            var dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            dictionary.Add("response_type", "code");
            dictionary.Add("client_id", Options.ClientId);
            dictionary.Add("redirect_uri", redirectUri);
            AddQueryString(dictionary, properties, OAuthChallengeProperties.ScopeKey, new Func<ICollection<string>, string>(FormatScope), Options.Scope);
            AddQueryString(dictionary, properties, HHChallengeProperties.AccessTypeKey, Options.AccessType);
            AddQueryString(dictionary, properties, HHChallengeProperties.ApprovalPromptKey);
            AddQueryString(dictionary, properties, HHChallengeProperties.PromptParameterKey);
            AddQueryString(dictionary, properties, HHChallengeProperties.LoginHintKey);
            AddQueryString(dictionary, properties, HHChallengeProperties.IncludeGrantedScopesKey, v => v?.ToString().ToLower(), new bool?());
            var str = Options.StateDataFormat.Protect(properties);
            dictionary.Add("state", str);

            return QueryHelpers.AddQueryString(Options.AuthorizationEndpoint, dictionary);
        }

        private void AddQueryString<T>(
                  IDictionary<string, string> queryStrings,
                  AuthenticationProperties properties,
                  string name,
                  Func<T, string> formatter,
                  T defaultValue)
        {
            var str = (string)null;
            var parameter = properties.GetParameter<T>(name);
            if (parameter != null)
                str = formatter(parameter);
            else if (!properties.Items.TryGetValue(name, out str))
                str = formatter(defaultValue);
            properties.Items.Remove(name);
            if (str == null)
                return;
            queryStrings[name] = str;
        }

        private void AddQueryString(
                  IDictionary<string, string> queryStrings,
                  AuthenticationProperties properties,
                  string name,
                  string defaultValue = null)
        {
            AddQueryString(queryStrings, properties, name, x => x, defaultValue);
        }
    }
}
