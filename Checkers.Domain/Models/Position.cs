namespace Checkers.Domain.Models;

/// <summary>
/// Represents a square on the board.
/// </summary>
public class Position
{
    // TODO: fix this later, coz this contradicts DDD
    /// <summary>
    /// Unique identifier for the position.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Row index.
    /// </summary>
    public int X { get; set; }
    
    /// <summary>
    /// Column index.
    /// </summary>
    public int Y { get; set; }
}