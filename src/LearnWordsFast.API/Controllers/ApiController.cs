using System;
using System.Collections.Generic;
using LearnWordsFast.API.Services;
using LearnWordsFast.API.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LearnWordsFast.API.Controllers
{
    public class ApiController : Controller
    {
        protected IUserManager UserManager => HttpContext.RequestServices.GetService(typeof(IUserManager)) as IUserManager;

        protected Guid UserId => Guid.Parse(UserManager.GetUserId(HttpContext.User));

        protected IActionResult Created(string location)
        {
            return new CreatedResult(location, null);
        }

        protected IActionResult Error()
        {
            return new BadRequestResult();
        }

        protected IActionResult Error(IEnumerable<string> errorMessages)
        {
            return new BadRequestObjectResult(new ErrorViewModel(errorMessages));
        }

        protected IActionResult Error(string error)
        {
            return new BadRequestObjectResult(new ErrorViewModel(new List<string>
            {
                error
            }));
        }
    }
}