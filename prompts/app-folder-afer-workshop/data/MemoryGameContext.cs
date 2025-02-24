using Microsoft.EntityFrameworkCore;
using app.Models;

namespace app.Data
{
    public class MemoryGameContext : DbContext
    {
        public MemoryGameContext(DbContextOptions<MemoryGameContext> options) : base(options) { }

        public DbSet<Card> Cards { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Leaderboard> Leaderboard { get; set; }
        public DbSet<GameCard> GameCards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameCard>()
                .HasKey(gc => new { gc.GameId, gc.CardId });

            modelBuilder.Entity<GameCard>()
                .HasOne(gc => gc.Game)
                .WithMany(g => g.GameCards)
                .HasForeignKey(gc => gc.GameId);

            modelBuilder.Entity<GameCard>()
                .HasOne(gc => gc.Card)
                .WithMany(c => c.GameCards)
                .HasForeignKey(gc => gc.CardId);
        }
    }
}