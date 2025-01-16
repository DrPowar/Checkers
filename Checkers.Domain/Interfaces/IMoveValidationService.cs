using Checkers.Domain.Models;

namespace Checkers.Domain.Interfaces;

public interface IMoveValidationService
{
    bool IsMoveValid(Game game, Move move);
}