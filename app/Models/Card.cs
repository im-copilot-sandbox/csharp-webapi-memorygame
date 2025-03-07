using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace app.Models
{
    public class Card
    {
        [JsonPropertyName("type")]
        [Required]
        public required string Type { get; set; }

        [JsonPropertyName("flipped")]
        public required bool Flipped { get; set; }
    }
}