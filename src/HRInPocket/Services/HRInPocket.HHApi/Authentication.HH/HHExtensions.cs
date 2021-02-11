
using System;

using HRInPocket.HHApi.Authentication.HH;

using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace HRInPocket.HHApi.Authentication.Extension.HH
{
    public static class HHExtensions
    {
        public static AuthenticationBuilder AddHH(this AuthenticationBuilder builder)
        {
            return builder.AddHH("HH", _ => { });
        }

        public static AuthenticationBuilder AddHH(this AuthenticationBuilder builder, Action<HHOptions> configureOptions)
        {
            return builder.AddHH("HH", configureOptions);
        }

        public static AuthenticationBuilder AddHH(this AuthenticationBuilder builder, string authenticationScheme, Action<HHOptions> configureOptions)
        {
            return builder.AddHH(authenticationScheme, HHDefault.DisplayName, configureOptions);
        }

        public static AuthenticationBuilder AddHH(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<HHOptions> configureOptions)
        {
            return builder.AddOAuth<HHOptions, HHHandler>(authenticationScheme, displayName, configureOptions);
        }
    }
}
