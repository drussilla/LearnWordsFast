using System.Linq;
using System.Threading.Tasks;
using LearnWordsFast.DAL.Models;
using LearnWordsFast.DAL.Repositories;
using LearnWordsFast.Services;
using LearnWordsFast.ViewModels;
using Microsoft.AspNet.Authorization;
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