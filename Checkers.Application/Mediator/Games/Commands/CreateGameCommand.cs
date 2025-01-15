using Checkers.Domain.Models;
using MediatR;

namespace Checkers.Application.Mediator.Games.Commands
{
    public record CreateGameCommand : IRequest<Game>;
}
