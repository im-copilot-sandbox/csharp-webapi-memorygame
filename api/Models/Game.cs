namespace Api.Models
{
    // Represents a game
    public record Game
    {
        // The player's handle
        public required string Handle { get; init; }

        // The number of turns taken in the game
        private int _turnsTaken;
        public int TurnsTaken
        {
            get => _turnsTaken;
            init
            {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(TurnsTaken), "Turns taken must be greater than or equal to 0.");
            }
            _turnsTaken = value;
            }
        }

        // The time taken thus far in the game in seconds
        private int _timeTaken;
        public int TimeTaken
        {
            get => _timeTaken;
            init
            {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(TimeTaken), "Time taken must be greater than or equal to 0.");
            }
            _timeTaken = value;
            }
        }

        // Indicates whether the game has been completed
        public bool GameCompleted { get; init; }

        // The cards in the game and their state
        public required List<Card> Cards { get; init; }
    }
}