using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace app.Models
{
    // Represents a leaderboard entry
    public record Leaderboard
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string Id { get; set; }
        // The handle of the player
        [JsonPropertyName("handle")]
        public required string Handle { get; init; }

        // The score achieved by the player
        [JsonPropertyName("score")]
        public int Score { get; init; }

        // The date and time when the game was completed
        [JsonPropertyName("last_played")]
        public DateTime LastPlayed { get; init; }
    }
}