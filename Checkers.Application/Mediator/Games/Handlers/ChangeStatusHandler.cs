using Checkers.Application.Mediator.Games.Commands;
using Checkers.Domain.Enums;
using Checkers.Domain.Interfaces;
using Checkers.Domain.Models;
using MediatR;

namespace Checkers.Application.Mediator.Games.Handlers;

public class ChangeStatusHandler(IGameService gameService) : IRequestHandler<ChangeStatusCommand, GameStatus>
{
    private readonly IGameService _gameService = 
        gameService ?? throw new ArgumentNullException(nameof(gameService));
    
    public async Task<GameStatus> Handle(ChangeStatusCommand request, CancellationToken cancellationToken)
    {
        Game? gameExist = await _gameService.GetGameById(request.GameId);
        if (gameExist == null)
        {
            throw new KeyNotFoundException($"Game with ID {request.GameId} does not exist.");
        }
            
        await _gameService.ChangeStatus(request.GameId, request.NewStatus);

        return gameExist.Status;
    }
}