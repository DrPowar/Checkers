using MediatR;

namespace Checkers.Application.Mediator.Games.Commands;

public record AssignPlayerToGameCommand (Guid GameId, Guid PlayerId) : IRequest<bool>;