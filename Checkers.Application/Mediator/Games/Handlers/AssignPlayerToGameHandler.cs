using Checkers.Application.Mediator.Games.Commands;
using Checkers.Domain.Interfaces;
using MediatR;

namespace Checkers.Application.Mediator.Games.Handlers;

public class AssignPlayerToGameHandler(IGameService gameService, IGameHub gameHub) : IRequestHandler<AssignPlayerToGameCommand, bool>
{
    private readonly IGameHub _gameHub =
        gameHub ?? throw new ArgumentNullException(nameof(gameHub));
    private readonly IGameService _gameService =
        gameService ?? throw new ArgumentNullException(nameof(gameService));

    public async Task<bool> Handle(AssignPlayerToGameCommand request, CancellationToken cancellationToken)
    {
        await _gameService.AssignPlayerToGame(request.GameId, request.PlayerId, cancellationToken);
        await _gameHub.NotifyPlayerJoined(request.GameId.ToString());

        return true;
    }
}