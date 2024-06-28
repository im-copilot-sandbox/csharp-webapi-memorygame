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

// Namespace declaration for organizing classes and other namespaces
namespace app.Services
{
    // Class to handle game data operations such as saving and retrieving game and leaderboard data
    public class GameData
    {
        // Asynchronously saves game data to a JSON file named after the player's handle
        public static async Task SaveGameAsync(Game game, string handle)
        {
            // Sanitize the handle to prevent directory traversal attacks
            string sanitizedHandle = SanitizeHandle(handle);
            // Combine the sanitized handle with the directory path to form the file path
            string filePath = Path.Combine("data", sanitizedHandle + ".json");
            // Ensure the file path is within the intended directory
            if (!IsPathWithinDirectory(filePath, "data")) throw new InvalidOperationException("Invalid file path.");

            // Serialize the game object to a JSON string
            var jsonString = JsonSerializer.Serialize(game);
            // Write the JSON string to the file asynchronously
            await File.WriteAllTextAsync(filePath, jsonString);
        }

        // Asynchronously retrieves game data for a given player handle
        public static async Task<Game?> RetrieveGameAsync(string handle)
        {
            // Sanitize the handle to prevent directory traversal attacks
            string sanitizedHandle = SanitizeHandle(handle);
            // Combine the sanitized handle with the directory path to form the file path
            string filePath = Path.Combine("data", sanitizedHandle + ".json");
            // Check if the file exists and is within the intended directory
            if (!File.Exists(filePath) || !IsPathWithinDirectory(filePath, "data")) return null;

            // Read the JSON string from the file asynchronously
            var jsonString = await File.ReadAllTextAsync(filePath);
            // Deserialize the JSON string to a Game object
            return JsonSerializer.Deserialize<Game>(jsonString);
        }

        // Sanitizes the player handle to prevent directory traversal attacks
        private static string SanitizeHandle(string handle)
        {
            // Remove potentially dangerous directory traversal sequences
            return handle.Replace("../", "").Replace("..\\", "");
        }

        // Checks if a given file path is within a specified directory
        private static bool IsPathWithinDirectory(string filePath, string directory)
        {
            // Get the full path of the file and directory
            var fullPath = Path.GetFullPath(filePath);
            var directoryPath = Path.GetFullPath(directory);
            // Check if the file path starts with the directory path
            return fullPath.StartsWith(directoryPath, StringComparison.OrdinalIgnoreCase);
        }

        // Asynchronously saves a new entry to the leaderboard
        public static async Task SaveLeaderboardEntryAsync(string handle, int score)
        {
            // Define the file path for the leaderboard JSON file
            const string leaderboardFile = $"data/leaderboard.json";
            List<Leaderboard> leaderboard;
            // Check if the leaderboard file exists
            if (File.Exists(leaderboardFile))
            {
                // Read the existing leaderboard data
                string leaderboardContent = await File.ReadAllTextAsync(leaderboardFile);
                // Deserialize the JSON string to a list of Leaderboard objects
                leaderboard = JsonSerializer.Deserialize<List<Leaderboard>>(leaderboardContent) ?? new List<Leaderboard>();
            }
            else
            {
                // Initialize a new leaderboard list if the file does not exist
                leaderboard = new List<Leaderboard>();
            }
            // Check if an entry for the handle already exists
            var entry = leaderboard.FirstOrDefault(e => e.Handle == handle);
            if (entry != null)
            {
                // Update the existing entry
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
                // Add a new entry to the leaderboard
                leaderboard.Add(new Leaderboard
                {
                    Handle = handle,
                    Score = score,
                    LastPlayed = DateTime.UtcNow
                });
            }
            // Serialize the updated leaderboard to a JSON string
            string updatedContent = JsonSerializer.Serialize(leaderboard, new JsonSerializerOptions { WriteIndented = true });
            // Write the JSON string to the file asynchronously
            await File.WriteAllTextAsync(leaderboardFile, updatedContent);
        }

        // Asynchronously retrieves the leaderboard data
        public static async Task<List<Leaderboard>> RetrieveLeaderboardAsync()
        {
            // Define the file path for the leaderboard JSON file
            const string leaderboardFile = $"data/leaderboard.json";
            // Check if the leaderboard file exists
            if (!File.Exists(leaderboardFile)) return new List<Leaderboard>();
            // Read the leaderboard data from the file asynchronously
            var jsonString = await File.ReadAllTextAsync(leaderboardFile);
            // Deserialize the JSON string to a list of Leaderboard objects
            return JsonSerializer.Deserialize<List<Leaderboard>>(jsonString) ?? new List<Leaderboard>();
        }
    }
}