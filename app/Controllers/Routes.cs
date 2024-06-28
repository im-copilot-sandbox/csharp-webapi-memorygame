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

        // POST /game - Save game data
        [HttpPost("game")]
        public async Task<IActionResult> PostGame([FromBody] Game game)
        {
            // Validation checks for game data
            if (game == null)
            {
                return BadRequest("Game data is required.");
            }

            if (string.IsNullOrEmpty(game.Handle))
            {
                return BadRequest("Game handle is required.");
            }

            // Ensure the number of cards is even
            if (game.Cards == null || game.Cards.Count % 2 != 0)
            {
                return BadRequest("The number of cards must be even.");
            }

            foreach (var card in game.Cards)
            {
                if (string.IsNullOrEmpty(card.Type))
                {
                    return BadRequest("Each card must have a valid type.");
                }
            }

            // Save game data asynchronously
            await GameData.SaveGameAsync(game, game.Handle);
            return Ok();
        }

        // GET /game/handle - Retrieve game data by handle
        [HttpGet("game/{handle}")]
        public async Task<IActionResult> GetGame(string handle)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(handle))
            {
                return BadRequest("Handle is required.");
            }

            // Retrieve game data asynchronously
            var game = await GameData.RetrieveGameAsync(handle);
            if (game == null)
            {
                return NotFound($"Game data for player {handle} not found.");
            }
            return Ok(game);
        }

        // POST /leaderboard - Save leaderboard entry
        [HttpPost("leaderboard")]
        public async Task<IActionResult> PostLeaderboard([FromBody] LeaderboardEntry entry)
        {
            // Validate leaderboard entry
            if (entry == null || string.IsNullOrEmpty(entry.Handle) || entry.Score <= 0)
            {
                return BadRequest("Invalid leaderboard entry.");
            }

            // Check if game data for the handle exists
            var gameData = await GameData.RetrieveGameAsync(entry.Handle);
            if (gameData == null)
            {
                return BadRequest("Invalid handle. No game data found.");
            }

            // Save leaderboard entry asynchronously
            await GameData.SaveLeaderboardEntryAsync(entry.Handle, entry.Score);
            return Ok("Leaderboard entry saved successfully.");
        }

        // Leaderboard entry model
        public class LeaderboardEntry
        {
            public string Handle { get; set; }
            public int Score { get; set; }
        }

        // GET /leaderboard - Retrieve top ten leaderboard entries
        [HttpGet("leaderboard")]
        public async Task<ActionResult<List<Leaderboard>>> GetLeaderboard()
        {
            // Retrieve leaderboard data asynchronously
            var leaderboard = await GameData.RetrieveLeaderboardAsync();
            // Validate the leaderboard data is not null or empty
            if (leaderboard == null || !leaderboard.Any())
            {
                return NotFound("Leaderboard data is not available.");
            }

            // Sort and take the top ten entries
            var topTen = leaderboard.OrderByDescending(l => l.Score).Take(10).ToList();
            return Ok(topTen);
        }
    }
}