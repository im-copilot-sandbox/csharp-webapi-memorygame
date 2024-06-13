using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Define file paths
const string gamesFilePath = "games.json";
const string leaderboardFilePath = "leaderboard.json";

// Get / - Retrieve a greeting
app.MapGet("/", () =>
{
    return Results.Ok("Welcome to the Memory Game API");
});


// Modify the POST /game endpoint to save game information to a JSON file
app.MapPost("/game", async ([FromBody] Game game) =>
{
    var games = await LoadGamesAsync(gamesFilePath) ?? new List<Game>();
    games.Add(game);
    await SaveGamesAsync(games, gamesFilePath);
    return Results.Ok();
});

// Modify the GET /games/{handle} endpoint to retrieve games from a JSON file
app.MapGet("/games/{handle}", async (string handle) =>
{
    var games = await LoadGamesAsync(gamesFilePath) ?? new List<Game>();
    var filteredGames = games.Where(g => g.PlayerHandle == handle)
                             .OrderByDescending(g => g.SaveDate)
                             .Take(5);
    return Results.Ok(filteredGames);
});

// Modify the POST /leaderboard endpoint to save leaderboard entries to a JSON file
app.MapPost("/leaderboard", async ([FromBody] LeaderboardEntry entry) =>
{
    var leaderboard = await LoadLeaderboardAsync(leaderboardFilePath) ?? new List<LeaderboardEntry>();
    leaderboard.Add(entry);
    await SaveLeaderboardAsync(leaderboard, leaderboardFilePath);
    return Results.Ok();
});

// Modify the GET /leaderboard endpoint to retrieve leaderboard entries from a JSON file
app.MapGet("/leaderboard", async () =>
{
    var leaderboard = await LoadLeaderboardAsync(leaderboardFilePath) ?? new List<LeaderboardEntry>();
    var topPlayers = leaderboard.OrderByDescending(l => l.Score).Take(10);
    return Results.Ok(topPlayers);
});

static async Task SaveGamesAsync(List<Game> games, string filePath)
{
    var jsonString = JsonSerializer.Serialize(games);
    await File.WriteAllTextAsync(filePath, jsonString);
}

static async Task<List<Game>> LoadGamesAsync(string filePath)
{
    if (!File.Exists(filePath)) return new List<Game>();
    var jsonString = await File.ReadAllTextAsync(filePath);
    return JsonSerializer.Deserialize<List<Game>>(jsonString) ?? new List<Game>();
}

static async Task SaveLeaderboardAsync(List<LeaderboardEntry> leaderboardEntries, string filePath)
{
    var jsonString = JsonSerializer.Serialize(leaderboardEntries);
    await File.WriteAllTextAsync(filePath, jsonString);
}

static async Task<List<LeaderboardEntry>> LoadLeaderboardAsync(string filePath)
{
    if (!File.Exists(filePath)) return new List<LeaderboardEntry>();
    var jsonString = await File.ReadAllTextAsync(filePath);
    return JsonSerializer.Deserialize<List<LeaderboardEntry>>(jsonString) ?? new List<LeaderboardEntry>();
}

app.Run();

// Models
public record Game(Guid Id, string PlayerHandle, string Handle, int TurnsTaken, TimeSpan TimeTaken, TimeSpan TimeLeft, List<Card> Cards, DateTime SaveDate);
    
public record Card(string CardType, string State);

public record LeaderboardEntry(DateTime DateTimePlayed, string PlayerHandle, int Score, int Turns, TimeSpan TimeTaken);
