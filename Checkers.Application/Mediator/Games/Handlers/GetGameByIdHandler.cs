using Checkers.Application.Mediator.Games.Queries;
using Checkers.Domain.Interfaces;
using Checkers.Domain.Models;
using MediatR;

namespace Checkers.Application.Mediator.Games.Handlers
{
    public class GetGameByIdHandler(IGameService gameService) : IRequestHandler<GetGameByIdQuery, Game?>
    {
        private readonly IGameService
            _gameService = gameService ?? throw new ArgumentNullException(nameof(gameService));

        public async Task<Game?> Handle(GetGameByIdQuery request, CancellationToken cancellationToken)
        {
            Game? gameFromRepo = await _gameService.GetGameById(request.GameId);
            if (gameFromRepo == null)
            {
                throw new KeyNotFoundException($"Game with ID {request.GameId} does not exist.");
            }
            
            return gameFromRepo;
        }
    }
}
