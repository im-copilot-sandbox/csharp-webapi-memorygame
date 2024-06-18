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
            var response = await client.GetAsync("/greeting"); // Assuming the route is "/greeting"
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            var expected = "Welcome to the Memory Game API!";
            Console.WriteLine(responseString);
            Assert.Equal(expected, responseString.Trim('"'));
        }

        [Fact]
        public async Task PostGame_ReturnsSuccess()
        {
            var client = _factory.CreateClient();
            var game = new Game
            {
                Handle = "testHandle",
                TurnsTaken = 10,
                TimeTaken = 120,
                GameCompleted = true,
                Cards = new List<Card>
                {
                    new Card { CardType = "number", State = "hidden" }
                }
            };

            var response = await client.PostAsJsonAsync("/game", game);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("saved successfully", responseString);
        }

        [Fact]
        public async Task PostGame_WhenHandleIsEmpty_ReturnsBadRequest()
        {
            var client = _factory.CreateClient();
            var game = new Game
            {
                Handle = "", // Empty handle to trigger BadRequest
                Cards = new List<Card>
        {
            new Card { CardType = "number", State = "hidden" }
        }
            };

            var response = await client.PostAsJsonAsync("/game", game);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Invalid game data or missing handle", responseString);
        }

        [Fact]
        public async Task GetGameByHandle_ReturnsGame()
        {
            var client = _factory.CreateClient();
            var handle = "testHandle"; // Ensure this handle exists or mock the response

            var response = await client.GetAsync($"/game/{handle}");
            response.EnsureSuccessStatusCode();
            var game = await response.Content.ReadFromJsonAsync<Game>();

            Assert.NotNull(game);
            Assert.Equal(handle, game.Handle);
        }

        [Fact]
        public async Task PostLeaderboard_ReturnsSuccess()
        {
            var client = _factory.CreateClient();
            var entry = new Leaderboard
            {
                Handle = "testHandle",
                Score = 100,
                DateTimePlayed = DateTime.Now
            };

            var response = await client.PostAsJsonAsync("/leaderboard", entry);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("saved successfully", responseString);
        }

        [Fact]
        public async Task GetLeaderboard_ReturnsTopTenEntries()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/leaderboard");
            response.EnsureSuccessStatusCode();
            var entries = await response.Content.ReadFromJsonAsync<List<Leaderboard>>();

            Assert.NotNull(entries);
            Assert.True(entries.Count <= 10);
            Assert.Equal(entries, entries.OrderByDescending(e => e.Score).Take(10).ToList());
        }
    }
}