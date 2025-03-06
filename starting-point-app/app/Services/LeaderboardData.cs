using app.Data;
using app.Models;
using MongoDB.Driver;

namespace app.Services
{
    public class LeaderboardData
    {
        private readonly MongoDbContext _context;

        public LeaderboardData(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<List<Leaderboard>> GetTopLeaderboardEntriesAsync()
        {
            return await _context.Leaderboards.Find(_ => true)
                .SortByDescending(entry => entry.Score)
                .Limit(10)
                .ToListAsync();
        }

        public async Task CreateLeaderboardEntryAsync(Leaderboard entry)
        {
            await _context.Leaderboards.InsertOneAsync(entry);
        }
    }
}