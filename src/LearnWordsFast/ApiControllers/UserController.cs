using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LearnWordsFast.DAL.Models;
using LearnWordsFast.DAL.Repositories;
using LearnWordsFast.Infrastructure;
using LearnWordsFast.Services;
using LearnWordsFast.ViewModels;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;

namespace LearnWordsFast.ApiControllers
{
    [Route("api/user")]
    public class UserController : ApiController
    {
        private readonly ISignInManager _signInManager;
        private readonly IUserManager _userManager;
        private readonly ILanguageRepository _languageRepository;
        
        public UserController(ISignInManager signInManager, IUserManager userManager, ILanguageRepository languageRepository)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _languageRepository = languageRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginViewModel loginViewModel)
        {
            var result = await _signInManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password);
            if (!result.Succeeded)
            {
                return Error();
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
            var user = await _userManager.FindById(Context.User.GetId());
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
            var user = await _userManager.FindById(Context.User.GetId());
            if (user == null)
            {
                return NotFound();
            }

            IdentityResult result =
                await
                    _userManager.ChangePasswordAsync(user, updatePasswordViewModel.OldPassword,
                        updatePasswordViewModel.NewPassword);

            if (result.Succeeded)
            {
                return Ok();
            }

            return Error(result.Errors.Select(x => x.Description));
        }

        [Authorize]
        [HttpPut("languages")]
        public async Task<IActionResult> UpadateLanguages([FromBody]UpdateLanguagesViewModel viewModel)
        {
            var user = await _userManager.FindById(Context.User.GetId());
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.UpdateAsync(user);
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

            var mainLanguage = _languageRepository.Get(requestModel.MainLanguage);
            if (mainLanguage == null)
            {
                return Error("Main language is not found");
            }

            var trainingLanguage = _languageRepository.Get(requestModel.TrainingLanguage);
            if (trainingLanguage == null)
            {
                return Error("Training languages is not found");
            }

            var user = new User
            {
                Email = requestModel.Email,
                MainLanguage = mainLanguage,
                TrainingLanguage = trainingLanguage
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
                    var additionalLanguage = _languageRepository.Get(additionalLanguageId);
                    if (additionalLanguage == null)
                    {
                        return Error("Additional language is not found");
                    }

                    user.AdditionalLanguages.Add(additionalLanguage);
                }
            }

            var result = await _userManager.CreateAsync(user, requestModel.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user);
                return Created("/api/user/" + user.Id);
            }

            return Error(result.Errors.Select(x => x.Description));
        }
    }
}