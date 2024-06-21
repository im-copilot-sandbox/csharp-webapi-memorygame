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
            // Arrange
            var client = _factory.CreateClient();
            var newGame = new Game
            {
                Handle = "",
                TurnsTaken = 3,
                TimeTaken = 90,
                GameCompleted = true,
                Cards = new List<Card>
                {
                    new Card { CardType = "number", State = "hidden" }
                }
            };

            // Act
            var response = await client.PostAsJsonAsync("/game", newGame);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task PostLeaderboard_ReturnsSuccess()
        {
            // Arrange
            var client = _factory.CreateClient();
            var newEntry = new Leaderboard
            {
                Handle = "player123",
                Score = 100,
                DateTimePlayed = DateTime.UtcNow
            };

            // Act
            var response = await client.PostAsJsonAsync("/leaderboard", newEntry);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Contains("Leaderboard entry for player123 saved successfully.", responseString);
        }

        [Fact]
        public async Task GetLeaderboard_ReturnsTopTenEntries()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/leaderboard");
            response.EnsureSuccessStatusCode();
            var leaderboardEntries = await response.Content.ReadFromJsonAsync<List<Leaderboard>>();

            // Assert
            Assert.NotNull(leaderboardEntries);
            Assert.True(leaderboardEntries.Count <= 10, "Should return up to 10 leaderboard entries.");
            Assert.True(leaderboardEntries.SequenceEqual(leaderboardEntries.OrderByDescending(entry => entry.Score)), "Entries should be ordered by score descending.");
        }
    }
}