
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;

namespace HRInPocket.Authentication.HH
{
    public class HHOptions : OAuthOptions
    {
        public HHOptions() : base()
        {
            CallbackPath = new PathString("/signin-hh");
            AuthorizationEndpoint = HHDefault.AuthorizationEndpoint;
            TokenEndpoint = HHDefault.TokenEndpoint;
            UserInformationEndpoint = HHDefault.UserInformationEndpoint;
            Scope.Add("openid");
            Scope.Add("profile");
            Scope.Add("email");
            ClaimActions.MapJsonKey("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", "id");
            ClaimActions.MapJsonKey("http://schemas.xmlsoap.org/claims/EmailAddress", "email");
            ClaimActions.MapJsonKey("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname", "first_name");
            ClaimActions.MapJsonKey("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname", "last_name");

        }
        public string AccessType { get; set; }

    }
}
