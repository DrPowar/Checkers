using Checkers.Domain.Enums;
using Checkers.Domain.Interfaces;
using Checkers.Domain.Models;
using System.Runtime.CompilerServices;

namespace Checkers.Infrastructure.Services
{
    public class GameService : IGameService
    {
        public Task<Game> CreateGame(List<Piece> board)
        {
            Game game = new Game(board);
            
            return Task.FromResult(game);
        }

        public Task EndGame(Guid gameId)
        {
            throw new NotImplementedException();
        }

        public Task<Game> GetGameById(Guid gameId)
        {
            throw new NotImplementedException();
        }

        public Task<GameStatus> GetGameStatus(Guid gameId)
        {
            throw new NotImplementedException();
        }

        public Task StartGame(Guid gameId)
        {
            throw new NotImplementedException();
        }
    }
}
