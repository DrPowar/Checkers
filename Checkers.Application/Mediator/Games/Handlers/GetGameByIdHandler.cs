using Checkers.Application.Mediator.Games.Queries;
using Checkers.Domain.Interfaces;
using Checkers.Domain.Models;
using MediatR;

namespace Checkers.Application.Mediator.Games.Handlers
{
    internal class GetGameByIdHandler : IRequestHandler<GetGameByIdQuery, Game>
    {
        private readonly IGameService _gameService;

        public async Task<Game> Handle(GetGameByIdQuery request, CancellationToken cancellationToken)
        {
            return await _gameService.GetGameById(request.GameId);
        }
    }
}
