using System.Text.Json;
using api.Models;

namespace api.Services
{
    public class GameData
    {
        private const string DataDirectory = "data";

        private const string leaderboardFile = $"{DataDirectory}/leaderboard.json";

        /// <summary>
        /// Saves the game to a file.
        /// </summary>
        /// <param name="game">The game to save.</param>
        /// <param name="handle">The handle of the game.</param>
        public static async Task SaveGameAsync(Game game, string handle)
        {
            string filePath = $"{DataDirectory}/{handle}.json";
            var jsonString = JsonSerializer.Serialize(game);
            await File.WriteAllTextAsync(filePath, jsonString);
        }

        /// <summary>
        /// Loads the games from a file.
        /// </summary>
        /// <param name="filePath">The file path to load the games from.</param>
        /// <returns>The loaded games.</returns>
        public static async Task<Game?> RetrieveGameAsync(string handle)
        {
            string filePath = $"{DataDirectory}/{handle}.json";
            if (!File.Exists(filePath)) return null;
            var jsonString = await File.ReadAllTextAsync(filePath);
            return JsonSerializer.Deserialize<Game>(jsonString);
        }

        /// <summary>
        /// Saves the leaderboard entries to a file.
        /// </summary>
        /// <param name="leaderboardEntries">The leaderboard entries to save.</param>
        public static async Task SaveLeaderboardEntryAsync(List<Leaderboard> leaderboardEntries)
        {
            var jsonString = JsonSerializer.Serialize(leaderboardEntries);
            await File.WriteAllTextAsync(leaderboardFile, jsonString);
        }
        /// <summary>
        /// Loads the leaderboard entries from a file.
        /// </summary>
        /// <returns>The loaded leaderboard entries.</returns>
        public static async Task<List<Leaderboard>> RetrieveLeaderboardAsync()
        {
            if (!File.Exists(leaderboardFile)) return new List<Leaderboard>();
            var jsonString = await File.ReadAllTextAsync(leaderboardFile);
            return JsonSerializer.Deserialize<List<Leaderboard>>(jsonString) ?? new List<Leaderboard>();
        }
    }
}