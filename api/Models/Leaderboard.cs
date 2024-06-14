namespace api.Models
{
    // Represents a leaderboard entry
    public record Leaderboard
    {
        // The handle of the player
        public required string Handle { get; init; }

        // The score achieved by the player
        public int Score { get; init; }

        // The date and time when the game was completed
        public DateTime DateTimePlayed { get; init; }
    }
}