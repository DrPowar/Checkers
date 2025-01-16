namespace Checkers.Domain.DTOs
{
    public record AssignPlayerToGameRequest(Guid GameId, Guid PlayerId);
}
