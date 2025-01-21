using Checkers.Domain.Models;

namespace Checkers.Domain.Interfaces
{
    public interface IBoardService
    {
        Task<List<Piece>> InitializeBoard(CancellationToken cancellationToken = default);

        Task<List<Piece>> GetBoard(Guid gameId, CancellationToken cancellationToken = default);
    }
}
