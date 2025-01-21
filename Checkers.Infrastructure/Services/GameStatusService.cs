using Checkers.Domain.Enums;
using Checkers.Domain.Interfaces;
using Checkers.Domain.Models;

namespace Checkers.Infrastructure.Services;

public class GameStatusService : IStatusService
{
    public async Task<bool> IsGameOver(Game game, CancellationToken cancellationToken = default)
    {
        if (GetPieceCount(game, PieceColorType.Black) == 0 || GetPieceCount(game, PieceColorType.White) == 0)
        {
            return true;
        }

        var nextPlayer = game.Players.First(p => p.PieceColor != game.CurrentTurn!.PieceColor);
        return !await HasAvailableMoves(game, nextPlayer, cancellationToken);
    }
    
    public Player? GetWinner(Game game)
    {
        var blackPieces = GetPieceCount(game, PieceColorType.Black);
        var whitePieces = GetPieceCount(game, PieceColorType.White);

        if (blackPieces > 0 && whitePieces == 0)
        {
            return game.Players.First(p => p.PieceColor == PieceColorType.Black);
        }

        if (whitePieces > 0 && blackPieces == 0)
        {
            return game.Players.First(p => p.PieceColor == PieceColorType.White);
        }

        return null;
    }
    
    private int GetPieceCount(Game game, PieceColorType pieceColor)
    {
        return game.Board.Count(p => p.PieceColor == pieceColor);
    }

    private async Task<bool> HasAvailableMoves(Game game, Player player, CancellationToken cancellationToken = default)
    {
        var tasks = game.Board
            .Where(p => p.PieceColor == player.PieceColor)
            .Select(piece => CanMove(piece, game, cancellationToken));

        var results = await Task.WhenAll(tasks);
        return results.Any(canMove => canMove);
    }

    private async Task<bool> CanMove(Piece piece, Game game, CancellationToken cancellationToken = default)
    {
        var directions = piece.IsKing
            ? new List<(int, int)> { (1, 1), (1, -1), (-1, 1), (-1, -1) }
            : piece.PieceColor == PieceColorType.White
                ? new List<(int, int)> { (1, 1), (1, -1) }
                : new List<(int, int)> { (-1, 1), (-1, -1) };

        var tasks = directions.Select(direction => CanMoveOrJump(piece, direction, game, cancellationToken));
        var results = await Task.WhenAll(tasks);

        return results.Any(result => result);
    }
    
    private async Task<bool> CanMoveOrJump(Piece piece, (int dx, int dy) direction, Game game, CancellationToken cancellationToken)
    {
        var (dx, dy) = direction;

        var canMoveTask = Task.Run(() => IsValidMove(piece, dx, dy, game), cancellationToken);
        var canJumpTask = Task.Run(() => IsValidJump(piece, dx, dy, game), cancellationToken);

        await Task.WhenAll(canMoveTask, canJumpTask);

        return canMoveTask.Result || canJumpTask.Result;
    }

    private bool IsValidMove(Piece piece, int dx, int dy, Game game)
    {
        int newX = piece.Position.X + dx;
        int newY = piece.Position.Y + dy;

        return IsWithinBounds(newX, newY) &&
               !game.Board.Any(p => p.Position.X == newX && p.Position.Y == newY);
    }

    private bool IsValidJump(Piece piece, int dx, int dy, Game game)
    {
        int newX = piece.Position.X + 2 * dx;
        int newY = piece.Position.Y + 2 * dy;

        if (!IsWithinBounds(newX, newY))
            return false;

        int midX = piece.Position.X + dx;
        int midY = piece.Position.Y + dy;

        var capturedPiece = game.Board.FirstOrDefault(p =>
            p.Position.X == midX &&
            p.Position.Y == midY &&
            p.PieceColor != piece.PieceColor);

        return capturedPiece != null &&
               !game.Board.Any(p => p.Position.X == newX && p.Position.Y == newY);
    }

    private bool IsWithinBounds(int x, int y)
    {
        return x is >= 0 and < 8 && y is >= 0 and < 8;
    }
}