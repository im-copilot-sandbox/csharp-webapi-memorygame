using app.Models;
using app.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

public class GameDataTests
{
    private const string DataDirectory = "data";
    private const string LeaderboardFile = "data/leaderboard.json";

    public GameDataTests()
    {
        // Ensure the data directory exists for the tests
        if (!Directory.Exists(DataDirectory))
        {
            Directory.CreateDirectory(DataDirectory);
        }
    }

    [Fact]
    public async Task SaveGameAsync_SavesGameDataCorrectly()
    {
        // Arrange
        var game = new Game
        {
            Handle = "testPlayer",
            TurnsTaken = 5,
            TimeTaken = 100,
            Cards = new List<Card>
            {
                new Card { Type = "1", Flipped = false },
                new Card { Type = "2", Flipped = true }
            }
        };

        // Act
        await GameData.SaveGameAsync(game, game.Handle);

        // Assert
        string filePath = Path.Combine(DataDirectory, game.Handle + ".json");
        Assert.True(File.Exists(filePath));

        var savedGame = JsonSerializer.Deserialize<Game>(await File.ReadAllTextAsync(filePath));
        Assert.NotNull(savedGame);
        Assert.Equal(game.Handle, savedGame.Handle);
        Assert.Equal(game.TurnsTaken, savedGame.TurnsTaken);
        Assert.Equal(game.TimeTaken, savedGame.TimeTaken);
        Assert.Equal(game.Cards.Count, savedGame.Cards.Count);
    }

    [Fact]
    public async Task RetrieveGameAsync_RetrievesGameDataCorrectly()
    {
        // Arrange
        string handle = "testPlayer";
        string filePath = Path.Combine(DataDirectory, handle + ".json");

        // Act
        var retrievedGame = await GameData.RetrieveGameAsync(handle);

        // Assert
        Assert.NotNull(retrievedGame);
        Assert.Equal(handle, retrievedGame.Handle);
    }

    [Fact]
    public async Task SaveLeaderboardEntryAsync_SavesLeaderboardEntryCorrectly()
    {
        // Arrange
        string handle = "leaderboardTestPlayer";
        int score = 300;

        // Act
        await GameData.SaveLeaderboardEntryAsync(handle, score);

        // Assert
        Assert.True(File.Exists(LeaderboardFile));

        var leaderboard = JsonSerializer.Deserialize<List<Leaderboard>>(await File.ReadAllTextAsync(LeaderboardFile));
        Assert.NotNull(leaderboard);
        Assert.Contains(leaderboard, entry => entry.Handle == handle && entry.Score == score);
    }

    [Fact]
    public async Task RetrieveLeaderboardAsync_RetrievesLeaderboardCorrectly()
    {
        // Act
        var leaderboard = await GameData.RetrieveLeaderboardAsync();

        // Assert
        Assert.NotNull(leaderboard);
        Assert.NotEmpty(leaderboard);
    }

    // Cleanup after tests
    public void Dispose()
    {
        // Cleanup code to remove test data files
        if (Directory.Exists(DataDirectory))
        {
            Directory.Delete(DataDirectory, true);
        }
    }
}