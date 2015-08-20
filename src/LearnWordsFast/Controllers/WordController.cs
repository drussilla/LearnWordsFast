using System;
using LearnWordsFast.Models;
using LearnWordsFast.Repositories;
using LearnWordsFast.Services;
using Microsoft.AspNet.Mvc;

namespace LearnWordsFast.Controllers
{
    public class WordController : Controller
    {
        private readonly IWordRepository wordRepository;
        private readonly ITrainingService trainingService;

        public WordController(IWordRepository wordRepository, ITrainingService trainingService)
        {
            this.wordRepository = wordRepository;
            this.trainingService = trainingService;
        }

        public IActionResult Practice()
        {
            var wordForTraininig = trainingService.GetNextWord();
            return View(wordForTraininig);
        }

        [HttpPost]
        public IActionResult PracticeFinished(Guid id)
        {
            var word = wordRepository.Get(id);
            trainingService.FinishTraining(word);
            return RedirectToAction(nameof(Practice));
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