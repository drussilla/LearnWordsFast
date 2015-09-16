﻿using System;
using System.Collections.Generic;
using LearnWordsFast.DAL.Models;
using LearnWordsFast.DAL.Repositories;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.Logging;

namespace LearnWordsFast.ApiControllers
{
    [Route("api/word")]
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
            return Ok(_wordRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            _log.LogInformation($"Get word with id {id}");
            return Ok(_wordRepository.Get(id));
        }

        [HttpPost]
        public IActionResult Create([FromBody]Word word)
        {
            _log.LogInformation($"Add word with id {word.Id}");
            _wordRepository.Add(word);
            return Created("/api/word/" + word.Id);
        }
    }
}