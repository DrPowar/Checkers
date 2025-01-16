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

        public PlayerService(IBaseRepository<Player> playerRepository, IBaseRepository<Game> gameRepository)
        {
            _playerRepository = playerRepository;
            _gameRepository = gameRepository;
        }

        public async Task AssignPlayerToGame(Guid playerId, Game game)
        {
            Player player = await _playerRepository.Get(playerId);

            //TODO: check if number of players < 2
            if (game.Players.Any())
            {
                player.PieceColor = PieceColorType.White;
            }

            if(player != null)
            {
                game.Players.Add(player);
                _gameRepository.Update(game);
                await _gameRepository.SaveChanges();
            }
        }

        public async Task<Player> CreatePlayer(string Name)
        {
            Player player = new Player
            {
                Id = Guid.NewGuid(),
                Name = Name
            };

            _playerRepository.Add(player);
            await _playerRepository.SaveChanges();

            return player;
        }

        public async Task<Player> GetPlayerById(Guid playerId)
        {
            return await _playerRepository.Get(playerId);
        }
    }
}
