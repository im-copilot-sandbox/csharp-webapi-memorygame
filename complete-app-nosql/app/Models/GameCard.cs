using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace app.Models
{
    public class GameCard
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string Id { get; set; }

        public int GameId { get; set; }
        public required Game Game { get; set; }
        public int CardId { get; set; }
        public required Card Card { get; set; }
    }
}