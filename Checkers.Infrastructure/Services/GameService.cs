using Checkers.Domain.Enums;
using Checkers.Domain.Interfaces;
using Checkers.Domain.Interfaces.Repositories;
using Checkers.Domain.Models;

namespace Checkers.Infrastructure.Services
{
    public class GameService(
        IBaseRepository<Game> gameRepository, 
        IPlayerService playerService
    ) : IGameService
    {
        private readonly IBaseRepository<Game> _gameRepository = 
            gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
        
        private readonly IPlayerService _playerService = 
            playerService ?? throw new ArgumentNullException(nameof(playerService));

        public async Task<Game> CreateGameAndSave(List<Piece> board, CancellationToken cancellationToken = default)
        {
            Game game = new Game
            {
                Id = Guid.NewGuid(),
                Players = new List<Player>(2),
                Board = board,
                Status = GameStatus.Paused
            };

            game.CurrentTurn = game.Players.FirstOrDefault();

            _gameRepository.Add(game, cancellationToken);
            await _gameRepository.SaveChanges(cancellationToken);

            return game;
        }
        
        public async Task<Game?> GetGameById(Guid gameId, CancellationToken cancellationToken = default)
        {
            return await _gameRepository.Get(gameId, cancellationToken);
        }

        public async Task ChangeStatus(Guid gameId, GameStatus newStatus, CancellationToken cancellationToken = default)
        {
            Game? game = await _gameRepository.Get(gameId, cancellationToken);

            if (game == null)
            {
                throw new KeyNotFoundException($"Game with ID {gameId} does not exist.");
            }
            
            game.Status = newStatus;
            await _gameRepository.SaveChanges(cancellationToken);
        }
        
        public async Task<GameStatus> GetGameStatus(Guid gameId, CancellationToken cancellationToken = default)
        {
            Game? game = await _gameRepository.Get(gameId, cancellationToken);

            if (game == null)
            {
                throw new KeyNotFoundException($"Game with ID {gameId} does not exist.");
            }

            return game.Status;
        }

        public async Task AssignPlayerToGame(Guid gameId, Guid playerId, CancellationToken cancellationToken = default)
        {
            Game? game = await _gameRepository.Get(gameId, cancellationToken);
            if (game == null)
            {
                throw new KeyNotFoundException($"Game with ID {gameId} does not exist.");
            }
            
            Player? player = await _playerService.GetPlayerById(playerId, cancellationToken);
            if (player == null)
            {
                throw new KeyNotFoundException($"Player with ID {playerId} does not exist.");
            }

            int countOfAssignedPlayers = game.Players.Where(p => p != null).Count();
            player.PieceColor = countOfAssignedPlayers switch
            {
                0 => PieceColorType.Black,
                1 => AssignSecondPlayerAndSetCurrentTurn(game),
                _ => throw new InvalidOperationException("The game already has the maximum number of players.")
            };

            game.Players.Add(player);
            await _gameRepository.SaveChanges(cancellationToken);
        }
        
        private PieceColorType AssignSecondPlayerAndSetCurrentTurn(Game game)
        {
            game.CurrentTurn = game.Players.FirstOrDefault();
            return PieceColorType.White;
        }
    }
}
