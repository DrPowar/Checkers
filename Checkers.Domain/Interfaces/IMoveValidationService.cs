using Checkers.Domain.Models;

namespace Checkers.Domain.Interfaces;

public interface IMoveValidationService
{
    bool ValidateMove(Game game, Move move);
}