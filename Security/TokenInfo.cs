using System;
using Microsoft.AspNetCore.Authentication;

namespace AuthenticatedWebApi.Security
{
    public class TokenInfo 
    {
        public TokenInfo() {}

        public TokenInfo(AuthenticationTicket ticket) 
        {
            CreatedAt = DateTime.Now;
            Ticket = ticket;
        }
        
        public DateTime CreatedAt { get; set; }
        
        public AuthenticationTicket Ticket { get; set; }
    }
}