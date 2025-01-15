using Checkers.Domain.Models;

namespace Checkers.Domain.Interfaces
{
    public interface IBoardService
    {
        Task<List<Piece>> InitializeBoard();

        Task<List<Piece>> GetBoard(Guid gameId);

        Task<List<Piece>> UpdateBoard(Guid gameId);
    }
}
