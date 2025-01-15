using Checkers.Domain.Enums;

namespace Checkers.Domain.Models;

/// <summary>
/// Represents a piece on the board.
/// </summary>
public class Piece
{
    /// <summary>
    /// Unique identifier for the piece.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// The piece's current position on the board.
    /// </summary>
    public Position Position { get; set; } = null!;
    
    /// <summary>
    /// Indicates color of piece
    /// </summary>
    public PieceColorType PieceColor { get; set; }
    
    /// <summary>
    /// Indicates if the piece has been "kinged."
    /// </summary>
    public bool IsKing { get; set; }
}