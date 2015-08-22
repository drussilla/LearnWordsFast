using System;
using LearnWordsFast.DAL.Models;
using LearnWordsFast.DAL.Repositories;
using LearnWordsFast.Services;
using Microsoft.AspNet.Mvc;

namespace LearnWordsFast.Controllers
{
    public class WordController : Controller
    {
        private readonly IWordRepository wordRepository;
        private readonly ITrainingService trainingService;
        private readonly IDateTimeService dateTimeService;

        public WordController(IWordRepository wordRepository, ITrainingService trainingService, IDateTimeService dateTimeService)
        {
            this.wordRepository = wordRepository;
            this.trainingService = trainingService;
            this.dateTimeService = dateTimeService;
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

        public IActionResult Index()
        {
            return RedirectToAction(nameof(Add));
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
                Translation = translation,
                AddedDateTime = dateTimeService.Now
            };

            wordRepository.Add(newWord);

            return RedirectToAction("Add");
        }
    }
}