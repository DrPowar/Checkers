using Checkers.Domain.Interfaces;
using Checkers.Domain.Models;

namespace Checkers.Infrastructure.Services;

public class RuleService(IMoveValidationService moveValidationService, IStatusService statusService)
    : IRuleService
{
    private readonly IMoveValidationService _moveValidationService =
        moveValidationService ?? throw new ArgumentNullException(nameof(moveValidationService));

    private readonly IStatusService _statusService =
        statusService ?? throw new ArgumentNullException(nameof(statusService));

    public bool IsMoveValid(Game game, Move move)
    {
        return _moveValidationService.IsMoveValid(game, move);
    }

    public bool IsGameOver(Game game)
    {
        return _statusService.IsGameOver(game);
    }

    public Player GetWinner(Game game)
    {
        return _statusService.GetWinner(game)
               ?? throw new InvalidOperationException("The game is not over yet or there is no winner.");
    }
} 