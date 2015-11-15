using System;
using LearnWordsFast.DAL.Repositories;
using LearnWordsFast.Exceptions;
using LearnWordsFast.Infrastructure;
using LearnWordsFast.Services;
using LearnWordsFast.ViewModels.TrainingController;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.Logging;

namespace LearnWordsFast.ApiControllers
{
    [Route("api/Training")]
    [Authorize]
    public class TrainingController : ApiController
    {
        private readonly ITrainingService _trainingService;
        private readonly ILogger<TrainingController> _log;

        public TrainingController(
            ITrainingService trainingService,
            ILogger<TrainingController> log)
        {
            _trainingService = trainingService;
            _log = log;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _log.LogInformation("Get next training");
            var training = _trainingService.CreateTraining(UserId);
            if (training == null)
            {
                return NotFound();
            }

            return Ok(training);
        }

        [HttpPost]
        public IActionResult Finish([FromBody]TrainingResultViewModel[] results)
        {
            _log.LogInformation("Finish training");
            foreach (var result in results)
            {
                try
                {
                    _trainingService.FinishTraining(UserId, result);
                }
                catch (NotFoundException)
                {
                    return NotFound(result.WordId);
                }
            }
            
            return Ok();
        }
    }
}