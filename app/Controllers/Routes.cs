using Microsoft.AspNetCore.Mvc;
using app.Models;
using app.Services;
using System.Text.Json;
using System.IO;
using System.Linq;

namespace app.Controllers
{
    [ApiController]
    [Route("api")]
    public class RoutesController : ControllerBase
    {
        private readonly GameData _gameData;

        public RoutesController()
        {
            _gameData = new GameData();
        }

        // Route to fetch data from the JSON file
        [HttpGet("greeting")]
        public ActionResult<string> Greeting()
        {
            return Ok("Hello, siva!");
        }

        // POST /api/game
        [HttpPost("game")]
        public ActionResult<string> PostGame([FromBody] Game game)
        {
            if (game == null || string.IsNullOrWhiteSpace(game.Handle))
            {
                return BadRequest("Invalid game data.");
            }

            try
            {
                _gameData.SaveGame(game);
                return Ok("Game data saved successfully.");
            }
            catch (UnauthorizedAccessException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET /api/game/{handle}
        [HttpGet("game/{handle}")]
        public ActionResult<Game> GetGame(string handle)
        {
            if (string.IsNullOrWhiteSpace(handle))
            {
                return BadRequest("Handle is required.");
            }

            try
            {
                var game = _gameData.GetGame(handle);

                if (game == null)
                {
                    return NotFound("Game data not found.");
                }

                return Ok(game);
            }
            catch (UnauthorizedAccessException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST /api/leaderboard
        [HttpPost("leaderboard")]
        public ActionResult<string> PostLeaderboard([FromBody] Leaderboard entry)
        {
            if (entry == null || string.IsNullOrWhiteSpace(entry.Handle))
            {
                return BadRequest("Invalid leaderboard entry.");
            }

            try
            {
                var leaderboardEntries = _gameData.GetLeaderboardEntries() ?? new List<Leaderboard>();
                leaderboardEntries.Add(entry);
                _gameData.SaveLeaderboardEntries(leaderboardEntries);
                return Ok("Leaderboard entry saved successfully.");
            }
            catch (UnauthorizedAccessException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET /api/leaderboard
        [HttpGet("leaderboard")]
        public ActionResult<IEnumerable<Leaderboard>> GetLeaderboard()
        {
            try
            {
                var leaderboardEntries = _gameData.GetLeaderboardEntries();

                if (leaderboardEntries == null || !leaderboardEntries.Any())
                {
                    return NotFound("No leaderboard entries found.");
                }

                // Order by score descending and take top 10
                var topPlayers = leaderboardEntries
                    .OrderByDescending(entry => entry.Score)
                    .Take(10)
                    .ToList();

                return Ok(topPlayers);
            }
            catch (UnauthorizedAccessException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
