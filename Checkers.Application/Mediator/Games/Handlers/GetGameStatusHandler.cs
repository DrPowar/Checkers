using Checkers.Application.Mediator.Games.Queries;
using Checkers.Domain.Enums;
using Checkers.Domain.Interfaces;
using MediatR;

namespace Checkers.Application.Mediator.Games.Handlers
{
    public class GetGameStatusHandler : IRequestHandler<GetGameStatusQuery, GameStatus>
    {
        private readonly IGameService _gameService;

        public async Task<GameStatus> Handle(GetGameStatusQuery request, CancellationToken cancellationToken)
        {
            return await _gameService.GetGameStatus(request.GameId);
        }
    }
}
