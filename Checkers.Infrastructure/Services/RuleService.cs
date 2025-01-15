using Checkers.Domain.Enums;
using Checkers.Domain.Interfaces;
using Checkers.Domain.Models;

namespace Checkers.Infrastructure.Services;

public class RuleService : IRuleService
{
    public bool IsMoveValid(Game game, Move move)
    {
        if (game == null)
        {
            throw new KeyNotFoundException($"Game does not exist.");
        }

        if (move == null)
        {
            throw new KeyNotFoundException($"Move does not exist.");
        }

        if (game.Status != GameStatus.InProgress)
        {
            throw new ArgumentException("Game is not in progress.");
        }

        if (move.PlayerId != game.CurrentTurn?.Id)
        {
            throw new ArgumentException("Player is not in progress.");
        }
        
        if (!IsDiagonalMovement(move.From, move.To))
        {
            throw new ArgumentException("Move is not diagonal.");
        }
        
        Piece? piece = game.Board.FirstOrDefault(p => p.Position.X == move.From.X && p.Position.Y == move.From.Y);
        if (!IsValidPiece(piece, move.Player!.PieceColor))
        {
            throw new ArgumentException("Invalid piece.");
        }

        if (IsTargetOccupied(game, move.To))
        {
            throw new ArgumentException("Target position is occupied.");
        }
        
        if (!piece!.IsKing && !IsCorrectDirection(piece, move))
        {
            throw new ArgumentException("Invalid direction.");
        }

        if (IsJumpMove(move) && !IsValidJump(game, move, piece))
        {
            throw new ArgumentException("Invalid jump move.");
        }
        
        return true;
    }

    public bool IsGameOver(Game gameId)
    {
        throw new NotImplementedException();
    }

    public Player GetWinner(Game gameId)
    {
        throw new NotImplementedException();
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
        
        Piece? capturedPiece = game.Board.FirstOrDefault(p => p.Position.X == midX && p.Position.Y == midY);
        return capturedPiece != null && capturedPiece.PieceColor != piece.PieceColor;
    }
}