using Checkers.Application.Mediator.Games.Commands;
using Checkers.Domain.Enums;
using Checkers.Domain.Interfaces;
using Checkers.Domain.Models;
using MediatR;

namespace Checkers.Application.Mediator.Games.Handlers
{
    public class StartGameHandler(IGameService gameService) : IRequestHandler<StartGameCommand, GameStatus>
    {
        private readonly IGameService
            _gameService = gameService ?? throw new ArgumentNullException(nameof(gameService));

        public async Task<GameStatus> Handle(StartGameCommand request, CancellationToken cancellationToken)
        {
            Game? gameExist = await _gameService.GetGameById(request.GameId);
            if (gameExist == null)
            {
                throw new KeyNotFoundException($"Game with ID {request.GameId} does not exist.");
            }
            
            await _gameService.StartGame(gameExist.Id);

            return gameExist.Status;
        }
    }
}
