using Checkers.Domain.Enums;
using Checkers.Domain.Interfaces;
using Checkers.Domain.Interfaces.Repositories;
using Checkers.Domain.Models;

namespace Checkers.Infrastructure.Services
{
    public class GameService : IGameService
    {
        private readonly IBaseRepository<Game> _gameRepository;

        public GameService(IBaseRepository<Game> gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<Game> CreateGame(List<Piece> board)
        {
            Game game = new Game
            {
                Id = Guid.NewGuid(),
                Players = new List<Player>(2),
                Board = board,
                Status = GameStatus.Paused
            };

            game.CurrentTurn = game.Players.FirstOrDefault();

            _gameRepository.Add(game);
            await _gameRepository.SaveChanges();

            return game;
        }

        public async Task EndGame(Guid gameId)
        {
            Game game = await _gameRepository.Get(gameId);

            if (game != null)
            {
                game.Status = GameStatus.Completed;
                _gameRepository.Update(game);
            }
        }

        public async Task<Game> GetGameById(Guid gameId)
        {
            return await _gameRepository.Get(gameId);
        }

        public async Task<GameStatus> GetGameStatus(Guid gameId)
        {
            Game game = await _gameRepository.Get(gameId);

            return game.Status;
        }

        public async Task StartGame(Guid gameId)
        {
            Game game = await _gameRepository.Get(gameId);

            if(game != null)
            {
                game.Status = GameStatus.InProgress;
                _gameRepository.Update(game);
            }
        }
    }
}
