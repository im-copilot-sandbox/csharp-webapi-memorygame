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
    var games = await LoadData<List<Game>>(gamesFilePath) ?? new List<Game>();
    games.Add(game);
    await SaveData(gamesFilePath, games);
    return Results.Ok();
});

// Modify the GET /games/{handle} endpoint to retrieve games from a JSON file
app.MapGet("/games/{handle}", async (string handle) =>
{
    var games = await LoadData<List<Game>>(gamesFilePath) ?? new List<Game>();
    var filteredGames = games.Where(g => g.PlayerHandle == handle)
                             .OrderByDescending(g => g.SaveDate)
                             .Take(5);
    return Results.Ok(filteredGames);
});

// Modify the POST /leaderboard endpoint to save leaderboard entries to a JSON file
app.MapPost("/leaderboard", async ([FromBody] LeaderboardEntry entry) =>
{
    var leaderboard = await LoadData<List<LeaderboardEntry>>(leaderboardFilePath) ?? new List<LeaderboardEntry>();
    leaderboard.Add(entry);
    await SaveData(leaderboardFilePath, leaderboard);
    return Results.Ok();
});

// Modify the GET /leaderboard endpoint to retrieve leaderboard entries from a JSON file
app.MapGet("/leaderboard", async () =>
{
    var leaderboard = await LoadData<List<LeaderboardEntry>>(leaderboardFilePath) ?? new List<LeaderboardEntry>();
    var topPlayers = leaderboard.OrderByDescending(l => l.Score).Take(10);
    return Results.Ok(topPlayers);
});

// Utility methods for loading and saving data
async Task<T?> LoadData<T>(string filePath) where T : class
{
    if (!File.Exists(filePath)) return null;
    var jsonData = await File.ReadAllTextAsync(filePath);
    return JsonSerializer.Deserialize<T>(jsonData);
}

async Task SaveData<T>(string filePath, T data)
{
    var jsonData = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
    await File.WriteAllTextAsync(filePath, jsonData);
}

app.Run();

// Models
public record Game(Guid Id, string PlayerHandle, string Handle, int TurnsTaken, TimeSpan TimeTaken, TimeSpan TimeLeft, List<Card> Cards, DateTime SaveDate);
    
public record Card(string CardType, string State);

public record LeaderboardEntry(DateTime DateTimePlayed, string Handle, int Score);
