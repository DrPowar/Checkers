using Checkers.Domain.Enums;
using Checkers.Domain.Interfaces;
using Checkers.Domain.Interfaces.Repositories;
using Checkers.Domain.Models;

namespace Checkers.Infrastructure.Services
{
    public class BoardService : IBoardService
    {
        private readonly IBaseRepository<Game> _gameRepository;

        public async Task<List<Piece>> GetBoard(Guid gameId)
        {
            Game game = await _gameRepository.Get(gameId);

            return game.Board;
        }

        public Task<List<Piece>> InitializeBoard()
        {
            List<Piece> pieces = new List<Piece>();

            for (int row = 0; row < 3; row++)
            {
                for (int col = (row % 2 == 0) ? 0 : 1; col < 8; col += 2)
                {
                    pieces.Add(new Piece
                    {
                        Id = Guid.NewGuid(),
                        Position = new Position { Id = Guid.NewGuid(), X = row, Y = col },
                        PieceColor = PieceColorType.White,
                        IsKing = false
                    });
                }
            }

            for (int row = 5; row < 8; row++)
            {
                for (int col = (row % 2 == 0) ? 0 : 1; col < 8; col += 2)
                {
                    pieces.Add(new Piece
                    {
                        Id = Guid.NewGuid(),
                        Position = new Position { Id = Guid.NewGuid(), X = row, Y = col },
                        PieceColor = PieceColorType.Black,
                        IsKing = false
                    });
                }
            }

            return Task.FromResult(pieces);
        }

        public Task<List<Piece>> UpdateBoard(Guid gameId)
        {
            throw new NotImplementedException();
        }
    }
}
