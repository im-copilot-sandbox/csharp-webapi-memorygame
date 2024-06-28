using Microsoft.AspNetCore.Mvc;
using Xunit;
using app.Controllers;
using app.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace app.Tests
{
    public class RoutesControllerTests
    {
        [Fact]
        public void Greeting_ReturnsHelloWorld()
        {
            // Arrange
            var controller = new RoutesController();

            // Act
            var result = controller.Greeting();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal("Hello, World!", okResult.Value);
        }

        [Fact]
        public async Task PostGame_ReturnsBadRequest_WhenGameIsNull()
        {
            // Arrange
            var controller = new RoutesController();

            // Act
            var result = await controller.PostGame(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GetGame_ReturnsBadRequest_WhenHandleIsInvalid()
        {
            // Arrange
            var controller = new RoutesController();

            // Act
            var result = await controller.GetGame("");

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task PostLeaderboard_ReturnsBadRequest_WhenEntryIsInvalid()
        {
            // Arrange
            var controller = new RoutesController();
            var entry = new RoutesController.LeaderboardEntry { Handle = "", Score = 0 };

            // Act
            var result = await controller.PostLeaderboard(entry);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}