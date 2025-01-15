using Checkers.Domain.Models;
using MediatR;

namespace Checkers.Application.Mediator.Games.Commands
{
    internal class CreateGameCommand : IRequest<Game>
    {
    }
}
