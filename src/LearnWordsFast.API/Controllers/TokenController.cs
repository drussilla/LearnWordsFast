using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using LearnWordsFast.API.Infrastructure;
using LearnWordsFast.API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace LearnWordsFast.API.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : ApiController
    {
        private readonly TokenAuthOptions _tokenOptions;

        public TokenController(TokenAuthOptions tokenOptions)
        {
            _tokenOptions = tokenOptions;
        }

        [HttpPost]
        public IActionResult Post([FromBody] LoginViewModel req)
        {
            // Obviously, at this point you need to validate the username and password against whatever system you wish.
            if (req.Email == "TEST" && req.Password == "TEST")
            {
                DateTime? expires = DateTime.UtcNow.AddMinutes(2);
                var token = GetToken(req.Email, expires);
                return Ok(token);// new { authenticated = true, entityId = 1, token, tokenExpires = expires };
            }

            return Error("");
        }


        private string GetToken(string user, DateTime? expires)
        {
            var handler = new JwtSecurityTokenHandler();

            ClaimsIdentity identity = new ClaimsIdentity(new GenericIdentity(user, "TokenAuth"), new[]
            {
                new Claim("EntityID", "1", ClaimValueTypes.Integer),
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", "12")
            });

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor()
            {
                Issuer = _tokenOptions.Issuer,
                Audience = _tokenOptions.Audience,
                SigningCredentials = _tokenOptions.SigningCredentials,
                Subject = identity,
                Expires = expires
            });
                
            return handler.WriteToken(securityToken);
        }
    }
}