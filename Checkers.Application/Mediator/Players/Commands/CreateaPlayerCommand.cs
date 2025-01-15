using Checkers.Domain.Enums;
using Checkers.Domain.Models;
using MediatR;

namespace Checkers.Application.Mediator.Players.Commands
{
    public record CreateaPlayerCommand(string Name, PieceColorType PieceColor) : IRequest<Player>;
}
