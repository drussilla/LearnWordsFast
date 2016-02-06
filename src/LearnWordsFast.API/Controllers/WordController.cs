using System;
using System.Linq;
using LearnWordsFast.API.Infrastructure;
using LearnWordsFast.API.ViewModels.WordController;
using LearnWordsFast.DAL.Models;
using LearnWordsFast.DAL.Repositories;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;

namespace LearnWordsFast.API.Controllers
{
    [Route("api/word")]
    [Authorize]
    public class WordController : ApiController
    {
        private readonly IWordRepository _wordRepository;
        private readonly ILanguageRepository _languageRepository;
        private readonly ILogger<WordController> _log;

        public WordController(IWordRepository wordRepository, ILanguageRepository languageRepository, ILogger<WordController> log)
        {
            _wordRepository = wordRepository;
            _languageRepository = languageRepository;
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
            _log.LogInformation($"Add word {word.Word} translated to {word.Translation}");
            var translationModel = word.Translation.ToModel();
            var wordModel = new Word
            {
                UserId = UserId,
                Original = word.Word.Text,
                LanguageId = word.Word.Language,
                Translation = translationModel,
                AddedDateTime = DateTime.Now,
                Context = word.Context
            };

            if (word.AdditionalTranslations != null && word.AdditionalTranslations.Count > 0)
            {
                wordModel.AdditionalTranslations = word.AdditionalTranslations
                    .Select(x => new WordAdditionalTranslation { WordId = wordModel.Id, Translation = x.ToModel() }).ToList();
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