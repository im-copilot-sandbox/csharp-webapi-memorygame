using Microsoft.AspNetCore.Mvc;
using app.Models;
using app.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace app.Controllers
{
    [ApiController]
    [Route("")]
    public class RoutesController : ControllerBase
    {
        private readonly ILogger<RoutesController> _logger;

        public RoutesController(ILogger<RoutesController> logger)
        {
            _logger = logger;
        }

        // Route to fetch data from the JSON file
        [HttpGet("greeting")]
        public ActionResult<string> Greeting()
        {
            _logger.LogInformation("Greeting endpoint called.");
            return Ok("Hello, csharp");
        }

        // POST /game
        [HttpPost("game")]
        public async Task<IActionResult> PostGame([FromBody] Game game)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return await HandleRequestAsync(
                async () =>
                {
                    await GameData.SaveGameAsync(game, game.Handle);
                    return Ok();
                },
                "Game data saved successfully for handle: {Handle}",
                "Error saving game data for handle: {Handle}",
                game.Handle
            );
        }

        // GET /game/handle
        [HttpGet("game/{handle}")]
        public async Task<IActionResult> GetGame(string handle)
        {
            _logger.LogInformation("GetGame called for handle: {Handle}", handle);

            if (string.IsNullOrWhiteSpace(handle))
            {
                _logger.LogWarning("GetGame called with an empty handle.");
                return BadRequest(new { message = "Handle is required." });
            }

            return await HandleRequestAsync(
                async () =>
                {
                    var game = await GameData.RetrieveGameAsync(handle);
                    return game == null ? NotFound(new { message = $"Game data for player {handle} not found." }) : Ok(game);
                },
                "Game data retrieved successfully for handle: {Handle}",
                "Error retrieving game data for handle: {Handle}",
                handle
            );
        }

        // POST /leaderboard
        [HttpPost("leaderboard")]
        public async Task<IActionResult> PostLeaderboard([FromBody] Leaderboard entry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return await HandleRequestAsync(
                async () =>
                {
                    var gameData = await GameData.RetrieveGameAsync(entry.Handle);
                    if (gameData == null)
                    {
                        _logger.LogWarning("PostLeaderboard called with an invalid handle: {Handle}", entry.Handle);
                        return BadRequest(new { message = "Invalid handle. No game data found." });
                    }

                    await GameData.SaveLeaderboardEntryAsync(entry.Handle, entry.Score);
                    return Ok(new { message = "Leaderboard entry saved successfully." });
                },
                "Leaderboard entry saved successfully for handle: {Handle}",
                "Error saving leaderboard entry for handle: {Handle}",
                entry.Handle
            );
        }

        // GET /leaderboard
        [HttpGet("leaderboard")]
        public async Task<IActionResult> GetLeaderboard()
        {
            _logger.LogInformation("GetLeaderboard called.");

            return await HandleRequestAsync(
                async () =>
                {
                    var leaderboard = await GameData.RetrieveLeaderboardAsync();
                    if (leaderboard == null || !leaderboard.Any())
                    {
                        _logger.LogWarning("Leaderboard data is not available.");
                        return NotFound(new { message = "Leaderboard data is not available." });
                    }

                    var topTen = leaderboard.OrderByDescending(l => l.Score).Take(10).ToList();
                    return Ok(topTen);
                },
                "Leaderboard data retrieved successfully.",
                "Error retrieving leaderboard data."
            );
        }

        private async Task<IActionResult> HandleRequestAsync(Func<Task<IActionResult>> action, string successMessage, string errorMessage, string? handle = null)
        {
            try
            {
                var result = await action();
                _logger.LogInformation(successMessage, handle);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, errorMessage, handle);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred.", details = ex.Message });
            }
        }

        private async Task<IActionResult> HandleRequestAsync<T>(Func<Task<T>> action, string successMessage, string errorMessage, string handle, Func<T, IActionResult> resultHandler)
        {
            try
            {
                var result = await action();
                _logger.LogInformation(successMessage, handle);
                return resultHandler(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, errorMessage, handle);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred.", details = ex.Message });
            }
        }

        public class LeaderboardEntry
        {
            [Required]
            public string Handle { get; set; }
            
            [Range(0, int.MaxValue)]
            public int Score { get; set; }
        }
    }
}