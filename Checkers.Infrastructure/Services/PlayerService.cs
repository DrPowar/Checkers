using Checkers.Domain.Enums;
using Checkers.Domain.Interfaces;
using Checkers.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Infrastructure.Services
{
    public class PlayerService : IPlayerService
    {
        public Task AssignPlayerToGame(Guid playerId, Guid gameId)
        {
            throw new NotImplementedException();
        }

        public Task<Player> CreatePlayer(string Name, PieceColorType pieceColor)
        {
            Player player = new Player
            {
                Id = Guid.NewGuid(),
                Name = Name,
                PieceColor = pieceColor
            };

            return Task.FromResult(player);
        }

        public Task<Player> GetPlayerById(Guid playerId)
        {
            throw new NotImplementedException();
        }
    }
}
