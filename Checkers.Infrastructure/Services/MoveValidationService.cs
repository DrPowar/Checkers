using Checkers.Domain.Enums;
using Checkers.Domain.Interfaces;
using Checkers.Domain.Models;

namespace Checkers.Infrastructure.Services;

public class MoveValidationService : IMoveValidationService
{
    public bool IsMoveValid(Game game, Move move)
    {
        if (game == null || move == null)
        {
            throw new ArgumentNullException("Game or Move cannot be null.");
        }

        if (game.Status != GameStatus.InProgress)
        {
            throw new InvalidOperationException("Game is not in progress.");
        }

        if (move.PlayerId != game.CurrentTurn?.Id)
        {
            throw new InvalidOperationException("It's not the player's turn.");
        }

        if (!IsDiagonalMovement(move.From, move.To))
        {
            throw new InvalidOperationException("Move must be diagonal.");
        }

        var piece = game.Board.FirstOrDefault(p => p.Position.X == move.From.X && p.Position.Y == move.From.Y);
        if (!IsValidPiece(piece, move.Player!.PieceColor))
        {
            throw new InvalidOperationException("Invalid piece.");
        }

        if (IsTargetOccupied(game, move.To))
        {
            throw new InvalidOperationException("Target position is occupied.");
        }

        if (!piece!.IsKing && !IsCorrectDirection(piece, move))
        {
            throw new InvalidOperationException("Invalid direction for a non-king piece.");
        }

        if (IsJumpMove(move) && !IsValidJump(game, move, piece))
        {
            throw new InvalidOperationException("Invalid jump move.");
        }

        return true;
    }
    private bool IsDiagonalMovement(Position from, Position to)
    {
        return Math.Abs(from.X - to.X) == Math.Abs(from.Y - to.Y);
    }

    private bool IsValidPiece(Piece? piece, PieceColorType pieceTargetColor)
    {
        return piece != null && piece.PieceColor == pieceTargetColor;
    }

    private bool IsTargetOccupied(Game game, Position to)
    {
        return game.Board.Any(p => p.Position.X == to.X && p.Position.Y == to.Y);
    }

    private bool IsCorrectDirection(Piece piece, Move move)
    {
        int forwardDirection = piece.PieceColor == PieceColorType.White ? 1 : -1;
        return move.To.Y - move.From.Y == forwardDirection;
    }

    private bool IsJumpMove(Move move)
    {
        return Math.Abs(move.From.X - move.To.X) == 2 && Math.Abs(move.From.Y - move.To.Y) == 2;
    }

    private bool IsValidJump(Game game, Move move, Piece piece)
    {
        int midX = (move.From.X + move.To.X) / 2;
        int midY = (move.From.Y + move.To.Y) / 2;

        var capturedPiece = game.Board.FirstOrDefault(p =>
            p.Position.X == midX &&
            p.Position.Y == midY &&
            p.PieceColor != piece.PieceColor);

        return capturedPiece != null &&
               !game.Board.Any(p => p.Position.X == move.To.X && p.Position.Y == move.To.Y);
    }
}