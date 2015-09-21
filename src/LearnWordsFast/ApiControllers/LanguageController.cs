using LearnWordsFast.DAL.Repositories;
using Microsoft.AspNet.Mvc;

namespace LearnWordsFast.ApiControllers
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