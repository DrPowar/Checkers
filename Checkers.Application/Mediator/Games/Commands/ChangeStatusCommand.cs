using Checkers.Domain.Enums;
using MediatR;

namespace Checkers.Application.Mediator.Games.Commands;

public record ChangeStatusCommand(Guid GameId, GameStatus NewStatus) : IRequest<GameStatus>;
