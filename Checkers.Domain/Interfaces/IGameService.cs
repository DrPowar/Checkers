using Checkers.Domain.Enums;
using Checkers.Domain.Models;
using System.Threading.Tasks;

namespace Checkers.Domain.Interfaces
{
    public interface IGameService
    {
        Task<Game> CreateGame(List<Piece> board);

        Task<Game?> GetGameById(Guid gameId);

        Task StartGame(Guid gameId);

        Task EndGame(Guid gameId);

        Task<GameStatus> GetGameStatus(Guid gameId);
    }
}
