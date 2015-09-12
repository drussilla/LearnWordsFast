using System.Threading.Tasks;
using LearnWordsFast.DAL.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;

namespace LearnWordsFast.ApiControllers
{
    [Route("api/user")]
    public class UserController
    {
        private readonly SignInManager<User> _signInManager;

        public UserController(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<string> Login(string email, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, true, false);

            return "test";
        }
    }
}