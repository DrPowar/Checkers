using Checkers.Domain.Models;
using MediatR;

namespace Checkers.Application.Mediator.Games.Queries
{
    public record GetGameByIdQuery(Guid GameId) : IRequest<Game>;
}
