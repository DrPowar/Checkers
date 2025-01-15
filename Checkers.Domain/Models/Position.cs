namespace Checkers.Domain.Models;

/// <summary>
/// Represents a square on the board.
/// </summary>
public class Position
{
    /// <summary>
    /// Row index.
    /// </summary>
    public int X { get; set; }
    
    /// <summary>
    /// Column index.
    /// </summary>
    public int Y { get; set; }
}