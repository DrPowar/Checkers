using Checkers.Application.Mediator.Games.Commands;
using Checkers.Domain.Interfaces;
using MediatR;

namespace Checkers.Application.Mediator.Games.Handlers
{
    public class EndGameHandler : IRequestHandler<EndGameCommand, bool>
    {
        private readonly IGameService _gameService;

        public async Task<bool> Handle(EndGameCommand request, CancellationToken cancellationToken)
        {
            await _gameService.EndGame(request.GameId);
            return true;
        }
    }
}
