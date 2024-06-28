using System.Text.Json.Serialization;

namespace app.Models
{
    // Represents a card in the memory game.
    public class Card
    {
        // Gets or sets the type of the card, i.e. image, color, number, etc.
        [JsonPropertyName("type")]
        public required string Type { get; set; }

        // Gets or sets a value indicating whether the card is hidden or flipped.
        [JsonPropertyName("flipped")]
        public required Boolean Flipped { get; set; }
    }
}