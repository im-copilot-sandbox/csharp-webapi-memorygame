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

                // Since `Card.Flipped` is a boolean, it's inherently valid (true or false).
                // No need for an explicit check here, but you could add additional logic
                // if there are specific rules about when cards can be flipped.
            }

            await GameData.SaveGameAsync(game, game.Handle);
            return Ok();
        }

        // GET /game/handle
        [HttpGet("game/{handle}")]
        public async Task<IActionResult> GetGame(string handle)
        {
            // Step 1: Validate input
            if (string.IsNullOrWhiteSpace(handle))
            {
                return BadRequest("Handle is required.");
            }

            // Step 2: (Optional) Further validation, e.g., regex for specific patterns
            // Example: Validate handle is alphanumeric (uncomment to use)
            // var isValidHandle = Regex.IsMatch(handle, "^[a-zA-Z0-9]*$");
            // if (!isValidHandle)
            // {
            //     return BadRequest("Handle is invalid.");
            // }

            // Existing logic to retrieve game
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

            // Check if game data for the handle exists
            var gameData = await GameData.RetrieveGameAsync(entry.Handle);
            if (gameData == null)
            {
                return BadRequest("Invalid handle. No game data found.");
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
            // Validate the leaderboard data is not null or empty
            if (leaderboard == null || !leaderboard.Any())
            {
                return NotFound("Leaderboard data is not available.");
            }

            // Proceed with the existing logic to sort and take the top ten entries
            var topTen = leaderboard.OrderByDescending(l => l.Score).Take(10).ToList();
            return Ok(topTen);
        }
    }
}
