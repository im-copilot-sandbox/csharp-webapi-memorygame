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
        public ActionResult<string> PostGame([FromBody] Game game)
        {
            // TODO: Save the following in a JSON file
            // named with the player handle
            // - name under the handle
            // - # of turns
            // - time taken
            // - time left
            // - number of cards and their position
            // - which ones flipped vs. hidden
            return Ok();
        }

        // GET /game/handle
        [HttpGet("game/{handle}")]
        public ActionResult<string> GetGame(string handle)
        {
            // TODO: Retrieve info about the game stored via POST /game
            return Ok();
        }

        // POST /leaderboard
        [HttpPost("leaderboard")]
        public ActionResult<string> PostLeaderboard([FromBody] Leaderboard entry)
        {
            // TODO: Save the following
            // - player handle
            // - score
            // - date/time last played
            return Ok();
        }

        // GET /leaderboard
        [HttpGet("leaderboard")]
        public ActionResult<string> GetLeaderboard()
        {
            // TODO: Retrieve top 10 players in score desc order
            // - player handle
            // - score
            // - date/time last played
            return Ok();
        }
    }
}
