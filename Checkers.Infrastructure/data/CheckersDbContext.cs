using Checkers.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Checkers.Infrastructure.Data;

public class CheckersDbContext : DbContext
{
    public DbSet<Game> Games { get; set; }
    public DbSet<Piece> Pieces { get; set; }
    public DbSet<Move> Moves { get; set; }
    public DbSet<Player> Players { get; set; }
    
    public CheckersDbContext(DbContextOptions<CheckersDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Move>()
            .HasOne(m => m.Game)
            .WithMany(g => g.Moves)
            .HasForeignKey(m => m.GameId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Move>()
            .HasOne(m => m.Player)
            .WithMany(p => p.Moves)
            .HasForeignKey(m => m.PlayerId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Move>()
            .HasOne(m => m.From)
            .WithMany()
            .HasForeignKey(m => m.FromId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Move>()
            .HasOne(m => m.To)
            .WithMany()
            .HasForeignKey(m => m.ToId)
            .OnDelete(DeleteBehavior.Restrict);
    }

}