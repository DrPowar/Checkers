using Checkers.Application.Mediator.Boards.Command;
using Checkers.Domain.Interfaces;
using Checkers.Domain.Models;
using MediatR;

namespace Checkers.Application.Mediator.Boards.Handlers;

public class MakeMoveHandler(
    IPlayerService playerService, 
    IGameService gameService,
    IGameEngineService gameEngineService)
    : IRequestHandler<MakeMoveCommand, bool>
{
    private readonly IPlayerService _playerService =
        playerService ?? throw new ArgumentNullException(nameof(playerService));

    private readonly IGameService _gameService = 
        gameService ?? throw new ArgumentNullException(nameof(gameService));

    private readonly IGameEngineService _gameEngineService = 
        gameEngineService ?? throw new ArgumentNullException(nameof(gameEngineService));

    public async Task<bool> Handle(MakeMoveCommand request, CancellationToken cancellationToken)
    {
        Game? game = await _gameService.GetGameById(request.GameId, cancellationToken);
        if (game == null)
        {
            throw new KeyNotFoundException($"Game with ID {request.GameId} does not exist.");
        }
        
        Player? player = await _playerService.GetPlayerById(request.PlayerId, cancellationToken);
        if (player == null)
        {
            throw new KeyNotFoundException($"Player with ID {request.GameId} does not exist.");
        }

        Move move = new Move
        {
            GameId = request.GameId,
            PlayerId = request.PlayerId,
            Player = player,
            Game = game,
            From = new Position
            {
                X = request.FromX,
                Y = request.FromY
            },
            To = new Position
            {
                X = request.ToX,
                Y = request.ToY
            }
        };
        
        bool isMoveSuccess = await _gameEngineService.MakeMove(request.GameId, request.PlayerId, move.From, move.To, cancellationToken);
        if (!isMoveSuccess)
        {
            throw new ApplicationException("An error occurred while making a move.");
        }
        
        return true;
    }
}