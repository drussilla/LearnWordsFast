using System;
using LearnWordsFast.DAL.Repositories;
using LearnWordsFast.Infrastructure;
using LearnWordsFast.Services;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.Logging;

namespace LearnWordsFast.ApiControllers
{
    [Route("api/Training")]
    [Authorize]
    public class TrainingController : ApiController
    {
        private readonly IWordRepository _wordRepository;
        private readonly ITrainingService _trainingService;
        private readonly ILogger<TrainingController> _log;

        public TrainingController(
            IWordRepository wordRepository, 
            ITrainingService trainingService,
            ILogger<TrainingController> log)
        {
            _wordRepository = wordRepository;
            _trainingService = trainingService;
            _log = log;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _log.LogInformation("Get next training");
            var training = _trainingService.CreateTraining(User.GetId());
            if (training == null)
            {
                return NotFound();
            }

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