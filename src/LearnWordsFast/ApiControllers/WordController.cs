using System;
using System.Linq;
using LearnWordsFast.DAL.Models;
using LearnWordsFast.DAL.Repositories;
using LearnWordsFast.Infrastructure;
using LearnWordsFast.ViewModels.WordController;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;

namespace LearnWordsFast.ApiControllers
{
    [Route("api/word")]
    [Authorize]
    public class WordController : ApiController
    {
        private readonly IWordRepository _wordRepository;
        private readonly ILogger<WordController> _log;

        public WordController(IWordRepository wordRepository, ILogger<WordController> log)
        {
            _wordRepository = wordRepository;
            _log = log;
        }
        
        public IActionResult GetAll()
        {
            _log.LogInformation("Get all words");
            return Ok(_wordRepository.GetAll(User.GetId()).Select(x => new WordViewModel(x)));
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            _log.LogInformation($"Get word with id {id}");
            var word = _wordRepository.Get(id, UserId);
            if (word == null)
            {
                _log.LogWarning($"Word with id {id} not found. User: {UserId}");
                return NotFound();
            }

            return Ok(new WordViewModel(word));
        }

        [HttpPost]
        public IActionResult Create([FromBody]WordViewModel word)
        {
            _log.LogInformation($"Add word {word.Original} translated to {word.Translation}");
            var wordModel = new Word
            {
                UserId = UserId,
                Original = word.Original,
                Language = new Language(word.Language),
                Translation = word.Translation.ToModel(),
                AddedDateTime = DateTime.Now,
                Context = word.Context
            };

            if (word.AdditionalTranslations != null && word.AdditionalTranslations.Count > 0)
            {
                wordModel.AdditionalTranslations = word.AdditionalTranslations
                    .Select(x => x.ToModel()).ToList();
            }

            _wordRepository.Add(wordModel);
            return Created("/api/word/" + wordModel.Id);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _log.LogInformation($"Delete word {id}");

            var word = _wordRepository.Get(id, UserId);
            if (word == null)
            {
                _log.LogWarning($"Word with id {id} not found. User: {UserId}");
                return NotFound();
            }

            _wordRepository.Delete(word);

            return Ok();
        }
    }
}