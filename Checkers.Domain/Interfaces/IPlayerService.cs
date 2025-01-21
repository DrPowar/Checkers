using Checkers.Domain.Models;

namespace Checkers.Domain.Interfaces
{
    public interface IPlayerService
    {
        Task<Player> CreatePlayer(string name, CancellationToken cancellationToken = default);

        Task<Player?> GetPlayerById(Guid playerId, CancellationToken cancellationToken = default);
    }
}
