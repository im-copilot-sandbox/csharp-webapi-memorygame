using System.Text.Json;
using app.Models;
using app.Data;
using Microsoft.EntityFrameworkCore;

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
        private readonly MemoryGameContext _context;

        public GameData(MemoryGameContext context)
        {
            _context = context;
        }

        public async Task SaveGameAsync(Game game)
        {
            var existingGame = await _context.Games.Include(g => g.Cards).FirstOrDefaultAsync(g => g.Handle == game.Handle);
            if (existingGame != null)
            {
                _context.Games.Remove(existingGame);
            }

            _context.Games.Add(game);
            await _context.SaveChangesAsync();
        }

        public async Task<Game?> RetrieveGameAsync(string handle)
        {
            return await _context.Games.Include(g => g.Cards).FirstOrDefaultAsync(g => g.Handle == handle);
        }

        public async Task SaveLeaderboardEntryAsync(string handle, int score)
        {
            var entry = await _context.Leaderboard.FirstOrDefaultAsync(e => e.Handle == handle);
            if (entry != null)
            {
                entry = new Leaderboard
                {
                    Handle = handle,
                    Score = score,
                    LastPlayed = DateTime.UtcNow
                };
                _context.Leaderboard.Update(entry);
            }
            else
            {
                _context.Leaderboard.Add(new Leaderboard
                {
                    Handle = handle,
                    Score = score,
                    LastPlayed = DateTime.UtcNow
                });
            }

            await _context.SaveChangesAsync();
        }

        public async Task<List<Leaderboard>> RetrieveLeaderboardAsync()
        {
            return await _context.Leaderboard.OrderByDescending(l => l.Score).ToListAsync();
        }
    }
}