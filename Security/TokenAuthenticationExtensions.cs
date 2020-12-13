using Microsoft.Extensions.DependencyInjection;

namespace AuthenticatedWebApi.Security
{
    public static class TokenAuthenticationExtensions 
    {
        public static void UseTokenAuthentication(this IServiceCollection services) 
        {
            services.AddAuthentication(options => {
                options.DefaultScheme = TokenAuthenticationSchemeOptions.Name;
            })
            .AddScheme<TokenAuthenticationSchemeOptions, TokenAuthenticationHandler>(
                TokenAuthenticationSchemeOptions.Name, option => {}
            );
        }
    } 
}