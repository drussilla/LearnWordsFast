using System;
using LearnWordsFast.DAL.Repositories;
using LearnWordsFast.Repositories;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.Logging;

namespace LearnWordsFast.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWordRepository wordRepository;
        private readonly ILogger log;

        public HomeController(IWordRepository wordRepository, ILogger<HomeController> log)
        {
            this.wordRepository = wordRepository;
            this.log = log;
        }

        public IActionResult Index()
        {
            log.LogInformation("Request to index");
            try
            {
                var all = wordRepository.GetAll();
                return View(all);
            }
            catch (Exception ex)
            {
                log.LogError(1, $"DB error. Message: {ex.Message}", ex);
                return null;
            }
            
        }
    }
}