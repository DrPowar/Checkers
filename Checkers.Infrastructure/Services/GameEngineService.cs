using Checkers.Domain.Interfaces;
using Checkers.Domain.Interfaces.Repositories;
using Checkers.Domain.Models;

namespace Checkers.Infrastructure.Services;

public class GameEngineService(
    IBaseRepository<Game> gameRepository,
    IBaseRepository<Player> playerRepository,
    IMoveValidationService moveValidationService, 
    IStatusService statusService
) : IGameEngineService
{
    private readonly IBaseRepository<Game> _gameRepository = 
        gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
    
    private readonly IBaseRepository<Player> _playerRepository = 
        playerRepository ?? throw new ArgumentNullException(nameof(playerRepository));
    
    private readonly IMoveValidationService _moveValidationService =
        moveValidationService ?? throw new ArgumentNullException(nameof(moveValidationService));

    private readonly IStatusService _statusService =
        statusService ?? throw new ArgumentNullException(nameof(statusService));

    
    public Task<bool> CheckGameOver(Game game, CancellationToken token = default)
    {
        return _statusService.IsGameOver(game, token);
    }

    public Player? GetWinner(Game game)
    {
        return _statusService.GetWinner(game);
    }

    public async Task<bool> MakeMove(Guid gameId, Guid playerId, Position from, Position to, CancellationToken cancellationToken = default)
    {
        Game? game = await _gameRepository.Get(gameId, cancellationToken);
        if (game == null)
        {
            throw new KeyNotFoundException($"Game with ID {gameId} does not exist.");
        }
        
        Player? player = await _playerRepository.Get(playerId, cancellationToken);
        if (player == null)
        {
            throw new KeyNotFoundException($"Player with ID {playerId} does not exist.");
        }
        
        Move move = new Move
        {
            PlayerId = playerId,
            Player = player,
            GameId = gameId,
            Game = game,
            From = from,
            To = to
        };
        
        bool isValidMove = _moveValidationService.ValidateMove(game, move);
        if (!isValidMove)
        {
            throw new ArgumentException("Invalid move");
        }
        
        Piece piece = game.Board.First(p => p.Position.X == from.X && p.Position.Y == from.Y);
        piece.Position.X = to.X;
        piece.Position.Y = to.Y;

        await _gameRepository.SaveChanges(cancellationToken);
        return true;
    }

    public Task<List<Move>> GetAvailableMoves(Guid gameId, Guid playerId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}