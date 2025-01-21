using MediatR;

namespace Checkers.Application.Mediator.Boards.Command;

public record MakeMoveCommand(
    Guid GameId, 
    Guid PlayerId,
    int FromX,
    int FromY,
    int ToX,
    int ToY
) : IRequest<bool>;
