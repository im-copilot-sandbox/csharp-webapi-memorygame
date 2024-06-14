namespace api.Models
{
    public record Leaderboard
    {
        public DateTime DateTimePlayed { get; init; }
        public required string PlayerHandle { get; init; }
        public int Score { get; init; }
        public int Turns { get; init; }
        public TimeSpan TimeTaken { get; init; }
    }
}