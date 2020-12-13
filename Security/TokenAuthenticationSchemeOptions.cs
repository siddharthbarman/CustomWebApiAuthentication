using Microsoft.AspNetCore.Authentication;

namespace AuthenticatedWebApi.Security
{
    public class TokenAuthenticationSchemeOptions : AuthenticationSchemeOptions 
    {
        public const string Name = "TokenAuthenticationScheme";
    }
}