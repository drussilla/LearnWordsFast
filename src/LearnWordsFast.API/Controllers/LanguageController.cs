﻿using LearnWordsFast.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LearnWordsFast.API.Controllers
{
    [Route("api/language")]
    public class LanguageController : ApiController
    {
        private readonly ILanguageRepository _languageRepository;

        public LanguageController(ILanguageRepository languageRepository)
        {
            _languageRepository = languageRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_languageRepository.GetAll());
        }
    }
}