using Microsoft.AspNetCore.Mvc;
using api.Models;
using api.Services;
using System.Text.Json;

namespace api.Routes
{
    public static class GameEndpoints
    {
        public static void MapGameEndpoints(WebApplication app)
        {
            app.MapGet("/greeting", () =>
            {
                return Results.Ok("Welcome to the Memory Game API!");
            });

            app.MapPost("/game", async ([FromBody] Game game) =>
            {
                if (game == null) return Results.BadRequest("Invalid game data.");

                await GameData.SaveGameAsync(game, game.Handle);
                return Results.Ok($"Game data for {game.Handle} saved successfully.");
            });

            app.MapGet("/game/{handle}", async (string handle) =>
            {
                var game = await GameData.RetrieveGameAsync(handle);
                if (game == null) return Results.NotFound($"No game found for handle: {handle}");

                return Results.Ok(game);
            });

            app.MapPost("/leaderboard", async ([FromBody] Leaderboard entry) =>
            {
                if (entry == null) return Results.BadRequest("Invalid leaderboard entry.");

                var leaderboardEntries = await GameData.RetrieveLeaderboardAsync();
                leaderboardEntries.Add(entry);
                await GameData.SaveLeaderboardEntryAsync(leaderboardEntries);

                return Results.Ok($"Leaderboard entry for {entry.Handle} saved successfully.");
            });

            app.MapGet("/leaderboard", async () =>
            {
                var leaderboardEntries = await GameData.RetrieveLeaderboardAsync();
                var topTenEntries = leaderboardEntries.OrderByDescending(entry => entry.Score).Take(10);

                return Results.Ok(topTenEntries);
            });
        }
    }
}