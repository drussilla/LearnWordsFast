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

        public IList<Word> GetAll()
        {
            _log.LogInformation("Get all words");
            return _wordRepository.GetAll();
        }

        [HttpGet("{id}")]
        public Word Get(Guid id)
        {
            _log.LogInformation($"Get word with id {id}");
            return _wordRepository.Get(id);
        }

        [HttpPost]
        public void Create([FromBody]Word word)
        {
            _log.LogInformation($"Add word with id {word.Id}");
            _wordRepository.Add(word);
        }
    }
}