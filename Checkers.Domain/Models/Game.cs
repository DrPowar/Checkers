using Checkers.Domain.Enums;

namespace Checkers.Domain.Models;

/// <summary>
/// Represents a game session.
/// </summary>
public class Game
{
    /// <summary>
    /// Unique identifier for the game.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Players participating in the game.
    /// </summary>
    public List<Player> Players { get; set; } = new List<Player>();
    
    /// <summary>
    /// Represents the current state of the game board.
    /// </summary>
    public List<Piece> Board { get; set; } = new List<Piece>();
    
    /// <summary>
    /// Indicates the game's current state.
    /// </summary>
    public GameStatus Status { get; set; }
    
    /// <summary>
    /// Specifies which player's turn it is.
    /// </summary>
    public Player CurrentTurn { get; set; }
}