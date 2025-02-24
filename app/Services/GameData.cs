using System.Text.Json;
using System.IO;
using app.Models;

namespace app.Services
{
    public class GameData
    {
        private readonly string basePath = Path.GetFullPath("data");
        private readonly string leaderboardFilePath;

        public GameData()
        {
            leaderboardFilePath = Path.Combine(basePath, "leaderboard.json");
        }

        public List<Leaderboard> GetLeaderboardEntries()
        {
            if (!File.Exists(leaderboardFilePath))
            {
                return new List<Leaderboard>();
            }

            var leaderboardJson = File.ReadAllText(leaderboardFilePath);
            var leaderboardEntries = JsonSerializer.Deserialize<List<Leaderboard>>(leaderboardJson);

            return leaderboardEntries ?? new List<Leaderboard>();
        }

        /// <summary>
        /// Saves the leaderboard entries to a JSON file.
        /// </summary>
        /// <param name="entries">The list of leaderboard entries to save.</param>
        public void SaveLeaderboardEntries(List<Leaderboard> entries)
        {
            var leaderboardJson = JsonSerializer.Serialize(entries);
            File.WriteAllText(leaderboardFilePath, leaderboardJson);
        }

        public Game? GetGame(string handle)
        {
            var gameFilePath = GetValidatedFilePath(handle);
            if (!File.Exists(gameFilePath))
            {
                return null;
            }

            var gameJson = File.ReadAllText(gameFilePath);
            return JsonSerializer.Deserialize<Game>(gameJson);
        }

        public void SaveGame(Game game)
        {
            var gameFilePath = GetValidatedFilePath(game.Handle);
            var gameJson = JsonSerializer.Serialize(game);
            File.WriteAllText(gameFilePath, gameJson);
        }

        private string GetValidatedFilePath(string handle)
        {
            var filePath = Path.GetFullPath(Path.Combine(basePath, $"{handle}.json"));

            if (!filePath.StartsWith(basePath))
            {
                throw new UnauthorizedAccessException("Invalid file path.");
            }

            return filePath;
        }
    }
}