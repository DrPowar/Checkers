using Checkers.Application.Mediator.Players.Commands;
using Checkers.Domain.Interfaces;
using Checkers.Domain.Models;
using MediatR;

namespace Checkers.Application.Mediator.Players.Handers
{
    public class AssignPlayerHandler : IRequestHandler<AssignPlayerCommand, bool>
    {
        private readonly IPlayerService _playerService;
        private readonly IGameService _gameService;

        public async Task<bool> Handle(AssignPlayerCommand request, CancellationToken cancellationToken)
        {
            Game game = await _gameService.GetGameById(request.GameId);
            await _playerService.AssignPlayerToGame(request.PlayerId, game);

            return true;
        }
    }
}
