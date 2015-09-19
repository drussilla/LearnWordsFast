using LearnWordsFast.ViewModels;
using Microsoft.AspNet.Mvc;

namespace LearnWordsFast.ApiControllers
{
    public class ApiController : Controller
    {
        protected IActionResult Ok()
        {
            return new ContentResult();
        }

        protected IActionResult Ok(object value)
        {
            return new ObjectResult(value);
        }

        protected IActionResult Created(string location)
        {
            return new CreatedResult(location, null);
        }

        protected IActionResult Error()
        {
            return new BadRequestResult();
        }

        protected IActionResult Error(string errorMessage)
        {
            return new BadRequestObjectResult(new ErrorViewModel
            {
                Error = errorMessage
            });
        }

        protected IActionResult NotFound()
        {
            return new HttpNotFoundResult();
        }

        protected IActionResult NotFound(object value)
        {
            return new HttpNotFoundObjectResult(value);
        }
    }
}