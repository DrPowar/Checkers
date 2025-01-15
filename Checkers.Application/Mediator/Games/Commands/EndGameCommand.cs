using MediatR;

namespace Checkers.Application.Mediator.Games.Commands
{
    public record EndGameCommand(Guid GameId) : IRequest<bool>;
}
