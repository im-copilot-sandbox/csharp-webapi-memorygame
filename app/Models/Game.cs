using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace app.Models
{
    public record Game
    {
        [JsonPropertyName("handle")]
        [Required]
        public required string Handle { get; init; }

        [JsonPropertyName("turns_taken")]
        [Range(0, int.MaxValue)]
        public int TurnsTaken { get; init; }

        [JsonPropertyName("time_taken")]
        [Range(0, int.MaxValue)]
        public int TimeTaken { get; init; }

        [JsonPropertyName("cards")]
        [MinLength(2)]
        public List<Card> Cards { get; init; } = new();
    }
}