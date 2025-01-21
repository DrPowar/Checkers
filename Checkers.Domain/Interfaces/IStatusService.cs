using Checkers.Domain.Models;

namespace Checkers.Domain.Interfaces;

public interface IStatusService
{
    Task<bool> IsGameOver(Game game, CancellationToken token = default);
    Player? GetWinner(Game game);
}