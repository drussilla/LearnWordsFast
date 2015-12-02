using System.Linq;
using System.Threading.Tasks;
using LearnWordsFast.DAL.Models;
using LearnWordsFast.Infrastructure;
using LearnWordsFast.Services;
using LearnWordsFast.ViewModels;
using LearnWordsFast.ViewModels.UserController;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using NHibernate.Exceptions;

namespace LearnWordsFast.ApiControllers
{
    [Route("api/user")]
    public class UserController : ApiController
    {
        private readonly ISignInManager _signInManager;
        private readonly IUserManager _userManager;
        
        public UserController(ISignInManager signInManager, IUserManager userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginViewModel loginViewModel)
        {
            if (loginViewModel.Email == null || loginViewModel.Password == null)
            {
                return Error("Email and password should be filled");
            }
            var result = await _signInManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password);
            if (!result.Succeeded)
            {
                return Error("Wrong password or email");
            }

            return Ok();
        }

        [Authorize]
        [HttpPost("logout")]
        public async void Logout()
        {
            await _signInManager.SignOutAsync();
        }

        [Authorize]
        [HttpGet("info")]
        public async Task<IActionResult> GetInfo()
        {
            var user = await _userManager.FindById(HttpContext.User.GetId());
            if (user == null)
            {
                return NotFound();
            }

            return Ok(new UserViewModel(user));
        }

        [Authorize]
        [HttpPut("password")]
        public async Task<IActionResult> UpdatePassword([FromBody]UpdatePasswordViewModel updatePasswordViewModel)
        {
            var user = await _userManager.FindById(HttpContext.User.GetId());
            if (user == null)
            {
                return NotFound();
            }

            IdentityResult result =
                await
                    _userManager.ChangePasswordAsync(user, updatePasswordViewModel.OldPassword,
                        updatePasswordViewModel.NewPassword);

            if (!result.Succeeded)
            {
                return Error(result.Errors.Select(x => x.Description));
            }

            return Ok();
        }

        [Authorize]
        [HttpPut("languages")]
        public async Task<IActionResult> UpdateLanguages([FromBody]UpdateLanguagesViewModel requestModel)
        {
            var user = await _userManager.FindById(HttpContext.User.GetId());
            if (user == null)
            {
                return NotFound();
            }

            user.MainLanguage = new Language(requestModel.MainLanguage);
            user.TrainingLanguage = new Language(requestModel.TrainingLanguage);
            user.AdditionalLanguages.Clear();
            if (requestModel.AdditionalLanguages != null && requestModel.AdditionalLanguages.Count != 0)
            {
                foreach (var additionalLanguage in requestModel.AdditionalLanguages)
                {
                    user.AdditionalLanguages.Add(new Language(additionalLanguage));
                }
            }

            IdentityResult result;
            try
            {
                result = await _userManager.UpdateAsync(user);
            }
            catch (GenericADOException ex) when (ex.InnerException != null && (string)ex.InnerException.Data["Code"] == "23503")
            {
                return Error("Language not found");
            }
            
            if (!result.Succeeded)
            {
                return Error(result.Errors.Select(x => x.Description));
            }

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateUserViewModel requestModel)
        {
            if (requestModel.MainLanguage == requestModel.TrainingLanguage)
            {
                return Error("You should select unique languages");
            }

            var user = new User
            {
                Email = requestModel.Email,
                MainLanguage = new Language(requestModel.MainLanguage),
                TrainingLanguage = new Language(requestModel.TrainingLanguage)
            };

            if (requestModel.AdditionalLanguages != null)
            {
                if (requestModel.AdditionalLanguages.Any(x => x == requestModel.MainLanguage) ||
                    requestModel.AdditionalLanguages.Any(x => x == requestModel.TrainingLanguage) ||
                    requestModel.AdditionalLanguages.Count != requestModel.AdditionalLanguages.Distinct().Count())
                {
                    return Error("You should select unique languages");
                }

                foreach (var additionalLanguageId in requestModel.AdditionalLanguages)
                {
                    user.AdditionalLanguages.Add(new Language(additionalLanguageId));
                }
            }

            IdentityResult result;
            try
            {
                result = await _userManager.CreateAsync(user, requestModel.Password);
            }
            catch (GenericADOException ex)
                when (ex.InnerException != null && (string) ex.InnerException.Data["Code"] == "23503")
            {
                return Error("Language not found");
            }

            if (!result.Succeeded)
            {
                return Error(result.Errors.Select(x => x.Description));
            }

            await _signInManager.SignInAsync(user);
            return Created("/api/user/" + user.Id);
        }
    }
}