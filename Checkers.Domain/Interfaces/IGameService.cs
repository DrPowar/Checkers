using Checkers.Domain.Enums;
using Checkers.Domain.Models;

namespace Checkers.Domain.Interfaces
{
    public interface IGameService
    {
        Task<Game> CreateGameAndSave(List<Piece> board, CancellationToken cancellationToken = default);
        Task<Game?> GetGameById(Guid gameId, CancellationToken cancellationToken = default);
        Task ChangeStatus(Guid gameId, GameStatus newStatus, CancellationToken cancellationToken = default);
        Task<GameStatus> GetGameStatus(Guid gameId, CancellationToken cancellationToken = default);
        Task AssignPlayerToGame(Guid gameId, Guid playerId, CancellationToken cancellationToken = default);
    }
}
