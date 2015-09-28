using System;
using System.Collections.Generic;
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
            _log.LogInformation("Get word for next training");
            var next = _trainingService.GetNextWord(User.GetId());
            if (next == null)
            {
                return NotFound();

            }

            return Ok(next);
        }

        [HttpPost("{id}")]
        public IActionResult Finish(Guid id)
        {
            var word = _wordRepository.Get(id, Context.User.GetId());
            if (word == null)
            {
                return NotFound();
            }

            _trainingService.FinishTraining(word);
            return Ok();
        }

        [HttpGet("test")]
        public List<string> Test()
        {
            return new List<string> { "test", "test2" };
        }
    }
}