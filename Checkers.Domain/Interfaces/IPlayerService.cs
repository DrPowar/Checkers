﻿using Checkers.Domain.Enums;
using Checkers.Domain.Models;

namespace Checkers.Domain.Interfaces
{
    public interface IPlayerService
    {
        Task<Player> CreatePlayer(string Name, PieceColorType pieceColor);

        Task<Player> GetPlayerById(Guid playerId);

        Task AssignPlayerToGame(Guid playerId, Game game);
    }
}
