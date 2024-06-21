using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Api.Services;

namespace api.Routes
{
    public static class GameEndpoints
    {
        public static void MapGameEndpoints(WebApplication app)
        {
            // Get / - Retrieve a greeting
            app.MapGet("/greeting", () =>
            {
                return Results.Ok("Welcome to the Memory Game API!");
            });

            // POST /game - Save game information
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

            // GET /game/handle - Retrieve the last game a player has played
            app.MapGet("/game/{handle}", (string handle) =>
            {
                // TODO: Retrieve info about the game stored via POST /game    
                return Results.Ok($"Games played by player with handle: {handle} - to be implemented");
            });

            // POST /leaderboard - Save leaderboard entry
            app.MapPost("/leaderboard", ([FromBody] Leaderboard entry) =>
            {
                // TODO: Save the following
                // - player handle
                // - score
                // - date/time last played 
                return Results.Ok();
            });

            // GET /leaderboard - Retrieve top 10 players in score descending order
            app.MapGet("/leaderboard", () =>
            {
                // TODO: Retrieve top 10 players in score desc order
                // - player handle
                // - score
                // - date/time last played 
                return Results.Ok("Top 10 players in descending order of score - to be implemented");
            });
        }
    }
}