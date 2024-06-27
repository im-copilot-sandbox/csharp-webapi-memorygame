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
           // TODO: Implement test for the greeting endpoint 
        }

        [Fact]
        public async Task GetGameByHandle_ReturnsGame()
        {
          // TODO: Implement test for the GetGameByHandle endpoint 
        }

        [Fact]
        public async Task PostGame_ReturnsSuccess()
        {
           // TODO: Implement test for the PostGame endpoint 
        }

        [Fact]
        public async Task PostGame_WhenHandleIsEmpty_ReturnsBadRequest()
        {
          // TODO: Implement test for the PostGame endpoint when the handle is empty 
        }

        [Fact]
        public async Task PostLeaderboard_ReturnsSuccess()
        {
          // TODO: Implement test for the PostLeaderboard endpoint 
        }

        [Fact]
        public async Task GetLeaderboard_ReturnsTopTenEntries()
        {
         // TODO: Implement test for the GetLeaderboard endpoint 
        }
    }
}