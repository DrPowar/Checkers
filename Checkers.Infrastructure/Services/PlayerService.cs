using Checkers.Domain.Interfaces;
using Checkers.Domain.Interfaces.Repositories;
using Checkers.Domain.Models;

namespace Checkers.Infrastructure.Services
{
    public class PlayerService(IBaseRepository<Player> playerRepository)
        : IPlayerService
    {
        private readonly IBaseRepository<Player> _playerRepository = 
            playerRepository ?? throw new ArgumentNullException(nameof(playerRepository));

        public async Task<Player> CreatePlayer(string name, CancellationToken cancellationToken = default)
        {
            Player player = new Player
            {
                Id = Guid.NewGuid(),
                Name = name
            };

            _playerRepository.Add(player, cancellationToken);
            await _playerRepository.SaveChanges(cancellationToken);

            return player;
        }

        public async Task<Player?> GetPlayerById(Guid playerId, CancellationToken cancellationToken = default)
        {
            return await _playerRepository.Get(playerId, cancellationToken);
        }
    }
}
