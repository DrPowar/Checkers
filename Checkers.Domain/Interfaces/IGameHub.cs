namespace Checkers.Domain.Interfaces
{
    public interface IGameHub
    {
        Task SendMoveToGroup(string gameId, string move);
        Task NotifyPlayerJoined(string gameId);
    }
}
