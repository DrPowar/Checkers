using Checkers.Domain.Enums;
using MediatR;

namespace Checkers.Application.Mediator.Games.Queries
{
    public record GetGameStatusQuery(Guid GameId) : IRequest<GameStatus>;
}
