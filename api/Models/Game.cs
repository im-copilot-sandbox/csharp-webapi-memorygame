namespace api.Models
{
    // Represents a game
    public record Game
    {
        // The player's handle
        public required string PlayerHandle { get; init; }

        // The number of turns taken in the game
        public int TurnsTaken { get; init; }

        // The time taken thus far in the game in seconds
        public int TimeTaken { get; init; }

        // The time left in the game in seconds
        public int TimeLeft { get; init; }

        // The list of cards in the game
        public required List<Card> Cards { get; init; }

        // The date when the game was last saved
        public DateTime LastPlayedOn { get; init; }
    }
}