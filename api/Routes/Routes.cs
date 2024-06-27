using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Api.Services;

namespace Api.Routes
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
            app.MapPost("/game", async ([FromBody] Game game) =>
            {
                if (game == null || string.IsNullOrWhiteSpace(game.Handle))
                    { return Results.BadRequest("Invalid game data or missing handle."); }

                await GameData.SaveGameAsync(game, game.Handle);
                return Results.Ok($"Game data for {game.Handle} saved successfully.");
            });

            // GET /game/{handle} - Retrieve game information by handle
            app.MapGet("/game/{handle}", async (string handle) =>
            {
                var game = await GameData.RetrieveGameAsync(handle);
                if (game == null) return Results.NotFound($"No game found for handle: {handle}");

                return Results.Ok(game);
            });

            // POST /leaderboard - Save leaderboard entry
            app.MapPost("/leaderboard", async ([FromBody] Leaderboard entry) =>
            {
                if (entry == null) return Results.BadRequest("Invalid leaderboard entry.");

                var leaderboardEntries = await GameData.RetrieveLeaderboardAsync();
                leaderboardEntries.Add(entry);
                await GameData.SaveLeaderboardEntryAsync(leaderboardEntries);

                return Results.Ok($"Leaderboard entry for {entry.Handle} saved successfully.");
            });

            // GET /leaderboard - Retrieve top 10 leaderboard entries
            app.MapGet("/leaderboard", async () =>
            {
                var leaderboardEntries = await GameData.RetrieveLeaderboardAsync();
                var topTenEntries = leaderboardEntries.OrderByDescending(entry => entry.Score).Take(10);

                return Results.Ok(topTenEntries);
            });
        }
    }
}