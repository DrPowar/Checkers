using Checkers.Application.Mediator.Players.Queries;
using Checkers.Domain.Interfaces;
using Checkers.Domain.Models;
using MediatR;

namespace Checkers.Application.Mediator.Players.Handlers
{
    public class GetPlayerByIdHandler(IPlayerService playerService) : IRequestHandler<GetPlayerByIdQuery, Player>
    {
        private readonly IPlayerService _playerService = 
            playerService ?? throw new ArgumentNullException(nameof(playerService));

        public async Task<Player> Handle(GetPlayerByIdQuery request, CancellationToken cancellationToken)
        {
            Player? playerFromRepo = await _playerService.GetPlayerById(request.PlayerId);
            if (playerFromRepo == null)
            {
                throw new KeyNotFoundException($"Player with ID {request.PlayerId} does not exist.");
            }
            
            return playerFromRepo;
        }
    }
}
