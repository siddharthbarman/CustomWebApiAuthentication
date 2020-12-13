using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AuthenticatedWebApi.Security
{
    public class TokenAuthenticationHandler : AuthenticationHandler<TokenAuthenticationSchemeOptions>
    {
        public TokenAuthenticationHandler(IOptionsMonitor<TokenAuthenticationSchemeOptions> options,
            ILoggerFactory logger, 
            UrlEncoder encoder, 
            ISystemClock clock,
            ITokenService tokenService
            ) : base(options, logger, encoder, clock) 
            {
                _tokenService = tokenService;
            }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            string authToken = this.Request.Headers[AUTHORIZATION_TOKEN_HEADER];
            if (string.IsNullOrEmpty(authToken)) 
            {
                return Task.FromResult(AuthenticateResult.Fail("Authorization header not found"));
            }

            AuthenticationTicket authTicket = _tokenService.ValidateToken(authToken);
            if (authTicket == null) 
            {
                return Task.FromResult(AuthenticateResult.Fail(""));
            }

            return Task.FromResult(AuthenticateResult.Success(authTicket));
        }

        private ITokenService _tokenService;
        private const string AUTHORIZATION_TOKEN_HEADER = "AuthToken";
    }
}