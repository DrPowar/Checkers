using Checkers.Domain.Models;

namespace Checkers.Domain.Interfaces;

public interface IGameEngineService
{
    Task<bool> CheckGameOver(Game game, CancellationToken token = default);
    Player? GetWinner(Game game);
    Task<bool> MakeMove(Guid gameId, Guid playerId, Position from, Position to, CancellationToken cancellationToken = default);
    Task<List<Move>> GetAvailableMoves(Guid gameId, Guid playerId, CancellationToken cancellationToken = default);
}