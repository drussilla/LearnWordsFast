using LearnWordsFast.Models;
using LearnWordsFast.Repositories;
using Microsoft.AspNet.Mvc;

namespace LearnWordsFast.Controllers
{
    public class WordController : Controller
    {
        private readonly IWordRepository wordRepository;

        public WordController(IWordRepository wordRepository)
        {
            this.wordRepository = wordRepository;
        }

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
            var newWord = new Word
            {
                Original = word,
                Translation = translation
            };

            wordRepository.Add(newWord);

            return RedirectToAction("Add");
        }
    }
}