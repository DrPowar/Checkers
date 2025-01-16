using Checkers.Domain.Enums;

namespace Checkers.Domain.Models;

/// <summary>
/// Represents a player in the game.
/// </summary>
public class Player
{
    /// <summary>
    /// Unique identifier for the player.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Name of the player.
    /// </summary>
    public string Name { get; set; } = null!;
    
    /// <summary>
    /// Indicates which color the player plays.
    /// </summary>
    public PieceColorType PieceColor { get; set; }

    /// <summary>
    /// Collection of previous moves
    /// </summary>
    public ICollection<Move> Moves { get; set; } = new List<Move>();
}