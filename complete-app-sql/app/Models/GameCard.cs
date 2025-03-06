namespace app.Models
{
    public class GameCard
    {
        public int GameId { get; set; }
        public Game Game { get; set; }
        public int CardId { get; set; }
        public Card Card { get; set; }
    }
}