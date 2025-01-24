using Checkers.Domain.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Checkers.Api.Hubs
{
    public class GameHub : Hub, IGameHub
    {
        public async Task SendMoveToGroup(string gameId, string move)
        {
            await Clients.Group(gameId).SendAsync("ReceiveMove", move);
        }

        public async Task NotifyPlayerJoined(string gameId)
        {
            if (!Context.Items.ContainsKey(gameId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, gameId);
                Context.Items[gameId] = true;
            }
            await Clients.Group(gameId).SendAsync("PlayerJoined");
        }
    }
}
