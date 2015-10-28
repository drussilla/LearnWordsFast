using Microsoft.AspNet.Mvc;

namespace LearnWordsFast.Controllers
{
    public class HomeController : Controller
    {
        [ResponseCache(CacheProfileName = "IndexPage")]
        public IActionResult Index()
        {
            return View();
        }
    }
}