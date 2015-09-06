using Xunit;
using LearnWordsFast.Controllers;
using LearnWordsFast.DAL.Repositories;
using LearnWordsFast.Repositories;
using Microsoft.Framework.Logging;
using Moq;

namespace LearnWordsFast.Test.Controllers
{
    public class HomeControllerTest
    {
        [Fact]
        public void Index_AllWordsReturned()
        {
            var repository = new Mock<IWordRepository>();
            var log = new Mock<ILogger<HomeController>>();
            var target = new HomeController(repository.Object, log.Object);

            target.Index();
            
            repository.Verify(x => x.GetAll(), Times.Once);
        }
    }
}