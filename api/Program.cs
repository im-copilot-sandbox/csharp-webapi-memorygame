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

// Get / - Retrieve a greeting
app.MapGet("/", () =>
{
    return Results.Ok("Welcome to the Memory Game API");
});

// GET /games/handle - Retrieve the games a player has played with their scores
app.MapGet("/games/{handle}", (string handle) =>
{
    // Implement logic to retrieve the last 5 saved games by the player, sorted by save date in descending order

    return Results.Ok($"Games played by player with handle: {handle} - to be implemented");
});

// POST /game - Save game information
app.MapPost("/game", ([FromBody] Game game) =>
{
    // Implement logic to save a game

    return Results.Ok();
});

// GET /game/{gameId} - Retrieve information about a specific game
app.MapGet("/game/{gameId}", (Guid gameId) =>
{
    // Implement the logic to retrieve game information by gameId

    return Results.Ok($"Game information for game with ID: {gameId} - to be implemented");
});

// POST /leaderboard - Save leaderboard entry
app.MapPost("/leaderboard", ([FromBody] LeaderboardEntry entry) =>
{
    // Implement logic to save a leaderboard entry

    return Results.Ok();
});

// GET /leaderboard - Retrieve top 10 players in score descending order
app.MapGet("/leaderboard", () =>
{
    // Logic to retrieve top 10 players

    return Results.Ok("Top 10 players in descending order of score - to be implemented");
});

app.Run();

// Models
public record Game(Guid Id, string PlayerHandle, string Name, int Turns, TimeSpan TimeTaken, TimeSpan TimeLeft, List<Card> Cards, DateTime SaveDate);
    
public record Card(int Position, bool IsFlipped, bool IsMatched);

public record LeaderboardEntry(DateTime DateTimePlayed, string PlayerHandle, int Score, int Turns, TimeSpan TimeTaken);

public class DataStore
{
    private const string GamesFileName = "games.json";
    private const string LeaderboardFileName = "leaderboard.json";
    private List<Game> games = new List<Game>();
    private List<LeaderboardEntry> leaderboardEntries = new List<LeaderboardEntry>();

    public DataStore()
    {
        LoadData();
    }

    public void SaveGame(Game game)
    {
        games.Add(game);
        SaveData();
    }

    public IEnumerable<Game> GetGamesByHandle(string handle)
    {
        return games.Where(g => g.PlayerHandle == handle)
                    .OrderByDescending(g => g.SaveDate)
                    .Take(5);
    }

    public Game GetGameById(Guid gameId)
    {
        return games.FirstOrDefault(g => g.Id == gameId);
    }

    public void SaveLeaderboardEntry(LeaderboardEntry entry)
    {
        leaderboardEntries.Add(entry);
        SaveData();
    }

    public IEnumerable<LeaderboardEntry> GetTopPlayers(int count)
    {
        return leaderboardEntries.OrderByDescending(l => l.Score).Take(count);
    }

    private void SaveData()
    {
        var gamesJson = JsonSerializer.Serialize(games);
        File.WriteAllText(GamesFileName, gamesJson);

        var leaderboardJson = JsonSerializer.Serialize(leaderboardEntries);
        File.WriteAllText(LeaderboardFileName, leaderboardJson);
    }

    private void LoadData()
    {
        if (File.Exists(GamesFileName))
        {
            var gamesJson = File.ReadAllText(GamesFileName);
            games = JsonSerializer.Deserialize<List<Game>>(gamesJson) ?? new List<Game>();
        }

        if (File.Exists(LeaderboardFileName))
        {
            var leaderboardJson = File.ReadAllText(LeaderboardFileName);
            leaderboardEntries = JsonSerializer.Deserialize<List<LeaderboardEntry>>(leaderboardJson) ?? new List<LeaderboardEntry>();
        }
    }
}