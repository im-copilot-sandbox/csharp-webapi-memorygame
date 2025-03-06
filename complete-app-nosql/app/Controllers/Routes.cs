using Microsoft.AspNetCore.Mvc;
using app.Models;
using app.Services;

namespace app.Controllers
{
    [ApiController]
    [Route("")]
    public class RoutesController : ControllerBase
    {
        private readonly GameData _gameData;
        private readonly LeaderboardData _leaderboardData;

        public RoutesController(GameData gameData, LeaderboardData leaderboardData)
        {
            _gameData = gameData;
            _leaderboardData = leaderboardData;
        }

        // Route to fetch data from the JSON file
        [HttpGet("greeting")]
        public ActionResult<string> Greeting()
        {
            return Ok("Hello, World!");
        }

        // POST /game
        [HttpPost("game")]
        public async Task<ActionResult> PostGame([FromBody] Game game)
        {
            await _gameData.CreateGameAsync(game);
            return CreatedAtAction(nameof(GetGame), new { handle = game.Handle }, game);
        }

        // GET /game/handle
        [HttpGet("game/{handle}")]
        public async Task<ActionResult<Game>> GetGame(string handle)
        {
            var game = await _gameData.GetGameAsync(handle);
            if (game == null)
            {
                return NotFound();
            }
            return Ok(game);
        }

        // POST /leaderboard
        [HttpPost("leaderboard")]
        public async Task<ActionResult> PostLeaderboard([FromBody] Leaderboard entry)
        {
            await _leaderboardData.CreateLeaderboardEntryAsync(entry);
            return CreatedAtAction(nameof(GetLeaderboard), new { id = entry.Id }, entry);
        }

        // GET /leaderboard
        [HttpGet("leaderboard")]
        public async Task<ActionResult<List<Leaderboard>>> GetLeaderboard()
        {
            var leaderboard = await _leaderboardData.GetTopLeaderboardEntriesAsync();
            return Ok(leaderboard);
        }
    }
}
