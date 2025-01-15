using Checkers.Domain.Models;

namespace Checkers.Domain.Interfaces
{
    public interface IRuleService
    {
        bool IsMoveValid(Game game, Move move);

        bool IsGameOver(Game gameId);

        Player GetWinner(Game gameId);
    }
}
