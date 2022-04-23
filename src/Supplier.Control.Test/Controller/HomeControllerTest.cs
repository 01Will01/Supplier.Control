
using Microsoft.Extensions.Logging;
using Moq;
using Rest.Api.Minimal.Supplier.Presentation.Controllers;
using Xunit;

namespace Supplier.Control.Test.Controller
{
    public class HomeControllerTest
    {
        private readonly HomeController _homeController;
        public HomeControllerTest()
        {
            var loggerMock = new Mock<ILogger<HomeController>>().Object;
            _homeController = new HomeController(loggerMock);
        }

        [Fact]
        public void Index_Test()
        {
            var results = _homeController.Index();
            Assert.NotNull(results);
        }

        [Fact]
        public void Privacy_Test()
        {
            var results = _homeController.Privacy();
            Assert.NotNull(results);
        }
    }
}
