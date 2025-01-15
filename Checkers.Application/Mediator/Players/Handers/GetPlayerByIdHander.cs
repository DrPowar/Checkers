using Checkers.Application.Mediator.Players.Queries;
using Checkers.Domain.Interfaces;
using Checkers.Domain.Models;
using MediatR;

namespace Checkers.Application.Mediator.Players.Handers
{
    public class GetPlayerByIdHander : IRequestHandler<GetPlayerByIdQuery, Player>
    {
        private readonly IPlayerService _playerService;

        public async Task<Player> Handle(GetPlayerByIdQuery request, CancellationToken cancellationToken)
        {
            return await _playerService.GetPlayerById(request.PlayerId);
        }
    }
}
