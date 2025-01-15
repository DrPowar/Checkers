using Checkers.Domain.Models;
using MediatR;

namespace Checkers.Application.Mediator.Players.Queries
{
    public record GetPlayerByIdQuery(Guid PlayerId) : IRequest<Player>;
}
