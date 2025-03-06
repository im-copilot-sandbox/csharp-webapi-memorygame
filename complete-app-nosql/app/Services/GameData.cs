using app.Data;
using app.Models;
using MongoDB.Driver;

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

namespace app.Services
{
    public class GameData 
    {
        private readonly MongoDbContext _context;

        public GameData(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<List<Game>> GetGamesAsync()
        {
            return await _context.Games.Find(_ => true).ToListAsync();
        }

        public async Task<Game> GetGameAsync(string id)
        {
            return await _context.Games.Find(game => game.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateGameAsync(Game game)
        {
            await _context.Games.InsertOneAsync(game);
        }

        public async Task UpdateGameAsync(string id, Game game)
        {
            await _context.Games.ReplaceOneAsync(g => g.Id == id, game);
        }

        public async Task DeleteGameAsync(string id)
        {
            await _context.Games.DeleteOneAsync(g => g.Id == id);
        }
    }
}