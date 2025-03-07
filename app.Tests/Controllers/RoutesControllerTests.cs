using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using app.Controllers;
using app.Models;
using app.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace app.Tests
{
    public class RoutesControllerTests
    {
        private readonly Mock<ILogger<RoutesController>> _mockLogger;
        private readonly RoutesController _controller;

        public RoutesControllerTests()
        {
            _mockLogger = new Mock<ILogger<RoutesController>>();
            _controller = new RoutesController(_mockLogger.Object);
        }

        [Fact]
        public void Greeting_ReturnsOkResult()
        {
            var result = _controller.Greeting();
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal("Hello, csharp", okResult.Value);
        }

        [Fact]
        public async Task PostGame_InvalidModel_ReturnsBadRequest()
        {
            _controller.ModelState.AddModelError("Handle", "Required");

            var game = new Game
            {
                Handle = "test_handle"
            };
            var result = await _controller.PostGame(game);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GetGame_EmptyHandle_ReturnsBadRequest()
        {
            var result = await _controller.GetGame(string.Empty);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task PostLeaderboard_InvalidModel_ReturnsBadRequest()
        {
            _controller.ModelState.AddModelError("Handle", "Required");

            var entry = new Leaderboard
            {
                Handle = "test_handle"
            };
            var result = await _controller.PostLeaderboard(entry);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GetLeaderboard_ReturnsOkResult()
        {
            var result = await _controller.GetLeaderboard();
            Assert.IsType<OkObjectResult>(result);
        }
    }
}