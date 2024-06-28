using System.Text.Json.Serialization;

namespace app.Models
{
    public record Game
    {
        [JsonPropertyName("handle")]
        public required string Handle { get; init; }

        [JsonPropertyName("turns_taken")]
        public int TurnsTaken { get; init; }

        [JsonPropertyName("time_taken")]
        public int TimeTaken { get; init; }

        [JsonPropertyName("cards")]
        public List<Card> Cards { get; init; } = new();
    }
}