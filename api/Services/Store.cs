using System.Text.Json;
using api.Models;

namespace api.Services
{
    public class Store
    {
        /// <summary>
        /// Saves the leaderboard entries to a file.
        /// </summary>
        /// <param name="leaderboardEntries">The leaderboard entries to save.</param>
        /// <param name="filePath">The file path to save the leaderboard entries to.</param>
        public static async Task SaveLeaderboardAsync(List<Leaderboard> leaderboardEntries, string filePath)
        {
            var jsonString = JsonSerializer.Serialize(leaderboardEntries);
            await File.WriteAllTextAsync(filePath, jsonString);
        }

        /// <summary>
        /// Loads the leaderboard entries from a file.
        /// </summary>
        /// <param name="filePath">The file path to load the leaderboard entries from.</param>
        /// <returns>The loaded leaderboard entries.</returns>
        public static async Task<List<Leaderboard>> LoadLeaderboardAsync(string filePath)
        {
            if (!File.Exists(filePath)) return new List<Leaderboard>();
            var jsonString = await File.ReadAllTextAsync(filePath);
            return JsonSerializer.Deserialize<List<Leaderboard>>(jsonString) ?? new List<Leaderboard>();
        }

        /// <summary>
        /// Saves the games to a file.
        /// </summary>
        /// <param name="games">The games to save.</param>
        /// <param name="filePath">The file path to save the games to.</param>
        public static async Task SaveGamesAsync(List<Game> games, string filePath)
        {
            var jsonString = JsonSerializer.Serialize(games);
            await File.WriteAllTextAsync(filePath, jsonString);
        }

        /// <summary>
        /// Loads the games from a file.
        /// </summary>
        /// <param name="filePath">The file path to load the games from.</param>
        /// <returns>The loaded games.</returns>
        public static async Task<List<Game>> LoadGamesAsync(string filePath)
        {
            if (!File.Exists(filePath)) return new List<Game>();
            var jsonString = await File.ReadAllTextAsync(filePath);
            return JsonSerializer.Deserialize<List<Game>>(jsonString) ?? new List<Game>();
        }
    }
}