using Checkers.Application.Mediator.Players.Commands;
using Checkers.Domain.Interfaces;
using Checkers.Domain.Models;
using MediatR;

namespace Checkers.Application.Mediator.Players.Handers
{
    public class CreatePlayerHandler : IRequestHandler<CreateaPlayerCommand, Player>
    {
        private readonly IPlayerService _playerService;

        public async Task<Player> Handle(CreateaPlayerCommand request, CancellationToken cancellationToken)
        {
            return await _playerService.CreatePlayer(request.Name);
        }
    }
}
