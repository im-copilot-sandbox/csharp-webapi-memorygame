using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Api.Models;

namespace Api.Tests
{
    public class RouteTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public RouteTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GreetingEndpoint_ReturnsWelcome()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/greeting");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            var expected = "Welcome to the Memory Game API!";
            Console.WriteLine(responseString);
            Assert.Equal(expected, responseString.Trim('"'));
        }

        [Fact]
        public async Task GetGameByHandle_ReturnsGame()
        {
            // Arrange
            var client = _factory.CreateClient();
            var testHandle = "testHandle";

            // Act
            var response = await client.GetAsync($"/game/{testHandle}");
            response.EnsureSuccessStatusCode();
            var game = await response.Content.ReadFromJsonAsync<Game>();

            // Assert
            Assert.NotNull(game);
            Assert.Equal(testHandle, game.Handle);
            Assert.True(game.Cards.Any(), "Game should have at least one card.");
            Assert.Contains(game.Cards, card => !string.IsNullOrWhiteSpace(card.CardType) && !string.IsNullOrWhiteSpace(card.State));
        }

        [Fact]
        public async Task PostGame_ReturnsSuccess()
        {
            // Arrange
            var client = _factory.CreateClient();
            var newGame = new Game
            {
                Handle = "testGameHandle",
                TurnsTaken = 5,
                TimeTaken = 120,
                GameCompleted = false,
                Cards = new List<Card>
        {
            new Card { CardType = "image", State = "hidden" },
            new Card { CardType = "color", State = "flipped" }
        }
            };

            // Act
            var response = await client.PostAsJsonAsync("/game", newGame);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Contains("Game data for testGameHandle saved successfully.", responseString);
        }

        [Fact]
        public async Task PostGame_WhenHandleIsEmpty_ReturnsBadRequest()
        {
        }


        [Fact]
        public async Task PostLeaderboard_ReturnsSuccess()
        {
        }

        [Fact]
        public async Task GetLeaderboard_ReturnsTopTenEntries()
        {
        }
    }
}