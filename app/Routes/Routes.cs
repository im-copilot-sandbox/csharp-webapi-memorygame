using Microsoft.AspNetCore.Mvc;
using app.Models;

namespace app.Routes
{
    public static class GameEndpoints
    {
        public static void MapGameEndpoints(WebApplication app)
        {
            // Route to fetch data from the JSON file
            app.MapGet("/greeting", () =>
            {
                return Results.Ok("Hello, World!");
            });

            // POST /game
            app.MapPost("/game", ([FromBody] Game game) =>
            {
                // TODO: Save the following in a JSON file
                // named with the player handle
                // - name under the handle
                // - # of turns
                // - time taken
                // - time left
                // - number of cards and their position
                // - which ones flipped vs. hidden
                return Results.Ok();
            });

            // GET /game/handle
            app.MapGet("/game/{handle}", (string handle) =>
            {
                // TODO: Retrieve info about the game stored via POST /game
                return Results.Ok();
            });

            // POST /leaderboard
            app.MapPost("/leaderboard", ([FromBody] Leaderboard entry) =>
            {
                // TODO: Save the following
                // - player handle
                // - score
                // - date/time last played
                return Results.Ok();
            });

            // GET /leaderboard
            app.MapGet("/leaderboard", () =>
            {
                // TODO: Retrieve top 10 players in score desc order
                // - player handle
                // - score
                // - date/time last played
                return Results.Ok();
            });
        }
    }
}
