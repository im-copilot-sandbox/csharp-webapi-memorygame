using MongoDB.Driver;
using app.Models;

namespace app.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("MemoryGameDatabase"));
            _database = client.GetDatabase(configuration["DatabaseName"]);
        }

        public IMongoCollection<Card> Cards => _database.GetCollection<Card>("Cards");
        public IMongoCollection<Game> Games => _database.GetCollection<Game>("Games");
        public IMongoCollection<Leaderboard> Leaderboards => _database.GetCollection<Leaderboard>("Leaderboards");
    }
}