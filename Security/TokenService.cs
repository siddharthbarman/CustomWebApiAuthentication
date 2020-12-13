using System.Buffers.Text;
using System.Text;
using System.Security.Cryptography;
using System;
using System.Collections.Concurrent;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AuthenticatedWebApi.Security
{
    public class TokenService : ITokenService 
    {
        public TokenService(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        public bool Authenticate(string username, string password, out string token) 
        {
            User user = ValidateCredentials(username, password);
            if (user != null) 
            {
                token = Guid.NewGuid().ToString();
                AuthenticationTicket ticket = CreateAuthenticationTicket(username);
                _tokens.TryAdd(token, new TokenInfo(ticket));
                return true;
            }
            else 
            {
                token = null;
                return false;
            }
        }

        public AuthenticationTicket ValidateToken(string token) 
        {
            TokenInfo tokenInfo = null;
            if (_tokens.TryGetValue(token, out tokenInfo)) 
            {
                return tokenInfo.Ticket;
            }
            else 
            {
                return null;
            }
        }

        private AuthenticationTicket CreateAuthenticationTicket(string user) 
        {
            Claim[] claims = new Claim[] 
            {
                new Claim(ClaimTypes.NameIdentifier, user)            
            };        
            
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, nameof(TokenAuthenticationHandler));
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            AuthenticationTicket authTicket = new AuthenticationTicket(claimsPrincipal, TokenAuthenticationSchemeOptions.Name);

            return authTicket;
        }

        private User ValidateCredentials(string username, string password)
        {
            MemoryStream stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(password));
            byte[] hashBytes = _cryptoProvider.ComputeHash(stream);
            string hashString = System.Convert.ToBase64String(hashBytes);
            
            List<User> users = _configuration.GetSection("Users").Get<List<User>>();            
            return users.Where(u => u.Name == username && u.Password == hashString).FirstOrDefault();            
        }

        private ConcurrentDictionary<string, TokenInfo> _tokens = new ConcurrentDictionary<string, TokenInfo>(); 
        private IConfiguration _configuration;
        SHA256 _cryptoProvider = SHA256CryptoServiceProvider.Create();
            
        
    }
}