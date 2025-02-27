using System.Text.Json;
using app.Models;

/*
all JSON files should be stored in the data directory.

leaderboard data should be stored using the following format;
leaderboard.json
 [
    {"handle": "player1", "score": 100, "last_played": "2022-01-01T12:00:00"},
    {"handle": "player2", "score": 200, "last_played": "2022-01-02T12:00:00"}
 ]


game data should be stored using the player's handle as the filename
game data should be stored using the following format;

handle.json
{
    "handle": "player1",
    "turns_taken": 10,
    "time_taken": 120,
    "cards": [
        {"type": 1, "flipped": false},
        {"type": 2, "flipped": true},
        {"type": 1, "flipped": false},
        {"type": 2, "flipped": true}
    ]
}
*/

namespace app.Services
{
    public class GameData
    {

        public static async Task SaveGameAsync(Game game, string handle)
        {
            string filePath = $"data/{handle}.json";
            var jsonString = JsonSerializer.Serialize(game);
            await File.WriteAllTextAsync(filePath, jsonString);
        }

        public static async Task<Game?> RetrieveGameAsync(string handle)
        {
            string filePath = $"data/{handle}.json";
            if (!File.Exists(filePath)) return null;
            var jsonString = await File.ReadAllTextAsync(filePath);
            return JsonSerializer.Deserialize<Game>(jsonString);
        }

        public static async Task SaveLeaderboardEntryAsync(string handle, int score)
        {
            const string leaderboardFile = $"data/leaderboard.json";
            List<Leaderboard> leaderboard;
            if (File.Exists(leaderboardFile))
            {
                string leaderboardContent = await File.ReadAllTextAsync(leaderboardFile);
                leaderboard = JsonSerializer.Deserialize<List<Leaderboard>>(leaderboardContent) ?? new List<Leaderboard>();
            }
            else
            {
                leaderboard = new List<Leaderboard>();
            }

            var entry = leaderboard.FirstOrDefault(e => e.Handle == handle);
            if (entry != null)
            {
                leaderboard.Remove(entry);
                leaderboard.Add(new Leaderboard
                {
                    Handle = handle,
                    Score = score,
                    LastPlayed = DateTime.UtcNow
                });
            }
            else
            {
                leaderboard.Add(new Leaderboard
                {
                    Handle = handle,
                    Score = score,
                    LastPlayed = DateTime.UtcNow
                });
            }

            string updatedContent = JsonSerializer.Serialize(leaderboard, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(leaderboardFile, updatedContent);
        }
        public static async Task<List<Leaderboard>> RetrieveLeaderboardAsync()
        {
            const string leaderboardFile = $"data/leaderboard.json";
            if (!File.Exists(leaderboardFile)) return new List<Leaderboard>();
            var jsonString = await File.ReadAllTextAsync(leaderboardFile);
            return JsonSerializer.Deserialize<List<Leaderboard>>(jsonString) ?? new List<Leaderboard>();
        }
    }
}