using Microsoft.AspNetCore.Mvc;
using app.Models;
using app.Services;

namespace app.Controllers
{
    [ApiController]
    [Route("")]
    public class RoutesController : ControllerBase
    {
        // Route to fetch data from the JSON file
        [HttpGet("greeting")]
        public ActionResult<string> Greeting()
        {
            return Ok("Hello, World!");
        }

        // POST /game
        [HttpPost("game")]
        public async Task<IActionResult> PostGame([FromBody] Game game)
        {
            if (game == null || string.IsNullOrEmpty(game.Handle))
            {
                return BadRequest("Invalid game data.");
            }

            await GameData.SaveGameAsync(game, game.Handle);
            return Ok();
        }

        // GET /game/handle
        [HttpGet("game/{handle}")]
        public async Task<IActionResult> GetGame(string handle)
        {
            var game = await GameData.RetrieveGameAsync(handle);
            if (game == null)
            {
                return NotFound($"Game data for player {handle} not found.");
            }
            return Ok(game);
        }

        // POST /leaderboard
        [HttpPost("leaderboard")]
        public async Task<IActionResult> PostLeaderboard([FromBody] LeaderboardEntry entry)
        {
            if (entry == null || string.IsNullOrEmpty(entry.Handle) || entry.Score <= 0)
            {
                return BadRequest("Invalid leaderboard entry.");
            }

            await GameData.SaveLeaderboardEntryAsync(entry.Handle, entry.Score);
            return Ok("Leaderboard entry saved successfully.");
        }

        public class LeaderboardEntry
        {
            public string Handle { get; set; }
            public int Score { get; set; }
        }

        // GET /leaderboard
        [HttpGet("leaderboard")]
        public async Task<ActionResult<List<Leaderboard>>> GetLeaderboard()
        {
            var leaderboard = await GameData.RetrieveLeaderboardAsync();
            var topTen = leaderboard.OrderByDescending(l => l.Score).Take(10).ToList();
            return Ok(topTen);
        }
    }
}
