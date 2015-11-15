using System;
using System.Collections.Generic;
using System.Linq;
using LearnWordsFast.DAL.Models;
using LearnWordsFast.DAL.Repositories;
using LearnWordsFast.Infrastructure;
using LearnWordsFast.Services;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.Logging;

namespace LearnWordsFast.ApiControllers
{
    [Route("api/Practice")]
    [Authorize]
    public class PracticeController : ApiController
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
        public IActionResult Get()
        {
            _log.LogInformation("Get next training");
            var nextWord = _trainingService.GetNextWord(User.GetId());
            if (nextWord == null)
            {
                _log.LogInformation("Word for next training not found");
                return NotFound();
            }

            _log.LogInformation("Word for next training found: " + nextWord.Id);
            var training = _trainingService.CreateTraining(nextWord);
            return Ok(training);
        }

        [HttpPost("{id}")]
        public IActionResult Finish(Guid id)
        {
            var word = _wordRepository.Get(id, Context.User.GetId());
            if (word == null)
            {
                return NotFound();
            }

            _trainingService.FinishTraining(word, true, 10.0f);
            return Ok();
        }
    }
}