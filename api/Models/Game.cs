namespace api.Models
{
    // Represents a game
    public record Game
    {
        // The player's handle
        public required string Handle { get; init; }

        // The number of turns taken in the game
        public int TurnsTaken { get; init; }

        // The time taken thus far in the game in seconds
        public int TimeTaken { get; init; }

        // Indicates whether the game has been completed
        public bool GameCompleted { get; init; }

        // The cards in the game and their state
        public required List<Card> Cards { get; init; }
    }
}