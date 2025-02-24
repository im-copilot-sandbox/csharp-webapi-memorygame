     using Xunit;
     using app.Controllers;
     using app.Models;
     using app.Services;
     using Microsoft.AspNetCore.Mvc;
     using System.Collections.Generic;

public class RoutesControllerTests
{
    private readonly RoutesController _controller;
    private readonly GameData _gameData;

    public RoutesControllerTests()
    {
        _gameData = new GameData();
        _controller = new RoutesController();
    }

    [Fact]
    public void Greeting_ReturnsHelloSiva()
    {
        // Act
        var result = _controller.Greeting();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal("Hello, siva!", okResult.Value);
    }

    [Fact]
    public void PostGame_ValidGame_ReturnsOk()
    {
        // Arrange
        var game = new Game
        {
            Handle = "player1",
            TurnsTaken = 10,
            TimeTaken = 100,
            Cards = new List<Card>
            {
                new Card { Type = "type1", Flipped = true },
                new Card { Type = "type2", Flipped = false }
            }
        };

        // Act
        var result = _controller.PostGame(game);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal("Game data saved successfully.", okResult.Value);
    }

    [Fact]
    public void PostGame_InvalidGame_ReturnsBadRequest()
    {
        // Arrange
        Game? game = null;

        // Act
        var result = _controller.PostGame(game);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal("Invalid game data.", badRequestResult.Value);
    }

    [Fact]
    public void GetGame_ValidHandle_ReturnsGame()
    {
        // Arrange
        var handle = "player1";

        // Act
        var result = _controller.GetGame(handle);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var game = Assert.IsType<Game>(okResult.Value);
        Assert.Equal(handle, game.Handle);
    }

    [Fact]
    public void GetGame_InvalidHandle_ReturnsBadRequest()
    {
        // Arrange
        string? handle = null;

        // Act
        var result = _controller.GetGame(handle);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal("Handle is required.", badRequestResult.Value);
    }

    [Fact]
    public void PostLeaderboard_ValidEntry_ReturnsOk()
    {
        // Arrange
        var entry = new Leaderboard
        {
            Handle = "player1",
            Score = 100,
            LastPlayed = DateTime.Now
        };

        // Act
        var result = _controller.PostLeaderboard(entry);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal("Leaderboard entry saved successfully.", okResult.Value);
    }

    [Fact]
    public void PostLeaderboard_InvalidEntry_ReturnsBadRequest()
    {
        // Arrange
        Leaderboard? entry = null;

        // Act
        var result = _controller.PostLeaderboard(entry);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal("Invalid leaderboard entry.", badRequestResult.Value);
    }

    [Fact]
    public void GetLeaderboard_ReturnsTop10Players()
    {
        // Act
        var result = _controller.GetLeaderboard();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var leaderboard = Assert.IsType<List<Leaderboard>>(okResult.Value);
        Assert.True(leaderboard.Count <= 10);
    }
}
     