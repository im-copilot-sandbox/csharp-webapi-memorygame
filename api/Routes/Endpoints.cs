using Microsoft.AspNetCore.Mvc;
using api.Models;
using api.Services;

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
                string filePath = $"./Data/Games/{game.PlayerHandle}.json";
                var games = await Store.LoadGamesAsync(filePath);
                games.Add(game);
                await Store.SaveGamesAsync(games, filePath);
                return Results.Ok();
            });

            app.MapGet("/game/{handle}", async (string handle) =>
            {
                string filePath = $"./Data/Games/{handle}.json";
                var games = await Store.LoadGamesAsync(filePath);
                var lastGame = games.OrderByDescending(g => g.LastPlayedOn).FirstOrDefault();
                return lastGame != null ? Results.Ok(lastGame) : Results.NotFound($"No games found for player {handle}.");
            });

            app.MapPost("/leaderboard", async ([FromBody] Leaderboard entry) =>
            {
                string filePath = "./Data/Leaderboard.json";
                var leaderboardEntries = await Store.LoadLeaderboardAsync(filePath);
                leaderboardEntries.Add(entry);
                await Store.SaveLeaderboardAsync(leaderboardEntries, filePath);
                return Results.Ok();
            });

            app.MapGet("/leaderboard", async () =>
            {
                string filePath = "./Data/Leaderboard.json";
                var leaderboardEntries = await Store.LoadLeaderboardAsync(filePath);
                var topPlayers = leaderboardEntries.OrderByDescending(entry => entry.Score).Take(10);
                return Results.Ok(topPlayers);
            });
        }
    }
}