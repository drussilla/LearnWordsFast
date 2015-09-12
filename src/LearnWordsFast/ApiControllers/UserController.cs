using System.Threading.Tasks;
using LearnWordsFast.DAL.Models;
using LearnWordsFast.ViewModels;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;

namespace LearnWordsFast.ApiControllers
{
    [Route("api/user")]
    public class UserController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager; 

        public UserController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("login")]
        public async Task<string> Login([FromBody]LoginViewModel loginViewModel)
        {
            var result = await _signInManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password, true, false);

            return "test";
        }

        [HttpPost("create")]
        public async void Create([FromBody]RegisterViewModel registerViewModel)
        {
            var user = new User {Email = registerViewModel.Email};
            var result = await _userManager.CreateAsync(user, registerViewModel.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
            }
        }

        [HttpGet("test")]
        [Authorize]
        public string Test()
        {
            return Context.User.Identity.Name;

        }
    }
}