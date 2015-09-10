using System;
using LearnWordsFast.DAL.Models;
using LearnWordsFast.DAL.Repositories;
using LearnWordsFast.Services;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.Logging;

namespace LearnWordsFast.ApiControllers
{
    [Route("api/Practice")]
    public class PracticeController : Controller
    {
        private readonly IWordRepository _wordRepository;
        private readonly ITrainingService _trainingService;
        private readonly ILogger<PracticeController> _log;

        public PracticeController(
            IWordRepository wordRepository, 
            ITrainingService trainingService,
            ILogger<PracticeController> log)
        {
            _wordRepository = wordRepository;
            _trainingService = trainingService;
            _log = log;
        }

        [HttpGet]
        public Word Get()
        {
            _log.LogInformation("Get word for next training");
            return _trainingService.GetNextWord();
        }

        [HttpPost("{id}")]
        public void Finish(Guid id)
        {
            var word = _wordRepository.Get(id);
            _trainingService.FinishTraining(word);
        }
    }
}