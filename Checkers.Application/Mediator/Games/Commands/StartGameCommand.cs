using MediatR;

namespace Checkers.Application.Mediator.Games.Commands
{
    public record StartGameCommand(Guid GameId) : IRequest<bool>;
}
