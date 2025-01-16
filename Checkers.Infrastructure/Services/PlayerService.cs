using Checkers.Domain.Enums;
using Checkers.Domain.Interfaces;
using Checkers.Domain.Interfaces.Repositories;
using Checkers.Domain.Models;

namespace Checkers.Infrastructure.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IBaseRepository<Game> _gameRepository;
        private readonly IBaseRepository<Player> _playerRepository;

        public async Task AssignPlayerToGame(Guid playerId, Game game)
        {
            Player player = await _playerRepository.Get(playerId);

            if(player != null)
            {
                game.Players.Add(player);
                _gameRepository.Update(game);
            }
        }

        public Task<Player> CreatePlayer(string Name)
        {
            Player player = new Player
            {
                Id = Guid.NewGuid(),
                Name = Name
            };

            _playerRepository.Add(player);

            return Task.FromResult(player);
        }

        public async Task<Player> GetPlayerById(Guid playerId)
        {
            return await _playerRepository.Get(playerId);
        }
    }
}
