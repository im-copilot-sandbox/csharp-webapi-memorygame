namespace api.Models
{
    // Represents a leaderboard entry
    public record Leaderboard
    {
        // The date and time when the game was completed
        public DateTime PlayedOn { get; init; }

        // The handle of the player
        public required string PlayerHandle { get; init; }

        // The score achieved by the player
        public int Score { get; init; }
    }
}