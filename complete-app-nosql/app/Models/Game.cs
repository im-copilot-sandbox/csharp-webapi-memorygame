using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace app.Models
{
    public class Game
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string Id { get; set; }
        [JsonPropertyName("handle")]
        public required string Handle { get; set; }

        [JsonPropertyName("turns_taken")]
        public int TurnsTaken { get; set; }

        [JsonPropertyName("time_taken")]
        public int TimeTaken { get; set; }

        [JsonPropertyName("cards")]
        public List<Card> Cards { get; set; } = new();

        public int GameId { get; set; }

        public ICollection<GameCard> GameCards { get; set; } = [];

    }
}