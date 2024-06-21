namespace Api.Models
{
    // Represents a leaderboard entry
    public record Leaderboard
    {
        // The handle of the player
        public required string Handle { get; init; }

        // The score achieved by the player
        private int _score;

        public int Score
        {
            get => _score;
            init
            {
                if (value < 0)
                {
                    throw new ArgumentException("Score must be greater than or equal to 0.");
                }
                _score = value;
            }
        }

        // The date and time when the game was completed
        public required DateTime DateTimePlayed { get; init; }
    }
}