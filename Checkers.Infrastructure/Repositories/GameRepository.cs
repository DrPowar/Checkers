using Checkers.Domain.Models;
using Checkers.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Checkers.Infrastructure.Repositories;

public class GameRepository(CheckersDbContext context) : BaseRepository<Game>(context)
{
    public override Task<Game?> Get(Guid id, CancellationToken cancellationToken = default)
    {
        return Context.Games
            .Where(g => g.Id == id)
            .Include(g => g.Players)
            .Include(g => g.Board)
            .ThenInclude(b => b.Position)
            .FirstOrDefaultAsync(cancellationToken);
    }
}