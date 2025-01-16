using Checkers.Domain.Models;

namespace Checkers.Domain.Interfaces
{
    public interface IMoveService
    {
        bool ValidMove(Guid gameId, Move move);

        void MakeMove(Guid gameId, Move move);

        List<Move> GetAvaibleMoves(Guid gameId, Piece piece);
    }
}
