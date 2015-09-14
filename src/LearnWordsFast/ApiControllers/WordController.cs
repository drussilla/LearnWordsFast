using System;
using System.Collections.Generic;
using LearnWordsFast.DAL.Models;
using LearnWordsFast.DAL.Repositories;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.Logging;

namespace LearnWordsFast.ApiControllers
{
    [Route("api/Word")]
    public class WordController : Controller
    {
        private readonly IWordRepository _wordRepository;
        private readonly ILogger<WordController> _log;

        public WordController(IWordRepository wordRepository, ILogger<WordController> log)
        {
            _wordRepository = wordRepository;
            _log = log;
        }

        public Result<IList<Word>> GetAll()
        {
            _log.LogInformation("Get all words");
            return Result<IList<Word>>.Ok(_wordRepository.GetAll());
        }

        [HttpGet("{id}")]
        public Result<Word> Get(Guid id)
        {
            _log.LogInformation($"Get word with id {id}");
            return Result<Word>.Ok(_wordRepository.Get(id));
        }

        [HttpPost]
        public Result Create([FromBody]Word word)
        {
            _log.LogInformation($"Add word with id {word.Id}");
            _wordRepository.Add(word);
            return Result.Ok();
        }
    }
}