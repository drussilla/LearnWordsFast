using Microsoft.AspNet.Mvc;

namespace LearnWordsFast.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}