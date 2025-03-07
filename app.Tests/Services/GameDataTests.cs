using Xunit;
using System.Threading.Tasks;
using app.Models;
using app.Services;
using System.IO;
using System.Text.Json;
using System.Collections.Generic; // Add this line

namespace app.Tests.Services
{
    public class GameDataTests
    {
        [Fact]
        public async Task SaveGameAsync_ValidGame_SavesToFile()
        {
            // Arrange
            var game = new Game
            {
                Handle = "test_handle",
                TurnsTaken = 10,
                TimeTaken = 120,
                Cards = new List<Card>
                {
                    new Card { Type = "1", Flipped = false },
                    new Card { Type = "2", Flipped = true }
                }
            };

            var filePath = Path.Combine("data", "test_handle.json");

            // Act
            await GameData.SaveGameAsync(game, game.Handle);

            // Assert
            Assert.True(File.Exists(filePath));

            var savedGame = JsonSerializer.Deserialize<Game>(await File.ReadAllTextAsync(filePath));
            Assert.NotNull(savedGame);
            Assert.Equal(game.Handle, savedGame.Handle);
            Assert.Equal(game.TurnsTaken, savedGame.TurnsTaken);
            Assert.Equal(game.TimeTaken, savedGame.TimeTaken);
            Assert.Equal(game.Cards.Count, savedGame.Cards.Count);

            // Cleanup
            File.Delete(filePath);
        }

        [Fact]
        public async Task RetrieveGameAsync_ValidHandle_ReturnsGame()
        {
            // Arrange
            var game = new Game
            {
                Handle = "test_handle",
                TurnsTaken = 10,
                TimeTaken = 120,
                Cards = new List<Card>
                {
                    new Card { Type = "1", Flipped = false },
                    new Card { Type = "2", Flipped = true }
                }
            };

            var filePath = Path.Combine("data", "test_handle.json");
            await File.WriteAllTextAsync(filePath, JsonSerializer.Serialize(game));

            // Act
            var retrievedGame = await GameData.RetrieveGameAsync(game.Handle);

            // Assert
            Assert.NotNull(retrievedGame);
            Assert.Equal(game.Handle, retrievedGame.Handle);
            Assert.Equal(game.TurnsTaken, retrievedGame.TurnsTaken);
            Assert.Equal(game.TimeTaken, retrievedGame.TimeTaken);
            Assert.Equal(game.Cards.Count, retrievedGame.Cards.Count);

            // Cleanup
            File.Delete(filePath);
        }

        [Fact]
        public async Task RetrieveGameAsync_InvalidHandle_ReturnsNull()
        {
            // Act
            var retrievedGame = await GameData.RetrieveGameAsync("invalid_handle");

            // Assert
            Assert.Null(retrievedGame);
        }
    }
}