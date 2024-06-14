namespace api.Models
{
    public record Game
    {
        public Guid Id { get; init; }
        public required string PlayerHandle { get; init; }
        public int TurnsTaken { get; init; }
        public TimeSpan TimeTaken { get; init; }
        public TimeSpan TimeLeft { get; init; }
        public List<Card> Cards { get; init; }
        public DateTime SaveDate { get; init; }
    }
}