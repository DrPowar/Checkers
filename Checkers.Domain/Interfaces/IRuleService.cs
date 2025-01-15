using Checkers.Domain.Models;

namespace Checkers.Domain.Interfaces
{
    public interface IRuleService
    {
        bool IsMoveValid(Guid gameId, Move move);

        bool IsGameOver(Guid gameId);

        Player GetWinner(Guid gameId);
    }
}
