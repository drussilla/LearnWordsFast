using LearnWordsFast.Models;
using LearnWordsFast.Repositories;
using Microsoft.AspNet.Mvc;

namespace LearnWordsFast.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWordRepository wordRepository;

        public HomeController(IWordRepository wordRepository)
        {
            this.wordRepository = wordRepository;
        }

        public IActionResult Index()
        {
            var all = wordRepository.GetAll();
            return View(new WordList { Words = all });
        }
    }
}