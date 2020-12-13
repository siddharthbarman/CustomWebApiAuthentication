using Microsoft.AspNetCore.Authentication;

namespace AuthenticatedWebApi.Security
{
    public interface ITokenService 
    {
        bool Authenticate(string user, string password, out string token);
        AuthenticationTicket ValidateToken(string token);
    }
}