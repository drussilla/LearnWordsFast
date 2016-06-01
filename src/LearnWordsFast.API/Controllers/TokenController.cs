using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using LearnWordsFast.API.Infrastructure;
using LearnWordsFast.API.ViewModels;
using LearnWordsFast.DAL.Models;
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
        public async Task<IActionResult> Post([FromBody] LoginViewModel req)
        {
            var user = await UserManager.FindByEmailAsync(req.Email);
            if (user == null)
            {
                Error("User not found");
            }
            
            if (await UserManager.CheckPasswordAsync(user, req.Password))
            {
                DateTime? expires = DateTime.UtcNow.AddDays(10);
                var token = GetToken(user, expires);
                return Ok(token);
            }

            return Error("");
        }


        private string GetToken(User user, DateTime? expires)
        {
            var handler = new JwtSecurityTokenHandler();

            ClaimsIdentity identity = new ClaimsIdentity(new GenericIdentity(user.Email, "TokenAuth"), new[]
            {
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", user.Id.ToString())
            });

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
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