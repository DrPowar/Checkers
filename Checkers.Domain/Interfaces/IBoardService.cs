using Checkers.Domain.Models;

namespace Checkers.Domain.Interfaces
{
    public interface IBoardService
    {
        List<Piece> InitializeBoard();

        List<Piece> GetBoard(Guid gameId);

        List<Piece> UpdateBoard(Guid gameId);
    }
}
