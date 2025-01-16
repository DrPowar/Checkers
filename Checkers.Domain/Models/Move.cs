namespace Checkers.Domain.Models;

/// <summary>
/// Represents a single move made by a player.
/// </summary>
public class Move
{
    /// <summary>
    /// Unique identifier for the move.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// The game to which the move belongs.
    /// </summary>
    public Guid GameId { get; set; }
    
    public Game? Game { get; set; }
    
    /// <summary>
    /// The player who made the move.
    /// </summary>
    public Guid PlayerId { get; set; }
    
    public Player? Player { get; set; }
    
    public Guid FromId { get; set; }

    /// <summary>
    /// Starting position of the piece.
    /// </summary>
    public Position From { get; set; }
    
    public Guid ToId { get; set; }

    /// <summary>
    /// Ending position of the piece.
    /// </summary>
    public Position To { get; set; }
    
    /// <summary>
    /// If the move captured an opponent's piece, this stores the captured piece.
    /// </summary>
    public List<Piece> CapturedPieces { get; set; } = new List<Piece>();
}