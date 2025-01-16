using Checkers.Domain.Models;

namespace Checkers.Domain.Interfaces;

public interface IStatusService
{
    bool IsGameOver(Game game);
    Player? GetWinner(Game game);
}