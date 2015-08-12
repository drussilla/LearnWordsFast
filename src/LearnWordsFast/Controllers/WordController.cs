using Microsoft.AspNet.Mvc;

namespace LearnWordsFast.Controllers
{
    public class WordController : Controller
    {
        public IActionResult Practice()
        {
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost("AddWord")]
        public IActionResult AddWord(string word, string translation)
        {

            return RedirectToAction("Add");
        }
    }
}