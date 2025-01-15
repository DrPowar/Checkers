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
    
}