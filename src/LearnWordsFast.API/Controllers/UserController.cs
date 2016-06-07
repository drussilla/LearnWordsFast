using System;
using System.Linq;
using System.Threading.Tasks;
using LearnWordsFast.DAL.Models;
using LearnWordsFast.API.ViewModels.UserController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LearnWordsFast.API.Controllers
{
    [Route("api/user")]
    public class UserController : ApiController
    {
        [Authorize("Bearer")]
        [HttpGet("info")]
        public async Task<IActionResult> GetInfo()
        {
            var user = await UserManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(new UserViewModel(user));
        }

        [Authorize("Bearer")]
        [HttpPut("password")]
        public async Task<IActionResult> UpdatePassword([FromBody]UpdatePasswordViewModel updatePasswordViewModel)
        {
            var user = await UserManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return NotFound();
            }

            IdentityResult result =
                await
                    UserManager.ChangePasswordAsync(user, updatePasswordViewModel.OldPassword,
                        updatePasswordViewModel.NewPassword);

            if (!result.Succeeded)
            {
                return Error(result.Errors.Select(x => x.Description));
            }

            return Ok();
        }

        [Authorize("Bearer")]
        [HttpPut("languages")]
        public async Task<IActionResult> UpdateLanguages([FromBody]UpdateLanguagesViewModel requestModel)
        {
            var user = await UserManager.FindByIdAsync(UserId.ToString());
            if (user == null)
            {
                return NotFound();
            }

            user.MainLanguageId = requestModel.MainLanguage;
            user.TrainingLanguageId = requestModel.TrainingLanguage;
            user.AdditionalLanguages.Clear();
            if (requestModel.AdditionalLanguages != null && requestModel.AdditionalLanguages.Count != 0)
            {
                foreach (var additionalLanguage in requestModel.AdditionalLanguages)
                {
                    user.AdditionalLanguages.Add(new UserAdditionalLanguage { UserId = user.Id, LanguageId = additionalLanguage });
                }
            }

            IdentityResult result;
            try
            {
                result = await UserManager.UpdateAsync(user);
            }
            catch (Exception)
            {
                return Error("Language not found");
            }
            
            if (!result.Succeeded)
            {
                return Error(result.Errors.Select(x => x.Description));
            }

            return Ok();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody]CreateUserViewModel requestModel)
        {
            if (requestModel.MainLanguage == requestModel.TrainingLanguage)
            {
                return Error("You should select unique languages");
            }

            var user = new User
            {
                Email = requestModel.Email,
                MainLanguageId = requestModel.MainLanguage,
                TrainingLanguageId = requestModel.TrainingLanguage
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
                    user.AdditionalLanguages.Add(new UserAdditionalLanguage {UserId = user.Id, LanguageId = additionalLanguageId });
                }
            }

            IdentityResult result;
            try
            {
                result = await UserManager.CreateAsync(user, requestModel.Password);
            }
            catch (Exception)
            {
                return Error("Language not found");
            }

            if (!result.Succeeded)
            {
                return Error(result.Errors.Select(x => x.Description));
            }
            
            return Created("/api/user/" + user.Id);
        }
    }
}