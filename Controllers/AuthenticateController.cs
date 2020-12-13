using AuthenticatedWebApi.Models;
using AuthenticatedWebApi.Security;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticatedWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticateController : ControllerBase
    {
        public AuthenticateController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }
        
        [HttpPost]
        public IActionResult Post(AuthenticateModel model) 
        {
            string token;
            if (_tokenService.Authenticate(model.Username, model.Password, out token)) 
            {            
                return Ok(token);
            }
            else 
            {
                return Unauthorized();
            }
        }

        private ITokenService _tokenService;
    }
}