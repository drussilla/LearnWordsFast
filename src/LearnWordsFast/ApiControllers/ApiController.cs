using Microsoft.AspNet.Mvc;

namespace LearnWordsFast.ApiControllers
{
    public class ApiController : Controller
    {
        public IActionResult Ok()
        {
            return new ContentResult();
        }

        public IActionResult Ok(object value)
        {
            return new ObjectResult(value);
        }

        public IActionResult Created(string location)
        {
            return new CreatedResult(location, null);
        }

        public IActionResult Error()
        {
            return new BadRequestResult();
        }

        public IActionResult Error(string errorMessage)
        {
            return new BadRequestObjectResult(errorMessage);
        }

        public IActionResult NotFound()
        {
            return new HttpNotFoundResult();
        }

        public IActionResult NotFound(object value)
        {
            return new HttpNotFoundObjectResult(value);
        }
    }
}