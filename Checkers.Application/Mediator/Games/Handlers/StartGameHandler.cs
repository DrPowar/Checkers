using Checkers.Application.Mediator.Games.Commands;
using Checkers.Domain.Interfaces;
using MediatR;

namespace Checkers.Application.Mediator.Games.Handlers
{
    internal class StartGameHandler : IRequestHandler<StartGameCommand, bool>
    {
        private readonly IGameService _gameService;

        public async Task<bool> Handle(StartGameCommand request, CancellationToken cancellationToken)
        {
            await _gameService.StartGame(request.GameId);
            return true;
        }
    }
}
