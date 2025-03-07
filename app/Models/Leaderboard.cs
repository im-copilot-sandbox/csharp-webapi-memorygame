using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace app.Models
{
    public record Leaderboard
    {
        [JsonPropertyName("handle")]
        [Required]
        public required string Handle { get; init; }

        [JsonPropertyName("score")]
        [Range(0, int.MaxValue)]
        public int Score { get; init; }

        [JsonPropertyName("last_played")]
        public DateTime LastPlayed { get; init; }
    }
}