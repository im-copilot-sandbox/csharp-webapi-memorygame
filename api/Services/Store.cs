using System.Text.Json;
using api.Models;

namespace api.Services
{
    public class Store
    {
        public static async Task SaveLeaderboardAsync(List<Leaderboard> leaderboardEntries, string filePath)
        {
            var jsonString = JsonSerializer.Serialize(leaderboardEntries);
            await File.WriteAllTextAsync(filePath, jsonString);
        }

        public static async Task<List<Leaderboard>> LoadLeaderboardAsync(string filePath)
        {
            if (!File.Exists(filePath)) return new List<Leaderboard>();
            var jsonString = await File.ReadAllTextAsync(filePath);
            return JsonSerializer.Deserialize<List<Leaderboard>>(jsonString) ?? new List<Leaderboard>();
        }
        public static async Task SaveGamesAsync(List<Game> games, string filePath)
        {
            var jsonString = JsonSerializer.Serialize(games);
            await File.WriteAllTextAsync(filePath, jsonString);
        }

        public static async Task<List<Game>> LoadGamesAsync(string filePath)
        {
            if (!File.Exists(filePath)) return new List<Game>();
            var jsonString = await File.ReadAllTextAsync(filePath);
            return JsonSerializer.Deserialize<List<Game>>(jsonString) ?? new List<Game>();
        }
    }
}