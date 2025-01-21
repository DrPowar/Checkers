using Checkers.Application.Mediator.Games.Commands;
using Checkers.Domain.Interfaces;
using MediatR;

namespace Checkers.Application.Mediator.Games.Handlers;

public class AssignPlayerToGameHandler(IGameService gameService) : IRequestHandler<AssignPlayerToGameCommand, bool>
{
    private readonly IGameService _gameService = 
        gameService ?? throw new ArgumentNullException(nameof(gameService));

    public async Task<bool> Handle(AssignPlayerToGameCommand request, CancellationToken cancellationToken)
    {
        await _gameService.AssignPlayerToGame(request.GameId, request.PlayerId, cancellationToken);

        return true;
    }
}